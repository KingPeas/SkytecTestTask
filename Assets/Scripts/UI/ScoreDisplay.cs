using KingDOM.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KingDOM.Platformer2D
{
    public class ScoreDisplay : MonoBehaviour
    {

        public Text text = null;
        public bool IsMe = true;
        private CharacterData data = null;
        // Use this for initialization
        void Awake()
        {
            if (text == null) text = GetComponent<Text>();
            Sender.AddEvent(EventName.SCORE_INIT, hnScoreInit);
        }
        private void Update()
        {
            if (text)
                text.text = data.Score.ToString();
        }

        // Update is called once per frame
        void OnDestroy()
        {
            UnRegister();
        }

        void UnRegister()
        {
            Sender.RemoveEvent(EventName.SCORE_INIT, hnScoreInit);
        }

        private void hnScoreInit(SimpleEvent obj)
        {
            CharacterData data = null;
            if (!obj.TryGetParm<CharacterData>(ParmName.SOURCE, ref data)) return;
            bool isMe = true;
            if (!obj.TryGetParm<bool>(ParmName.IS_ME, ref isMe)) return;

            if (this.IsMe != isMe) return;
            if (!text || data == null) return;
            this.data = data;
            UnRegister();


        }

    }
}
