using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallParryScript : MonoBehaviour
{
    [SerializeField]
    public GameObject stateDrivenCam;

    [SerializeField]
    public Camera inCam;

    private InputManagerB inputManager;

    private CharManagerScript charManager;

    private Animator animator;

    [SerializeField]
    public GameObject[] prefabs;

    [SerializeField]
    public Collider winCon;

    [SerializeField]
    public bool loseCon;

    [SerializeField]
    public Vector3 spawnPointPos;

    [SerializeField]
    public Vector3 spawnPointRot;

    [SerializeField]
    internal float wallJumpForce;

    [SerializeField]
    internal float wallJumpLength;

    [SerializeField]
    internal float wallJumpHeight;

    [SerializeField]
    internal float wallFallingSpeed;

    [SerializeField]
    internal bool wallJumping = false;

    [SerializeField]
    internal bool wallJumpingTrigger = false;

    [SerializeField]
    internal bool wallParrying = false;

    [SerializeField]
    internal float wallParryTimer;

    [SerializeField]
    internal float wallParryTimerMax;

    [SerializeField]
    internal bool wallParryFreeze = false;

    [SerializeField]
    internal float wallParryFreezeTime;

    [SerializeField]
    internal float wallParryFreezeTimeMax;

    [SerializeField]
    internal bool wallHanging = false;

    [SerializeField]
    internal float wallHangingTimer;

    [SerializeField]
    internal float wallHangingTimerMax;

    [SerializeField]
    internal bool wallJumpCharging = false;

    [SerializeField]
    internal float wallJumpChargeTimer;

    [SerializeField]
    internal float wallJumpChargeTimerMax;

    [SerializeField]
    internal float jumpForce;

    [SerializeField]
    internal bool chargingJump = false;

    [SerializeField]
    internal bool jumping;

    [SerializeField]
    internal float chargeTimer;

    [SerializeField]
    internal float chargeTimerMax;

    [SerializeField]
    internal float savedForce;

    [SerializeField]
    internal Vector2 savedForceVector;

    [SerializeField]
    internal bool falling = false;

    [SerializeField]
    internal float fallingTimer;

    [SerializeField]
    internal float fallingTimerMax;

    //Last Jump Data

    [SerializeField]
    internal Vector3 contactPoint;

    [SerializeField]
    internal Vector3 impulseVector;

    [SerializeField]
    internal Vector3 relativeSpeedVector;

    public float debugCheck;



    [SerializeField]
    internal float[] chargeJumpTimeThresholds;

    [SerializeField]
    internal float[] chargeJumpMultipliers;

    [SerializeField]
    internal float[] wallChargeTimeThresholds;

    [SerializeField]
    internal float[] wallChargeMultipliers;

    bool startSwitch = false;
    bool startSwitch2 = false;

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

        inputManager.OnStartTouchPrimary += Hold;
        inputManager.OnEndTouchPrimary += Release;
        inputManager.currentCam = inCam;
        if (!startSwitch)
        {
            startSwitch = true;
        }
        else
        {
            inputManager.DisableMainScreenFunctions();
        }

        charManager.TriggerWithPlayer += Win;
        charManager.CollideWithPlayer += WallHang;
    }

    private void OnDisable()
    {
        if (charManager.gameObject.activeSelf)
        {
            charManager.SetCharActive(false);
        }

        inputManager.OnStartTouchPrimary -= Hold;
        inputManager.OnEndTouchPrimary -= Release;
        inputManager.currentCam = inputManager.cam;
        if (!startSwitch2)
        {
            startSwitch2 = true;
        }
        else
        {
            inputManager.EnableMainScreenFunctions();
        }

        charManager.TriggerWithPlayer -= Win;
        charManager.CollideWithPlayer -= WallHang;
    }

    void Update()
    {
        /*
        if (wallHanging)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                wallJumpingTrigger = true;
                wallJumping = true;
            }
        }
        */

    }

    void FixedUpdate()
    {
        debugCheck = charManager.ySpeed;
        if(charManager.ySpeed <= -15f)
        {
            Debug.Log("InCondition");
            falling = true;
        }

        if(!falling)
        {
            if (chargingJump)
            {
                chargeTimer = chargeTimer - Time.fixedDeltaTime;

                if (chargeTimer <= 0f)
                {
                    chargingJump = false;
                }
            }

            if (wallHanging)
            {
                //charManager.mesh.materials[0] = charManager.testMats[0];
                wallHangingTimer = wallHangingTimer - Time.fixedDeltaTime;
                charManager.rb.velocity = Vector3.zero;

                if (wallHangingTimer <= 0f)
                {
                    wallHanging = false;
                    falling = true;
                    fallingTimer = fallingTimerMax;
                }
            }

            if(wallJumpCharging)
            {
                wallHanging = false;
                charManager.mesh.materials[0].color = Color.red;
                wallJumpChargeTimer = wallJumpChargeTimer - Time.fixedDeltaTime;
                charManager.rb.velocity = Vector3.zero;
                
                if(wallJumpChargeTimer <= 0f)
                {
                    Debug.Log("InWallJumpDead");
                    charManager.mesh.materials[0].color = Color.white;
                    wallJumpCharging = false;
                    falling = true;
                    fallingTimer = fallingTimerMax;
                }
            }

            if (wallParryFreeze)
            {
                wallParryFreezeTime = wallParryFreezeTime - Time.fixedDeltaTime;

                if (wallParryFreezeTime <= 0f)
                {
                    wallParryFreeze = false;
                }
            }
        }
        else if(falling)
        {
            fallingTimer = fallingTimer - Time.fixedDeltaTime;

            if(fallingTimer <=0f)
            {
                falling = false;
                jumping = false;
                Lose();
            }
        }

        

        /*
        if(chargingJump && charge >= 0f)
        {
            charge = charge - Time.fixedDeltaTime;
        }

        if(wallParrying && wallParryTimer >= 0f)
        {
            wallParryTimer = wallParryTimer - Time.fixedDeltaTime;
        }

        if(wallParrying && wallParryTimer <= 0f)
        {
            wallParrying = false;
            wallParryFreezeTime = wallParryFreezeTimeMax;
        }

        if (wallParryFreezeTime >= 0f)
        {
            wallParryFreezeTime = wallParryFreezeTime - Time.fixedDeltaTime;
        }
        */

        /*
        if (wallJumpingTrigger)
        {
            Debug.Log(new Vector3(charManager.collisionScript.impulseVector.x * wallJumpLength, wallJumpHeight, 0f).normalized);

            charManager.rb.velocity = new Vector3(charManager.collisionScript.impulseVector.x * wallJumpLength, wallJumpHeight, 0f).normalized * wallJumpForce;
            wallJumpingTrigger = false;
        }
        */
    }

    public void Hold(Vector2 position, float time)
    {
        if(!wallHanging&&!jumping)
        {
            charManager.effectScript.BlueCircle(charManager.transform.position, false);
            chargingJump = true;
            chargeTimer = chargeTimerMax;
        }

        if (!wallHanging && jumping)
        {
            wallParryFreeze = true;
            wallParryFreezeTime = wallParryFreezeTimeMax;
        }

        if (wallHanging)
        {
            wallJumpChargeTimer = wallJumpChargeTimerMax;
            wallJumpCharging = true;
        }
    }

    public void Release(Vector2 position, float time)
    {
        if (chargingJump)
        {
            Vector2 newPos = Utils.ScreenToWorld(inputManager.currentCam, position);
            //Vector3 aimedDirection =  position - new Vector2(charManager.transform.position.x, charManager.transform.position.y);
            Vector2 aimedDirection = newPos - new Vector2(charManager.transform.position.x, charManager.transform.position.y);
            chargingJump = false;
            if (chargeTimer < chargeJumpTimeThresholds[0])
            {
                /*
                Debug.Log("Threshold: 0, time: " + charge);
                Debug.Log("CharPosition: " + new Vector2(charManager.transform.position.x, charManager.transform.position.y));
                Debug.Log("Target: " + newPos);
                Debug.Log("Normalized: " + aimedDirection.normalized);
                */
                //charManager.rb.AddForce(Vector3.up * jumpForce *multipliers[0]);
                charManager.rb.AddForce(aimedDirection.normalized * jumpForce * chargeJumpMultipliers[0]);
            }
            else if (chargeTimer < chargeJumpTimeThresholds[1])
            {
                charManager.rb.AddForce(aimedDirection.normalized * jumpForce * chargeJumpMultipliers[1]);
            }
            else if (chargeTimer < chargeJumpTimeThresholds[2])
            {
                charManager.rb.AddForce(aimedDirection.normalized * jumpForce * chargeJumpMultipliers[2]);
            }
            else if (chargeTimer < chargeJumpTimeThresholds[3])
            {
                charManager.rb.AddForce(aimedDirection.normalized * jumpForce * chargeJumpMultipliers[3]);
            }
            charManager.effectScript.BlueCircle(charManager.transform.position, true);
            chargeTimer = 0f;
            jumping = true;
        }
        else if (wallJumpCharging)
        {
            /*
            Vector2 newPos = Utils.ScreenToWorld(inputManager.currentCam, position);
            //Vector3 aimedDirection =  position - new Vector2(charManager.transform.position.x, charManager.transform.position.y);
            Vector2 aimedDirection = newPos - new Vector2(charManager.transform.position.x, charManager.transform.position.y);
            wallJumpCharging = false;
            */
            Vector3 aimedDirection = new Vector3(-impulseVector.x, -impulseVector.y, 0f) * relativeSpeedVector.magnitude;
            Debug.Log("aimedDirection: " + aimedDirection);
            Debug.Log("impulseVector.x: " + -impulseVector.x);
            Debug.Log("impulseVector.y: " + -impulseVector.y);

            charManager.mesh.materials[0].color = Color.white;
            if (wallJumpChargeTimer < chargeJumpTimeThresholds[0])
            {
                charManager.rb.AddForce(aimedDirection  * chargeJumpMultipliers[0]);
            }
            else if (wallJumpChargeTimer < chargeJumpTimeThresholds[1])
            {
                charManager.rb.AddForce(aimedDirection * chargeJumpMultipliers[1]);
            }
            else if (wallJumpChargeTimer < chargeJumpTimeThresholds[2])
            {
                charManager.rb.AddForce(aimedDirection * chargeJumpMultipliers[2]);
            }
            else if (wallJumpChargeTimer < chargeJumpTimeThresholds[3])
            {
                charManager.rb.AddForce(aimedDirection * chargeJumpMultipliers[3]);
            }

            Debug.Log("InReleaseEnd");
            wallJumpChargeTimer = 0f;
            jumping = true;

        }
    }

    public void WallParry()
    {
        wallParrying = true;
        wallParryTimer = wallParryTimerMax;
    }

    public void WallHang(Collision collision)
    {
        Debug.Log("Tag is: " + collision.collider.tag);
        
        if(!wallParryFreeze)
        {
            if (collision.collider.tag == "Wall")
            {
                /*
                if(wallParrying)
                {
                    wallHanging = true;
                }
                else if(wallParryFreeze)
                {
                    crashing = true;
                }
                else
                {
                    normalHanging = true;
                }
                */
                jumping = false;
                wallHanging = true;
                wallHangingTimer = wallHangingTimerMax;

                contactPoint = collision.GetContact(0).point;
                impulseVector = collision.impulse.normalized;
                relativeSpeedVector = collision.relativeVelocity;
            }
        }
        else if (collision.collider.tag == "Ground")
        {
            if(jumping)
            {
                jumping = false;
                Lose();
            }
        }
    }

    public void Win(Collider collider)
    {
        if (collider.gameObject.tag == "WinCon")
        {
            Debug.Log("Win!");
            gameObject.SetActive(false);
        }
    }

    public void Lose()
    {
        Debug.Log("Lose!");
        gameObject.SetActive(false);
    }
}


//La idea del salto principal es que T termine con una fuerza neta permanente que tiene que aprovechar para llegar a la cima antes de que se acabe. Mientras mejor el parry menos fuerza se pierde con cada rebote; nunca un parry es mas fuerte que el anterior o que el salto principal.
//Si bien un salto principal perfecto le da mas fuerza neta a T tambien vuelve mas rapido su movimiento y por ende mas dificil de optimizar.
//El angulo: T pierde al caer, y esto puede pasar de tres maneras:
//a) El parry se activa demasiado temprano y T se estampa contra la pared, perdiendo fuerza, y no reacciona lo suficientemente rapido a su propio blunder.
//b) T se queda sin fuerza antes de llegar a la cima debido a que el angulo ya no le alcanza para ganar altura.
//c) El jugador aprieta el boton de parry demasiado tarde y T choca contra la pared y se cae.