using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]

    public abstract class Powerup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            SpaceShip S_Ship = collision.transform.root.GetComponent<SpaceShip>();

            if (S_Ship != null && Player.Instance.ActiveShip)
            {
                OnPickedUp(S_Ship);
                Destroy(gameObject);
            }
        }

        protected abstract void OnPickedUp(SpaceShip SS_Ship);

    }
}

