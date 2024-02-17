using AdventureAssembly.Core;
using AdventureAssembly.Input;
using AdventureAssembly.Units.Bosses;
using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Enemies;
using AdventureAssembly.Units.Interactables;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AdventureAssembly.Units.Heroes
{
    public class HeroManager : CharacterManager<Hero>
    {
        [PropertySpace]
        [Title("Components")]
        [SerializeField] private Hero _heroPrefab;

        [PropertySpace]
        [Title("Settings")]
        [Tooltip("How much time between each Hero Ability update? Measured in seconds.")]
        [OdinSerialize] public float UpdateInterval { get; private set; }

        public Vector2Int NextMovementVector { get; private set; } = Vector2Int.up;
        public Vector2Int LastMovementVector { get; private set; } = Vector2Int.zero;
        public Vector2Int HorizontalMovementVector { get; private set; }
        public Vector2Int VerticalMovementVector { get; private set; }

        // Dictionary of all current hero positions
        private Dictionary<Vector2Int, Hero> _heroPositions = new Dictionary<Vector2Int, Hero>();

        // The coroutine that updates the ability timers of all heroes
        private Coroutine _updateCoroutine = null;

        private void OnEnable()
        {
            TickManager.TickUpdate += OnTurnUpdate;
            InputManager.MovementVector.ValueChanged += OnMovementVectorChanged;

            _updateCoroutine = StartCoroutine(UpdateCoroutine());
        }

        private void OnDisable()
        {
            TickManager.TickUpdate -= OnTurnUpdate;
            InputManager.MovementVector.ValueChanged -= OnMovementVectorChanged;

            if (_updateCoroutine != null)
            {
                StopCoroutine(_updateCoroutine);
            }
        }

        private IEnumerator UpdateCoroutine()
        {
            yield return new WaitForSeconds(UpdateInterval);

            foreach (Hero hero in Units)
            {
                hero.OnUpdate(UpdateInterval);
            }

            _updateCoroutine = StartCoroutine(UpdateCoroutine());
        }

        private void OnMovementVectorChanged(Vector2Int direction)
        {
            // Ignore input that is moving backwards
            if (Units.Count > 1 && direction == -LastMovementVector)
            {
                return;
            }

            NextMovementVector = direction;
        }

        private void OnHeroDied(Character unit)
        {
            unit.Died -= OnHeroDied;

            RepairUnitPositions((Hero)unit);
        }

        private void OnGameLost()
        {
            StopCoroutine(_updateCoroutine);
            _updateCoroutine = null;
            Debug.Log("You lost the game!");
            this.enabled = false;
        }

        private void OnTurnUpdate()
        {
            // If there are no Hero units left, you lose :)
            if (Units.Count == 0)
            {
                OnGameLost();
                return;
            }

            UpdateUnitPositions();
        }

        private void UpdateUnitPositions()
        {
            Hero firstHero = Units.First();

            // Move the start of the party (Indicator and player root)
            Vector2Int nextPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y) + NextMovementVector;

            void RecalculateNextPosition()
            {
                nextPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
            }

            void FirstHeroCollisionDeath()
            {
                firstHero.CollisionDeath = true;
                firstHero.Die();
                RecalculateNextPosition();
            }

            // Check if the next position is within the level bounds
            if (!LevelMap.Instance.IsWithinPlayerMap(nextPosition))
            {
                FirstHeroCollisionDeath();
            }

            // Check if player is moving into another unit
            if (GridManager.TryGetUnit(nextPosition, out Unit unit))
            {
                // Interact with interactable units
                if (unit is InteractableUnit)
                {
                    ((InteractableUnit)unit).OnInteract();
                }

                // If player collided with enemy, deal damage to each other
                if (unit is Enemy)
                {
                    Enemy enemy = (Enemy)unit;

                    // If enemy has less health, kill enemy
                    if (enemy is not Boss || enemy.CurrentHealth <= firstHero.CurrentHealth)
                    {
                        enemy.Die();
                    }
                    // If hero has less health, kill hero, reset next pos
                    else if (enemy.CurrentHealth > firstHero.CurrentHealth)
                    {
                        FirstHeroCollisionDeath();
                    }
                }
            }

            // Move the head position
            transform.DOMove((Vector2)nextPosition, TickManager.Instance.TickInterval).SetEase(Ease.OutCubic);

            // Clear the dictionary of hero positions
            _heroPositions.Clear();

            // Should we kill remaining heroes when looping through the list (self-collision)
            bool killRemaining = false;
            // List of heroes to destroy after this turn (cleanup)
            HashSet<Hero> killHeroes = new HashSet<Hero>();

            // Loop through all heroes and move them towards the hero in front of them
            Vector2Int position = nextPosition;
            foreach (Hero hero in Units)
            {
                // If a self-collision occurred, kill remaining heroes
                if (killRemaining)
                {
                    killHeroes.Add(hero);
                    continue;
                }

                // If new position is already occupied by another hero, kill remaining (self-collision)
                Vector2Int movement = new Vector2Int(position.x - hero.Position.x, position.y - hero.Position.y);
                if (!_heroPositions.TryAdd(hero.Position + movement, hero))
                {
                    killRemaining = true;
                    killHeroes.Add(hero);
                    continue;
                }

                // Move hero to new position
                hero.Move(movement);

                position = hero.LastPosition;
            }

            // Kill all heroes in the list of to kill
            foreach (Hero hero in killHeroes)
            {
                hero.Die();
            }

            // Store last movement vector
            LastMovementVector = NextMovementVector;
        }

        /// <summary>
        /// Repairs the empty spaces in the snake by setting Hero positions to the empty space in front of them.
        /// </summary>
        /// <param name="hero">The hero that died</param>
        private void RepairUnitPositions(Hero hero)
        {
            // Don't repair the snake if the front unit died in a hazard collision
            if (hero.CollisionDeath)
            {
                return;
            }

            for (int i = Units.Count - 1; i > Units.IndexOf(hero); i--)
            {
                Units[i].SetPosition(Units[i - 1].Position, false);
            }
        }

        public void AddHeroToSnake(HeroData heroData)
        {
            // Calculate hero spawn position
            Hero lastHero = Units.LastOrDefault();
            Vector2Int spawnPosition = lastHero == null ?
                new Vector2Int((int)transform.position.x, (int)transform.position.y) :
                lastHero.LastPosition;

            // Instantiate new hero gameobject
            Hero hero = heroData.Create(spawnPosition);
            hero.Died += OnHeroDied;
        }
    }
}