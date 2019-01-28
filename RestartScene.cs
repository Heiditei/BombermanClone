using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(ButtonAction);
    }

    void ButtonAction()
    {
        Scene scene = SceneManager.GetActiveScene();
        string thisLevel = scene.name;
        Debug.Log("Active scene is '" + scene.name + "'.");

        SceneManager.LoadScene("" + thisLevel);
        Debug.Log("Loading scene '" + thisLevel + "'.");
    }
}
