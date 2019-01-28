using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MultiplayerButtons : MonoBehaviour
{
    public Button button;

    void Start()
    {
        button.onClick.AddListener(ButtonAction);
    }

    public void ButtonAction()
    {
        string scene = gameObject.name;
        switch (scene)
        {
            case "SinglePlayerButton":
                Debug.Log("SP");
                GlobalControl.Instance.isSinglePlayer = true;
                SceneManager.LoadScene("LevelCode");
                break;

            case "OnlineButton":
                Debug.Log("Online");
                SceneManager.LoadScene("MultiplayerMenu");
                break;
            case "HotseatButton":
                Debug.Log("HS");
                SceneManager.LoadScene("LevelCode");
                GlobalControl.Instance.isSinglePlayer = false;
                break;
        }
    }
}