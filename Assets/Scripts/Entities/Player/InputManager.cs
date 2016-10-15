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
    public bool justChangedColors;

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

                if (keyHoldDelay < 0 && !justChangedColors)
                {
                    baseUIManager.EnableColorPicker();
                    lookForInput = false;
                    colorSelector = true;
                }          
            }

            if (Input.GetKeyUp(Switch) || Input.GetKeyUp(SwitchController))
            {
                if (justChangedColors)
                {
                    justChangedColors = false;
                    return;
                }
                             
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
            if (xAxis > 0.9f) //Right
            {
                playerController.SwitchHold("Yellow");
                colorSelector = false;
                lookForInput = true;
                baseUIManager.DisableColorPicker();
                keyHoldDelay = keyHoldDelayReset;
                justChangedColors = true;
            }

            if (xAxis < -0.9f) //Left
            {
                playerController.SwitchHold("Green");
                colorSelector = false;
                lookForInput = true;
                baseUIManager.DisableColorPicker();
                keyHoldDelay = keyHoldDelayReset;
                justChangedColors = true;
            }

            if (yAxis > 0.9f) //Up
            {
                playerController.SwitchHold("Red");
                colorSelector = false;
                lookForInput = true;
                baseUIManager.DisableColorPicker();
                keyHoldDelay = keyHoldDelayReset;
                justChangedColors = true;
            }

            if (yAxis < -0.9f) //Down
            {
                playerController.SwitchHold("Blue");
                colorSelector = false;
                lookForInput = true;
                baseUIManager.DisableColorPicker();
                keyHoldDelay = keyHoldDelayReset;
                justChangedColors = true;
            }
        }
        
    }
}
