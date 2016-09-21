using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody myRB;

    [Header("Player")]
    public GameObject currentPlayer;
    private GameObject player1;
    private GameObject player2;
    private GameObject player3;
    private GameObject player4;

    [Header("Player Variables")]
    public float moveDistance;
    public float rayDistance;
    public float moveDelayTimer;
    private float moveDelayTimerInit;

    [Header("Player Booleans")]
    public bool canMove;
    public bool canAttach;
    public bool isAttachedLeft;
    public bool isAttachedRight;

    [Header("Layer Masks")]
    public LayerMask solidMask;
    public LayerMask attachMask;

	void Start ()
    {
        myRB = GetComponent<Rigidbody>();

        currentPlayer = gameObject;

        moveDelayTimerInit = moveDelayTimer;
	}
	
	void Update ()
    {
	
	}

    public void Move(float xAxis, float yAxis)
    {
        if (xAxis > 0)
        {
            moveDelayTimer -= Time.deltaTime;

            if (moveDelayTimer < 0)
            {
                if (RequestMove(currentPlayer.transform.position + new Vector3(moveDistance, 0, 0)))
                {
                    transform.position += new Vector3(moveDistance, 0, 0);
                    moveDelayTimer = moveDelayTimerInit;
                }
            }
        }

        if (xAxis < 0)
        {
            moveDelayTimer -= Time.deltaTime;

            if (moveDelayTimer < 0)
            {
                if (RequestMove(currentPlayer.transform.position + new Vector3(-moveDistance, 0, 0)))
                {
                    transform.position += new Vector3(-moveDistance, 0, 0);
                    moveDelayTimer = moveDelayTimerInit;
                }
            }
        }
    }


    public void Attach()
    {
        if (canAttach)
        {

        }

        if (isAttachedLeft || isAttachedRight)
        {

        }
    }

    public void Switch()
    {

    }

    void RaycastCheck()
    {
        
    }

    bool RequestMove (Vector3 RequestPosition)
    {
        if (!Physics.Raycast(currentPlayer.transform.position, RequestPosition, rayDistance, attachMask))
        {
            return true;
        }
        else
        {
            moveDelayTimer = moveDelayTimerInit;
            return false;         
        }

    }
}
