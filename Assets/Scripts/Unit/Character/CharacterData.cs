using KingDOM.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class CharacterData : MoveUnitData
    {

        public int Score = 0;

        private void Awake()
        {
            Sender.AddEvent(EventName.DESTROYER, hnDestroyer);
        }
        private void OnDestroy()
        {
            Sender.RemoveEvent(EventName.DESTROYER, hnDestroyer);
        }

        private void Start()
        {
            Sender.SendEvent(EventName.SCORE_INIT, this, ParmName.SOURCE, this, ParmName.IS_ME, true);
        }

         private void hnDestroyer(SimpleEvent obj)
        {
            UnitData source = null;
            if (!obj.TryGetParm<UnitData>(ParmName.SOURCE, ref source) || source != this) return;
            UnitData target = null;
            if (!obj.TryGetParm<UnitData>(ParmName.TARGET, ref target)) return;

            if (target is EnemyData) Score += 3; // за убийство противника 3 очка
            else if (!(target is CharacterData)) Score += 1; // за убийство своего очков не даем, а за другие объекты по одному очку
        }

   }
}
