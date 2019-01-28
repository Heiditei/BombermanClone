using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryConditions : MonoBehaviour {
    public bool victoryConditionsMet;

    [SerializeField]
    private GameObject nextLevelPanel;

    [SerializeField]
    private GameObject gameOverPanel;

    private int count;

    void Start()
    {
        victoryConditionsMet = false;
        count = 0;
    }
    

    void Update () {
        GameObject[] liveEnemies;
        GameObject[] liveCharacters;

        //Finds gameObjects with given tag
        liveEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        liveCharacters = GameObject.FindGameObjectsWithTag("Character");


        if (liveEnemies.Length == 0 && liveCharacters.Length == 1)
        {
            Debug.Log("No game objects are tagged with 'Enemy'");
                victoryConditionsMet = true;
        }
        else
        {
            Debug.Log("Enemies left: " + liveEnemies.Length);
            Debug.Log("Characters left: " + liveCharacters.Length);
        }

        if (liveCharacters.Length < 1)
        {
            this.gameOverPanel.SetActive(true);
        }

        //Ticks the box in unity, thus starting a chain reaction
        if (victoryConditionsMet == true)
        {
            GameObject winner = GameObject.FindGameObjectWithTag("Character");
            if(count == 0)
            {
                winner.GetComponent<PlayerScore>().SetPoints(1000);
                winner.GetComponent<PlayerScore>().SavePlayer();
                count++;
            }
            this.nextLevelPanel.SetActive(true);
        }

    }
    public bool GetStatus()
    {
        return victoryConditionsMet;
    }
    public void SetStatus(bool status)
    {
        victoryConditionsMet = status;
    }
    }

