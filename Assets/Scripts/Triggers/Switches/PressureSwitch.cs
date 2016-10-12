using UnityEngine;
using System.Collections;

public class PressureSwitch : MonoBehaviour
{
    private ObjectiveManager objectiveManager;

    [Header("Color")]
    public string switchColor; //1: Red, 2: Blue, 3: Green, 4: Yellow

    [Header("Boolean Variables")]
    public GameObject targetDoor;
    public bool isPressed;


	void Start ()
    {
        objectiveManager = GameObject.FindGameObjectWithTag("ObjectiveManager").GetComponent<ObjectiveManager>();
	}

    void Update()
    {
        RelayInformation();
    }

    void OnTriggerStay(Collider player)
    {
        if (switchColor + "Cube" == player.gameObject.tag)
        {
            isPressed = true;
        }
    }

    void OnTriggerExit(Collider player)
    {
        if (switchColor + "Cube" == player.gameObject.tag)
        {
            isPressed = false;
        }
    }

    void RelayInformation()
    {
        if (isPressed)
        {
            targetDoor.GetComponent<DoorController>().isClosing = false;
            targetDoor.GetComponent<DoorController>().isOpening = true;
        }
        else
        {
            targetDoor.GetComponent<DoorController>().isOpening = false;
            targetDoor.GetComponent<DoorController>().isClosing = true;
        }
    }
	
}
