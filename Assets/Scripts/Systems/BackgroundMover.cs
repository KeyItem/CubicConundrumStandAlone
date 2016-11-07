using UnityEngine;
using System.Collections;

public class BackgroundMover : MonoBehaviour 
{
    private TimerClass timerClass;

    [Header ("Movement Variables")]
    public float moveSpeed;
    public float moveDelay;
    public float moveAmount;

    private Vector3 topPos;
    private Vector3 bottomPos;

    [Header ("Movement Booleans")]
    public bool startedMoving;
    public bool isGoingUp = true;
    public bool isGoingDown;

	void Start () 
    {
        moveDelay = Random.Range(1, 4);

        timerClass = new TimerClass();
        timerClass.ResetTimer(moveDelay);

        CalculatePositions();
	}
	
	void Update () 
    {
        Timer();

        if (startedMoving)
        {
            Move();
        }
	}

    void Move()
    {
        if (isGoingUp)
        {
            transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;
        }

        if (isGoingDown)
        {
            transform.position += new Vector3(0, -moveSpeed, 0) * Time.deltaTime;
        }

        if (transform.position.y > topPos.y)
        {
            isGoingUp = false;
            isGoingDown = true;
        }

        if (transform.position.y < bottomPos.y)
        {
            isGoingDown = false;
            isGoingUp = true;
        }
    }

    void CalculatePositions()
    {
        topPos = transform.position + new Vector3(0, moveAmount, 0);
        bottomPos = transform.position - new Vector3(0, moveAmount, 0);
    }

    void Timer()
    {
        if (!startedMoving)
        {
            if (timerClass.TimerIsDone())
            {
                startedMoving = true;
            }
        }
    }
}
