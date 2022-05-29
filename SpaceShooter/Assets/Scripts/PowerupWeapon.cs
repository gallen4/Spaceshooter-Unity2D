using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerupWeapon : Powerup
    {
        [SerializeField] private TurretProperties m_TurretProperties;


        protected override void OnPickedUp(SpaceShip SS_Ship)
        {
            SS_Ship.AssignedWeapon(m_TurretProperties);
        }
    }
}
