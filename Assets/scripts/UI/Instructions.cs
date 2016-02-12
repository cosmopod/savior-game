using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{

    Text text;
    float timeReading = 3f;
    float timeElapsed = 0f;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {

        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeReading)
            StartCoroutine("ReduceAlpha");
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

}
