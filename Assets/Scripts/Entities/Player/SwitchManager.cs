using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwitchManager : MonoBehaviour
{
    private PlayerController playerController;

    [Header ("Switch Variables")]
    public int numberOfCubesAvailable;
    public int currentCube;

    public List<GameObject> cubes;

    public GameObject godObject;
    public GameObject cubeHolder;

    void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("GodObject").GetComponent<PlayerController>();

        godObject = gameObject;

        cubeHolder = GameObject.FindGameObjectWithTag("CubeHolder");

        if (numberOfCubesAvailable > 1)
        {
            playerController.canSwitch = true;
        }
    }

	void Start ()
    {
        DefineCubeList();

        currentCube = 0;
	}
	
    public void DefineCubeList()
    {
        switch (numberOfCubesAvailable)
        {
            case 1:
                cubes.Add(GameObject.FindGameObjectWithTag("RedCube"));
                break;
            case 2:
                cubes.Add(GameObject.FindGameObjectWithTag("RedCube"));
                cubes.Add(GameObject.FindGameObjectWithTag("BlueCube"));
                break;
            case 3:
                cubes.Add(GameObject.FindGameObjectWithTag("RedCube"));
                cubes.Add(GameObject.FindGameObjectWithTag("BlueCube"));
                cubes.Add(GameObject.FindGameObjectWithTag("GreenCube"));
                break;
            case 4:
                cubes.Add(GameObject.FindGameObjectWithTag("RedCube"));
                cubes.Add(GameObject.FindGameObjectWithTag("BlueCube"));
                cubes.Add(GameObject.FindGameObjectWithTag("GreenCube"));
                cubes.Add(GameObject.FindGameObjectWithTag("YellowCube"));
                break;
        }
    }

    public void SwitchCube()
    {
        currentCube++;

        if (currentCube > numberOfCubesAvailable - 1)
        {
            currentCube = 0;
        }

        GameObject previousChild = godObject.transform.GetChild(0).gameObject;
        previousChild.transform.SetParent(cubeHolder.transform);
        playerController.RemoveOutline(previousChild);

        cubes[currentCube].transform.SetParent(godObject.transform);

        playerController.SyncPlayer();

    }
}
