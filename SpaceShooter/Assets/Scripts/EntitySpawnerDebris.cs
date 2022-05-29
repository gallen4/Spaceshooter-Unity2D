using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class EntitySpawnerDebris : MonoBehaviour
    {
        [SerializeField] private Destructible[] m_DebrisPrefab;
        [SerializeField] private int m_NumDebris;
        [SerializeField] private CircleArea m_Area;
        [SerializeField] private float m_RandomSpeed;

        private void Start()
        {
            for (int i = 0; i < m_NumDebris; ++i)
            {
                SpawnDebris();

            }
        }

        private void SpawnDebris()
        {
            int index = Random.Range(0, m_DebrisPrefab.Length);
            GameObject G_Debris = Instantiate(m_DebrisPrefab[index].gameObject);

            G_Debris.transform.position = m_Area.GetDotInsideZone();
            G_Debris.GetComponent<Destructible>().EventOnDeath.AddListener(OnDebrisDie);

            Rigidbody2D RB = G_Debris.GetComponent<Rigidbody2D>();

            if(RB != null && m_RandomSpeed > 0)
            {
                RB.velocity = (Vector2) UnityEngine.Random.insideUnitSphere * m_RandomSpeed;
            }
        }

        private void OnDebrisDie()
        {
            SpawnDebris();
        }


    }
}
