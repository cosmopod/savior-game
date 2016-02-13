using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{

    public enum GameState { Play, GameOver, Win };
    public GameState gameState;
    bool readBriefing;


    public bool ReadBriefing
    {
        get { return readBriefing; }
        set { readBriefing = value; }
    }

    protected GameManager() { }

    private void Awake()
    {
      
    }


    // Use this for initialization
    void Start()
    {
        ReadBriefing = !ReadBriefing;
    }

    // Update is called once per frame
    void Update()
    {
        if (Helicopter.Instance.IsWorking())
        {
            gameState = GameState.Play;

        }
        else
        {
            gameState = GameState.GameOver;
        }



        if (City.Instance.FuelTanks == City.Instance.MaxFuelTanks)
        {
            gameState = GameState.Win;
        }

    }
}
