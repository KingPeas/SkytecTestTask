using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class EnemyDetectBullet : EnemyDetector
    {
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet) {
                data.ai.detectBullet = true;
                //Debug.Log(data.ai.detectBullet);
            }
        }

        protected override void OnTriggerExit2D(Collider2D collision)
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet) data.ai.detectBullet = false;
        }
    }
}