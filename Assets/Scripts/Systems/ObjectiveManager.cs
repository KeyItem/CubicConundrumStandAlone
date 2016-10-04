using UnityEngine;
using System.Collections;

public class ObjectiveManager : MonoBehaviour
{
    [Header ("Doors")]
    public GameObject[] doors;

    [Header("Puzzle Type")]
    public bool multipleSteps;
    public bool singleStep;

    [Header("Puzzle Requirements")]
    public int numberOfSteps;

    public bool redTrigger;
    public bool blueTrigger;
    public bool greenTrigger;
    public bool yellowTrigger;

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    public void OpenDoor(int doorNumber)
    {
        doors[doorNumber].SetActive(false);
    }
}
