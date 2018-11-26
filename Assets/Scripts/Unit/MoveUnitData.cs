using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class MoveUnitData : UnitData
    {
        [Serializable]
        public class Avatar
        {
            public Rigidbody2D body = null;
            public Transform render = null;
            public Animator animator = null;
            [AnimatorParameter(SourceName = "animator")]
            public int AnimParameter = 0;
            public float TimeRespawn = 1f;
        }
        [Serializable]
        public class Move
        {
            internal bool isGrounded = false;
            public float speed = 3f;
            public float ForceJump = 15;
            public Vector2 moveDirection = Vector2.zero;
            public Vector2 lookDirection = Vector2.zero;
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
        }
        [Serializable]
        public class Attack
        {
            public bool Active = false;
            public bool CanAttack = true;
            public Vector2 direction = Vector2.zero;
            public UnitBrain.BrainState StateAttack = UnitBrain.BrainState.Attack;
            public int numWeapon = -1;
            public float TimeAttack = 0.5f;
        }

        public Avatar avatar;
        public Move move;
        public Attack attack;
        public GameObject[] Weapons = null;

        void Awake()
        {
            if (!avatar.body) avatar.body = GetComponent<Rigidbody2D>();
            if (!avatar.animator && avatar.render) avatar.animator = avatar.render.GetComponent<Animator>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            var contacts = Physics2D.OverlapCircleAll(transform.position, 0.2f);//, ~gameObject.layer);
            move.isGrounded = contacts.Length > 1;
        }

        public GameObject GetWeapon()
        {
            var n = attack.numWeapon;
            if (n >= 0 && n < Weapons.Length)
                return Weapons[n];
            return null;
        }

        void OnDrawGizmos()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 0.2f);
        }
    }
}