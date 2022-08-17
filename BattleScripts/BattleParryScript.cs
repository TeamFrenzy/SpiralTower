using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleParryScript : MonoBehaviour
{
    [SerializeField] public bool isBParrying;
    [SerializeField] private float bParryingCurrentTime;
    [SerializeField] private float bParryingMaxTime;

    [SerializeField] private bool isDowntime;
    [SerializeField] private float downtimeCurrentTime;
    [SerializeField] private float downtimeMaxTime;

    [SerializeField] private bool isSideStepping;
    [SerializeField] public float sideSteppingDistance;
    public float sideStepElapsedTime;
    public float sideStepDesiredDuration;
    public Vector3 sideStepStartPosition;
    public Vector3 sideStepEndPosition;
    public float sideStepSpeed;

    [SerializeField] private GameObject tamakiObject;
    [SerializeField] private GameObject nanamiObject;

    [SerializeField] private Animator animator;

    private BattleManager battleManager;

    //private TestInput testInputActions;

    float afterParryTimer;

    //Charge Shot Variables
    public bool charging;
    public float chargeCounter;

    //Missfire: si mantenes apretado el CS demasiado tiempo te explota en la cara?

    //Variables knockback
    public bool hit;

    //Variables dodge
    public bool dodging;

    public bool isDown;

    BSInputManager bsInputManager;

    private void Awake()
    {
        isDown = false;
        dodging = false;
        battleManager = BattleManager.Instance;
        isBParrying = false;
        // testInputActions = new TestInput();
        bsInputManager = BSInputManager.Instance;
        GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = Color.white;
    }

    private void OnEnable()
    {
        bsInputManager.OnTapPrimary += Action;
        // testInputActions.Enable();
        // testInputActions.TestPlayer.XDown.started += BParry;
        // testInputActions.TestPlayer.ZDown.started += Charging;
        // testInputActions.TestPlayer.ZDown.performed += ChargeShot;
        // testInputActions.TestPlayer.ZDown.canceled += CancelShot;
    }

    private void OnDisable()
    {
        bsInputManager.OnTapPrimary -= Action;
        // testInputActions.Disable();
        // testInputActions.TestPlayer.XDown.started -= BParry;
        //  testInputActions.TestPlayer.ZDown.started -= Charging;
        // testInputActions.TestPlayer.ZDown.performed -= ChargeShot;
        // testInputActions.TestPlayer.ZDown.canceled += CancelShot;
    }

    public void Action(Vector2 position, float time)
    {
        Debug.Log("Actual position: " + position);
        if(position.x > 400f)
        {
            Debug.Log("Right Half");
            if(!charging)
            {
                Charging();
            }
            else if(charging)
            {
                ChargeShot();
            }
        }
        else
        {
            Debug.Log("Left Half");
            BParry();
        }
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position+new Vector3(0f,2f,0f), transform.right);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);

        Ray ray2 = new Ray(transform.position + new Vector3(0f, 2f, 0f), -transform.right);
        Debug.DrawRay(ray2.origin, ray2.direction * 100f, Color.cyan);

        if(hit)
        {
            if(nanamiObject.GetComponentInChildren<NanamiManager>().currentAction == NanamiManager.Action.Blast || nanamiObject.GetComponentInChildren<NanamiManager>().currentAction == NanamiManager.Action.ReBlast)
            {
                //Debug.Log("Hit By Blast");
                transform.position = Vector3.MoveTowards(transform.position, nanamiObject.GetComponentInChildren<NanamiManager>().knockbackNewLocation, nanamiObject.GetComponentInChildren<NanamiManager>().knockBackSpeed * Time.deltaTime);

                if (transform.position == nanamiObject.GetComponentInChildren<NanamiManager>().knockbackNewLocation)
                {
                    hit = false;
                }
            }
            else if(nanamiObject.GetComponentInChildren<NanamiManager>().currentAction == NanamiManager.Action.JumpDeathBlow)
            {
                //Debug.Log("Hit By JumpDeathBlow. Game Over.");
                hit = false;
            }
        }

        if (afterParryTimer>0f)
        {
            afterParryTimer = afterParryTimer - Time.deltaTime;
            if(afterParryTimer<=0f)
            {
               // Debug.Log("Triggered");
                //animator.Play("Improved Idle");
            }
        }

        if(charging)
        {
            chargeCounter = chargeCounter + Time.deltaTime;
        }

        if(isBParrying)
        {
            bParryingCurrentTime = bParryingCurrentTime - Time.deltaTime;
            if(bParryingCurrentTime<=0f)
            {
                GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = Color.red;
                isBParrying = false;
                isDowntime = true;
                downtimeCurrentTime = downtimeMaxTime;
            }
        }
        if(isDowntime)
        {
            downtimeCurrentTime = downtimeCurrentTime - Time.deltaTime;
            {
                if(downtimeCurrentTime<=0f)
                {
                    GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = Color.white;
                    isDowntime = false;
                }
            }
        }
        if(isSideStepping)
        {
            //Ojo que el timer de Blast podria terminar antes de que Tamaki termine de evadir
            if (nanamiObject.GetComponentInChildren<NanamiManager>().currentAction == NanamiManager.Action.Blast || nanamiObject.GetComponentInChildren<NanamiManager>().currentAction == NanamiManager.Action.ReBlast || nanamiObject.GetComponentInChildren<NanamiManager>().currentAction == NanamiManager.Action.SpearShot)
            {
                tamakiObject.transform.position = Vector3.MoveTowards(tamakiObject.transform.position, sideStepEndPosition, sideStepSpeed * Time.deltaTime);
                if (tamakiObject.transform.position == sideStepEndPosition)
                {
                   // Debug.Log("Dodged Blast");
                    isSideStepping = false;
                    animator.Play("Improved Idle");
                }
            }
            else if(nanamiObject.GetComponentInChildren<NanamiManager>().currentAction == NanamiManager.Action.JumpDeathBlow)
            {
                tamakiObject.transform.position = Vector3.MoveTowards(tamakiObject.transform.position, sideStepEndPosition, sideStepSpeed * Time.deltaTime);
                if (tamakiObject.transform.position == sideStepEndPosition)
                {
                   // Debug.Log("Dodged JumpDeathBlow");
                    isSideStepping = false;
                    animator.Play("Improved Idle");
                }
            }
        }
    }

    void BParry()
    {
       // Debug.Log("BParryTriggered");
        if(!isBParrying && !isDowntime &&!charging)
        {
            //GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = Color.green;
            isBParrying = true;
            GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = Color.green;
            bParryingCurrentTime = bParryingMaxTime;
        }
    }

    void Charging()
    {
        if(!charging)
        {
            animator.Play("Charging");

        }
       // Debug.Log("Charging");
        charging = true;
    }

    void ChargeShot()
    {
        Debug.Log("Shooting " + chargeCounter);
        animator.Play("Recoiling");
        nanamiObject.GetComponentInChildren<NanamiManager>().chargedShotExecuted = true;
        chargeCounter = 0f;
        charging = false;
    }

    void CancelShot(InputAction.CallbackContext obj)
    {
      //  Debug.Log("Cancelling");
        chargeCounter = 0f;
        charging = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isBParrying)
        {
            if (battleManager.phase == BattleManager.Phase.Charge)
            {
               
            }
            isSideStepping = true;
            //sideStepEndPosition = new Vector3(tamakiObject.transform.localPosition.x + sideSteppingDistance, tamakiObject.transform.localPosition.y, tamakiObject.transform.localPosition.z);

            //El dodge siempre tiene que ser en direccion perpendicular al blast.
            //Paso 1: Obtener la direccion a la que hacer el dodge basandose en la distancia de Tamaki al centro del mapa.

            animator.Play("Standing Dodge Left");
            //animator.Play("Spear_Dodge_Back");
        }
        else
        {
            //Debug.Log("Attacked!");
            //animator.Play("Stunned");
        }
    }
}


