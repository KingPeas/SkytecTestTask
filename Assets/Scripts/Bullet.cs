using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 moveDirection = Vector3.zero;
        public MakeDamage damage = null;
        public SpriteRenderer render = null;
        public float speed = 10f;
        public float TimeLife = 2f;
        private float timeLeft = 2f;
        private Collider2D collider;
        private Rigidbody2D body;

        public Vector3 MoveDirection
        {
            get
            {
                return moveDirection;
            }

            set
            {
                moveDirection = value;
                if (render && moveDirection.x < 0) render.flipY = true;
            }
        }

        private void Awake()
        {
            //timeLeft = TimeLife;
            Destroy(gameObject, TimeLife);
            collider = GetComponent<Collider2D>();
            transform.SetParent(null);
            body = GetComponent<Rigidbody2D>();
            MoveDirection.Normalize();
            damage = GetComponent<MakeDamage>();
            render = GetComponentInChildren<SpriteRenderer>();
        }
        // Update is called once per frame
        void Update()
        {
            /*timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                Destroy(gameObject);
                return;
            }*/
            if (body)
            {
                body.MovePosition(transform.position + MoveDirection * speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + MoveDirection, speed * Time.deltaTime);
            }
            //transform.rotation = Quaternion.LookRotation(moveDirection, new Vector3(0, 0, -1)); 
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            MoveDirection = Vector3.zero;
            enabled = false;
            if (collider != null) collider.enabled = false;
            if (body) body.simulated = false;
            if (damage) damage.enabled = false;
            transform.SetParent(collision.transform);
        }


    }
}