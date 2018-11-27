using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class EnemyDetectAttackZone : EnemyDetector
    {

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            CharacterData player = collision.GetComponent<CharacterData>();
            if (player)
            {
                data.ai.detectAttackZone = true;
                //Debug.Log(data.ai.detectAttackZone);
            }
        }

        protected override void OnTriggerExit2D(Collider2D collision)
        {
            CharacterData player = collision.GetComponent<CharacterData>();
            if (player)
            {
                data.ai.detectAttackZone = false;
                //Debug.Log(data.ai.detectAttackZone);
            }
        }
    }
}