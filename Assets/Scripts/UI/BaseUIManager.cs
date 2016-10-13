using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BaseUIManager : MonoBehaviour
{
    public static BaseUIManager baseUIManager;

    [Header("UI Elements")]
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

    public static void PauseGameUI()
    {
       if (canPause)
        {
            if (StateManager.isPaused)
            {
                Debug.Log("Unpausing");
            }
            else if (!StateManager.isPaused)
            {
                Debug.Log("Pausing");
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
