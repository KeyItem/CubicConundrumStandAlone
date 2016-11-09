using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BaseUIManager : MonoBehaviour
{
    public static BaseUIManager baseUIManager;

    [Header("UI Elements")]
    public static GameObject PauseUI;
    public GameObject colorPickerUI;

    [Header ("Booleans")]
    public bool isColorPicking;

	void Start ()
    {
        PauseUI = transform.GetChild(01).gameObject;

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

    public static void PauseGameUI()
    {
       if (StateManager.canPause)
        {
            if (StateManager.isPaused)
            {
               //PauseUI.SetActive(true);
            }
            else if (!StateManager.isPaused)
            {
                //PauseUI.SetActive(false);
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
