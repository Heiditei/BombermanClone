using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public float score;
    public int backUpScore;

    void Start()
    {
        switch (gameObject.name)
        {
            case "Blue":
                score = GlobalControl.Instance.p1Score;
                backUpScore = Scores.p1Score;
                break;
            case "Green":
                score = GlobalControl.Instance.p2Score;
                backUpScore = Scores.p2Score;
                break;
            case "Red":
                score = GlobalControl.Instance.p3Score;
                backUpScore = Scores.p3Score;
                break;
            case "Purple":
                score = GlobalControl.Instance.p4Score;
                backUpScore = Scores.p4Score;
                break;
        }
    }


    public void SetPoints(int points)
    {
        score += points;
        backUpScore += points;
        Debug.Log(gameObject.name + " Player  Score: " + score);
    }


    public float GetPoints()
    {
        Debug.Log(gameObject.name + " Player  Score: " + score);
        Debug.Log(gameObject.name + " Player  buScore: " + backUpScore);

        return score;
        
    }


    public void SavePlayer()
    {
        switch (gameObject.name) {
            case "Blue":
                GlobalControl.Instance.p1Score = score;
                Scores.p1Score = backUpScore;
                break;
            case "Green":
                GlobalControl.Instance.p2Score = score;
                Scores.p2Score = backUpScore;
                break;
            case "Red":
                GlobalControl.Instance.p3Score = score;
                Scores.p3Score = backUpScore;
                break;
            case "Purple":
                GlobalControl.Instance.p4Score = score;
                Scores.p4Score = backUpScore;
                break;
        }
    }

}
