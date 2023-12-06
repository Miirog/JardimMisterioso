using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public Button digBtn;
    public Button stopBtn;
    public Button doubleBtn;

    int LastPlantValue;
    public TMP_Text Results;

    PlayerDeck getplantsvalue;

    SnakeDeck getsnakevalue;
    // Start is called before the first frame update
    void Start()
    {
        digBtn.onClick.AddListener(() => DigClicked());
        stopBtn.onClick.AddListener(() => StopClicked());
        doubleBtn.onClick.AddListener(() => DoubleClicked());

        //disables doubleButton on start
        doubleBtn.interactable = false;

        //Calls the function at the start so it gives you 2 random plants
        DigClicked();
        DigClicked();
        SnakeTurn();
        SnakeTurn();
    }


    private void DigClicked()
    {
        //Generates a new random number between 0 and 2 to create a new Plant
        System.Random ran = new();
        //It is (0, 3) because last number in this case (3) is ignored.
        int RandomNumber = ran.Next(0, 3);
        LastPlantValue = RandomNumber;
        getplantsvalue = GameObject.FindGameObjectWithTag("Plants").GetComponent<PlayerDeck>();
        getplantsvalue.AddTotalPlant(RandomNumber);

        //Activates Double Button
        doubleBtn.interactable = true;
    }
    private void StopClicked()
    {
        //Disables all Buttons
        digBtn.interactable = false;
        stopBtn.interactable = false;
        doubleBtn.interactable = false;

        //Start Snake Turn
        StartCoroutine(SnakeTurnWithDelay());

    }

    //DoubleButton is a one time thing!
    private void DoubleClicked()
    {
        //Gets the last plant Value and call it again
        getplantsvalue = GameObject.FindGameObjectWithTag("Plants").GetComponent<PlayerDeck>();
        getplantsvalue.AddTotalPlant(LastPlantValue);

        //Disables DoubleButton
        doubleBtn.interactable = false;

    }





    private void SnakeTurn()
    {
        //Generates a new random number between 0 and 2 to create a new Plant
        System.Random ran = new();
        //It is (0, 3) because last number in this case (3) is ignored.
        int RandomNumber = ran.Next(0, 3);
        getsnakevalue = GameObject.FindGameObjectWithTag("Snake").GetComponent<SnakeDeck>();
        getsnakevalue.AddTotalPlant(RandomNumber);


    }

    IEnumerator SnakeTurnWithDelay()
    {
        while (getplantsvalue.TotalValue >= getsnakevalue.TotalSnakeValue)
        {
            SnakeTurn();
            if (getplantsvalue.TotalValue < getsnakevalue.TotalSnakeValue)
            {
                if (getsnakevalue.TotalSnakeValue >= 15)
                {
                    PlayerWin();
                }
                else
                {
                    PlayerLost();
                }
            }

            // Waits 0.5 seconds to call function again
            yield return new WaitForSeconds(0.5f);
        }
    }


    public void PlayerLost()
    {
        Results.text = "Você perdeu";
    }

    public void PlayerWin()
    {
        Results.text = "Você ganhou!!";
    }
}