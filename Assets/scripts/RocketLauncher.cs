using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RocketLauncher : MonoBehaviour
{

    float rotationSpeed = 100f;
    public LayerMask mask;
    bool enemyDetected = false;
    float reloadTime = 1f;
    float armingMissile = 0f;
    int poolSize = 4;
    GameObject enemy;
    Transform barrel;
    public GameObject missilePrefab;

    public List<GameObject> missileList = new List<GameObject>();
    private static RocketLauncher instance = null;

    Vector3 enemyDistance;

    // Singleton de Lanzador de cohetes
    public static RocketLauncher Instance
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
     //   DontDestroyOnLoad(this.gameObject);

        barrel = gameObject.transform.GetChild(0);
    }

    // Use this for initialization
    void Start()
    {
       
    }

    public bool EnemyDetected
    {
        get { return enemyDetected; }
        set
        {
            enemyDetected = value;
        }
    }

    public GameObject Enemy
    {
        get { return enemy; }
        set
        {
            enemy = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Play) return;

        if (enemyDetected)
        {

            Vector3 enemyPosition = enemy.transform.position;
            enemyDistance = enemyPosition - transform.position;
            Vector3 normalizedEnemyDistance = enemyDistance.normalized;

            //usamos solo las coordenadas (x,z) del vector distancia porque nos interesa que la rotacion sea plana con respecto al eje y del lanza misiles 
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.right, new Vector3(normalizedEnemyDistance.x, 0, normalizedEnemyDistance.z)), Time.deltaTime * 3f);

            armingMissile += Time.deltaTime;
            if (armingMissile >= reloadTime)
            {
                LaunchRocket();
                armingMissile = 0f;
            }

        }
        else
        {
            armingMissile = 0f;
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }



    }

    void OnTriggerStay(Collider col)
    {

        if ((mask.value & 1 << col.gameObject.layer) > 0)
        {
            if (Enemy == null && !enemyDetected)
            {
                enemyDetected = true;
                Enemy = col.gameObject;
            }

        }
    }

    void LaunchRocket()
    {

        GameObject missile = GetMissile();

        if (Enemy != null)
            missile.GetComponent<Missile>().target = Enemy.transform;

    }

    private GameObject GetMissile()
    {

        GameObject obj = null;

        if (missileList.Count > poolSize)
        {

            obj = missileList[missileList.Count - 1];
            missileList.Remove(obj);

            obj.SetActive(true);
            obj.transform.position = barrel.position;
            obj.transform.rotation = barrel.rotation;

        }
        else {

            obj = Instantiate(missilePrefab, barrel.position, Quaternion.Euler(enemyDistance.normalized)) as GameObject;
            obj.GetComponent<Rigidbody>().isKinematic = false;

        }

        return obj;

    }

    public void StoreMissile(GameObject go)
    {

        missileList.Add(go);
        go.GetComponent<Rigidbody>().velocity = Vector3.zero;
        go.SetActive(false);

    }

  
}
