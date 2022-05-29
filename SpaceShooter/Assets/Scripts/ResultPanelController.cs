using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceShooter
{


    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private Text m_Kills;
        [SerializeField] private Text m_Score;
        [SerializeField] private Text m_Time;
        [SerializeField] private Text m_ScoreResult;

        [SerializeField] private Text m_Result;

        [SerializeField] private Text m_ButtonText;

        private bool m_Success;

        private void Start()
        {
            gameObject.SetActive(false);


        }

        public void ShowResults(LevelStatistics levelResults, bool success)
        {
            m_Kills.text = "Kills: " + levelResults.NumKills.ToString();
            m_Score.text = "Score: " + levelResults.Score.ToString();
            m_Time.text = "Time: " + levelResults.Time.ToString();

            if (levelResults.Time < 20)
            {
                levelResults.ScoreResult = (int)(((float)levelResults.Score + (float)levelResults.NumKills) * 1.8f);
                m_ScoreResult.text = "Score Result: " + levelResults.ScoreResult.ToString();
            }

            gameObject.SetActive(true);

            m_Success = success;

            m_Result.text = success ? "Win" : "Lose";
            m_ButtonText.text = success ? "Next" : "Restart";

            Time.timeScale = 0;
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;

            if(m_Success)
            {
                LevelSequenceController.Instance.AdvanceLevel();
            }
            else
            {
                LevelSequenceController.Instance.RestartLevel();
            }



        }

    }
}
