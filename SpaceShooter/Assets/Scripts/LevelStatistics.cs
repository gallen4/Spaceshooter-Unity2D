using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{


    public class LevelStatistics : MonoBehaviour
    {
        MainMenuController mainMenu = new MainMenuController();

        public int NumKills;
        public int Score;
        public int Time;
        public int ScoreResult;

        public void Reset()
        {
            mainMenu.Score = ScoreResult;
            NumKills = 0;
            Score = 0;
            Time = 0;
        }
    }
}
