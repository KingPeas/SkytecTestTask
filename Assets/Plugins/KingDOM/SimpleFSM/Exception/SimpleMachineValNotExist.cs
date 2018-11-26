﻿namespace KingDOM.SimpleFSM.Exception
{
    public class SimpleMachineValNotExist : System.Exception
    {
        public SimpleMachineValNotExist() : base() { }
        public SimpleMachineValNotExist(string message) : base(message) { }
        public SimpleMachineValNotExist(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected SimpleMachineValNotExist(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}