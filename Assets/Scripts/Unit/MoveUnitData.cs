using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class MoveUnitData : UnitData
    {
        public Rigidbody2D body = null;
        public Transform Avatar = null;
        private bool isGrounded = false;
        private Vector2 moveDirection = Vector2.zero;

        public bool IsGrounded
        {
            get
            {
                return isGrounded;
            }

            private set
            {
                isGrounded = value;
            }
        }

        public Vector2 MoveDirection
        {
            get
            {
                return moveDirection;
            }

            set
            {
                moveDirection = value;
            }
        }

        private void Awake()
        {
            if (!body) body = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            IsGrounded = Physics2D.OverlapCircleAll(transform.position, 0.2f).Length > 1;
        }
    }
}