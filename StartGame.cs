using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        GlobalControl.Instance.p1Score = 0;
        GlobalControl.Instance.p2Score = 0;
        GlobalControl.Instance.p3Score = 0;
        GlobalControl.Instance.p4Score = 0;
        GlobalControl.Instance.p1Wins = 0;
        GlobalControl.Instance.p2Wins = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKey)
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
