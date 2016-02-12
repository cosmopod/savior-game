using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{


    float missileVelocity = 70f;
    float turn = 20f;
    Rigidbody homingMissile;
    float fuseDelay;
    GameObject missileMod;
    public GameObject explosion;
    float lifeTime = 2f;
    float livingTime = 0f;
    public float damage = 10f;
    

    public LayerMask mask;
    public Transform target;


    // Use this for initialization
    void Start()
    {
        homingMissile = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        livingTime += Time.deltaTime;
        if (livingTime > lifeTime)
        {
            livingTime = 0f;
            Die();
        }
    }

    void FixedUpdate()
    {

        if (target == null || homingMissile == null)
            return;

        homingMissile.velocity = transform.forward * missileVelocity;

        var targetRotation = Quaternion.LookRotation(target.position - transform.position);

        homingMissile.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turn));

    }

    void OnTriggerEnter(Collider col)
    {

        if ((mask.value & 1 << col.gameObject.layer) > 0)
        {
            Helicopter.Instance.CurrentLife -= damage;
            Die();
        }
    }

    void Die()
    {
        livingTime = 0f;     
        RocketLauncher.Instance.StoreMissile(gameObject);

        GameObject instantiatedExplosion = (GameObject)GameObject.Instantiate(explosion, transform.position, transform.rotation);
        Destroy(instantiatedExplosion, 4f);
    }
}
