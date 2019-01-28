using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class TitleButtons : MonoBehaviour
{
    public Button button;

    void Start()
    {
        button.onClick.AddListener(ButtonAction);
    }

    public void ButtonAction()
    {
        string scene = gameObject.name;
        switch ( scene)
        {
            case "StartGameButton":
                GlobalControl.Instance.isSinglePlayer = false;
                GameObject.Find("GlobalDataCarrier").transform.GetChild(0).gameObject.SetActive(false);
                SceneManager.LoadScene("Level1");
                break;

            case "MultiplayerButton":
                SceneManager.LoadScene("GameMode");
                break;
            case "LevelCodeButton":
                SceneManager.LoadScene("LevelCode");
                break;
            case "CreditsButton":
                SceneManager.LoadScene("Credits");
                break;
        }
    }
}