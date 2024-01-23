using AdventureAssembly.Core;
using AdventureAssembly.Core.Extensions;
using AdventureAssembly.Input;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

namespace AdventureAssembly.Units
{
    public class Player : SerializedMonoBehaviour
    {
        [PropertySpace]
        [Title("Components")]
        [SerializeField] private Hero _heroPrefab;

        [PropertySpace]
        [Title("Debug")]
        [OdinSerialize] private List<HeroData> _startingHeroes = new List<HeroData>();
        [OdinSerialize] private List<HeroData> _debugAddHeroes = new List<HeroData>();
        [OdinSerialize] public List<Hero> Heroes { get; private set; } = new List<Hero>();

        public Vector2Int LastMovementVector { get; private set; } = Vector2Int.zero;

        // Dictionary of positions for each hero
        private Dictionary<Vector2Int, Hero> _heroPositions = new Dictionary<Vector2Int, Hero>();

        private void OnEnable()
        {
            TurnManager.TurnUpdate += OnTurnUpdate;
        }

        private void OnDisable()
        {
            TurnManager.TurnUpdate -= OnTurnUpdate;
        }

        private void Start()
        {
            // Debug add starting Heroes
            foreach (HeroData heroData in _startingHeroes)
            {
                AddHero(heroData);
            }
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown("g"))
            {
                foreach (HeroData heroData in _debugAddHeroes)
                {
                    AddHero(heroData);
                }
            }
        }

        private void OnTurnUpdate()
        {
            UpdateUnitPositions();
        }

        private void UpdateUnitPositions()
        {
            // Don't allow moving backwards into yourself
            Vector2Int movementVector = InputManager.MovementVector;
            if (Heroes.Count > 1 && movementVector == -LastMovementVector)
            {
                movementVector = LastMovementVector;
            }
            LastMovementVector = movementVector;

            // Move the start of the party (Indicator and player root)
            Vector2Int startPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y) + movementVector;
            transform.DOMove((Vector2)startPosition, TurnManager.Instance.TurnInterval).SetEase(Ease.OutCubic);

            // Clear the dictionary of hero positions
            _heroPositions.Clear();

            // Should we kill remaining heroes when looping through the list (self-collision)
            bool killRemaining = false;
            // List of heroes to destroy after this turn (cleanup)
            HashSet<Hero> killHeroes = new HashSet<Hero>();

            // Loop through all heroes and move them towards the hero in front of them
            Vector2Int position = startPosition;
            foreach (Hero hero in Heroes)
            {
                // If a self-collision occurred, kill remaining heroes
                if (killRemaining)
                {
                    killHeroes.Add(hero);
                    continue;
                }

                // Move hero to new position
                hero.Move(new Vector2Int(position.x - hero.Position.x, position.y - hero.Position.y));
                position = hero.LastPosition;

                // Register hero position in position dictionary (self-collision)
                if (!_heroPositions.TryAdd(hero.Position, hero))
                {
                    Debug.Log($"{hero.HeroData.Name} and onward should die.");
                    killRemaining = true;
                    killHeroes.Add(hero);
                    continue;
                }
            }

            // Kill all heroes in the list of to kill
            foreach (Hero hero in killHeroes)
            {
                hero.Die();
            }
        }

        private void OnHeroDied(Unit unit)
        {
            Heroes.Remove((Hero)unit);
        }

        public void AddHero(HeroData heroData)
        {
            Debug.Log($"Adding new hero to player: {heroData.name}");

            // Calculate hero spawn position
            Hero lastHero = Heroes.LastOrDefault();
            Vector2Int spawnPosition = lastHero == null ?
                new Vector2Int((int)transform.position.x, (int)transform.position.y) :
                new Vector2Int(lastHero.Position.x, lastHero.Position.y - 1);

            // Instantiate new hero gameobject
            Hero hero = Instantiate(_heroPrefab, (Vector2)spawnPosition, Quaternion.identity);
            hero.transform.SetParent(transform);

            // Initialize hero data
            hero.Initialize(heroData, spawnPosition);
            hero.Died += OnHeroDied;

            // Add hero to list
            Heroes.Add(hero);
        }
    }
}