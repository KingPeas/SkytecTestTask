using System;
using System.Collections.Generic;
using KingDOM.SimpleFSM.Exception;

namespace KingDOM.SimpleFSM
{
    public class Parameters
    {
        public object this[string name]
        {
            get
            {
                if(parameters.ContainsKey(name))
                    return parameters[name];
                else
                {
                    throw new SimpleMachineValNotExist("Попытка получить параметр не определенный в машине состояний.");
                }
            }
            set
            {
                if (parameters.ContainsKey(name)) 
                    parameters[name] = value;
                else
                {
                    parameters.Add(name, value);
                }
            }
        }

        internal T Get<T>(string name)
        {
            T val = default(T);
            if (parameters.ContainsKey(name) && parameters[name] is T)
                val = (T) parameters[name];
            else
            {
                throw new SimpleMachineValNotExist(string.Format("Попытка получить параметр {1}({0}) не определенный в машине состояний.", typeof(T).Name, name));
            }

            return val;
        }   
     
        private Dictionary<string, object> parameters;

        internal Parameters()
        {
            parameters = new Dictionary<string, object>();
        }
    }

}

