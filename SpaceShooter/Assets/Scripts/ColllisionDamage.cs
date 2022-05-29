using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceShooter
{
    public class ColllisionDamage : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoundary";

        [SerializeField] private float m_SpeedDamageModifier;
        [SerializeField] private float m_Damage;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == IgnoreTag) return;

            var m_Destructble = transform.root.GetComponent<Destructible>();

            if(m_Destructble != null)
            {
               m_Destructble.ApplyDamage((int)m_Damage + (int)(m_SpeedDamageModifier * collision.relativeVelocity.magnitude));
            }
        }

    }
}




            