using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class EnemyDetectHole : EnemyDetector
    {
        private ContactFilter2D contactFilter;
        private Collider2D myCollider;
        public LayerMask layerMask;
        protected override void Awake()
        {
            base.Awake();
            contactFilter.useTriggers = true;
            contactFilter.SetLayerMask(layerMask);
            contactFilter.useLayerMask = true;
            myCollider = GetComponent<Collider2D>();
        }
        private void FixedUpdate()
        {
            data.ai.detectHole = data.move.IsGrounded && !myCollider.IsTouching(contactFilter);
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {            
        }

        protected override void OnTriggerExit2D(Collider2D collision)
        {                         
        }
    }
}