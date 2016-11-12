using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
    private SFXManager sfxManager;

    [Header("Color")]
    public string switchColor; //1: Red, 2: Blue, 3: Green, 4: Yellow

    [Header("Boolean Variables")]
    public GameObject targetDoor;
    public bool isPressed;

    void Start()
    {
        sfxManager = GameObject.FindGameObjectWithTag("SFXManager").GetComponent<SFXManager>();
    }

    void Update()
    {
        RelayInformation();
    }

    void OnTriggerEnter(Collider player)
    {
        if (switchColor + "Cube" == player.gameObject.tag)
        {
            isPressed = true;
            sfxManager.PlaySFX(01);
        }
    }

    void RelayInformation()
    {
        if (isPressed)
        {
            targetDoor.GetComponent<DoorController>().isClosing = false;
            targetDoor.GetComponent<DoorController>().isOpening = true;
            Destroy(gameObject);
        }  
    }

}

