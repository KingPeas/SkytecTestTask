using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class GameLogic : MonoBehaviour
    {

        public static GameLogic Instance { get; private set; }

        private List<UnitData> units = null;
        // Use this for initialization
        void Awake()
        {
            if (Instance == null) Instance = this;
            else
            {
                Destroy(this);
                return;
            }
            DontDestroyOnLoad(gameObject);
            units = new List<UnitData>();
        }

        // Update is called once per frame
        public void RegisterUnit(UnitData data)
        {
            if (!units.Contains(data)) units.Add(data);
        }

        public void UnRegisterUnit(UnitData data)
        {
            if (units.Contains(data)) units.Remove(data);
        }

        public void ApplyModifiers(UnitData getter, UnitModifier modifier)
        {
            if (!getter || !modifier) return;
            Type targetType = null;
            if (getter is CharacterData)
            {
                targetType = typeof(EnemyData);
            }
            else if (getter is EnemyData)
            {
                targetType = typeof(CharacterData);
            }

            if (targetType != null)
            {
                foreach (var unit in units)
                {
                    if (targetType.IsInstanceOfType(unit))
                    {
                        var mod = Instantiate(modifier, unit.transform);
                        mod.transform.localPosition = Vector3.zero;
                        mod.transform.localRotation = Quaternion.identity;
                    }
                }
            }
        }


    }
}