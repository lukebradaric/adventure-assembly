using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _baseMenu;

    public void LoadGame()
    {
        SceneManager.LoadScene("Play");
    }
    public void CloseOptions()
    {
        _baseMenu.SetActive(true);
        _optionsMenu.SetActive(false);
    }
    public void OpenOptions()
    {
        _baseMenu.SetActive(false);
        _optionsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void FullScreenToggle()
    {
        if (Screen.fullScreen)
        {
            Screen.fullScreen = false;
        }
        else
        {
            Screen.fullScreen = true;
        }
    }
}
