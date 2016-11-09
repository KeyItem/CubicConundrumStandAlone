using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManagerInstance;

	void Start ()
    {
        InstanceManagement();
	}
	
	void Update ()
    {
	
	}

    void InstanceManagement()
    {
        if (levelManagerInstance != null && levelManagerInstance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            levelManagerInstance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public static void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void LoadPreviousLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public static void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void BackToMainMenuWrapper()
    {
        BackToMainMenu();
    }

    public static void LoadLevel (int levelID)
    {
        SceneManager.LoadScene(levelID);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
