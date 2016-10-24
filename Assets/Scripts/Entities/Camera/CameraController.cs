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

    [Header("Transition Variables")]
    public float currentFOV;
    public float minFOV;
    public float maxFOV;
    public float transitionSpeed;
    private float t;

    public bool canFadeIn;
    public bool canFadeOut;

	void Start ()
    {
        playerController = GameObject.FindGameObjectWithTag("GodObject").GetComponent<PlayerController>();

        currentTarget = playerController.currentPlayer.transform;

        canFadeIn = true;
    }

    void Update()
    {
        currentFOV = Camera.main.fieldOfView;

        if (canFadeIn)
        {
            FadeIn();
        }

        if (canFadeOut)
        {
            FadeOut();
        }
    }
	
	void FixedUpdate ()
    {
        Vector3 worldPoint = Camera.main.WorldToViewportPoint(currentTarget.position);
        Vector3 delta = currentTarget.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, worldPoint.z));
        Vector3 dest = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, dest, ref velocity, cameraDelay);
	}

    public void FadeIn()
    {
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, maxFOV, t);

        t += transitionSpeed * Time.deltaTime;

        if (Camera.main.fieldOfView == maxFOV)
        {
            t = 0f;
            canFadeIn = false;
            playerController.canMove = true;
        }
    }

    public void FadeOut()
    {
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, minFOV, t);

        t += transitionSpeed * Time.deltaTime;

        playerController.canMove = false;

        if (Camera.main.fieldOfView == minFOV)
        {
            t = 0f;
            canFadeOut = false;
            LevelManager.LoadNextLevel();
        }
    }
}
