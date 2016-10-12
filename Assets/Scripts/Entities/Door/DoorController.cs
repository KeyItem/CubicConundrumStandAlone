using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    private Collider doorCollider;

    public float moveSpeed;
    public float moveDistance;

    public bool isOpen;
    public bool isOpening;
    public bool isClosing;

    public Vector3 originPos;
    public Vector3 openedPos;

    void Start()
    {
        doorCollider = GetComponent<Collider>();

        openedPos = new Vector3(transform.position.x, transform.position.y + moveDistance, transform.position.z);
        originPos = transform.position;
    }
	
	void Update ()
    {
	  if (isOpening)
        {
            OpenDoor();
        }

      if (isClosing)
        {
            CloseDoor();
        }

      if (isOpen)
        {
            doorCollider.enabled = false;
        }
        else
        {
            doorCollider.enabled = true;
        }
	}

    void OpenDoor ()
    {
        isClosing = false;

        if (isOpening)
        {
            transform.position = Vector3.Lerp(transform.position, openedPos, moveSpeed);

            if (transform.position == openedPos)
            {
                isOpening = false;
                isOpen = true;
            }
        }
    }    
    
    void CloseDoor ()
    {
        isOpening = false;

        if (isClosing)
        {
            transform.position = Vector3.Lerp(transform.position, originPos, moveSpeed);

            if (transform.position == originPos)
            {
                isClosing = false;
                isOpen = false;
            }
        }
    } 
}
