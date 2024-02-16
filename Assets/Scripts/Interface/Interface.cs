using Sirenix.OdinInspector;

namespace AdventureAssembly.Interface
{
    public abstract class Interface : SerializedMonoBehaviour
    {
        public virtual void OnShow() { }
        public virtual void OnHide() { }
    }
}