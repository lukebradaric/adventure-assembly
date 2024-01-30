using Sirenix.Serialization;
using TinyTools.Generics;

namespace AdventureAssembly.Interface
{
    public class InterfaceManager : Singleton<InterfaceManager>
    {
        [OdinSerialize] public HeroSelectionInterface HeroSelectionInterface { get; set; }
    }
}