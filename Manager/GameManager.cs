using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public int game_difficulty;

        [Header("Timer info")]
        public bool start_timer;
        public float timer;

        [Header("Level info")]
        public int levelNumber;

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);

        }

        private void Start()
        {
            if(game_difficulty == 0)
            {
               game_difficulty = PlayerPrefs.GetInt("GameDifficulty");
            }
        }

        private void Update()
        {
            if(start_timer)
            {
                timer += Time.deltaTime;
            }
        }

        //function for saving a difficulty of the game
        public void SaveGameDifficulty()
        {
            PlayerPrefs.SetInt("GameDifficulty", game_difficulty);
        }

        //function for saving the best time
        public void SaveBestTime()
        {
            float last_time = PlayerPrefs.GetFloat("Level" + levelNumber + "Best time", 999);

            if(timer < last_time)
            {
                PlayerPrefs.SetFloat("Level"+ levelNumber + "Best time", timer);
            }
        }

        public void SaveTotalFruitCollected()
        {
            int total_fruit = PlayerPrefs.GetInt("TotalFruitCollected");

            int newTotalFruit = total_fruit + PlayerManager.instance.fruits;

            PlayerPrefs.SetInt("TotalFruitCollected", newTotalFruit);

            //for saving amount of fruits collected at the specific level 
            PlayerPrefs.SetInt("Level" + levelNumber + "FruitCollected", PlayerManager.instance.fruits);
            PlayerManager.instance.fruits = 0;
        }

        public void SaveLevelInfo()
        {
            int nextLevelNumber =levelNumber + 1;
            PlayerPrefs.SetInt("Level" + nextLevelNumber + "Unlocked", 1);
        }

        public void SaveCharacterId()
        {

        }
    }
}
