using UnityEngine;
using System.Collections;

namespace KingDOM.Platformer2D
{
    public abstract class UnitModifier : MonoBehaviour
    {

        public float TimeEffect = 5f;
        protected UnitData data = null;
        internal MoveUnitData source = null;

        private void OnEnable()
        {
            data = GetComponentInParent<UnitData>();
            Activate();
            Destroy(gameObject, TimeEffect);
        }

        private void OnDestroy()
        {
            Deactivate();
        }

        public abstract void Activate();
        public abstract void Deactivate();
    }
}