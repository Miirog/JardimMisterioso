using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerDeck : MonoBehaviour
{
    public GameObject[] PlayerPlants;
    public int TotalValue = 0;
    int[] PlantValues = { 1, 2, 4 };
    List<int> TotalPlantsList = new List<int> { };
    public TMP_Text PlayerPoints;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerPoints = FindObjectOfType<TextMeshProUGUI>();


        //Set all Plants images to False
        foreach (GameObject PlayerPlant in PlayerPlants)
        {
            PlayerPlant.SetActive(false);
        }
    }

    //Update every frame
    void Update()
    {
        PlayerPoints.text = TotalValue.ToString();
    }

    //Calculates Total Value of Plants
    public void GetTotalValue(int v)
    {
        TotalValue += v;
    }

    //Get a new Plant Value
    public void GetPlantValue(int n)
    {
        GetTotalValue(PlantValues[n]);

    }

    //Add a new Plant to List + PlantValue
    public void AddTotalPlant(int n)
    {
        // Debug.Log(PlantValues[n]);
        TotalPlantsList.Add(PlantValues[n]);
        foreach (GameObject PlayerPlant in PlayerPlants)
        {
            if (PlayerPlant.tag == n.ToString() && !PlayerPlant.activeSelf)
            {
                PlayerPlant.SetActive(true);
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
            foreach (GameObject PlayerPlant in PlayerPlants)
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
                TotalValue = TotalValue - (DesiredItem * 3);
            }
            else
            {
                gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
                gameManager.PlayerLost();
            }


            //Adds next plant in list
            AddTotalPlant(listindex + 1);
        }
    }
}




