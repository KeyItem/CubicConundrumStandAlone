using UnityEngine;
using System.Collections;

public class ToNextLevel : MonoBehaviour
{
    private CameraController cameraController;

    void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "RedCube" || player.gameObject.tag == "BlueCube" || player.gameObject.tag == "GreenCube" || player.gameObject.tag == "YellowCube")
        {
            cameraController.canFadeOut = true;
        }
    }
	
}
