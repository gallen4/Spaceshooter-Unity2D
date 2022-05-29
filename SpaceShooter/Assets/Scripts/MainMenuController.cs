using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
   

    public class MainMenuController : SingletonBase<MainMenuController>
    {
        [SerializeField] private Text m_ScoreResult;

        [SerializeField] private SpaceShip m_DefaultShip;
        [SerializeField] private GameObject m_EpisodeSelection;
        [SerializeField] private GameObject m_ShipSelection;
        [SerializeField] private GameObject m_MainMenu;
        [SerializeField] private GameObject m_Results;

        public int Score;

        private void Start()
        {
            LevelSequenceController.PlayerShip = m_DefaultShip;
        }

        public void OnButtonStart()
        {
            m_EpisodeSelection.gameObject.SetActive(true);
            gameObject.SetActive(false);
            
        }

        public void OnSelectShip()
        {
            m_ShipSelection.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnResult()
        {
            LevelStatistics levelResults = new LevelStatistics();

             

            m_Results.SetActive(true);
            gameObject.SetActive(false);

            m_ScoreResult.text = "Score Result: " + Score.ToString();
        }

        public void onMainMenu()
        {
            m_MainMenu.SetActive(true);
            gameObject.SetActive(false);
        }


        public void onButtonExit()
        {
            Application.Quit();
        }


    }
}
