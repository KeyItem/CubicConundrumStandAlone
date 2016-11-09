using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    private PlayerController playerController;
    private SwitchManager switchManager;
    private BaseUIManager baseUIManager;

    [Header("Axis")]
    public float xAxis;
    public float yAxis;

    [Header("Controls")]
    public KeyCode Attach;
    public KeyCode Switch;
    public KeyCode AttachController;
    public KeyCode SwitchController;
    public KeyCode Pause;
    public KeyCode PauseController;
    public KeyCode Reset;
    public KeyCode ResetController;

    [Header("Variables")]
    public float keyHoldDelay;
    private float keyHoldDelayReset;

    [Header("Booleans")]
    public bool lookForInput;
    public bool colorSelector;
    public bool isDevMode;

	void Awake ()
    {
        playerController = GetComponent<PlayerController>();
        switchManager = GetComponent<SwitchManager>();
        baseUIManager = GameObject.FindGameObjectWithTag("BaseUI").GetComponent<BaseUIManager>();

        keyHoldDelayReset = keyHoldDelay;
	}
	
	void Update ()
    {
        CheckForInputs();
	}

    public void CheckForInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        if (lookForInput)
        {          
            playerController.Move(xAxis, yAxis);

            if (Input.GetKeyDown(Attach) || Input.GetKeyDown(AttachController))
            {
                playerController.moveManager.Attach();
            }

            if (Input.GetKey(Switch) || Input.GetKey(SwitchController))
            {
                keyHoldDelay -= Time.deltaTime;

                if (keyHoldDelay < 0)
                {
                    baseUIManager.EnableColorPicker();
                    lookForInput = false;
                    colorSelector = true;
                }          
            }

            if (Input.GetKeyUp(Switch) || Input.GetKeyUp(SwitchController))
            {                                       
                if (keyHoldDelay > 0)
                {
                    switchManager.SwitchCube();
                }
      
                keyHoldDelay = keyHoldDelayReset;
            }

            if (Input.GetKeyDown(Pause) || Input.GetKeyDown(PauseController))
            {
                StateManager.PauseGame();
            }

            if (Input.GetKeyDown(Reset) || Input.GetKeyDown(ResetController))
            {
                if (StateManager.state != "Paused")
                {
                    LevelManager.ReloadCurrentLevel();
                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                if (!isDevMode)
                {
                    isDevMode = true;
                }
                else if (isDevMode)
                {
                    isDevMode = false;
                }
            }

            if (isDevMode)
            {
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    LevelManager.LoadPreviousLevel();
                }

                if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    LevelManager.LoadNextLevel();
                }

                if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    LevelManager.BackToMainMenu();
                }
            }
        }

        if (colorSelector)
        {
            if (Input.GetKeyUp(Switch) || Input.GetKeyUp(SwitchController))
            {
                switchManager.SwitchTo();
                baseUIManager.DisableColorPicker();
                lookForInput = true;
                colorSelector = false;
                keyHoldDelay = keyHoldDelayReset;
            }

            if (xAxis > 0.9f) //Right
            {
                switchManager.RequestSwitch("Yellow");
            }

            if (xAxis < -0.9f) //Left
            {
                switchManager.RequestSwitch("Green");
            }

            if (yAxis > 0.9f) //Up
            {
                switchManager.RequestSwitch("Red");
            }

            if (yAxis < -0.9f) //Down
            {
                switchManager.RequestSwitch("Blue");
            }
        }
        
    }
}
