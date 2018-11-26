using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public abstract class Buff : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            UnitData data = collision.GetComponent<UnitData>();
            if (data)
            {
                Activate(data);
            }
        }

        protected abstract void Activate(UnitData data);        
    }
}