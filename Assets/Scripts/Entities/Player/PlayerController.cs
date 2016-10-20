using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public MovementManager moveManager;

    [Header("Player")]
    public GameObject currentPlayer;
    public Animator cubeAnimator;

    [Header("Player Variables")]
    public float moveDistance;
    public float rayDistance;
    public float inputDelayTimer;
    private float inputDelayTimerInit;

    [Header("AttachList")]
    public List<GameObject> attachList;

    [Header("Layer Masks")]
    public LayerMask allMask;
    public LayerMask solidMask;
    public LayerMask attachMask;
    public LayerMask cubeMask;

    [Header("Debug")]
    public Vector3[] directionVectors;
    [Space (10)]
    public Vector3 myClimbVec;
    [Space(10)]
    public Renderer playerRenderer;
    public Color selectedOutlineColor;
    public Color attachedOutlineColor;

    void Awake()
    {
        SyncPlayer();
    }

    void Start ()
    {
        inputDelayTimerInit = inputDelayTimer;
	}
	
    public void Move(float xAxis, float yAxis)
    {
        if (xAxis == 0 && yAxis == 0)
        {
            inputDelayTimer = 0;
        }

        if (xAxis > 0.5f)
        {
            inputDelayTimer -= Time.deltaTime;

            if (inputDelayTimer < 0)
            {
                if (RequestMove(Vector3.right)) //Try moving.
                {
                    cubeAnimator.Play("MoveRight", -1, 0f);
                    currentPlayer.transform.position += new Vector3(moveDistance, 0, 0);
                    inputDelayTimer = inputDelayTimerInit;
                }
            }
        }

        if (xAxis < -0.5f)
        {
            inputDelayTimer -= Time.deltaTime;

            if (inputDelayTimer < 0)
            {
                if (RequestMove(Vector3.left)) //Try moving
                {
                    cubeAnimator.Play("MoveLeft", -1, 0f);
                    currentPlayer.transform.position += new Vector3(-moveDistance, 0, 0);
                    inputDelayTimer = inputDelayTimerInit;
                }
            }
        }

        if (moveManager.isAttached)
        {
            if (yAxis > 0.5f)
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
                            if (moveManager.isAttachedLeft)
                            {
                                cubeAnimator.Play("MoveLeft", -1, 0f);
                                currentPlayer.transform.position += new Vector3(0, moveDistance, 0);
                                inputDelayTimer = inputDelayTimerInit;
                            }
                            else if (moveManager.isAttachedRight)
                            {
                                cubeAnimator.Play("MoveRight", -1, 0f);
                                currentPlayer.transform.position += new Vector3(0, moveDistance, 0);
                                inputDelayTimer = inputDelayTimerInit;
                            }
                        }
                    }
                }
            }

            if (yAxis < -0.5f)
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
                            if (moveManager.isAttachedLeft)
                            {
                                cubeAnimator.Play("MoveRight", -1, 0f);
                                currentPlayer.transform.position += new Vector3(0, -moveDistance, 0);
                                inputDelayTimer = inputDelayTimerInit;
                            }
                            else if (moveManager.isAttachedRight)
                            {
                                cubeAnimator.Play("MoveLeft", -1, 0f);
                                currentPlayer.transform.position += new Vector3(0, -moveDistance, 0);
                                inputDelayTimer = inputDelayTimerInit;
                            }
                        }
                    }
                }
            }
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
                if (Physics.Raycast(currentPlayer.transform.position, Vector3.up, rayDistance, cubeMask))
                {
                    if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(1, 1, 0), Vector3.one * 0.1f, Quaternion.identity, allMask)) //TopRight
                    {
                        myClimbVec = new Vector3(1, 1, 0);
                        cubeAnimator.Play("MoveRight", -1, 0f);
                        return true;
                    }
                }
                else
                {
                    if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(-1, 1, 0), Vector3.one * 0.1f, Quaternion.identity, allMask)) //TopLeft
                    {
                        moveManager.isAttachedLeft = false;
                        moveManager.wasLeft = true;
                        moveManager.wasRight = false;
                        myClimbVec = new Vector3(-1, 1, 0);
                        cubeAnimator.Play("MoveLeft", -1, 0f);
                        return true;
                    }

                    return false;
                }             
            }

            if (moveManager.isAttachedRight)
            {
                if (Physics.Raycast(currentPlayer.transform.position, Vector3.up, rayDistance, cubeMask))
                {
                    if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(-1, 1, 0), Vector3.one * 0.1f, Quaternion.identity, allMask)) //TopLeft
                    {
                        myClimbVec = new Vector3(-1, 1, 0);
                        cubeAnimator.Play("MoveLeft", -1, 0f);
                        return true;
                    }
                }
                else
                {
                    if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(1, 1, 0), Vector3.one * 0.1f, Quaternion.identity, allMask)) //TopRight
                    {
                        moveManager.isAttachedRight = false;
                        moveManager.wasLeft = false;
                        moveManager.wasRight = true;
                        myClimbVec = new Vector3(1, 1, 0);
                        cubeAnimator.Play("MoveRight", -1, 0f);
                        return true;
                    }

                    return false;
                }        
            }

            if (moveManager.isAttached)
            {
                if (Physics.Raycast(currentPlayer.transform.position, Vector3.up, rayDistance, attachMask))
                {
                    if (moveManager.wasLeft)
                    {
                        if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(1, 1, 0), Vector3.one * 0.1f, Quaternion.identity, allMask)) //TopRight
                        {
                            myClimbVec = new Vector3(1, 1, 0);
                            moveManager.wasRight = false;
                            moveManager.wasLeft = false;
                            cubeAnimator.Play("MoveRight", -1, 0f);
                            return true;
                        }

                        if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(-1, 1, 0), Vector3.one * 0.1f, Quaternion.identity, allMask)) //TopLeft
                        {
                            myClimbVec = new Vector3(-1, 1, 0);
                            moveManager.wasRight = false;
                            moveManager.wasLeft = false;
                            cubeAnimator.Play("MoveLeft", -1, 0f);
                            return true;
                        }

                        return false;
                    }

                    if (moveManager.wasRight)
                    {
                        if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(-1, 1, 0), Vector3.one * 0.1f, Quaternion.identity, allMask)) //TopLeft
                        {
                            myClimbVec = new Vector3(-1, 1, 0);
                            moveManager.wasRight = false;
                            moveManager.wasLeft = false;
                            cubeAnimator.Play("MoveLeft", -1, 0f);
                            return true;
                        }

                        if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(1, 1, 0), Vector3.one * 0.1f, Quaternion.identity, allMask)) //TopRight
                        {
                            myClimbVec = new Vector3(1, 1, 0);
                            moveManager.wasRight = false;
                            moveManager.wasLeft = false;
                            cubeAnimator.Play("MoveRight", -1, 0f);
                            return true;
                        }

                        return false;
                    }

                    if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(1, 1, 0), Vector3.one * 0.1f, Quaternion.identity, allMask)) //TopRight
                    {
                        myClimbVec = new Vector3(1, 1, 0);
                        cubeAnimator.Play("MoveRight", -1, 0f);
                        return true;
                    }

                    if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(-1, 1, 0), Vector3.one * 0.1f, Quaternion.identity, allMask)) //TopLeft
                    {
                        myClimbVec = new Vector3(-1, 1, 0);
                        cubeAnimator.Play("MoveLeft", -1, 0f);
                        return true;
                    }
                }

                return false;
            }

            return false;
        }
    
        if (direction == "Down") //GET DOWN
        {
            if (moveManager.isAttachedLeft)
            {
                if (Physics.Raycast(currentPlayer.transform.position, Vector3.down, rayDistance, cubeMask))
                {
                    if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(1, -1, 0), Vector3.one * 0.1f, Quaternion.identity, allMask)) //BottomRight
                    {
                        myClimbVec = new Vector3(1, -1, 0);
                        cubeAnimator.Play("MoveRight", -1, 0f);
                        return true;
                    }
                }
                else
                {
                    if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(-1, -1, 0), Vector3.one * 0.1f, Quaternion.identity, allMask)) //BottomLeft
                    {
                        moveManager.isAttachedLeft = false;
                        moveManager.wasLeft = true;
                        moveManager.wasRight = false;
                        myClimbVec = new Vector3(-1, -1, 0);
                        cubeAnimator.Play("MoveLeft", -1, 0f);
                        return true;
                    }

                    return false;
                }           
            }

            if (moveManager.isAttachedRight)
            {
                if (Physics.Raycast(currentPlayer.transform.position, Vector3.down, rayDistance, cubeMask))
                {
                    if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(-1, -1, 0), Vector3.one * 0.1f, Quaternion.identity, allMask)) //Bottomleft
                    {
                        myClimbVec = new Vector3(-1, -1, 0);
                        cubeAnimator.Play("MoveLeft", -1, 0f);
                        return true;
                    }
                }
                else
                {
                    if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(1, -1, 0), Vector3.one * 0.1f, Quaternion.identity, allMask)) //BottomRight
                    {
                        moveManager.isAttachedRight = false;
                        moveManager.wasLeft = false;
                        moveManager.wasRight = true;
                        myClimbVec = new Vector3(1, -1, 0);
                        cubeAnimator.Play("MoveRight", -1, 0f);
                        return true;
                    }
                }
                
                return false;
            }

            if (moveManager.isAttached)
            {
                if (Physics.Raycast(currentPlayer.transform.position, Vector3.down, rayDistance, attachMask))
                {
                    if (moveManager.wasLeft)
                    {
                        if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(-1, -1, 0), Vector3.one * 0.1f, Quaternion.identity, allMask)) //BottomLeft
                        {
                            myClimbVec = new Vector3(-1, -1, 0);
                            moveManager.wasRight = false;
                            moveManager.wasLeft = false;
                            cubeAnimator.Play("MoveLeft", -1, 0f);
                            return true;
                        }                   
                    }

                    if (moveManager.wasRight)
                    {
                        if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(1, -1, 0), Vector3.one * 0.1f, Quaternion.identity, allMask)) //BottomRight
                        {
                            myClimbVec = new Vector3(1, -1, 0);
                            moveManager.wasRight = false;
                            moveManager.wasLeft = false;
                            cubeAnimator.Play("MoveRight", -1, 0f);
                            return true;
                        }
                    }

                    if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(-1, -1, 0), Vector3.one * 0.1f, Quaternion.identity, allMask)) //Bottomleft
                    {
                        myClimbVec = new Vector3(-1, -1, 0);
                        cubeAnimator.Play("MoveLeft", -1, 0f);
                        return true;
                    }

                    if (!Physics.CheckBox(currentPlayer.transform.position + new Vector3(1, -1, 0), Vector3.one * 0.1f, Quaternion.identity, allMask)) //BottomRight
                    {
                        myClimbVec = new Vector3(1, -1, 0);
                        cubeAnimator.Play("MoveRight", -1, 0f);
                        return true;
                    }
                }

                return false;           
            }

            return false;
        }

        return false;
    }

    

    public void SyncPlayer() //Make Connections.
    {
        currentPlayer = gameObject.transform.GetChild(0).gameObject;
     
        moveManager = currentPlayer.GetComponent<MovementManager>();

        moveManager.isBeingControlled = true;

        playerRenderer = currentPlayer.transform.GetChild(0).GetComponent<Renderer>();

        cubeAnimator = currentPlayer.transform.GetChild(0).GetComponent<Animator>();

        SelectedOutline();
    }

    public void SelectedOutline()
    {
        if (moveManager.isAttached)
        {
            AttachedOutline();
        }
        else
        {
            playerRenderer.material.SetFloat("_Outline", 2);
            playerRenderer.material.SetColor("_OutlineColor", selectedOutlineColor);
        }
    }

    public void AttachedOutline()
    {
        playerRenderer.material.SetFloat("_Outline", 2);
        playerRenderer.material.SetColor("_OutlineColor", attachedOutlineColor);
    }

    public void RemoveOutline(GameObject target)
    {
        target.transform.GetChild(0).GetComponent<Renderer>().material.SetFloat("_Outline", 0);
    }

    public void PlayPulseAnim()
    {
        cubeAnimator.Play("Pulse", -1, 0f);
    }
}
