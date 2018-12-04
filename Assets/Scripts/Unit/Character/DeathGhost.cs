using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class DeathGhost : MonoBehaviour
    {

        public float PeriodDestroy = 0.1f;
        public float TimeLife = 3f;
        public Material MaterialGhost = null;
        public GameObject avatar = null;
        private List<GameObject> ghosts = null;

        private void Awake()
        {
            ghosts = new List<GameObject>();
        }
        private void OnDisable()
        {
            if (!MaterialGhost || !avatar) return;
            SpriteRenderer[] renderers = avatar.GetComponentsInChildren<SpriteRenderer>();
            GameObject[] ghosts = new GameObject[renderers.Length];
            SpriteRenderer renderer;
            for (int i = 0; i < renderers.Length; i++)
            {
                renderer = renderers[i];
                GameObject go = new GameObject(renderer.name);
                SpriteRenderer r = go.AddComponent<SpriteRenderer>();
                r.sprite = renderer.sprite;
                r.material = MaterialGhost;
                ghosts[i] = go;
                go.layer = gameObject.layer;
                go.transform.position = renderer.transform.position;
                go.transform.localScale = renderer.transform.lossyScale;
                go.transform.rotation = renderer.transform.rotation;
                //go.transform.SetParent(transform.parent);

            }
            Shuffle(ghosts);
            this.ghosts.AddRange(ghosts);
            InvokeRepeating("DestroyPiece", PeriodDestroy, PeriodDestroy);
        }

        private void OnDestroy()
        {
            CancelInvoke();
        }
        void DestroyPiece()
        {
            if (ghosts.Count <= 0)
            {
                CancelInvoke("DestroyPiece");
                return;
            }
            GameObject go = ghosts[0];
            Rigidbody2D body = go.AddComponent<Rigidbody2D>();
            body.gravityScale = -0.3f;
            go.AddComponent<CircleCollider2D>();
            Destroy(go, TimeLife);
            ghosts.RemoveAt(0);
        }

        //for shuffle number from array
        void Shuffle(GameObject[] array)
        {
            if (array == null || array.Length <= 0) return;

            int p = array.Length;
            for (int n = p - 1; n > 0; n--)
            {
                int r = Random.Range(0, n-1);
                GameObject t = array[r];
                array[r] = array[n];
                array[n] = t;
            }
        }
    }

}
