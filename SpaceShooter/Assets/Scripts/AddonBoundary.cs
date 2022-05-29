using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceShooter
{
    public class AddonBoundary : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == "Player")
            {
                Destroy(gameObject);
            }
        }

        #region Singleton
        public static AddonBoundary Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Warning! There is already a subject with boundary.");
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion

        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

        public enum Mode
        {
            Kill,
            Teleport
        }

        [SerializeField] private Mode m_LimitMode;
        public Mode LimitMode => m_LimitMode;

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, m_Radius);
        }
#endif

    }
}
