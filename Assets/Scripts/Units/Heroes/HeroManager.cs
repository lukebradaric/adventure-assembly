using AdventureAssembly.Core;
using AdventureAssembly.Input;
using AdventureAssembly.Units.Modifiers;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using System.Linq;
using TinyTools.Extensions;
using UnityEngine;

namespace AdventureAssembly.Units.Heroes
{
    public class HeroManager : UnitManager<Hero>
    {
        [PropertySpace]
        [Title("Components")]
        [SerializeField] private Hero _heroPrefab;

        [PropertySpace]
        [Title("Settings")]
        [OdinSerialize] public LayerMask HazardLayerMask { get; private set; }

        [PropertySpace]
        [Title("Debug")]
        [OdinSerialize] private List<HeroData> _startingHeroes = new List<HeroData>();
        [OdinSerialize] private List<HeroData> _debugAddHeroes = new List<HeroData>();
        [OdinSerialize] private List<UnitModifier> _startingModifiers = new List<UnitModifier>();

        public Vector2Int NextMovementVector { get; private set; } = Vector2Int.up;
        public Vector2Int LastMovementVector { get; private set; } = Vector2Int.zero;

        public Vector2Int HorizontalMovementVector { get; private set; }
        public Vector2Int VerticalMovementVector { get; private set; }

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
                SpawnHero(heroData);
            }

            foreach (UnitModifier modifier in _startingModifiers)
            {
                HeroManager.AddModifier(modifier);
            }
        }

        private void Update()
        {
            // TODO: Remove debug adding heroes
            if (UnityEngine.Input.GetKeyDown("g"))
            {
                SpawnHero(_debugAddHeroes.Random());
            }

            if (UnityEngine.Input.GetKeyDown("h"))
            {
                Units.Random().Die();
            }
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

        private void OnHeroDied(Unit unit)
        {
            RemoveUnit((Hero)unit);
        }

        private void OnTurnUpdate()
        {
            UpdateUnitPositions();
        }

        private void UpdateUnitPositions()
        {
            // Move the start of the party (Indicator and player root)
            Vector2Int startPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y) + NextMovementVector;

            // Check if there is a hazard at the new position
            if (IsHazardAtPosition(startPosition))
            {
                Units.First().Die();
                startPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
            }

            // Move the head position
            transform.DOMove((Vector2)startPosition, TickManager.Instance.TickInterval).SetEase(Ease.OutCubic);

            // Clear the dictionary of hero positions
            _heroPositions.Clear();

            // Should we kill remaining heroes when looping through the list (self-collision)
            bool killRemaining = false;
            // List of heroes to destroy after this turn (cleanup)
            HashSet<Hero> killHeroes = new HashSet<Hero>();

            // Loop through all heroes and move them towards the hero in front of them
            Vector2Int position = startPosition;
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
                hero.OnTick();
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

        private bool IsHazardAtPosition(Vector2Int position)
        {
            return Physics2D.OverlapCircleAll(position, 0.1f, HazardLayerMask).Length > 0;
        }

        public void SpawnHero(HeroData heroData)
        {
            // Calculate hero spawn position
            Hero lastHero = Units.LastOrDefault();
            Vector2Int spawnPosition = lastHero == null ?
                new Vector2Int((int)transform.position.x, (int)transform.position.y) :
                lastHero.LastPosition;

            // Instantiate new hero gameobject
            Hero hero = Instantiate(_heroPrefab, (Vector2)spawnPosition, Quaternion.identity);
            hero.transform.SetParent(transform);

            // Add hero to this unit manager
            AddUnit(hero);

            // Add all the heroes classes to the class manager
            ClassManager.AddClassesByHeroData(heroData);

            // Initialize the spawned hero
            hero.Initialize(heroData, spawnPosition);
        }

        public override void AddUnit(Hero hero)
        {
            base.AddUnit(hero);
            hero.Died += OnHeroDied;
        }

        public override void RemoveUnit(Hero hero)
        {
            ClassManager.RemoveClassesByHeroData(hero.HeroData);
            base.RemoveUnit(hero);
            hero.Died -= OnHeroDied;
        }
    }
}