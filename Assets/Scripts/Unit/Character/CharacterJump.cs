using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class CharacterJump : CharacterAction
    {


        public string BtnJump = "Jump";
        public float ForceJump = 15;
        

        // Update is called once per frame
        void Update()
        {
            
            if (data.IsGrounded && data.body && Input.GetButtonDown(BtnJump))
            {
                data.body.AddForce(Vector2.up * ForceJump, ForceMode2D.Impulse);
            }
        }
    }
}
