using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// Object, that can be destroyed. Has a hitpoints.
    /// </summary>
    public class Destructible : Entity
    {
        public enum Type
        {
            Debris,
            Normal
        }

        #region Properties
        /// <summary>
        /// Object ignores any damage.
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        /// <summary>
        /// Your started hitpoints.
        /// </summary>
        [SerializeField] private int m_HitPoints;
        /// <summary>
        /// Your current hitpoints.
        /// </summary>
        private int m_CurrentHitPoints;
        [SerializeField] GameObject Aster_Prefab;
        [SerializeField] private Type m_Type;
        public bool IsIndestrictible => m_Indestructible;
        public int HitPoints => m_CurrentHitPoints;
        #endregion

        #region UnityEvents
        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;
        }
        #endregion

        #region PublicAPI
        /// <summary>
        /// Taking damage to object.
        /// </summary>
        /// <param name="damage"> Damage to object. </param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrentHitPoints -= damage;

            if (m_CurrentHitPoints <= 0)
                onDeath();
        }
        #endregion

        #region Death
        /// <summary>
        /// Destroying object, when it has less than 0 hitpoints.
        /// </summary>
        protected virtual void onDeath()
        {
            Vector3 SpawnPos = transform.position;

            if (m_Type == Type.Debris)
            {
                var AsteroidBroken = Instantiate(Aster_Prefab);

                for(int i = 0; i < 2; ++i)
                {
                     Instantiate(AsteroidBroken, SpawnPos, Quaternion.identity);
                     
                }

            }
            Destroy(gameObject);

        }

        private static HashSet<Destructible> m_AllDestructbles;
        public static IReadOnlyCollection<Destructible> AllDestructbles => m_AllDestructbles;

        protected virtual void OnEnable()
        {
            if(m_AllDestructbles == null)
            {
                m_AllDestructbles = new HashSet<Destructible>();

            }
            m_AllDestructbles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructbles.Remove(this);
        }

        public const int TeamIdNeutral = 0;

        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;



        #endregion


        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        #region Score

        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;


        #endregion
    }
}
