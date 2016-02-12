using UnityEngine;
using System.Collections;

public class BtnManager : MonoBehaviour
{
    Transform panel;

    // Use this for initialization
    void Start()
    {

        panel = transform.FindChild("Panel");
        if(panel != null)
        {
            panel.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if((GameManager.Instance.gameState == GameManager.GameState.Win) && (panel != null))
        {
            panel.gameObject.SetActive(true);
        }
    }


    public void RetryGame()
    {
        
        Application.LoadLevel("savior");
    }

    public void QuitAplication()
    {
        Application.Quit();
    }
}
