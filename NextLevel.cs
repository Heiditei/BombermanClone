using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject nextLevelPanel;

    public GameObject LevelManager;
    private VictoryConditions victoryConditions;

    // Update is called once per frame
    private void Start()
    {
        //VictoryConditions victoryConditions = LevelManager.GetComponent<VictoryConditions>();
        nextLevelPanel.SetActive (false);
        //victoryConditions.SetStatus (false);
    }

    private void Update()
    {
       
        if (this.nextLevelPanel.activeInHierarchy == true)// && victoryConditions.GetStatus() == true)
        {
            OfferNext();
        }
    }

    void OfferNext()
    {
        //Find active scene, and the name for it
        Scene scene = SceneManager.GetActiveScene();
        string thisLevel = scene.name;
        Debug.Log("Active scene is '" + scene.name + "'.");
 
        //All levels should be named string LevelX, where X is a number 
        //Find X and convert it to int
        char last = thisLevel[thisLevel.Length - 1];
        Debug.Log("Last: " + last);
        int levelCount = Convert.ToInt32(new string(last, 1));
        Debug.Log("LevelCount: " + levelCount);

        //To find the next level, logically number of active level +1 = next level
        int next = levelCount + 1;
        Debug.Log("Next: " + next);
        string nextLevel = "Level" + next;
        Debug.Log("NextLevel: " + nextLevel);

        //Find if next level is available with this method, if so offer it, if not -> EndGame
        if (Application.CanStreamedLevelBeLoaded("" + nextLevel))
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("" + nextLevel);
                Debug.Log("Loading scene '" + nextLevel + "'.");
            }
        }
        else
        {
            SceneManager.LoadScene("EndGame");
            Debug.Log("No more levels detected.");
        }
    }
}
