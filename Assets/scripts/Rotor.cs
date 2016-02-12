using UnityEngine;
using System.Collections;

public class Rotor : MonoBehaviour
{
    float rotationSpeed = 1000f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

    }
}
