using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunJumpScript : MonoBehaviour
{
    public GameObject stateDrivenCam;

    [SerializeField]
    public Camera inCam;

    private InputManagerB inputManager;
    private CharManagerScript charManager;

    private Animator animator;

    public bool focused = true;

    public bool accelerating;
    public float accelSpeed;
    public float jumpForce;
    public float xStart;
    public float xEnd;
    public Collider winCon;
    public Collider loseCon;
    public Vector3 spawnPointPos;
    public Vector3 spawnPointRot;

    bool startSwitch = false;

    private void Awake()
    {
        animator = stateDrivenCam.GetComponent<Animator>();
        inputManager = InputManagerB.Instance;
        charManager = CharManagerScript.Instance;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    

    private void OnEnable()
    {
        charManager.SetCharActive(true);
        charManager.rb.velocity = Vector3.zero;
        charManager.transform.SetPositionAndRotation(spawnPointPos, Quaternion.Euler(spawnPointRot));

        inputManager.OnTapPrimary += Jump;
        inputManager.currentCam = inCam;
        if (!startSwitch)
        {
            startSwitch = true;
        }
        else
        {
            inputManager.DisableMainScreenFunctions();
        }
        charManager.TriggerWithPlayer += End;
    }

    private void OnDisable()
    {
        if(charManager.gameObject.activeSelf)
        {
            charManager.SetCharActive(false);
        }
        inputManager.OnTapPrimary -= Jump;
        inputManager.currentCam = inputManager.cam;
        inputManager.EnableMainScreenFunctions();

        charManager.TriggerWithPlayer -= End;
    }

    void FixedUpdate()
    {
        charManager.rb.AddForce(new Vector3(1, 0, 0) * accelSpeed * Time.fixedDeltaTime);
        accelerating = true;


        if ((charManager.transform.position.x > xStart) && (charManager.transform.position.x < xEnd) && focused)
        {
            animator.Play("Overview");
            focused = !focused;
        }

        if ((charManager.transform.position.x > xEnd) && !focused)
        {
            animator.Play("Focused");
            focused = !focused;
        }
    }

    public void Jump(Vector2 position, float time)
    {
        if(charManager.GetComponent<CollisionScript>().grounded)
        {
            //La idea es que el jugador tenga que mantener apretado el click hasta un punto exacto en el que tenga que soltarlo y ahi se ejecute el salto. Una bola se forma alrededor del punto donde iria el pulgar, y una de las bolas mas grandes se cierra sobre esta pero sin llegar al centro, sino hasta la primera bola de las dos. Esto es para que el jugador pueda ver claramente el limite en el que tiene que soltar el boton; asi no lo tapa el pulgar.
            Debug.Log("InJump");
            charManager.rb.AddForce(Vector3.up * jumpForce);
        }
    }
    private void SwitchPriority(Vector2 position, float time)
    {
        Debug.Log("Switchin!!");
        if (focused)
        {
            animator.Play("Overview");
        }
        else
        {
            animator.Play("Focused");
        }
        focused = !focused;
    }

    public void End(Collider collider)
    {
        Debug.Log("In End");
        if (collider.gameObject.tag == "LoseCon")
        {
            Debug.Log("Lose!");
            SceneManager.LoadScene(0);
        }

        if (collider.gameObject.tag == "WinCon")
        {
            Debug.Log("Win!");
            SceneManager.LoadScene(0);
        }
    }
}

//Mas adelante, hacer que el landing sea un parry tambien o T cae rodando y se hace verga. O de ultima hacer que esto pase cuando la velocidad es demasiado alta.