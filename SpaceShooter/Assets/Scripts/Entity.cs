using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Based class of every gameobjects on scene.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        /// Name of object for user.
        /// </summary>
        [SerializeField] private string m_nickname;
        public string nickname => m_nickname;

    }
}

