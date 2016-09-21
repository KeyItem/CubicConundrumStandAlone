using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwitchManager : MonoBehaviour
{
    [Header ("Switch Variables")]
    public int numberOfCubesAvailable;
    public int currentCube;

    public List<GameObject> cubes;

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
}
