using UnityEngine;
using System.Collections;

public class BaseUIManager : MonoBehaviour
{
    public static BaseUIManager baseUIManager;

    public static bool canPause;
    public static bool isGamePaused;

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
}
