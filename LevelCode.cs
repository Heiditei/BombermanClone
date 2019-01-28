using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LevelCode : MonoBehaviour
{
    public Button button;
    public InputField userInputField;
    string input;

    void Start()
    {
        //button = GetComponent<Button>();
        button.onClick.AddListener(ProcessText);
    }
    void ProcessText()
    {
        Debug.Log("Button was pressed!");
        input = userInputField.text;
        Debug.Log(input);
        InsertCode(input);
    }
    void InsertCode(string input)
    {
     
        if (Application.CanStreamedLevelBeLoaded("" + input))
        {
            GameObject.Find("GlobalDataCarrier").transform.GetChild(0).gameObject.SetActive(false);
            SceneManager.LoadScene("" + input);
            Debug.Log("Loading scene '" + input + "'.");
        }

        else
        {
            SceneManager.LoadScene("LevelCode");
            Debug.Log("No such code detected.");
        }
    }

    void OnSumbit(InputField inputField)
    {
        if (Application.CanStreamedLevelBeLoaded("" + input))
        {
                SceneManager.LoadScene("" + input);
                Debug.Log("Loading scene '" + input + "'.");
        }

        else
        {
            SceneManager.LoadScene("LevelCode");
            Debug.Log("No such code detected.");
        }
    }
}

