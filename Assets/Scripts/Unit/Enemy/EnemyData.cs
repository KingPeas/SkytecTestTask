using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class EnemyData : MoveUnitData
    {

        [Serializable]
        public class AI
        {
            public CharacterData player = null;
            public bool detectHole = false;
            public bool detectBullet = false;
            public bool detectAttackZone = false;
            public bool detectBarrier = false;
            public float TimeFollowing = 5f;
            public float TimeBlock = 0.2f;
            public float SpeedRun = 4f;
        }
        public AI ai = new AI();
    }
}