/*
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleParryScript : MonoBehaviour
{
    [SerializeField] public bool isBParrying;
    [SerializeField] private float bParryingCurrentTime;
    [SerializeField] private float bParryingMaxTime;

    [SerializeField] private bool isDowntime;
    [SerializeField] private float downtimeCurrentTime;
    [SerializeField] private float downtimeMaxTime;

    [SerializeField] private bool isSideStepping;
    [SerializeField] public float sideSteppingDistance;
    public float sideStepElapsedTime;
    public float sideStepDesiredDuration;
    public Vector3 sideStepStartPosition;
    public Vector3 sideStepEndPosition;
    public float sideStepSpeed;

    [SerializeField] private GameObject tamakiObject;
    [SerializeField] private GameObject nanamiObject;

    [SerializeField] private Animator animator;

    private BattleManager battleManager;

    //private TestInput testInputActions;

    float afterParryTimer;

    //Charge Shot Variables
    public bool charging;
    public float chargeCounter;

    //Missfire: si mantenes apretado el CS demasiado tiempo te explota en la cara?

    //Variables knockback
    public bool hit;

    //Variables dodge
    public bool dodging;

    BSInputManager bsInputManager;

    private void Awake()
    {
        dodging = false;
        battleManager = BattleManager.Instance;
        isBParrying = false;
        // testInputActions = new TestInput();
        bsInputManager = BSInputManager.Instance;
        GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = Color.white;
    }

    private void OnEnable()
    {
        // testInputActions.Enable();
        // testInputActions.TestPlayer.XDown.started += BParry;
        // testInputActions.TestPlayer.ZDown.started += Charging;
        // testInputActions.TestPlayer.ZDown.performed += ChargeShot;
        // testInputActions.TestPlayer.ZDown.canceled += CancelShot;
    }

    private void OnDisable()
    {
       // testInputActions.Disable();
       // testInputActions.TestPlayer.XDown.started -= BParry;
      //  testInputActions.TestPlayer.ZDown.started -= Charging;
       // testInputActions.TestPlayer.ZDown.performed -= ChargeShot;
       // testInputActions.TestPlayer.ZDown.canceled += CancelShot;
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position+new Vector3(0f,2f,0f), transform.right);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);

        Ray ray2 = new Ray(transform.position + new Vector3(0f, 2f, 0f), -transform.right);
        Debug.DrawRay(ray2.origin, ray2.direction * 100f, Color.cyan);

        if(hit)
        {
            if(nanamiObject.GetComponentInChildren<NanamiManager>().currentAction == NanamiManager.Action.Blast || nanamiObject.GetComponentInChildren<NanamiManager>().currentAction == NanamiManager.Action.ReBlast)
            {
                //Debug.Log("Hit By Blast");
                transform.position = Vector3.MoveTowards(transform.position, nanamiObject.GetComponentInChildren<NanamiManager>().knockbackNewLocation, nanamiObject.GetComponentInChildren<NanamiManager>().knockBackSpeed * Time.deltaTime);

                if (transform.position == nanamiObject.GetComponentInChildren<NanamiManager>().knockbackNewLocation)
                {
                    hit = false;
                }
            }
            else if(nanamiObject.GetComponentInChildren<NanamiManager>().currentAction == NanamiManager.Action.JumpDeathBlow)
            {
                //Debug.Log("Hit By JumpDeathBlow. Game Over.");
                hit = false;
            }
        }

        if (afterParryTimer>0f)
        {
            afterParryTimer = afterParryTimer - Time.deltaTime;
            if(afterParryTimer<=0f)
            {
               // Debug.Log("Triggered");
                //animator.Play("Improved Idle");
            }
        }

        if(charging)
        {
            chargeCounter = chargeCounter + Time.deltaTime;
        }

        if(isBParrying)
        {
            bParryingCurrentTime = bParryingCurrentTime - Time.deltaTime;
            if(bParryingCurrentTime<=0f)
            {
                GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = Color.red;
                isBParrying = false;
                isDowntime = true;
                downtimeCurrentTime = downtimeMaxTime;
            }
        }
        if(isDowntime)
        {
            downtimeCurrentTime = downtimeCurrentTime - Time.deltaTime;
            {
                if(downtimeCurrentTime<=0f)
                {
                    GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = Color.white;
                    isDowntime = false;
                }
            }
        }
        if(isSideStepping)
        {
            //Ojo que el timer de Blast podria terminar antes de que Tamaki termine de evadir
            if (nanamiObject.GetComponentInChildren<NanamiManager>().currentAction == NanamiManager.Action.Blast || nanamiObject.GetComponentInChildren<NanamiManager>().currentAction == NanamiManager.Action.ReBlast)
            {
                tamakiObject.transform.position = Vector3.MoveTowards(tamakiObject.transform.position, sideStepEndPosition, sideStepSpeed * Time.deltaTime);
                if (tamakiObject.transform.position == sideStepEndPosition)
                {
                   // Debug.Log("Dodged Blast");
                    isSideStepping = false;
                    animator.Play("Improved Idle");
                }
            }
            else if(nanamiObject.GetComponentInChildren<NanamiManager>().currentAction == NanamiManager.Action.JumpDeathBlow)
            {
                tamakiObject.transform.position = Vector3.MoveTowards(tamakiObject.transform.position, sideStepEndPosition, sideStepSpeed * Time.deltaTime);
                if (tamakiObject.transform.position == sideStepEndPosition)
                {
                   // Debug.Log("Dodged JumpDeathBlow");
                    isSideStepping = false;
                    animator.Play("Improved Idle");
                }
            }
        }
    }

    void BParry(InputAction.CallbackContext obj)
    {
       // Debug.Log("BParryTriggered");
        if(!isBParrying && !isDowntime &&!charging)
        {
            //GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = Color.green;
            isBParrying = true;
            GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = Color.green;
            bParryingCurrentTime = bParryingMaxTime;
        }
    }

    void Charging(InputAction.CallbackContext obj)
    {
        if(!charging)
        {
            animator.Play("Charging");

        }
       // Debug.Log("Charging");
        charging = true;
    }

    void ChargeShot(InputAction.CallbackContext obj)
    {
        Debug.Log("Shooting " + chargeCounter);
        animator.Play("Recoiling");
        nanamiObject.GetComponentInChildren<NanamiManager>().chargedShotExecuted = true;
        chargeCounter = 0f;
        charging = false;
    }

    void CancelShot(InputAction.CallbackContext obj)
    {
      //  Debug.Log("Cancelling");
        chargeCounter = 0f;
        charging = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isBParrying)
        {
            if (battleManager.phase == BattleManager.Phase.Charge)
            {
               
            }
            isSideStepping = true;
            //sideStepEndPosition = new Vector3(tamakiObject.transform.localPosition.x + sideSteppingDistance, tamakiObject.transform.localPosition.y, tamakiObject.transform.localPosition.z);

            //El dodge siempre tiene que ser en direccion perpendicular al blast.
            //Paso 1: Obtener la direccion a la que hacer el dodge basandose en la distancia de Tamaki al centro del mapa.

            animator.Play("Standing Dodge Left");
            //animator.Play("Spear_Dodge_Back");
        }
        else
        {
            //Debug.Log("Attacked!");
            //animator.Play("Stunned");
        }
    }
}
*/