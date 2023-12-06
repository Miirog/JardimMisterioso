using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class SnakeDeck : MonoBehaviour
{
    public GameObject[] SnakePlants;
    public int TotalSnakeValue = 0;
    int[] PlantValues = { 1, 2, 4 };
    List<int> TotalPlantsList = new List<int> { };
    public TMP_Text SnakePoints;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // SnakePoints = FindObjectOfType<TextMeshProUGUI>();


        //Set all Plants images to False
        foreach (GameObject PlayerPlant in SnakePlants)
        {
            PlayerPlant.SetActive(false);
        }
    }

    //Update every frame
    void Update()
    {
        SnakePoints.text = TotalSnakeValue.ToString();
    }

    //Calculates Total Value of Plants
    public void GetTotalSnakeValue(int v)
    {
        TotalSnakeValue += v;
    }

    //Get a new Plant Value
    public void GetPlantValue(int n)
    {
        GetTotalSnakeValue(PlantValues[n]);

    }

    //Add a new Plant to List + PlantValue
    public void AddTotalPlant(int n)
    {
        TotalPlantsList.Add(PlantValues[n]);
        foreach (GameObject SnakePlant in SnakePlants)
        {
            if (SnakePlant.tag == n.ToString() && !SnakePlant.activeSelf)
            {
                SnakePlant.SetActive(true);
                break;

            }
        }
        CheckMaxPlants(PlantValues[n], n);
        GetPlantValue(n);

        //Defines Which plant should be active

    }

    //Function to check All Plants in List
    public void CheckMaxPlants(int n, int listindex)
    {
        int DesiredItem = n;
        int Counter = 0;
        //Checks if there's repeated plants in list
        foreach (int List in TotalPlantsList)
        {
            if (List == DesiredItem)
            {
                Counter++;
            }

        }

        //Removes plant that repeats itself 3 times and adds the next value plant
        if (Counter == 3)
        {
            //Remove plants from list
            foreach (GameObject PlayerPlant in SnakePlants)
            {
                if (PlayerPlant.tag == listindex.ToString())
                {
                    PlayerPlant.SetActive(false);
                    TotalPlantsList.Remove(DesiredItem);
                }
            }

            //Removes the points from the old row when full
            if (DesiredItem != 4)
            {
                TotalSnakeValue = TotalSnakeValue - (DesiredItem * 3);
            }
            else
            {
                gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
                gameManager.PlayerWin();
            }
            //Adds next plant in list
            AddTotalPlant(listindex + 1);
        }
    }
}




