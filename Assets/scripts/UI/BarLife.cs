using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarLife : MonoBehaviour {


    Image barLife;

	// Use this for initialization
	void Start () {

        barLife = GetComponent<Image>();
        if (barLife == null)
            Debug.LogError("No se ha encontrado la imagen.");
	}
	
	// Update is called once per frame
	void Update () {

        barLife.fillAmount = Helicopter.Instance.CurrentLife / Helicopter.Instance.Life;
	}
}
