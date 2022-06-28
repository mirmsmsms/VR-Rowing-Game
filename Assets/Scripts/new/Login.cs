using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    public TextMeshProUGUI userName;
    public APISystem api;


    public void SavePlayerName(string sceneMenu)
    {
        if (string.IsNullOrEmpty(userName.text))
        {
            Debug.Log("Enter the username");
        }
        else
        {
            PlayerPrefs.SetString("username", userName.text);
            FindObjectOfType<APISystem>().Register(userName.text, userName.text, userName.text, userName.text);
            Debug.Log(userName.text);
            Debug.Log("My name is : " + PlayerPrefs.GetString("username"));
        }
    }
    public void togame()
    {
        SceneManager.LoadScene("game");
    }
    public void quit()
    {
        Application.Quit();
    }
}
