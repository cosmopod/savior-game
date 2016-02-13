using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BtnManager : MonoBehaviour
{
    Transform panel;
    RawImage bannerTitle;
    public Texture winTexture;
    public Texture loseTexture;


    // Use this for initialization
    void Start()
    {
        panel = transform.FindChild("Panel");
        bannerTitle = panel.FindChild("BannerTitle").gameObject.GetComponent<RawImage>();
        panel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.Instance.gameState)
        {
            case GameManager.GameState.Play:
                panel.gameObject.SetActive(false);        
                break;

            case GameManager.GameState.Win:
                panel.gameObject.SetActive(true);
                bannerTitle.texture = winTexture;
                break;

            case GameManager.GameState.GameOver:
                panel.gameObject.SetActive(true);
                bannerTitle.texture = loseTexture;
                break;
        }
    }

    public void RetryGame()
    {
        GameManager.Instance.gameState = GameManager.GameState.Play;
        Application.LoadLevel(Application.loadedLevelName);
    }

    public void QuitAplication()
    {
        Application.Quit();
    }
}
