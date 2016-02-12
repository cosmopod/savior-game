using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class City : MonoBehaviour
{


    int numFuelTanks = 0;
    int maxFuelTanks;

    Dictionary<int, Color> beaconColors = new Dictionary<int, Color>();
    public Beacon beacon;

    private static City instance = null;

    // Singleton de la Ciudad
    public static City Instance
    {
        get
        {
            return instance;
        }
    }

    public int MaxFuelTanks
    {
        get { return maxFuelTanks; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        //  DontDestroyOnLoad(this.gameObject);

    }

    public int FuelTanks
    {
        get { return numFuelTanks; }
        set { numFuelTanks = value; }
    }

    // Use this for initialization
    void Start()
    {
        beaconColors.Add(0, Color.red);
        beaconColors.Add(1, Color.yellow);
        beaconColors.Add(2, Color.green);

        maxFuelTanks = beaconColors.Count - 1;
    }

    // Update is called once per frame
    void Update()
    {
        beacon.SetColor(beaconColors[FuelTanks]);
        if (FuelTanks == maxFuelTanks)
        {
            beacon.Working = true;
        }

    }
}
