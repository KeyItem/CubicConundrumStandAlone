using UnityEngine;
using System.Collections;

public class MovementManager : MonoBehaviour
{
    private PlayerController playerController;
    private SFXManager sfxManager;
    private Rigidbody myRB;

    [Header("Directional Vectors")]
    public Vector3[] directionalVecArray;

    [Header ("Player Booleans")]
    public bool canSwitch;
    public bool isBeingControlled;
    public bool isAttached;
    public bool isAttachedLeft;
    public bool isAttachedRight;
    public bool wasLeft;
    public bool wasRight;

    [Header("Player Variables")]
    public GameObject currentPlayer;
    public float rayDistance;

    [Header("Raycast Variables")]
    public GameObject objectAttachedTo;
    public RaycastHit rayHit;
    public LayerMask attachMask;
    public LayerMask allMask;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();

        playerController = GameObject.FindGameObjectWithTag("GodObject").GetComponent<PlayerController>();

        sfxManager = GameObject.FindGameObjectWithTag("SFXManager").GetComponent<SFXManager>();

        currentPlayer = playerController.currentPlayer;
    }

    void Update()
    {
        currentPlayer = playerController.currentPlayer;

        if (!CheckIfStillAttached())
        {
            Deattach();
        }

        CheckForWalls();     
    }

    public void Attach()
    {
        if (!isAttached)
        {
            if (RequestAttach())
            {
                currentPlayer.transform.position = new Vector3(currentPlayer.transform.position.x, Mathf.Round(currentPlayer.transform.position.y), currentPlayer.transform.position.z);
                playerController.AttachedOutline();
                isAttached = true;
                myRB.isKinematic = true;
                playerController.PlayPulseAnim();
                sfxManager.PlaySFX(0);
            }
        }
        else
        {
            Deattach();
        }
    }

    bool RequestAttach()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Physics.Raycast(currentPlayer.transform.position, directionalVecArray[i], out rayHit, rayDistance, attachMask))
            {
                if (i == 0)
                {
                    isAttachedLeft = true;
                }

                if (i == 1)
                {
                    isAttachedRight = true;
                }
                return true;
            }
        }
        return false;
    }

    void Deattach()
    {
        myRB.isKinematic = false;
        isAttached = false;
        isAttachedRight = false;
        isAttachedLeft = false;
        wasLeft = false;
        wasRight = false;
        playerController.SelectedOutline();
    }

    void CheckForWalls() //Check if you're still attached to a surface
    {
        if (isBeingControlled)
        {
            if (isAttached)
            {
                if (!CheckIfStillAttached())
                {
                    Deattach();
                }

                if (!isAttachedLeft || !isAttachedRight)
                {
                    if (Physics.Raycast(currentPlayer.transform.position, Vector3.left, rayDistance, attachMask))
                    {
                        isAttachedLeft = true;
                    }

                    if (Physics.Raycast(currentPlayer.transform.position, Vector3.right, rayDistance, attachMask))
                    {
                        isAttachedRight = true;
                    }
                }

                if (isAttachedLeft)
                {
                    if (!Physics.Raycast(currentPlayer.transform.position, Vector3.left, rayDistance, attachMask))
                    {
                        isAttachedLeft = false;
                    }
                }

                if (isAttachedRight)
                {
                    if (!Physics.Raycast(currentPlayer.transform.position, Vector3.right, rayDistance, attachMask))
                    {
                        isAttachedRight = false;
                    }
                }
            }
        }
    }

    bool CheckIfStillAttached()
    {
        for (int i = 0; i < 4; i++) //Check all cardinal directions to see if still attached to something, otherwise DeAttach
        {
            if (Physics.Raycast(transform.position, directionalVecArray[i], rayDistance, attachMask))
            {
                return true;
            }
        }

        return false;
    }
}
