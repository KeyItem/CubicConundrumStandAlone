using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BaseUIManager : MonoBehaviour
{
    public static BaseUIManager baseUIManager;

    [Header("UI Elements")]
    public GameObject PauseUI;
    public GameObject colorPickerUI;

    [Header ("Booleans")]
    public static bool canPause;
    public static bool isGamePaused;

    public  bool isColorPicking;

	void Start ()
    {
        InstanceManagement();
	}
	
	void Update ()
    {
	
	}

    void InstanceManagement()
    {
        if (baseUIManager != null && baseUIManager != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            baseUIManager = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PauseGameUI()
    {
       if (canPause)
        {
            if (StateManager.isPaused)
            {
                PauseUI.SetActive(false);
            }
            else if (!StateManager.isPaused)
            {
                PauseUI.SetActive(true);
            }
        }
    }

    public void EnableColorPicker ()
    {
        isColorPicking = true;
        colorPickerUI.SetActive(true);
    }

    public void DisableColorPicker()
    {
        isColorPicking = false;
        colorPickerUI.SetActive(false);
    }
}
