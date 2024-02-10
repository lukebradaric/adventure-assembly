using UnityEngine;

namespace TinyTools.ScriptableEvents
{
    [CreateAssetMenu(menuName = "TinyTools/ScriptableEvents/GameScriptableEvent")]
    public class GameScriptableEvent : ScriptableEvent<GameEventData>
    {
        public void Invoke()
        {
            Invoke(null, null);
        }

        public void Invoke(Component sender)
        {
            Invoke(sender, null);
        }

        public void Invoke(object data)
        {
            Invoke(null, data);
        }

        public void Invoke(Component sender, object Data)
        {
            base.Invoke(new GameEventData(sender, Data));
        }
    }
}
