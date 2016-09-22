using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    //Courtesy of http://answers.unity3d.com/questions/29183/2d-camera-smooth-follow.html

    private PlayerController playerController;

    [Header("Camera Variables")]
    public Transform currentTarget;

    public float cameraDelay;

    private Vector3 velocity = Vector3.zero;


	void Start ()
    {
        playerController = GameObject.FindGameObjectWithTag("GodObject").GetComponent<PlayerController>();

        currentTarget = playerController.currentPlayer.transform;      
	}
	
	void FixedUpdate ()
    {
        Vector3 worldPoint = Camera.main.WorldToViewportPoint(currentTarget.position);
        Vector3 delta = currentTarget.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, worldPoint.z));
        Vector3 dest = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, dest, ref velocity, cameraDelay);
	}
}
