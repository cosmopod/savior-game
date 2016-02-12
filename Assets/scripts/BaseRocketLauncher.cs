using UnityEngine;
using System.Collections;

public class BaseRocketLauncher : MonoBehaviour
{

    public LayerMask mask;
    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerExit(Collider col)
    {
        if ((mask.value & 1 << col.gameObject.layer) > 0)
        {
            RocketLauncher.Instance.EnemyDetected = false;
            RocketLauncher.Instance.Enemy = null;
        }
    }
}
