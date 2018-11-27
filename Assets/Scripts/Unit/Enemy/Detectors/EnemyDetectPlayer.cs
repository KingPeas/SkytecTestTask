using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class EnemyDetectPlayer : EnemyDetector
    {
        protected Vector2[] points = null;
        public int numRays = 7;
        private float lengthDetect = 0;
        private bool intoTrigger = false;
        private LayerMask layerMask = -1;

        protected override void Awake()
        {
            base.Awake();
            Collider2D collider = GetComponent<Collider2D>();
            lengthDetect = collider.bounds.size.x;
            layerMask = Physics2D.GetLayerCollisionMask(gameObject.layer);
            if (numRays < 2) numRays = 2;
            points = new Vector2[numRays];
            var pmin = collider.bounds.min;
            var pmax = collider.bounds.max;
            points[0] = transform.InverseTransformPoint(new Vector2(pmax.x, pmin.y));
            points[numRays - 1] = transform.InverseTransformPoint(new Vector2(pmax.x, pmax.y));
            Vector2 direction = points[numRays - 1] - points[0];
            if (numRays > 2)
            {
                float step = direction.magnitude / (numRays - 1);

                direction.Normalize();
                for (int i = 1; i < numRays - 1; i++)
                {
                    points[i] = points[i - 1] + direction * step;
                }
            }
            foreach (var point in points)
            {
                point.Normalize();
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            CharacterData player = collision.GetComponent<CharacterData>();
            if (player)
            {
                data.ai.player = RayToScan(player);
                //Debug.Log(data.ai.player);

            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            CharacterData player = collision.GetComponent<CharacterData>();
            if (player) intoTrigger = true; 
        }

        protected override void OnTriggerExit2D(Collider2D collision)
        {
            CharacterData player = collision.GetComponent<CharacterData>();
            if (player)
            {
                data.ai.player = null;
                intoTrigger = true;
            }
        }

        private CharacterData RayToScan(CharacterData player)
        {
            CharacterData ret = null;
            RaycastHit2D hit;

            foreach (var point in points)
            {
                hit = Physics2D.Raycast(transform.position, transform.TransformDirection(point), lengthDetect, layerMask);
                if (hit)
                {
                    if (hit.collider.gameObject == player.gameObject)
                    {
                        return player;
                    }
                }
            }
            return ret;
        }
    }
}
