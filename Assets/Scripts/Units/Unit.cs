using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace AdventureAssembly.Units
{
    public abstract class Unit : SerializedMonoBehaviour
    {
        private Vector2Int _position;
        public Vector2Int Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                GridManager.UpdateUnitPosition(this, _position);
            }
        }

        public event Action Destroyed;

        /// <summary>
        /// Initializes this Unit at position.
        /// </summary>
        /// <param name="position"></param>
        public virtual void Initialize(Vector2Int position)
        {
            GridManager.AddUnit(this, position);
            _position = position;
        }

        /// <summary>
        /// Sets this units position directly.
        /// </summary>
        /// <param name="position">The position to move to</param>
        /// <param name="teleport">Should the unit teleport to the new position</param>
        public void SetPosition(Vector2Int position, bool teleport = true)
        {
            Position = position;

            if (teleport)
            {
                transform.position = (Vector2)Position;
            }
        }

        /// <summary>
        /// Removes this unit from the UnitManager and destroys the gameobject.
        /// </summary>
        public virtual void Destroy()
        {
            GridManager.RemoveUnit(this);
            Destroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}