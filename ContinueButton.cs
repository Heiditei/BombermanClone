using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    public Button button;

    [SerializeField]
    private GameObject PausePanel;

    void Start()
    {
        button.onClick.AddListener(ButtonAction);
    }
    void ButtonAction()
    {
        this.PausePanel.SetActive(false);
    }
}