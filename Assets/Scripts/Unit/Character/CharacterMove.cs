using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{

    public class CharacterMove : CharacterAction
    {
        public string AxisX = "Horizontal";
        public string AxisY = "Vertical";

        // Update is called once per frame
        void Update()
        {
            float valX = Input.GetAxis(AxisX);
            float valY = Input.GetAxis(AxisY);
            Vector2 dir = new Vector2(valX, valY);
            data.move.moveDirection = dir;

            if (dir.sqrMagnitude > float.Epsilon)
            {
                
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(valX, 0, 0), data.move.speed * Time.deltaTime);
            }

            if (Mathf.Abs(valX) > 0.1) data.move.lookDirection = dir;
        }
    }

}