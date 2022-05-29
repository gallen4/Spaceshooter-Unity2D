using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    [SerializeField] private GameObject m_shipPrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        
            Debug.LogWarning("You've won!");
            Destroy(m_shipPrefab);
      
    }
}
