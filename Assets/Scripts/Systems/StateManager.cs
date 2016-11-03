using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour
{
    private BaseUIManager baseUIManager;

    public static StateManager stateManager;

    public static string state = "Playing";

    public static bool canPause = true;
    public static bool isPaused;

	void Start ()
    {
        baseUIManager = GameObject.FindGameObjectWithTag("BaseUI").GetComponent<BaseUIManager>();

        InstanceManagement();
    }
	
    void InstanceManagement()
    {
        if (stateManager != null && stateManager != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            stateManager = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public static void PauseGame()
    {
        if (canPause)
        {
            if (isPaused)
            {
                Time.timeScale = 1;               
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
            }
        }
    }
}
