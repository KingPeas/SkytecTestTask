namespace KingDOM.SimpleFSM.Exception
{
    public class SimpleMachineCanNotAddState : System.Exception
    {
        public SimpleMachineCanNotAddState() : base() { }
        public SimpleMachineCanNotAddState(string message) : base(message) { }
        public SimpleMachineCanNotAddState(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected SimpleMachineCanNotAddState(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}