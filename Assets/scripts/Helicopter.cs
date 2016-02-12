using UnityEngine;
using System.Collections;

public class Helicopter : MonoBehaviour
{

    public float speed = 5f;
    public float rotationSpeed = 2f;
    float downLimit = 10f;
    float upLimit = 100f;
    float leftLimit = 25f;
    float rightLimit = 455f;

    public float life = 100f;

    float currentLife;

    Quaternion faceRight = Quaternion.Euler(new Vector3(0f, 90f, 0f));
    Quaternion faceLeft = Quaternion.Euler(new Vector3(0f, 270f, 0f));

    bool loaded = false;
    bool turning = false;
    bool facingRight = true;
    bool canReleaseFuelTank;


    public LayerMask fuelTankLayer;
    public LayerMask cityLayer;

    GameObject fuelTank;

    RaycastHit hit;
    ParticleSystem steam;

    private static Helicopter instance = null;

    // Singleton de Helicoptero
    public static Helicopter Instance
    {
        get
        {
            return instance;
        }
    }

    public bool CanReleaseFuelTank
    {
        get { return canReleaseFuelTank; }
        set { canReleaseFuelTank = value; }
    }

    public float CurrentLife
    {
        get { return currentLife; }
        set { currentLife = value; }
    }

    public float Life
    {
        get { return life; }
        set { life = value; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }


        instance = this;
      //  DontDestroyOnLoad(this.gameObject);

    }
    // Use this for initialization
    void Start()
    {
        CurrentLife = Life;
        fuelTank = transform.FindChild("loaded_tank").gameObject;
        if (fuelTank != null)
            fuelTank.SetActive(false);

        steam = GetComponent<ParticleSystem>();
        steam.enableEmission = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Play) return;

        float translation = Input.GetAxis("Horizontal") * speed;
        float rotation = Input.GetAxis("Vertical") * rotationSpeed;

        float step = 500 * Time.deltaTime;

        if ((translation < 0 && facingRight) || (translation < 0 && turning))
        {
            turning = true;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, faceLeft, step);
            Vector3 forward = transform.rotation.eulerAngles;
            float distance = Vector3.Distance(forward, faceLeft.eulerAngles);
            if (distance < 0.1f)
            {
                facingRight = false;
                turning = false;
            }
        }
        if ((translation > 0 && !facingRight) || (translation > 0 && turning))
        {
            turning = true;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, faceRight, step);
            Vector3 forward = transform.rotation.eulerAngles;
            float distance = Vector3.Distance(forward, faceRight.eulerAngles);
            if (distance < 0.1f)
            {

                facingRight = true;
                turning = false;
            }
        }


        if (!turning)
        {

            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;
            float reverse = facingRight ? 1 : -1;

            transform.Translate(0, rotation, translation * reverse);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit), Mathf.Clamp(transform.position.y, downLimit, upLimit), transform.position.z);

        }


        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, 10f))
        {

            if ((fuelTankLayer.value & 1 << hit.transform.gameObject.layer) > 0 && !loaded)
            {
                if (Input.GetKeyDown("f"))
                    LoadFuelTank(hit.transform.gameObject);
            }
        }

        if (Input.GetKeyDown("g") && CanReleaseFuelTank && loaded)
        {
            ReleaseFuelTank();
            City.Instance.FuelTanks++;
        }

        if (CurrentLife <= Life * 0.5f)
            steam.enableEmission = true;
    }

    void LateUpdate()
    {
        float xPosHelicopter = gameObject.transform.position.x;
        Vector3 posCamera = Camera.main.transform.position;
        Vector3 newPosition = new Vector3(xPosHelicopter, posCamera.y, posCamera.z);
        Camera.main.transform.position = Vector3.Lerp(posCamera, newPosition, 5f);
    }

    public bool IsWorking()
    {
        return CurrentLife > 0;
    }

    public void LoadFuelTank(GameObject fuelTank)
    {
        loaded = true;
        fuelTank.SetActive(false);
        this.fuelTank.SetActive(true);
    }

    public void ReleaseFuelTank()
    {
        loaded = false;
        this.fuelTank.SetActive(false);

    }

    void OnTriggerStay(Collider col)
    {
        if ((cityLayer.value & 1 << col.gameObject.layer) > 0)
        {
            CanReleaseFuelTank = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        CanReleaseFuelTank = false;
    }

}
