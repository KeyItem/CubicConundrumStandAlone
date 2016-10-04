using UnityEngine;
using System.Collections;

public class CheckpointManager : MonoBehaviour
{
    [Header ("Checkpoint Variables")]
    public Vector3[] checkpoints;

    public int currentCheckpoint;

    public void SetNextCheckpoint()
    {
        currentCheckpoint++;
    }

    public void ResetPlayerToCheckpoint (GameObject player)
    {
        player.transform.position = checkpoints[currentCheckpoint];
    }
}
