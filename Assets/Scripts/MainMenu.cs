using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void DidPressStartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void DidPressMenuButton()
    {
        Debug.Log("Menu button pressed.");
    }

    public void DidPressQuitButton()
    {
        Debug.Log("Quit button pressed.");
        // Application.Quit();
    }
}
