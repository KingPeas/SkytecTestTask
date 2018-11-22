namespace KingDOM.SimpleFSM.Exception
{
    public class SimpleMachineCanNotAddTransition : System.Exception
    {
        public SimpleMachineCanNotAddTransition() : base() { }
        public SimpleMachineCanNotAddTransition(string message) : base(message) { }
        public SimpleMachineCanNotAddTransition(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected SimpleMachineCanNotAddTransition(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}