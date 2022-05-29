using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        Transform target;
        [SerializeField] private float m_Velocity;
        [SerializeField] private float m_LifeTime;
        [SerializeField] private int m_Damage;
        [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

        [SerializeField] public GameObject m_missile;

        private float m_Timer;


        private void Update()
        {

            float stepLength = Time.deltaTime * m_Velocity;
            Vector2 V_Step = transform.up * stepLength;

            RaycastHit2D R_Hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if(R_Hit)
            {
                Destructible D_Dest = R_Hit.collider.transform.root.GetComponent<Destructible>();

                if(D_Dest != null && D_Dest != m_Parent)
                {
                    D_Dest.ApplyDamage(m_Damage);

                    if(m_Parent == Player.Instance.ActiveShip)
                    {
                        Player.Instance.AddScore(D_Dest.ScoreValue);
                    }


                }
                OnProjectileEnter(R_Hit.collider, R_Hit.point);
            }

            m_Timer += Time.deltaTime;
            if(m_Timer > m_LifeTime)
            {
                Destroy(gameObject); 
            }

            transform.position += new Vector3(V_Step.x, V_Step.y, 0);
        }

        private void NearestTargetSearch()
        {
            Transform NearestEnemy = null;
            float nearestEnemyDistance = Mathf.Infinity;

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                float CurrentDistance = Vector2.Distance(transform.position, enemy.transform.position);
                if (CurrentDistance < nearestEnemyDistance)
                {
                    NearestEnemy = enemy.transform;
                    nearestEnemyDistance = CurrentDistance;
                }
            }
            if (NearestEnemy != null)
            {
                AutoFire(NearestEnemy);
            }

        }

        public void AutoFire(Transform enemy)
        {
            m_missile.GetComponent<Projectile>().SetTarget(enemy);
        }   

        public void SetTarget(Transform enemy)
        {
            target = enemy;
        }

        private void OnProjectileEnter(Collider2D C_col, Vector2 V_Pos)
        {
            Destroy(gameObject);      
        } 

        private Destructible m_Parent;
        public void SetParentShooter(Destructible Parent)
        {
            m_Parent = Parent;
        }


    }
}
