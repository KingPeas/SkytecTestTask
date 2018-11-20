using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class UnitData : MonoBehaviour
    {

        public float Energy = 1f;
        public Transform Avatar = null;
        public Animator animator = null;

        protected virtual void Awake()
        {
            if (!animator && Avatar) animator = Avatar.GetComponent<Animator>();
        }

    }
}
