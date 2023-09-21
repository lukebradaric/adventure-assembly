using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;

public class Character : Entity
{
    [PropertySpace]
    [Title("Class")]
    [OdinSerialize] public List<Class> Classes { get; private set; } = new List<Class>();

    private void OnEnable()
    {
        CharacterManager.Register(this);
    }

    private void OnDisable()
    {
        CharacterManager.Unregister(this);
    }
}
