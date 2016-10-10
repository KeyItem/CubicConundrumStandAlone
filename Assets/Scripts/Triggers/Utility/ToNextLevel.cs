using UnityEngine;
using System.Collections;

public class ToNextLevel : MonoBehaviour
{
    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "RedCube" || player.gameObject.tag == "BlueCube" || player.gameObject.tag == "GreenCube" || player.gameObject.tag == "YellowCube")
        {
            LevelManager.LoadNextLevel();
        }
    }
	
}
