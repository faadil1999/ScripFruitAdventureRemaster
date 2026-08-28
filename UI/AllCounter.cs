using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AllCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI fruitCollected;
    private bool gamePaused = false;
    [Header("Background sound")]
    [SerializeField] private int idBGSound;

    [Header("Menu gameObject")]
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pausedGameUI;
    [SerializeField] private GameObject endLevelGameUI;
    [SerializeField] private GameObject deadGameUI;

    [Header("End level paramerters")]
    [SerializeField] private TextMeshProUGUI yourTime;
    [SerializeField] private TextMeshProUGUI bestTime;
    [SerializeField] private TextMeshProUGUI collectedFruit;

    [Header("Controls")]
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private Button jumpButton;
    [SerializeField] private Button specialButton;
    [SerializeField] private Button action;
    [SerializeField] private Button pauseButton;
    public bool isPhoneTesting = false;

    private void Awake()
    {
        PlayerManager.instance.gameUi = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.levelNumber = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        SwitchUI(inGameUI);
        isPhoneTesting = false;
        AudioManager.instance.PlayBGSound(idBGSound);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCounter_UI();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckIfNotPaused();
        }
    }

    // this function is for checking if the game is paused
    private bool CheckIfNotPaused()
    {
        if(!gamePaused)
        {
            gamePaused = true;
            Time.timeScale = 0;
            SwitchUI(pausedGameUI);
            return true;
        }
        else
        {
            gamePaused = false;
            Time.timeScale = 1;
            SwitchUI(inGameUI);
            return false;
        }
    }
    private void UpdateCounter_UI()
    {
        timerText.text = "Timer: " + GameManager.instance.timer.ToString("00") + " s";
        fruitCollected.text = "Fruits: " + PlayerManager.instance.fruits;
    }

    public void SwitchUI(GameObject menu_ui)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            // desactive all menu ui
            transform.GetChild(i).gameObject.SetActive(false);
        }
        menu_ui.SetActive(true);
        if(menu_ui == inGameUI && isPhoneTesting == true)
        {
            joystick.gameObject.SetActive(true);
            jumpButton.gameObject.SetActive(true);
            specialButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(true); 
            action.gameObject.SetActive(true);
        }

        if(menu_ui == pausedGameUI)
        {
            pauseButton.gameObject.SetActive(false);
        }
    }

    public void LoadMainMenu()
    {
        AudioManager.instance.PlaySFX(4);
        Time.timeScale = 1;
        InitialisateData();
        AudioManager.instance.PlayBGSound(1);
        SceneManager.LoadScene("Menu");
    }

    //Function for restarting the current level
    public void ReloadCurrentLevel()
    {
        AudioManager.instance.PlaySFX(4);
        InitialisateData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Function for initiate all player's data
    private static void InitialisateData()
    {
        GameManager.instance.timer = 0;
        GameManager.instance.start_timer = false;
        PlayerManager.instance.fruits = 0;
    }

    public void ContinueGame()
    {
        CheckIfNotPaused();
    }

    public void OnEndLevel()
    {
        SwitchUI(endLevelGameUI);

        yourTime.text = "Your time: " + GameManager.instance.timer.ToString("00") + "s";
        bestTime.text = "Best time: " + PlayerPrefs.GetFloat("Level" + GameManager.instance.levelNumber + "Best time").ToString("00") + "s";
        collectedFruit.text = fruitCollected.text;
        GameManager.instance.timer = 0;
    }

    public void LoadNextLevel()
    {
        try
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        catch (Exception e)
        {
            Debug.LogError($"Erreur lors du chargement de la scène : {e.Message}");
        }
    }

    public void SwitchUiWhenDead()
    {
        SwitchUI(deadGameUI);
    }

    public void AssignPlayerControl(Player player)
    {
        player.joystick = joystick;
        jumpButton.onClick.RemoveAllListeners();
        jumpButton.onClick.AddListener(player.JumpButton);
        pauseButton.onClick.RemoveAllListeners();
        pauseButton.onClick.AddListener(ContinueGame);

    }
}
