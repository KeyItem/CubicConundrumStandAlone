using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody myRB;
    private SwitchManager switchManager;
    public MovementManager moveManager;

    [Header("Player")]
    public GameObject currentPlayer;

    [Header("Player Variables")]
    public float moveDistance;
    public float rayDistance;
    public float inputDelayTimer;
    private float inputDelayTimerInit;

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
                if (RequestMove(Vector3.right)) //Try moving.
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
                if (RequestMove(Vector3.left)) //Try moving
                {
                    currentPlayer.transform.position += new Vector3(-moveDistance, 0, 0);
                    inputDelayTimer = inputDelayTimerInit;
                }
            }
        }

        if (moveManager.isAttached)
        {
            if (yAxis > 0)
            {
                inputDelayTimer -= Time.deltaTime;

                if (inputDelayTimer < 0)
                {
                   if (RequestClimb("Up")) //Check if you can climb
                    {
                        currentPlayer.transform.position += myClimbVec;
                        inputDelayTimer = inputDelayTimerInit;
                    }
                    else
                    {
                        if (RequestMove(Vector3.up)) //If not, try moving.
                        {
                            if (moveManager.isAttachedLeft || moveManager.isAttachedRight)
                            {
                                currentPlayer.transform.position += new Vector3(0, moveDistance, 0);
                                inputDelayTimer = inputDelayTimerInit;
                            }
                        }
                    }
                }
            }

            if (yAxis < 0)
            {
                inputDelayTimer -= Time.deltaTime;

                if (inputDelayTimer < 0)
                {
                    if (RequestClimb("Down")) //Check if you can climb
                    {
                        currentPlayer.transform.position += myClimbVec;
                        inputDelayTimer = inputDelayTimerInit;
                    }
                    else
                    {
                        if (RequestMove(Vector3.down)) //If not, try moving.
                        {
                            if (moveManager.isAttachedLeft || moveManager.isAttachedRight)
                            {
                                currentPlayer.transform.position += new Vector3(0, -moveDistance, 0);
                                inputDelayTimer = inputDelayTimerInit;
                            }
                        }
                    }
                }
            }
        }
    }


    public void Attach()
    {
      if (!moveManager.isAttached)
        {
           if (RequestAttach())
            {
                currentPlayer.transform.position = new Vector3(currentPlayer.transform.position.x, Mathf.Round(currentPlayer.transform.position.y), currentPlayer.transform.position.z);
                AttachedOutline();
                moveManager.isAttached = true;
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
        moveManager.isAttached = false;
        moveManager.isAttachedRight = false;
        moveManager.isAttachedLeft = false;
        SelectedOutline();
    }

    public void Switch()
    {
        if (moveManager.canSwitch)
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

    bool RequestClimb (string direction)
    {
        if (direction == "Up")
        {
            if (moveManager.isAttachedLeft)
            {
                if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(-1, 1), Vector3.one * 0.1f, Quaternion.identity, allMask))
                {
                    moveManager.isAttachedLeft = false;
                    moveManager.wasLeft = true;
                    moveManager.wasRight = false;
                    myClimbVec = new Vector3(-1, 1, 0);
                    return true;
                }

                return false;
            }

            if (moveManager.isAttachedRight)
            {
                if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(1, 1), Vector3.one * 0.1f, Quaternion.identity, allMask))
                {
                    moveManager.isAttachedRight = false;
                    moveManager.wasLeft = false;
                    moveManager.wasRight = true;
                    myClimbVec = new Vector3(1, 1, 0);
                    return true;
                }

                return false;
            }

            if (moveManager.isAttached)
            {
                if (Physics.Raycast(currentPlayer.transform.position, Vector3.up, rayDistance, attachMask))
                {
                    if (moveManager.wasLeft)
                    {
                        if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(-1, 1), Vector3.one * 0.1f, Quaternion.identity, allMask))
                        {
                            myClimbVec = new Vector3(-1, -1, 0);
                            moveManager.wasRight = false;
                            moveManager.wasLeft = false;
                            return true;
                        }

                        return false;
                    }

                    if (moveManager.wasRight)
                    {
                        if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(1, 1), Vector3.one * 0.1f, Quaternion.identity, allMask))
                        {
                            myClimbVec = new Vector3(1, -1, 0);
                            moveManager.wasRight = false;
                            moveManager.wasLeft = false;
                            return true;
                        }

                        return false;
                    }
                }

                return false;
            }

            return false;
        }
    
        if (direction == "Down")
        {
            if (moveManager.isAttachedLeft)
            {
                if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(-1, -1), Vector3.one * 0.1f, Quaternion.identity, allMask))
                {
                    moveManager.isAttachedLeft = false;
                    moveManager.wasLeft = true;
                    moveManager.wasRight = false;
                    myClimbVec = new Vector3(-1, -1, 0);
                    return true;
                }

                return false;     
            }

            if (moveManager.isAttachedRight)
            {
                if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(1, -1), Vector3.one * 0.1f, Quaternion.identity, allMask))
                {
                    moveManager.isAttachedRight = false;
                    moveManager.wasLeft = false;
                    moveManager.wasRight = true;
                    myClimbVec = new Vector3(1, -1, 0);
                    return true;
                }

                return false;
            }

            if (moveManager.isAttached)
            {
                if (Physics.Raycast(currentPlayer.transform.position, Vector3.down, rayDistance, attachMask))
                {
                    if (moveManager.wasLeft)
                    {
                        if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(-1, -1), Vector3.one * 0.1f, Quaternion.identity, allMask))
                        {
                            myClimbVec = new Vector3(-1, -1, 0);
                            moveManager.wasRight = false;
                            moveManager.wasLeft = false;
                            return true;
                        }                   
                    }

                    if (moveManager.wasRight)
                    {
                        if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(1, -1), Vector3.one * 0.1f, Quaternion.identity, allMask))
                        {
                            myClimbVec = new Vector3(1, -1, 0);
                            moveManager.wasRight = false;
                            moveManager.wasLeft = false;
                            return true;
                        }
                    }
                }

                return false;           
            }

            return false;
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
                    moveManager.isAttachedRight = true;
                }

                if (i == 1)
                {
                    moveManager.isAttachedLeft = true;
                }
                return true;
            }
        }
        return false;
    }

    void CheckForWalls()
    {
        if (moveManager.isAttached)
        {
            for (int i = 0; i < directionVectors.Length; i++)
            {
                if (Physics.Raycast(currentPlayer.transform.position, directionVectors[i], moveDistance, attachMask))
                {
                    if (i == 0)
                    {
                        moveManager.isAttachedRight = true;
                        moveManager.isAttachedLeft = false;
                    }

                    if (i == 1)
                    {
                        moveManager.isAttachedLeft = true;
                        moveManager.isAttachedRight = false;
                    }
                    return;
                }
                if (i+1 == directionVectors.Length)
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

        moveManager = currentPlayer.GetComponent<MovementManager>();

        moveManager.isBeingControlled = true;

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
