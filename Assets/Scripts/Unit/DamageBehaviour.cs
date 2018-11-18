using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class DamageBehaviour : MonoBehaviour
    {
 
        // эффект от воздействия повреждения анализируемого типа 
        public enum DamageEffect
        {
            /// <summary>
            /// нет воздействия
            /// </summary>
            None,
            /// <summary>
            /// конкурирует с другим того же типа будет выбран максимальный
            /// </summary>
            Concurent,  
            /// <summary>
            ///  только это воздействие, остальные того же типа игнорируются
            /// </summary>
            OnlyThis, 
            /// <summary>
            /// добавляется к тому которое уже есть
            /// </summary>
            Add  
        }

        public DamageType Exposed = (DamageType)int.MaxValue;
        public List<DamageModificator> modificators = null;

        // Use this for initialization
        void Awake()
        {
            if (modificators == null) CreateDefaultModicators();
        }

        // Update is called once per frame
        public float CalcDamage(float power, DamageType kind = DamageType.Physics)
        {
            DamageType exposedFilter = Exposed;
            float damageValue = 0;
            Dictionary<DamageType, float> results = new Dictionary<DamageType, float>(); 
            foreach (DamageType s in Enum.GetValues(typeof(DamageType)))
            {
                if ((s & Exposed) > 0)
                {
                    results.Add(s, 0);
                }
            }
            //Считаем повреждения по каждому типу
            foreach (var modificator in modificators)
            {
                if ((modificator.type & exposedFilter) == 0) continue;
                switch (modificator.effect)
                {
                    case DamageEffect.OnlyThis:
                        results[modificator.type] = power * modificator.power;
                        exposedFilter &= ~modificator.type;
                        break;
                    case DamageEffect.Add:
                        results[modificator.type] = (results.ContainsKey(modificator.type)? results[modificator.type] : 0) + power * modificator.power;
                        break;
                    case DamageEffect.Concurent:
                        if (results.ContainsKey(modificator.type))
                        {
                            results[modificator.type] = Mathf.Max(results[modificator.type], power * modificator.power);
                        }
                        break;
                }
            }
            //Считаем суммарные повреждения
            foreach (var result in results)
            {
                damageValue += result.Value;
            }
            return damageValue;
        }

        void CreateDefaultModicators()
        {
            modificators = new List<DamageModificator>();
            foreach (DamageType s in Enum.GetValues(typeof(DamageType)))
            {
                if ((s & Exposed) > 0)
                {
                    modificators.Add(new DamageModificator { type = s, power = 1, effect = DamageEffect.Concurent });
                }
            }
        }

        [Serializable]
        public class DamageModificator
        {
            public DamageType type = DamageType.Physics;
            public float power = 1;
            public DamageEffect effect = DamageEffect.Concurent;
        }

    }

}