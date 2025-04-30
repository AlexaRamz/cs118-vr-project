using UnityEngine;

public class DynamicLight : MonoBehaviour
{
    // Source: https://www.youtube.com/watch?v=XJ1fCM6inao
    Light myLight;

    // Range Variables
    public bool changeRange = false;
    public float rangeSpeed = 1.0f;
    public float maxRange = 10.0f;

    // Intensity Variables
    public bool changeIntensity = false;
    public float intensitySpeed = 1.0f;
    public float maxIntensity = 10.0f;

    // Color Variables
    public bool changeColors = false;
    public float colorSpeed = 1.0f;
    public Color startColor;
    public Color endColor;

    float startTime;

    void Start()
    {
        myLight = GetComponent<Light>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeRange)
        {
            myLight.range = Mathf.PingPong(Time.time * rangeSpeed, maxRange);
        }

        if (changeIntensity)
        {
            myLight.intensity = Mathf.PingPong(Time.time * intensitySpeed, maxIntensity);
        }

        if (changeColors)
        {
            float t = Mathf.Sin(Time.time - startTime * colorSpeed);
            myLight.color = Color.Lerp(startColor, endColor, t); 
        }
    }
}
