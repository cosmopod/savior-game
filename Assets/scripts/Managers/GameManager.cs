using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    private static GameManager instance = null;
    public enum GameState {Play, GameOver, Win};
    public GameState gameState;


    // Singleton de GameManager
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        //TODO Tengo que preguntar por que los objetos que no se destruyen entre escenas bajan tanto el rendimiento de las escenas cargadas
        DontDestroyOnLoad(transform.gameObject);
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Helicopter.Instance.IsWorking() && gameState != GameState.Win)
        {
            gameState = GameState.Play;

        }
        else
        {
            gameState = GameState.GameOver;
        }



        if(City.Instance.FuelTanks == City.Instance.MaxFuelTanks)
        {
            gameState = GameState.Win;
        }


        if(gameState == GameState.GameOver)
        {
            Application.LoadLevel("gameOver");
        }
    }
}
