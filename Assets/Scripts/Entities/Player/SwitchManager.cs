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

    void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("GodObject").GetComponent<PlayerController>();
    }

	void Start ()
    {
        DefineCubeList();

        currentCube = 1;
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

        if (currentCube > numberOfCubesAvailable)
        {
            currentCube = 1;
        }

        GameObject previousChild = godObject.transform.GetChild(0).gameObject;
        previousChild.transform.SetParent(null);

        cubes[currentCube].transform.SetParent(godObject.transform);

        playerController.SyncPlayer();

    }
}
