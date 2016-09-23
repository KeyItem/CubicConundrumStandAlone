using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody myRB;
    private SwitchManager switchManager;

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
    public LayerMask allMask;
    public LayerMask solidMask;
    public LayerMask attachMask;

    [Header("Debug")]
    public Vector3[] directionVectors;
    public Vector3 myClimbVec;
    public Renderer playerRenderer;
    public Color selectedOutlineColor;
    public Color attachedOutlineColor;

    void Awake()
    {
        switchManager = GetComponent<SwitchManager>();

        SyncPlayer();
    }

    void Start ()
    {
        inputDelayTimerInit = inputDelayTimer;
	}
	
	void Update ()
    {
        CheckForWalls();
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
                   if (RequestClimb())
                    {
                        currentPlayer.transform.position += myClimbVec;
                        inputDelayTimer = inputDelayTimerInit;
                    }
                    else
                    {
                        if (RequestMove(Vector3.up))
                        {
                            currentPlayer.transform.position += new Vector3(0, moveDistance, 0);
                            inputDelayTimer = inputDelayTimerInit;
                        }
                    }
                }
            }

            if (yAxis < 0)
            {
                inputDelayTimer -= Time.deltaTime;

                if (inputDelayTimer < 0)
                {
                    if (RequestClimb())
                    {
                        currentPlayer.transform.position += myClimbVec;
                        inputDelayTimer = inputDelayTimerInit;
                    }
                    else
                    {
                        if (RequestMove(Vector3.up))
                        {
                            currentPlayer.transform.position += new Vector3(0, -moveDistance, 0);
                            inputDelayTimer = inputDelayTimerInit;
                        }
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
                currentPlayer.transform.position = new Vector3(currentPlayer.transform.position.x, Mathf.Round(currentPlayer.transform.position.y), currentPlayer.transform.position.z);
                AttachedOutline();
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
        SelectedOutline();
    }

    public void Switch()
    {
        if (canSwitch)
        {
            switchManager.SwitchCube();
        }
    }

    bool RequestMove (Vector3 requestedPosition)
    {
        if (!Physics.Raycast(currentPlayer.transform.position, requestedPosition, rayDistance, allMask)) //Checks if there is something in the way of movement, if not, return true.
        {
            return true;
        }
        else
        {
            inputDelayTimer = inputDelayTimerInit;
            return false;         
        }
    }

    bool RequestClimb ()
    {
        int x = 0;

        if (isAttachedLeft)
        {
            x = -1;
        }

        else if (isAttachedRight)
        {
            x = 1;
        }

        for (int y = -1; y < 2; y += 2)
        {
            if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(x, y, 0), Vector3.one * 0.1f, Quaternion.identity, allMask))
            {
                myClimbVec = new Vector3(x, y, 0);
                return true;
            }
        }

        return false;
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

    void CheckForWalls()
    {
        if (isAttached)
        {
            for (int i = 0; i < directionVectors.Length; i++)
            {
                if (Physics.Raycast(currentPlayer.transform.position, directionVectors[i], moveDistance, attachMask))
                {
                    return;
                }
                if (i+1 == directionVectors.Length) //Thanks to Ryon (Need to confirm on how this works)
                {
                    Deattach();
                }
            }
        }
    }

    public void SyncPlayer()
    {
        currentPlayer = gameObject.transform.GetChild(0).gameObject;
     
        myRB = currentPlayer.GetComponent<Rigidbody>();

        playerRenderer = currentPlayer.GetComponent<Renderer>();

        SelectedOutline();
    }

    void SelectedOutline()
    {
        playerRenderer.material.SetFloat("_Outline", 1);
        playerRenderer.material.SetColor("_OutlineColor", selectedOutlineColor);
    }

    void AttachedOutline()
    {
        playerRenderer.material.SetColor("_OutlineColor", attachedOutlineColor);
    }

    public void RemoveOutline(GameObject target)
    {
        target.GetComponent<Renderer>().material.SetFloat("_Outline", 0);
    }
}
