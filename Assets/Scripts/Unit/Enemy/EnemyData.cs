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
            public float distanceDetectPlayer = 8;
            public float distanceDetectHole = 1;
            public float distanceDetectBullet = 3;
            public float distanceDetectBlock = 2;
        }
    }
}