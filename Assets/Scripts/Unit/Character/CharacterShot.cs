using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KingDOM.Platformer2D
{
    public class CharacterShot : CharacterAttack
    {

        protected override void Update()
        {
            base.Update();

            GameObject go = data.GetWeapon();
            if (go)
            {
                Bullet bullet = go.GetComponent<Bullet>();
                bullet.MoveDirection = data.avatar.render.right;
                go.layer = gameObject.layer;
            }
        }
    }
}