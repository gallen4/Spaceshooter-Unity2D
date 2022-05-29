using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{


    public class PlayerShipSelectionController : MonoBehaviour
    {
        [SerializeField] private SpaceShip m_Ship;

        [SerializeField] private Text m_ShipName;
        [SerializeField] private Text m_HitPoints;
        [SerializeField] private Text m_Speed;
        [SerializeField] private Text m_Agility;

        

        [SerializeField] private Image m_ShipPreview;

        private void Start()
        {
            if(m_Ship != null)
            {
                m_ShipName.text = m_Ship.nickname;
                m_HitPoints.text = "HP : " + m_Ship.HitPoints.ToString();
                m_Speed.text = "SPEED : " + m_Ship.MaxLinearVelocity.ToString();
                m_Agility.text = "AGILITY : " + m_Ship.MaxAngularVelocity.ToString();
                m_ShipPreview.sprite = m_Ship.PreviewImage;
            }
        }
        public void OnSelectShip()
        {
            LevelSequenceController.PlayerShip = m_Ship;
            MainMenuController.Instance.gameObject.SetActive(true);
        }
    }
}
