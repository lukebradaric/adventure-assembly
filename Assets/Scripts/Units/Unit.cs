using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureAssembly.Units
{
    public abstract class Unit : SerializedMonoBehaviour
    {
        public Vector2Int Position { get; protected set; }

        /// <summary>
        /// Initializes this Unit at position.
        /// </summary>
        /// <param name="position"></param>
        public virtual void Initialize(Vector2Int position)
        {
            this.Position = position;
            GridManager.AddUnit(this, position);
        }

        /// <summary>
        /// Removes this unit from the UnitManager and destroys the gameobject.
        /// </summary>
        public virtual void Destroy()
        {
            GridManager.RemoveUnit(this);
            Destroy(gameObject);
        }
    }
}