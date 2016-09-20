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

    [Header("Player Booleans")]
    public bool canMove;
    public bool canAttach;
    public bool isAttachedLeft;
    public bool isAttachedRight;

    [Header("Layer Masks")]
    public LayerMask attachMask;

	void Start ()
    {
        myRB = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
	
	}

    public void Move()
    {

    }

    public void Attach()
    {

    }

    public void Switch()
    {

    }

    void RaycastCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.left, moveDistance, attachMask))
        {

        }

        if (Physics.Raycast(transform.position, Vector3.up, moveDistance, attachMask))
        {

        }

        if (Physics.Raycast(transform.position, Vector3.right, moveDistance, attachMask))
        {

        }

        if (Physics.Raycast(transform.position, Vector3.down, moveDistance, attachMask))
        {

        }
    }
}
