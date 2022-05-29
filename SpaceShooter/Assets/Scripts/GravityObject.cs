using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class GravityObject : MonoBehaviour
    {
        [SerializeField] private float m_Force;
        [SerializeField] private float m_Radius;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.attachedRigidbody == null) return;

            Vector2 Direction = transform.position - collision.transform.position;

            float Distantion = Direction.magnitude;

            if(Distantion < m_Radius)
            {
                Vector2 Force = Direction.normalized * m_Force * (Distantion / m_Radius);
                collision.attachedRigidbody.AddForce(Force, ForceMode2D.Force);
            }
        }
        #if UNITY_EDITOR
        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }
        #endif
    }
}
