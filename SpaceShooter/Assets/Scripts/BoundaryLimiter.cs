using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceShooter
{
    public class BoundaryLimiter : MonoBehaviour
    {
        private void Update()
        {
            if (Boundary.Instance == null) return;

            var LB = Boundary.Instance;
            var R = LB.Radius;

            if (transform.position.magnitude > R)
            {
                if (LB.LimitMode == Boundary.Mode.Limit)
                {
                    transform.position = transform.position.normalized * R;
                    
                }

                if (LB.LimitMode == Boundary.Mode.Teleport)
                {
                    transform.position = -transform.position.normalized * R;
                }


            }
        }
    }
}
