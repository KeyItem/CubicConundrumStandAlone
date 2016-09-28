using UnityEngine;
using System.Collections;

public class MovementManager : MonoBehaviour
{
    private Rigidbody myRB;

    public Vector3[] directionalVecArray;

    public bool canSwitch;
    public bool isBeingControlled;
    public bool isAttached;
    public bool isAttachedLeft;
    public bool isAttachedRight;
    public bool wasLeft;
    public bool wasRight;

    public float rayDistance;

    public LayerMask attachMask;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isBeingControlled)
        {
            if (!CheckIfStillAttached())
            {
                Deattach();
            }
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
    }
}
