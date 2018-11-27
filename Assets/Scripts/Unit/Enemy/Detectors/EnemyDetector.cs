using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{

    public abstract class EnemyDetector : MonoBehaviour
    {

        protected EnemyData data = null;

        protected virtual void Awake()
        {
            data = GetComponentInParent<EnemyData>();
        }

        protected abstract void OnTriggerEnter2D(Collider2D collision);

        protected abstract void OnTriggerExit2D(Collider2D collision);


    }

}