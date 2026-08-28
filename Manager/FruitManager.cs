using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FruitManager : MonoBehaviour
{
    [SerializeField] private Transform[] fruit_positions;
    [SerializeField] private GameObject prefab_fruit;
    [SerializeField] private bool is_random;
    private int fruit_index;
    // Start is called before the first frame update
    void Start()
    {
        fruit_positions = GetComponentsInChildren <Transform>();
        for (int i = 1; i < fruit_positions.Length; i++)
        {
            GameObject newFruit = Instantiate(prefab_fruit, fruit_positions[i]);
            if (is_random == true)
            {
                fruit_index = UnityEngine.Random.Range(0, Enum.GetNames(typeof(FruitTypes)).Length);
            }
            else
            {
                fruit_index++;
                if(fruit_index > Enum.GetNames(typeof(FruitTypes)).Length)
                    fruit_index = 0;
            }
            newFruit.GetComponent<Fruit>().FruitSetup(fruit_index);
            int level_number = GameManager.instance.levelNumber;
            int totalAmountFruits = PlayerPrefs.GetInt("Level" + level_number + "TotalFruits");

            if(totalAmountFruits != fruit_positions.Length -1 )
            {
                PlayerPrefs.SetInt("Level" + level_number + "TotalFruits", fruit_positions.Length - 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
