using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunningScript : MonoBehaviour
{
    public delegate void TriggerEventHandler(Collider collider);
    public event TriggerEventHandler TriggerWithPlayer;
    public delegate void CollisionEventHandler(Collision collision);
    public event CollisionEventHandler CollideWithPlayer;

    public Rigidbody rb;
    //Animator animator;

    public float jumpForce;

    public float accelSpeed;

    //CollisionScripts
    [SerializeField]
    internal bool idle;
    [SerializeField]
    public bool grounded;
    [SerializeField]
    internal bool rising;
    [SerializeField]
    internal bool falling;
    [SerializeField]
    internal bool runJumping;

    internal Vector3 contactPoint;
    internal Vector3 impulseVector;
    internal Vector3 relativeSpeedVector;

    //Parameters
    [SerializeField]
    internal float xSpeed;

    [SerializeField]
    internal float ySpeed;

    [SerializeField]
    internal float inertia;

    [SerializeField]
    internal float vInertia;

    RInputManager inputManager;

    //StartupTimer
    public GameObject three;
    public GameObject two;
    public GameObject one;
    public GameObject jumpButton;
    public float startUpTimer;

    //CamWork
    public Cinemachine.CinemachineVirtualCamera followCam;

    public Cinemachine.CinemachineVirtualCamera startCam;
    public float holeStart;
    public float holeEnd;

    public Cinemachine.CinemachineVirtualCamera cineCam;
    public float changePoint;

    public Cinemachine.CinemachineVirtualCamera cineCamCloseUp;
    public float camCloseUpDuration;

    //SpeedMenu
    public float newSpeed;
    public float changeRatio;
    public bool isSpeedSet;
    public TextMeshPro speedValue;
    public GameObject speedPanel;

    private void Awake()
    {
        newSpeed = 300f;
        isSpeedSet = false;
       inputManager = RInputManager.Instance;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        //animator = GetComponent<Animator>();
        grounded = true;
       // Time.timeScale = 0.5f;
        //Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }

    private void OnEnable()
    {
        inputManager.OnTapPrimary += Jump;
        inputManager.OnTapPrimary += Select;
        TriggerWithPlayer += End;
    }

    private void OnDisable()
    {
        inputManager.OnTapPrimary -= Jump;
        inputManager.OnTapPrimary -= Select;
        TriggerWithPlayer -= End;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //ModelData
        if(startUpTimer >5f)
        {
            xSpeed = rb.velocity.z;
            ySpeed = rb.velocity.y;
            inertia = Mathf.Abs(rb.velocity.z);
            vInertia = Mathf.Abs(rb.velocity.y);
            if(transform.position.y < -1f)
            {
                rb.AddForce(new Vector3(0, 0, -1) * accelSpeed * 3 * Time.fixedDeltaTime);
            }
            else
            {
                rb.AddForce(new Vector3(0, 0, 1) * accelSpeed * Time.fixedDeltaTime);
            }
        }
    }
    private void Update()
    {
        if(isSpeedSet)
        {
            startUpTimer = startUpTimer + Time.deltaTime;
        }
        if(startUpTimer <2f)
        {

        }
        else if(startUpTimer<3f)
        {
            three.SetActive(true);
        }
        else if (startUpTimer < 4f)
        {
            three.SetActive(false);
            two.SetActive(true);
        }
        else if (startUpTimer < 5f)
        {
            two.SetActive(false);
            one.SetActive(true);
        }
        else if (startUpTimer < 6f)
        {
            one.SetActive(false);
            jumpButton.SetActive(true);
        }
        // animator.SetFloat("FallSpeed", ySpeed);


        if (transform.position.z > holeStart && transform.position.z < holeEnd)
        {
            cineCam.m_Priority = 10;
            startCam.m_Priority = 20;
            followCam.m_Priority = 10;
        }
        else if (transform.position.z>changePoint && transform.position.z <holeStart)
        {
            cineCam.m_Priority = 20;
            startCam.m_Priority = 10;
            followCam.m_Priority = 10;
        }
        else if (startUpTimer<1f)
        {
            startCam.m_Priority = 20;
            followCam.m_Priority = 10;
            cineCam.m_Priority = 10;
        }
        else
        {
            followCam.m_Priority = 20;
            startCam.m_Priority = 10;
            cineCam.m_Priority = 10;
        }

        if(transform.position.y < -0.2f)
        {
            grounded = false;
        }
    }

    public void Jump(Vector2 position, float time)
    {
        if (grounded && startUpTimer > 6f)
        {
            rb.velocity = Vector3.zero;
            //rb.AddForce(Vector3.up * jumpForce);
            rb.AddForce(new Vector3(0f,0.4f,1f) * jumpForce);
            grounded = false;
            runJumping = true;
        }
    }

    public void Select(Vector2 screenPosition, float time)
    {
        Debug.Log("Select Attempt");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Debug.DrawRay(ray.origin, ray.direction * 50000000, Color.blue, 120f);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Selected object: " + hit.transform.gameObject.name);
            if(hit.transform.gameObject.name == "MinusArrow")
            {
                newSpeed = newSpeed - changeRatio;
                speedValue.text = newSpeed.ToString();
            }
            else if (hit.transform.gameObject.name == "PlusArrow")
            {
                newSpeed = newSpeed + changeRatio;
                speedValue.text = newSpeed.ToString();
            }
            else if(hit.transform.gameObject.name == "SetButton")
            {
                accelSpeed = newSpeed;
                speedPanel.SetActive(false);
                isSpeedSet = true;
            }
        }
    }

    public void End(Collider collider)
    {
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Trigger trigger!");
        if (TriggerWithPlayer != null) TriggerWithPlayer(collider);
    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
        rising = false;

        contactPoint = collision.GetContact(0).point;
        impulseVector = collision.impulse.normalized;
        relativeSpeedVector = collision.relativeVelocity;
        runJumping = false;

        if (collision.collider.gameObject.tag == "LoseCon")
        {
            Debug.Log("Lose!");
            SceneManager.LoadScene(0);
        }

        if (collision.collider.gameObject.tag == "WinCon")
        {
            Debug.Log("Win!");
            SceneManager.LoadScene(0);
        }
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunningScript : MonoBehaviour
{
    public delegate void TriggerEventHandler(Collider collider);
    public event TriggerEventHandler TriggerWithPlayer;
    public delegate void CollisionEventHandler(Collision collision);
    public event CollisionEventHandler CollideWithPlayer;

    public Rigidbody rb;
    //Animator animator;

    public float jumpForce;

    public float accelSpeed;

    //CollisionScripts
    [SerializeField]
    internal bool idle;
    [SerializeField]
    internal bool grounded;
    [SerializeField]
    internal bool rising;
    [SerializeField]
    internal bool falling;
    [SerializeField]
    internal bool runJumping;

    internal Vector3 contactPoint;
    internal Vector3 impulseVector;
    internal Vector3 relativeSpeedVector;

    //Parameters
    [SerializeField]
    internal float xSpeed;

    [SerializeField]
    internal float ySpeed;

    [SerializeField]
    internal float inertia;

    [SerializeField]
    internal float vInertia;

    RInputManager inputManager;

    //StartupTimer
    public GameObject three;
    public GameObject two;
    public GameObject one;
    public GameObject jumpButton;
    public float startUpTimer;

    //CamWork
    public Cinemachine.CinemachineVirtualCamera followCam;


    public Cinemachine.CinemachineVirtualCamera startCam;
    public float holeStart;
    public float holeEnd;

    public Cinemachine.CinemachineVirtualCamera cineCam;
    public float changePoint;

    public Cinemachine.CinemachineVirtualCamera cineCamCloseUp;
    public float camCloseUpDuration;

    private void Awake()
    {
       inputManager = RInputManager.Instance;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        //animator = GetComponent<Animator>();
        grounded = true;
    }

    private void OnEnable()
    {
        inputManager.OnTapPrimary += Jump;
        TriggerWithPlayer += End;
    }

    private void OnDisable()
    {
        inputManager.OnTapPrimary -= Jump;
        TriggerWithPlayer -= End;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //ModelData
        if(startUpTimer >5f)
        {
            xSpeed = rb.velocity.z;
            ySpeed = rb.velocity.y;
            inertia = Mathf.Abs(rb.velocity.z);
            vInertia = Mathf.Abs(rb.velocity.y);
            if(transform.position.y < -1f)
            {
                rb.AddForce(new Vector3(0, 0, -1) * accelSpeed * 3 * Time.fixedDeltaTime);
            }
            else
            {
                rb.AddForce(new Vector3(0, 0, 1) * accelSpeed * Time.fixedDeltaTime);
            }
        }
    }

    private void Update()
    {
        startUpTimer = startUpTimer + Time.deltaTime;
        if(startUpTimer <2f)
        {

        }
        else if(startUpTimer<3f)
        {
            three.SetActive(true);
        }
        else if (startUpTimer < 4f)
        {
            three.SetActive(false);
            two.SetActive(true);
        }
        else if (startUpTimer < 5f)
        {
            two.SetActive(false);
            one.SetActive(true);
        }
        else if (startUpTimer < 6f)
        {
            one.SetActive(false);
            jumpButton.SetActive(true);
        }
        // animator.SetFloat("FallSpeed", ySpeed);


        if (transform.position.z > holeStart && transform.position.z < holeEnd)
        {
            cineCam.m_Priority = 10;
            startCam.m_Priority = 20;
            followCam.m_Priority = 10;
        }
        else if (transform.position.z>changePoint && transform.position.z <holeStart)
        {
            cineCam.m_Priority = 20;
            startCam.m_Priority = 10;
            followCam.m_Priority = 10;
        }
        else if (startUpTimer<1f)
        {
            startCam.m_Priority = 20;
            followCam.m_Priority = 10;
            cineCam.m_Priority = 10;
        }
        else
        {
            followCam.m_Priority = 20;
            startCam.m_Priority = 10;
            cineCam.m_Priority = 10;
        }
    }

    public void Jump(Vector2 position, float time)
    {
        if (grounded && startUpTimer>6f)
        {
            rb.AddForce(Vector3.up * jumpForce);
            grounded = false;
            runJumping = true;
        }
    }

    public void End(Collider collider)
    {
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Trigger trigger!");
        if (TriggerWithPlayer != null) TriggerWithPlayer(collider);
    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
        rising = false;

        contactPoint = collision.GetContact(0).point;
        impulseVector = collision.impulse.normalized;
        relativeSpeedVector = collision.relativeVelocity;
        runJumping = false;


        if (collision.collider.gameObject.tag == "LoseCon")
        {
            Debug.Log("Lose!");
            SceneManager.LoadScene(0);
        }

        if (collision.collider.gameObject.tag == "WinCon")
        {
            Debug.Log("Win!");
            SceneManager.LoadScene(0);
        }
    }
}
*/