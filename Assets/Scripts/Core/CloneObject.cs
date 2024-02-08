using System;

namespace AdventureAssembly.Core
{
    /// <summary>
    /// Defines an object that is able to be cloned.
    /// </summary>
    /// <typeparam name="T">The object type</typeparam>
    [Serializable]
    public abstract class CloneObject<T>
    {
        /// <summary>
        /// Gets a clone of this object.
        /// </summary>
        /// <returns></returns>
        public T GetClone()
        {
            T clone = (T)this.MemberwiseClone();
            OnClone(clone);
            return clone;
        }

        public virtual void OnClone(T obj) { }
    }
}