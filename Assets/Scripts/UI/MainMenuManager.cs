using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour 
{
    [Header ("UI References")]
    public GameObject optionsMenu;

    [Header("UI Booleans")]
    public bool isOptionsMenuOpen;

    public void ManageOptionsMenu()
    {
        if (isOptionsMenuOpen)
        {
            optionsMenu.SetActive(false);
            isOptionsMenuOpen = false;
        }
        else
        {
            optionsMenu.SetActive(true);
            isOptionsMenuOpen = true;
        }
    }

	public void LoadFirstLevel()
    {
        LevelManager.LoadNextLevel();
    }

    public void QuitGame()
    {
        LevelManager.QuitGame();
    }
}
