using UnityEngine;
using System.Collections;

namespace SoftLab.Observer
{
    public class FollowMe : MonoBehaviour
    {
        public Transform Target = null;
        public bool Smoothing = true;
        public float TimeMoving = 0.5f;
        public Vector3 Scale = Vector3.one;
        // Use this for initialization

        private void OnEnable()
        {
            /*if (Target)
            {
                transform.position = Target.position;
                transform.rotation = Target.rotation;
            }*/
        }

        // Update is called once per frame
        void Update()
        {
            if (Target != null)
            {
                
                if (Smoothing)
                {
                    transform.position += ReScale(Vector3.Lerp(transform.position, Target.position, Time.deltaTime / TimeMoving) - transform.position);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Target.rotation, Time.deltaTime / TimeMoving);
                }
                else
                {
                    transform.position += ReScale(Target.position - transform.position);
                    transform.rotation = Target.rotation;

                }
                
                    
            }

        }

        private Vector3 ReScale(Vector3 change)
        {
            change.Scale(Scale);
            return change;
        }
    }
}
