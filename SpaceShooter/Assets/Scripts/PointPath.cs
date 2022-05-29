using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{


    public class PointPath : MonoBehaviour
    {
        public enum PathType
        {
            loop
        }

        public int movementDirection = 1;
        public PathType m_pathType;
        public int m_movingTo = 0;
        public Transform[] DotsPath;

        private void OnDrawGizmos()
        {
            if (DotsPath == null || DotsPath.Length < 2)
            {
                return;
            }

            for (int i = 1; i < DotsPath.Length; ++i)
            {
                Gizmos.DrawLine(DotsPath[i - 1].position, DotsPath[i].position);
            }

            if (m_pathType == PathType.loop)
            {
                Gizmos.DrawLine(DotsPath[0].position, DotsPath[DotsPath.Length - 1].position);
            }

        }

        public IEnumerator<Transform> GetNextPoint()
        {
            if (DotsPath == null || DotsPath.Length < 1)
            {
                yield break;
            }

            while (true)
            {
                yield return DotsPath[m_movingTo];

                if (DotsPath.Length == 1)
                {
                    continue;
                }
                m_movingTo = m_movingTo + movementDirection;

                if (m_pathType == PathType.loop)
                {
                    if (m_movingTo >= DotsPath.Length)
                    {
                        m_movingTo = 0;
                    }
                    if (m_movingTo < 0)
                    {
                        m_movingTo = DotsPath.Length - 1;
                    }
                }

            }
        }
    }
}
