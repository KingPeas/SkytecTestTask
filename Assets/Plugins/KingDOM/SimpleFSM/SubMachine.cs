using System;

namespace KingDOM.SimpleFSM
{
    
    public class SubMachine:State
    {
        private SimpleMachine child;
        private bool constancy = false;

        internal SubMachine(SimpleMachine parentMachine, String name):base(parentMachine, name)
        {
            child = new SimpleMachine();
        }

        public SubMachine Stability(bool resetAfterExit)
        {
            constancy = resetAfterExit;
            return this;
        }
    }
}
