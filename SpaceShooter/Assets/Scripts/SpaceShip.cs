using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{

    [RequireComponent(typeof(Rigidbody2D))]

    public class SpaceShip : Destructible
    {
        [SerializeField] private Sprite m_PreviewImage;
        /// <summary>
        /// Mass for automatically set up to rigid.
        /// </summary>
        [Header("Space Ship")]
        [SerializeField] private float m_mass;
        /// <summary>
        /// Pushing forward force.
        /// </summary>
        [SerializeField] private float m_thrust;
        /// <summary>
        /// Rotation force.
        /// </summary>
        [SerializeField] private float m_mobility;
        /// <summary>
        /// Maximal linear speed.
        /// </summary>
        [SerializeField] private float m_maxlinearvelocity;
        /// <summary>
        /// Maximal angular speed. Degree/sec.
        /// </summary>
        [SerializeField] private float m_maxangularvelocity;
        /// <summary>
        /// Saved link on rigid.
        /// </summary>
        private Rigidbody2D m_rigid;
        private float m_Delay = 5f;

        public float MaxLinearVelocity => m_maxlinearvelocity;
        public float MaxAngularVelocity => m_maxangularvelocity;
        public Sprite PreviewImage => m_PreviewImage;
        
       
        #region PublicAPI


        /// <summary>
        /// LinearVelocity control. -1.0 to +1.0.
        /// </summary>
        public float ThrustControl { get; set; }
        /// <summary>
        /// AngularVelocity control. -1.0 to +1.0.
        /// </summary>
        public float TorqueControl { get; set; }



        #endregion

        public void AddThrust(float value)
        {
            StartCoroutine(Delay_Cor(value));
            m_thrust += value;
        }
        public void ReduceThrust(float value)
        {
            m_thrust -= value;
        }
      

        IEnumerator Delay_Cor(float value)
        {
            yield return new WaitForSeconds(m_Delay);
            ReduceThrust((int) value);
        }


        #region Event
        protected override void Start()
        {
            base.Start();

            m_rigid = GetComponent<Rigidbody2D>();
            m_rigid.mass = m_mass;
            m_rigid.inertia = 1;

            Init();
        }
        private void FixedUpdate()
        {
            UpdateRigidBody();
            UpdateEnergyRegen();
        }
        #endregion
        /// <summary>
        /// Adding forces to ship.
        /// </summary>
        private void UpdateRigidBody()
        {
            m_rigid.AddForce(ThrustControl * m_thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

            m_rigid.AddForce(-m_rigid.velocity * (m_thrust / m_maxlinearvelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_rigid.AddTorque(TorqueControl * m_mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            m_rigid.AddTorque(-m_rigid.angularVelocity * (m_mobility / m_maxangularvelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

        }
        [SerializeField] Turret[] m_Turrets;
        
        public void Fire(TurretMode mode)
        {
            for(int i = 0; i < m_Turrets.Length; ++i)
            {
                if (m_Turrets[i].Mode == mode)
                        m_Turrets[i].Fire();
            }
        }
        [SerializeField] private int m_MaxEnergy;
        [SerializeField] private int m_MaxAmmo;
        [SerializeField] private int m_EnergyRegen;

        private float m_PrimaryEnergy;
        private int m_SecondaryAmmo;


        #region Ammo...
        public void AddEnergy(int E_Energy)
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + E_Energy, 0, m_MaxEnergy);

        }

        public void AddAmmo(int A_Ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + A_Ammo, 0, m_MaxAmmo);
        }

        private void Init()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }

        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy += (float) m_EnergyRegen * Time.fixedDeltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);
        }

        public bool DrawAmmo(int A_count)
        {
            if (A_count == 0)
                return true;

            if(m_SecondaryAmmo >= A_count)
            {
                m_SecondaryAmmo -= A_count;
                return true;
            }

            return false;
        }

        public bool DrawEnergy(int E_count)
        {
            if (E_count == 0)
                return true;

            if (m_PrimaryEnergy >= E_count)
            {
                m_PrimaryEnergy -= E_count;
                return true;
            }

            return false;
        }

        public void AssignedWeapon(TurretProperties T_Props)
        {
            for(int i = 0; i < m_Turrets.Length; ++i)
            {
                m_Turrets[i].AssignedLoadout(T_Props);
            }
        }
        #endregion


    }
}

