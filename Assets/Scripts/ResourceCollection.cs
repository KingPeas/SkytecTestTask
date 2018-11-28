using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class ResourceCollection : MonoBehaviour
    {
        [SerializeField]
        private UnitModifier[] modifiers = null;
        [SerializeField]
        private Buff[] buffs = null;
        private static ResourceCollection instance = null;

        public static ResourceCollection Instance { get { return Instance; } private set { Instance = value; } }

        // Use this for initialization
        void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
            }
        }

        public static UnitModifier GetModifier(int idx)
        {
            if (idx >= 0 && idx < Instance.modifiers.Length)
            {
                return Instance.modifiers[idx];
            }
            return null;
        }

        public static Buff GetBuff(int idx)
        {
            if (idx >= 0 && idx < Instance.buffs.Length)
            {
                return Instance.buffs[idx];
            }
            return null;
        }
    }
}
