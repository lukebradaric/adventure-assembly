using UnityEngine;

namespace TinyTools.ScriptableEvents
{
    public class GameEventData
    {
        public GameEventData(Component sender, object data)
        {
            this.Sender = sender;
            this.Data = data;
        }

        public GameEventData(Component sender)
        {
            this.Sender = sender;
            this.Data = null;
        }

        public GameEventData(object data)
        {
            this.Sender = null;
            this.Data = data;
        }

        public GameEventData()
        {
            Sender = null;
            Data = null;
        }

        public Component Sender { get; set; }
        public object Data { get; set; }
    }
}