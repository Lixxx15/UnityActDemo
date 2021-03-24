using UnityEngine;
using System.Collections;

namespace Forge3D
{
    public class F3DTrailExample : MonoBehaviour
    {
        public float Mult;
        public float TimeMult;
        public float _X;

        Vector3 defaultPos;

        // Use this for initialization
        void Start()
        {
            // Store initial position
            defaultPos = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            // Used in the example scene
            // Moves the trail by circular trajectory 
            transform.position = defaultPos +
                                 new Vector3(Mathf.Sin((Time.time-_X)*TimeMult)*Mult, 0f, Mathf.Cos((Time.time - _X) * TimeMult)*Mult);
        }
    }
}