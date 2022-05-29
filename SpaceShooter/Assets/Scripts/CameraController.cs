using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera m_Camera;
        [SerializeField] private Transform m_Target;
        [SerializeField] private float m_InterpolationLinear;
        [SerializeField] private float m_InterpolationAngular;
        [SerializeField] private float m_CameraZOffSet;
        [SerializeField] private float m_ForwardOffSet;

        private void FixedUpdate()
        {
            if (m_Target == null || m_Camera == null) return;

            Vector2 CameraPos = m_Camera.transform.position;
            Vector2 TargetPos = m_Target.position + m_Target.transform.up * m_ForwardOffSet;
            Vector2 NewCameraPos = Vector2.Lerp(CameraPos, TargetPos, m_InterpolationLinear * Time.deltaTime);

            m_Camera.transform.position = new Vector3(NewCameraPos.x, NewCameraPos.y, m_CameraZOffSet);

            if(m_InterpolationAngular > 0)
            {
                m_Camera.transform.rotation = Quaternion.Slerp(m_Camera.transform.rotation, m_Target.rotation, m_InterpolationAngular * Time.deltaTime);
            }
        }

        public void SetTarget(Transform newTarget)
        {
            m_Target = newTarget;
        }
    }
}
