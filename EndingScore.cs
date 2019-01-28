using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingScore : MonoBehaviour
{
    public string player1 = null;
    public string player2 = null;
    public string player3 = null;
    public string player4 = null;
    public float p1Score = 0;
    public float p2Score = 0;
    public float p3Score = 0;
    public float p4Score = 0;
    public float greatest = 0;
    public float smallest = 0;
    // Start is called before the first frame update
    void Start()
    {
        p1Score = GlobalControl.Instance.p1Score;
        p2Score = GlobalControl.Instance.p2Score;
        p3Score = GlobalControl.Instance.p3Score;
        p4Score = GlobalControl.Instance.p4Score;

        player1 = "Player 1: \n" + "\n Score: " + p1Score + "\n Wins: " + GlobalControl.Instance.p1Wins;
        player2 = "Player 2: \n" + "\n Score: " + p2Score + "\n Wins: " + GlobalControl.Instance.p2Wins;
        player3 = "Player 3: \n" + "\n Score: " + p3Score + "\n";
        player4 = "Player 4: \n" + "\n Score: " + p4Score + "\n";

        FindOrder();
        Debug.Log(greatest);
        Debug.Log(smallest);

    }



    void OnGUI()
    {
        Text p1txt = GameObject.Find("/Canvas/Player1Score").GetComponent<Text>();
        p1txt.text = player1;
        Text p2txt = GameObject.Find("/Canvas/Player2Score").GetComponent<Text>();
        p2txt.text = player2;
        Text p3txt = GameObject.Find("/Canvas/Player3Score").GetComponent<Text>();
        p3txt.text = player3;
        Text p4txt = GameObject.Find("/Canvas/Player4Score").GetComponent<Text>();
        p4txt.text = player4;
        // Make a text field that modifies stringToEdit.
        //player1 = GUI.TextField(new Rect(-150, -80, 100, 100), player1);
        //player2 = GUI.TextField(new Rect(-50, -80, 100, 100), player2);
        //player3 = GUI.TextField(new Rect(50, -80, 100, 100), player3);
        //player4 = GUI.TextField(new Rect(150, -80, 100, 100), player4);

    }
    void FindOrder ()
    {
        List<float> Scores = new List<float>();
        Scores.Add(p1Score);
        Scores.Add(p2Score);
        Scores.Add(p3Score);
        Scores.Add(p4Score);

        for (int i = 0; i < 4; i++)
        {
            if (Scores[i] > greatest)
            {
                Scores[i] = greatest;
            }
            if (Scores[i] < smallest)
            {
                Scores[i] = smallest;
            }
        }
    }

}
