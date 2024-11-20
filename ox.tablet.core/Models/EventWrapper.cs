using Akka.Actor;
using OX.IO;
using System;
using System.IO;

namespace OX.Tablet
{
    public class StringWrapper : ISerializable
    {
        public string Text { get; private set; }
        public virtual int Size => Text.GetVarSize();
        public StringWrapper() { }
        public StringWrapper(string text) { this.Text = text; }
        public void Serialize(BinaryWriter writer)
        {
            writer.WriteVarString(Text);
        }
        public void Deserialize(BinaryReader reader)
        {
            Text = reader.ReadVarString();
        }
    }
    public delegate void BlockChainHandler<T>(T obj);
    public class EventWrapper<T> : UntypedActor
    {
        private readonly Action<T> callback;

        public EventWrapper(Action<T> callback)
        {
            this.callback = callback;
            Context.System.EventStream.Subscribe(Self, typeof(T));
        }

        protected override void OnReceive(object message)
        {
            if (message is T obj) callback(obj);
        }

        protected override void PostStop()
        {
            Context.System.EventStream.Unsubscribe(Self);
            base.PostStop();
        }

        public static Props Props(Action<T> callback)
        {
            return Akka.Actor.Props.Create(() => new EventWrapper<T>(callback));
        }
    }
}
