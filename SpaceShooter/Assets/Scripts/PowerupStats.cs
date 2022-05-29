using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerupStats : Powerup
    {
        public enum EffectType
        {
            AddAmmo,
            AddEnergy,
            Vulnerability,
            AddSpeed
        }

        [SerializeField] private EffectType m_EffectType;
        [SerializeField] private float m_Value;
        [SerializeField] private float Lifetime;

        public int m_Delay = 2;
        protected override void OnPickedUp(SpaceShip SS_Ship)
        {
            if (m_EffectType == EffectType.AddEnergy)
            {
                SS_Ship.AddEnergy((int) m_Value);
            }
            if (m_EffectType == EffectType.AddAmmo)
            {
                SS_Ship.AddAmmo((int) m_Value);
            }
            if(m_EffectType == EffectType.Vulnerability)
            {
                SS_Ship.ApplyDamage((int) m_Value);
            }
            if (m_EffectType == EffectType.AddSpeed)
            {
                SS_Ship.AddThrust((int) m_Value);
            }
        }

        //public void Delay()
        //{
        //    Invoke("ReduceThrustl", m_Delay);
        //    Debug.LogError("KAVA");
        //}

        //public void ReduceThrust(SpaceShip SS_Ship)
        //{
        //    SS_Ship.ReduceThrust((int) 3000);
        //}
    }
}
