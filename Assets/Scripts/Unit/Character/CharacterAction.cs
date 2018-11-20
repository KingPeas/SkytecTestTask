using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class CharacterAction : MonoBehaviour
    {

        protected CharacterData data = null;

        // Use this for initialization
        void Awake()
        {
            data = GetComponent<CharacterData>();
            if (!data)
            {
                Debug.LogWarning("Нет данных по персонажу", this);
                enabled = false;
            }
        }
    }
}
