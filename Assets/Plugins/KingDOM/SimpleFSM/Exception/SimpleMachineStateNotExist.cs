namespace KingDOM.SimpleFSM.Exception
{
    public class SimpleMachineStateNotExist : System.Exception
    {
        public SimpleMachineStateNotExist() : base() { }
        public SimpleMachineStateNotExist(string message) : base(message) { }
        public SimpleMachineStateNotExist(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected SimpleMachineStateNotExist(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}