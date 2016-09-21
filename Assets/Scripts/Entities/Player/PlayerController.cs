using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody myRB;

    [Header("Player")]
    public GameObject currentPlayer;

    [Header("Player Variables")]
    public float moveDistance;
    public float rayDistance;
    public float inputDelayTimer;
    private float inputDelayTimerInit;

    [Header("Player Booleans")]
    public bool canMove;
    public bool canSwitch;
    public bool isAttached;
    public bool isAttachedLeft;
    public bool isAttachedRight;

    [Header("Layer Masks")]
    public LayerMask solidMask;
    public LayerMask attachMask;

    [Header("Debug")]
    public Vector3[] directionVectors;

	void Start ()
    {
        currentPlayer = gameObject.transform.GetChild(0).gameObject;

        myRB = currentPlayer.GetComponent<Rigidbody>();

        inputDelayTimerInit = inputDelayTimer;
	}
	
	void Update ()
    {
       
    }

    public void Move(float xAxis, float yAxis)
    {
        if (xAxis > 0)
        {
            inputDelayTimer -= Time.deltaTime;

            if (inputDelayTimer < 0)
            {
                if (RequestMove(Vector3.right))
                {
                    currentPlayer.transform.position += new Vector3(moveDistance, 0, 0);
                    inputDelayTimer = inputDelayTimerInit;
                }
            }
        }

        if (xAxis < 0)
        {
            inputDelayTimer -= Time.deltaTime;

            if (inputDelayTimer < 0)
            {
                if (RequestMove(Vector3.left))
                {
                    currentPlayer.transform.position += new Vector3(-moveDistance, 0, 0);
                    inputDelayTimer = inputDelayTimerInit;
                }
            }
        }

        if (isAttached)
        {
            if (yAxis > 0)
            {
                inputDelayTimer -= Time.deltaTime;

                if (inputDelayTimer < 0)
                {
                    if (RequestMove(Vector3.up))
                    {
                        currentPlayer.transform.position += new Vector3(0, moveDistance, 0);
                        inputDelayTimer = inputDelayTimerInit;
                    }
                }
            }

            if (yAxis < 0)
            {
                inputDelayTimer -= Time.deltaTime;

                if (inputDelayTimer < 0)
                {
                    if (RequestMove(Vector3.down))
                    {
                        currentPlayer.transform.position += new Vector3(0, -moveDistance, 0);
                        inputDelayTimer = inputDelayTimerInit;
                    }
                }
            }
        }
    }


    public void Attach()
    {
      if (!isAttached)
        {
           if (RequestAttach())
            {
                isAttached = true;
                myRB.isKinematic = true;
            }
        }
        else
        {
            Deattach();
        }
    }

    public void Deattach()
    {
        myRB.isKinematic = false;
        isAttached = false;
        isAttachedRight = false;
        isAttachedLeft = false;
    }

    public void Switch()
    {
        if (canSwitch)
        {

        }
    }

    bool RequestMove (Vector3 requestedPosition)
    {
        if (!Physics.Raycast(currentPlayer.transform.position, requestedPosition, rayDistance, attachMask))
        {
            return true;
        }
        else
        {
            inputDelayTimer = inputDelayTimerInit;
            return false;         
        }
    }

    bool RequestAttach()
    {
        for (int i = 0; i < directionVectors.Length; i++)
        {
            if (Physics.Raycast(currentPlayer.transform.position, directionVectors[i], moveDistance, attachMask))
            {           
                if (i == 0)
                {
                    isAttachedRight = true;
                }

                if (i == 1)
                {
                    isAttachedLeft = true;
                }
                return true;
            }
        }
        return false;
    }
}
