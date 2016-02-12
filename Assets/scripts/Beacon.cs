using UnityEngine;
using System.Collections;

public class Beacon : MonoBehaviour
{

    float maxIntensity = 2f;
    Light beacon;
    public bool working = false;


    public bool Working
    {
        get { return working; }
        set { working = value; }
    }

    void Awake()
    {
        beacon = GetComponent<Light>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!working)
        {
            beacon.intensity = Mathf.PingPong(Time.time, maxIntensity);
        }
        else
        {
            beacon.intensity = maxIntensity;
        }
    }


    public void SetColor(Color color)
    {
        beacon.color = color;
    }
}
