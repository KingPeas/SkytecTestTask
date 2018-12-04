using System.Collections;
using System.Collections.Generic;
using System;

namespace KingDOM.Platformer2D
{
    [Flags]
    public enum DamageType
    {

        Physics = 1,
        Fire = 2,
        Ice = 4,
        Water = 8,
        Electricity=16,
        Blood=32,
        Necro = 64,
        Poison = 128
    }
}
