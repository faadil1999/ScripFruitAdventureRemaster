using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace AdventureFruit
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject level_button;
        [SerializeField] private Transform parent_level_button;
        [SerializeField] private bool[] level_open;

        void Start()
        {
            PlayerPrefs.SetInt("Level" + 1 + "Unlocked", 1);

            AssignLevelBoolean();
            for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                if (!level_open[i]) 
                    return;

                string level_name = "Level " + i;
                GameObject new_button = Instantiate(level_button, parent_level_button);
                new_button.GetComponent<Button>().onClick.AddListener(() => LoadLevel(level_name));
                new_button.GetComponentInChildren<TextMeshProUGUI>().text = level_name;
                new_button.GetComponent<LevelButton>().UpdateTextInfo(i);
            }
        }

        private void AssignLevelBoolean()
        {
            for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;
                if (unlocked)
                {
                    level_open[i] = true;
                }
                else
                {
                    return;
                }
            }
        }

        public void LoadLevel(string level_name)
        {
            AudioManager.instance.StopBGSound();
            GameManager.instance.SaveGameDifficulty();
            SceneManager.LoadScene(level_name);
        }

        public void LoadNewGame()
        {
            for (int i = 2; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;
                if (unlocked)
                {
                    PlayerPrefs.SetInt("Level" + i + "Unlocked", 0);
                }
                else
                {
                    SceneManager.LoadScene("Level 1");
                    return;
                }
            }
        }

        public void LoadContinueGame()
        {
            for(int i = 2; i< SceneManager.sceneCountInBuildSettings; i++)
            {
                bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;
                if(!unlocked)
                {
                    SceneManager.LoadScene("Level " + (i - 1));
                    return;
                }
            }
        }
    }
}
