using UnityEngine;
using System.Collections;

public class OutOfBoundsManager : MonoBehaviour
{
    private CheckpointManager checkPointManager;

    void Start()
    {
        checkPointManager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
    }

    void OnTriggerEnter (Collider player)
    {
        if (player.gameObject.CompareTag ("RedCube") || player.gameObject.CompareTag("BlueCube") || player.gameObject.CompareTag("GreenCube") || player.gameObject.CompareTag("YellowCube"))
        {
            checkPointManager.ResetPlayerToCheckpoint(player.gameObject);
        }
    }
}
