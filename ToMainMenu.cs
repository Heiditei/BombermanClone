using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToMainMenu : MonoBehaviour
{
    public Button button;
    void Start()
    {
        button.onClick.AddListener(ButtonAction);
    }
    void ButtonAction()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
