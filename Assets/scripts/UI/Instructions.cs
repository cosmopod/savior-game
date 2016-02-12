using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Instructions : MonoBehaviour
{

    Text text;
    float timeReading = 7f;
    float timeElapsed = 0f;


    public float letterPause = 2f;
    public AudioClip typeSound1;

    string message;
    Text textComp;
    AudioSource audioLetter;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        audioLetter = GetComponent<AudioSource>();

        textComp = GetComponent<Text>();
        message = textComp.text;
        textComp.text = "";
        StartCoroutine(TypeText());
        
    }

    // Update is called once per frame
    void Update()
    {

        timeElapsed += Time.deltaTime;
       if (timeElapsed >= timeReading)
            StartCoroutine(ReduceAlpha());
    }

    IEnumerator ReduceAlpha()
    {
        while (text.color.a > 0f)
        {
            Color textColor = text.color;
            textColor.a -= Time.deltaTime;
            text.color = textColor;
            yield return new WaitForSeconds(10f);
        }
    }

    IEnumerator TypeText()
    {
        foreach (char letter in message.ToCharArray())
        {
            textComp.text += letter;
            yield return 0;
            audioLetter.PlayOneShot(typeSound1);
            yield return new WaitForSeconds(letterPause);
        }
    }

}
