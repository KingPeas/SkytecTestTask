using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class Gun : MonoBehaviour
    {

        public Bullet bullet = null;
        // Use this for initialization
        void OnEnable()
        {
            if (bullet)
            {
                Bullet newBullet = Instantiate(bullet, transform.position, transform.rotation, null);
                newBullet.gameObject.layer = gameObject.layer;
                newBullet.MoveDirection = transform.lossyScale.x > 0? transform.right: - transform.right;
                MakeDamage damage = newBullet.GetComponent<MakeDamage>();
                if (damage) damage.source = GetComponentInParent<MoveUnitData>();
            }
            gameObject.SetActive(false);
        }

        
    }
}