using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class EntitySpawner : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Loop
        }

        [SerializeField] private Entity[] m_EntityPrefab;
        [SerializeField] private CircleArea m_Area;
        [SerializeField] private SpawnMode m_Mode;
        [SerializeField] private int m_NumSpawns;
        [SerializeField] private float m_RespawnTime;

        private float m_Timer;

        private void Start()
        {
            if(m_Mode == SpawnMode.Start)
            {
                SpawnEntities();
            }
            m_Timer = m_RespawnTime;
        }
        private void Update()
        {
            if(m_Timer > 0)
            {
                m_Timer -= Time.deltaTime;
            }
            if(m_Mode == SpawnMode.Loop && m_Timer < 0)
            {
                SpawnEntities();
                m_Timer = m_RespawnTime;
            }
        }
        private void SpawnEntities()
        {
            for(int i = 0; i < m_NumSpawns; ++i)
            {
                int index = Random.Range(0, m_EntityPrefab.Length);

                GameObject Entity = Instantiate(m_EntityPrefab[index].gameObject);
                Entity.transform.position = m_Area.GetDotInsideZone();
            }
        }


    }
}
