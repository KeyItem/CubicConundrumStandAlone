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
    [Header("Current Player")]
    public GameObject currentPlayer;

	void Awake ()
    {
        playerController = GetComponent<PlayerController>();
	}
	
	void FixedUpdate ()
    {
        CheckForInputs();
	}

    public void CheckForInputs()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        if (xAxis != 0 || yAxis != 0)
        {
            if (xAxis > 0)
            {

            }

            if (xAxis < 0)
            {

            }

            if (yAxis > 0)
            {

            }

            if (yAxis < 0)
            {

            }
        }

        if (Input.GetKeyDown(Attach))
        {
            playerController.Attach();
        }

        if (Input.GetKeyDown(Switch))
        {
            playerController.Switch();
        }
    }
}
