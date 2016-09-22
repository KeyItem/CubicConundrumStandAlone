using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    private PlayerController playerController;

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

	void Awake ()
    {
        playerController = GetComponent<PlayerController>();
	}
	
	void Update ()
    {
        CheckForInputs();
	}

    public void CheckForInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        if (xAxis != 0 || yAxis != 0)
        {
            playerController.Move(xAxis, yAxis);
        }

        if (Input.GetKeyDown(Attach) || Input.GetKeyDown(AttachController))
        {
            playerController.Attach();
        }

        if (Input.GetKeyDown(Switch) || Input.GetKeyDown(SwitchController))
        {
            playerController.Switch();
        }

        if (Input.GetKeyDown(Pause) || Input.GetKeyDown(PauseController))
        {

        }
    }
}
