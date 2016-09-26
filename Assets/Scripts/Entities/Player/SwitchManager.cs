using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwitchManager : MonoBehaviour
{
    private PlayerController playerController;
    private CameraController cameraController;

    [Header ("Switch Variables")]
    public int numberOfCubesAvailable;
    public int currentCube;

    public List<GameObject> cubes;

    public GameObject godObject;
    public GameObject cubeHolder;

    void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("GodObject").GetComponent<PlayerController>();
        cameraController = Camera.main.GetComponent<CameraController>();

        godObject = gameObject;

        cubeHolder = GameObject.FindGameObjectWithTag("CubeHolder");

       
    }

	void Start ()
    {
        if (numberOfCubesAvailable > 1)
        {
            playerController.moveManager.canSwitch = true;
        }

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

        cameraController.currentTarget = cubes[currentCube].transform;

        playerController.SyncPlayer();
    }
}
