using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private VirtualJoystick m_MobileJoystick;
        [SerializeField] private SpaceShip m_TargetShip;
        [SerializeField] private ControlMode m_ControlMode;

        [SerializeField] private PointerClickHold m_MobilePrimaryFire;
        [SerializeField] private PointerClickHold m_MobileSecondaryFire;


        public void SetTargetShip(SpaceShip ship) => m_TargetShip = ship;

        public enum ControlMode
        {
            Keyboard,
            Mobile
        }

        private void Start()
        {
            if(m_ControlMode == ControlMode.Keyboard)
            {
                m_MobileJoystick.gameObject.SetActive(false);

                m_MobilePrimaryFire.gameObject.SetActive(false);
                m_MobileSecondaryFire.gameObject.SetActive(false);
            }

            else
            {
                m_MobileJoystick.gameObject.SetActive(true);    

                m_MobilePrimaryFire.gameObject.SetActive(true);
                m_MobileSecondaryFire.gameObject.SetActive(true);
            }    
                
        }

        private void Update()
        {
            if (m_TargetShip == null) return;

            if (m_ControlMode == ControlMode.Keyboard)
                KeyboardControl();

            if (m_ControlMode == ControlMode.Mobile)
                MobileControl();
        }

        private void MobileControl()
        {
            //var dir = m_MobileJoystick.Value;
            //m_TargetShip.ThrustControl = dir.y;
            //m_TargetShip.TorqueControl = -dir.x;

            Vector3 Direction = m_MobileJoystick.Value;

            var dot = Vector2.Dot(Direction, m_TargetShip.transform.up);
            var dot2 = Vector2.Dot(Direction, m_TargetShip.transform.right);

            if (m_MobilePrimaryFire.IsHold)
            {
                m_TargetShip.Fire(TurretMode.Primary);
            }
            if (m_MobileSecondaryFire.IsHold)
            {
                m_TargetShip.Fire(TurretMode.Secondary);
            }

            m_TargetShip.ThrustControl = Mathf.Max(0, dot);
            m_TargetShip.TorqueControl = -dot2;


        }

        private void KeyboardControl()
        {
            float _ThrustControl = 0f;
            float _TorqueControl = 0f;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                _ThrustControl = -1.0f;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                _ThrustControl = 1.0f;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _TorqueControl = 1.0f;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _TorqueControl = -1.0f;
            }

            if(Input.GetKey(KeyCode.Space))
            {
                m_TargetShip.Fire(TurretMode.Primary);
            }
            if (Input.GetKey(KeyCode.X))
            {
                m_TargetShip.Fire(TurretMode.Secondary);
            }
 
            m_TargetShip.ThrustControl = _ThrustControl;
            m_TargetShip.TorqueControl = _TorqueControl;
        }
            
    }
}

