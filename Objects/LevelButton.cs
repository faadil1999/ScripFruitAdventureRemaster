using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelName;
    [SerializeField] private TextMeshProUGUI bestTime;
    [SerializeField] private TextMeshProUGUI totalFruit;
    [SerializeField] private TextMeshProUGUI collectedFruit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateTextInfo(int levelNumber)
    {
        levelName.text = "Level";
        bestTime.text = "Best time: " + PlayerPrefs.GetFloat("Level" + levelNumber + "Best time").ToString("00") + " sec";
        collectedFruit.text = PlayerPrefs.GetInt("Level" + levelNumber + "FruitCollected").ToString();
        totalFruit.text = PlayerPrefs.GetInt("Level" + levelNumber + "TotalFruits").ToString();
    }
}
