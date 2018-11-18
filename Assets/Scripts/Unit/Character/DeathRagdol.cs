using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class DeathRagdol : MonoBehaviour
    {

        public float minTime = 0.5f;
        public float maxTime = 2f;

        private void OnDisable()
        {
            SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
            foreach (var renderer in renderers)
            {
                Animator anim = GetComponent<Animator>();
                if (anim) anim.enabled = false;
                GameObject go = renderer.gameObject;
                Collider2D[] scripts = GetComponents<Collider2D>();
                foreach (var script in scripts)
                {

                    script.enabled = false;
                }
                go.AddComponent<Rigidbody2D>();
                go.AddComponent<PolygonCollider2D>();
                float timeLeft = Random.Range(minTime, maxTime);
                Destroy(go, timeLeft);
                go.transform.SetParent(transform.parent);
            }

        }
    }
}