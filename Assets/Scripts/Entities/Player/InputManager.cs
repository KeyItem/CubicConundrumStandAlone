using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    private PlayerController playerController;
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

	void Awake ()
    {
        playerController = GetComponent<PlayerController>();
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
                playerController.Attach();
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
                    playerController.Switch();
                }

                if (colorSelector)
                {
                    lookForInput = true;
                    colorSelector = false;
                }

                keyHoldDelay = keyHoldDelayReset;
            }

            if (Input.GetKeyDown(Pause) || Input.GetKeyDown(PauseController))
            {
                StateManager.PauseGame();
            }

            if (Input.GetKeyDown(Reset) || Input.GetKeyDown(ResetController))
            {
                LevelManager.ReloadCurrentLevel();
            }
        }

        if (colorSelector)
        {
            if (xAxis > 0) //Right
            {
                playerController.SwitchHold("Yellow");
                colorSelector = false;
                lookForInput = true;
                baseUIManager.DisableColorPicker();
            }

            if (xAxis < 0) //Left
            {
                playerController.SwitchHold("Green");
                colorSelector = false;
                lookForInput = true;
                baseUIManager.DisableColorPicker();
            }

            if (yAxis > 0) //Up
            {
                playerController.SwitchHold("Red");
                colorSelector = false;
                lookForInput = true;
                baseUIManager.DisableColorPicker();
            }

            if (yAxis < 0) //Down
            {
                playerController.SwitchHold("Blue");
                colorSelector = false;
                lookForInput = true;
                baseUIManager.DisableColorPicker();
            }
        }
        
    }
}
