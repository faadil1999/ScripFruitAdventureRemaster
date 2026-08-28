using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchMenu : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    [SerializeField] private VolumeManager[] volumeManager;
    private void Start()
    {
        bool isUnlocked = PlayerPrefs.GetInt("Level" + 2 + "Unlocked") == 1;
        continueButton.SetActive(isUnlocked);
        
        for (int i = 0; i < volumeManager.Length; i++)
        {
            volumeManager[i].GetComponent<VolumeManager>().SetupVolume();
        }
    }
    public void SwitchMenuTo( GameObject menu_ui)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            // desactive all menu ui
            transform.GetChild(i).gameObject.SetActive(false);
        }
        //audio
        AudioManager.instance.PlaySFX(5);
        menu_ui.SetActive(true);
    }

    public void ActivatePlayerManager(GameObject menu_ui)
    {
        menu_ui.SetActive(true);
    }
    public void CloseGame()
    {
        Application.Quit();
    }

    public void WipePrefabs()
    {
        PlayerPrefs.DeleteAll();
        GameManager.instance.timer = 0;
        PlayerManager.instance.fruits = 0;
    }

    public void SetDifficulty(int difficulty) => GameManager.instance.game_difficulty = difficulty;
}
