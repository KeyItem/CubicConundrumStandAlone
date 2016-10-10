using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{
    private CameraController cameraController;

    [Header ("Sun and Moon")]
    public Light sunLight;
    public Light moonLight;

    [Header("Cycle Variables")]
    public float cycleLengthMinutes;
    public float currentTime;
    public float rotateSpeed;
    public float timer;
    private float timerReset;
    public bool isDay;
    public bool isNight;
    public bool canLerpDay;
    public bool canLerpNight;

    [Header("Color Variables")]
    public float lerpDuration;
    private float time;
    public Color dayColor;
    public Color nightColor;

	void Start ()
    {
        cameraController = Camera.main.GetComponent<CameraController>();

        rotateSpeed = 360.0f / (cycleLengthMinutes * 60f) * Time.deltaTime;

        timer = cycleLengthMinutes * 60f;

        timerReset = timer;

        dayColor = Camera.main.backgroundColor;
    }
	
	void Update ()
    {      
        RotateSunMoon();
        Timer();
        BackgroundColorLerp();
    }

    void RotateSunMoon()
    {       
        sunLight.transform.RotateAround(Vector3.zero, Vector3.forward, rotateSpeed);
        sunLight.transform.LookAt(Vector3.zero);

        moonLight.transform.RotateAround(Vector3.zero, Vector3.forward, rotateSpeed);
        moonLight.transform.LookAt(Vector3.zero);
    }

    void Timer()
    {
        timer -= Time.deltaTime;

        if (timer < timerReset / 2)
        {
            isDay = true;
            isNight = false;
        }

        if (timer > timerReset / 2)
        {
            isNight = true;
            isDay = false;
        }

        if (timer < 0)
        {
            timer = timerReset;
        }
    }

    void BackgroundColorLerp()
    {
        if (canLerpDay)
        {
            Camera.main.backgroundColor = Color.Lerp(nightColor, dayColor, time);

            if (Camera.main.backgroundColor == dayColor)
            {
                time = 0;
                canLerpDay = false;
            }
        }

        if (canLerpNight)
        {
            Camera.main.backgroundColor = Color.Lerp(dayColor, nightColor, time);

            if (Camera.main.backgroundColor == nightColor)
            {
                time = 0;
                canLerpNight = false;
            }
        }

        if (time < 1)
        {
            time += Time.deltaTime/lerpDuration;
        }
    }
}

