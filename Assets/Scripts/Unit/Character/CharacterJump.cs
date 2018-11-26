using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class CharacterJump : CharacterAction
    {


        public string BtnJump = "Jump";
        

        // Update is called once per frame
        void Update()
        {
            
            if (data.move.IsGrounded && data.avatar.body && Input.GetButtonDown(BtnJump))
            {
                data.avatar.body.AddForce(Vector2.up * data.move.ForceJump, ForceMode2D.Impulse);
            }
        }
    }
}
