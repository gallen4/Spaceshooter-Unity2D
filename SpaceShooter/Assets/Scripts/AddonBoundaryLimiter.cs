using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceShooter
{
    public class AddonBoundaryLimiter : MonoBehaviour
    {
        private void Update()
        {
            if (AddonBoundary.Instance == null) return;

            var LB = AddonBoundary.Instance;
            var R = LB.Radius;

            if (transform.position.magnitude > R)
            {
                if (LB.LimitMode == AddonBoundary.Mode.Kill)
                {
                    transform.position = transform.position.normalized * R;
                    Destroy(gameObject);
                }

                if (LB.LimitMode == AddonBoundary.Mode.Teleport)
                {
                    transform.position = -transform.position.normalized * R;
                }


            }
        }
    }
}
