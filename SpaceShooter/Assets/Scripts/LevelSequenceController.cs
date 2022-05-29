using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{


    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        public static string MainMenuSceneNickname = "MainMenu";

        public Episode CurrentEpisode { get; private set; }
        public int CurrentLevel { get; private set; }

        public bool LastLevelResult { get; private set; }

        public LevelStatistics LevelStatistics { get; private set; }

        public static SpaceShip PlayerShip { get; set; }

        public void StartEpisode(Episode E_Episode)
        {
            CurrentEpisode = E_Episode;
            CurrentLevel = 0;

            LevelStatistics = new LevelStatistics();
            LevelStatistics.Reset();    

            SceneManager.LoadScene(E_Episode.Levels[CurrentLevel]);

        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }


        public void FinishCurrentLevel(bool success)
        {
            LastLevelResult = success;

            Calculate();

            ResultPanelController.Instance.ShowResults(LevelStatistics, success);
            

        }

        public void AdvanceLevel()
        {
            LevelStatistics.Reset(); 

            CurrentLevel++;

            if(CurrentEpisode.Levels.Length <= CurrentLevel)
            {
                SceneManager.LoadScene(MainMenuSceneNickname);
            }
            else
            {
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            }

            
        }
        private void Calculate()
        {
            LevelStatistics.Score = Player.Instance.Score;
            LevelStatistics.NumKills = Player.Instance.NumKills;
            LevelStatistics.Time = (int)LevelController.Instance.LevelTime;
        }

    }
}

