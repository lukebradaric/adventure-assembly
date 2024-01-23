using AdventureAssembly.Core;
using AdventureAssembly.Input;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using System.Linq;
using TinyTools.Extensions;
using UnityEngine;

namespace AdventureAssembly.Units
{
    public class Player : SerializedMonoBehaviour
    {
        [PropertySpace]
        [Title("Components")]
        [SerializeField] private Hero _heroPrefab;
        [OdinSerialize] public List<Hero> Heroes { get; private set; } = new List<Hero>();

        [PropertySpace]
        [Title("Debug")]
        [OdinSerialize] private List<HeroData> _startingHeroes = new List<HeroData>();
        [OdinSerialize] private List<HeroData> _debugAddHeroes = new List<HeroData>();

        public Vector2Int NextMovementVector { get; private set; } = Vector2Int.up;
        public Vector2Int LastMovementVector { get; private set; } = Vector2Int.zero;

        private Dictionary<Vector2Int, Hero> _heroPositions = new Dictionary<Vector2Int, Hero>();

        private void OnEnable()
        {
            TickManager.TickUpdate += OnTurnUpdate;
            InputManager.MovementVector.ValueChanged += OnMovementVectorChanged;
        }

        private void OnDisable()
        {
            TickManager.TickUpdate -= OnTurnUpdate;
            InputManager.MovementVector.ValueChanged -= OnMovementVectorChanged;
        }

        private void Start()
        {
            // TODO: Remove debug starting hero
            foreach (HeroData heroData in _startingHeroes)
            {
                AddHero(heroData);
            }
        }

        private void Update()
        {
            // TODO: Remove debug adding heroes
            if (UnityEngine.Input.GetKeyDown("g"))
            {
                AddHero(_debugAddHeroes.Random());
            }
        }

        private void OnMovementVectorChanged(Vector2Int direction)
        {
            // Ignore input that is moving backwards
            if (Heroes.Count > 1 && direction == -LastMovementVector)
            {
                Debug.Log($"Trying to move in opposite direction. Last:{LastMovementVector} New:{direction}");
                return;
            }

            NextMovementVector = direction;
        }

        private void OnHeroDied(Unit unit)
        {
            RemoveHero((Hero)unit);
        }

        private void OnTurnUpdate()
        {
            UpdateUnitPositions();
        }

        private void UpdateUnitPositions()
        {
            // Move the start of the party (Indicator and player root)
            Vector2Int startPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y) + NextMovementVector;
            transform.DOMove((Vector2)startPosition, TickManager.Instance.TickInterval).SetEase(Ease.OutCubic);

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

                // If position is already occupied by another hero, kill remaining
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
            Debug.Log($"Moving: {NextMovementVector}");
            LastMovementVector = NextMovementVector;
        }

        public void AddHero(HeroData heroData)
        {
            Debug.Log($"Adding new hero to player: {heroData.name}");

            // Calculate hero spawn position
            Hero lastHero = Heroes.LastOrDefault();
            Vector2Int spawnPosition = lastHero == null ?
                new Vector2Int((int)transform.position.x, (int)transform.position.y) :
                lastHero.LastPosition;

            // Instantiate new hero gameobject
            Hero hero = Instantiate(_heroPrefab, (Vector2)spawnPosition, Quaternion.identity);
            hero.transform.SetParent(transform);

            // Initialize hero data
            hero.Initialize(heroData, spawnPosition);
            hero.Died += OnHeroDied;

            // Add hero to list
            Heroes.Add(hero);
        }

        public void RemoveHero(Hero hero)
        {
            if (!Heroes.Contains(hero))
            {
                Debug.LogError($"Player does not contain hero! {hero.name}");
            }

            Heroes.Remove(hero);
        }
    }
}