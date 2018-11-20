using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{

    public class CharacterMove : CharacterAction
    {
        public string AxisX = "Horizontal";
        public string AxisY = "Vertical";
        public float speed = 3f;

        // Update is called once per frame
        void Update()
        {
            float valX = Input.GetAxis(AxisX);
            float valY = Input.GetAxis(AxisY);
            Vector2 dir = new Vector2(valX, valY);
            data.MoveDirection = dir;

            if (dir.sqrMagnitude > float.Epsilon)
            {
                if (data.Avatar)
                {
                    if (Mathf.Abs(dir.x) > float.Epsilon)
                    {
                        int flip = dir.x < 0 ? -1 : 1;
                        Vector3 scale = data.Avatar.localScale;
                        scale.x = Mathf.Abs(scale.x) * Mathf.Sign(flip);
                        data.Avatar.localScale = scale;
                    }
                }
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(valX, 0, 0), speed * Time.deltaTime);
            }
            
        }
    }

}