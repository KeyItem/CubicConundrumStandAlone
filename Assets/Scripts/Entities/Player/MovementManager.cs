using UnityEngine;
using System.Collections;

public class MovementManager : MonoBehaviour
{
    private PlayerController playerController;
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

    [Header ("Player Variables")]
    public float rayDistance;

    [Header ("Layers")]
    public LayerMask attachMask;
    public LayerMask allMask;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();

        playerController = GameObject.FindGameObjectWithTag("GodObject").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!CheckIfStillAttached())
        {
            Deattach();
        }
    }

    bool CheckIfStillAttached()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Physics.Raycast(transform.position, directionalVecArray[i], rayDistance, attachMask))
            {
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
        
    }
  
}
