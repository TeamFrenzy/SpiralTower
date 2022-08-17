using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-2)]
public class CharManagerScript : Singleton<CharManagerScript>
{
    public delegate void TriggerEventHandler(Collider collider);
    public event TriggerEventHandler TriggerWithPlayer;
    public delegate void CollisionEventHandler(Collision collision);
    public event CollisionEventHandler CollideWithPlayer;


    [SerializeField]
    internal JumpScript jumpScript;

    [SerializeField]
    internal CollisionScript collisionScript;

    [SerializeField]
    internal DebugScript debugScript;

    [SerializeField]
    internal EffectScript effectScript;

    [SerializeField]
    internal ParryScript parryScript;

    [SerializeField]
    internal HorizontalScript horizontalScript;

    [SerializeField]
    internal AntiGravScript antiGravScript;

    [SerializeField]
    internal BoostScript boostScript;

    [SerializeField]
    internal BalanceScript balanceScript;

    [SerializeField]
    internal BrakeScript brakeScript;

    [SerializeField]
    internal WallClimbScript wallClimbScript;
    
    [SerializeField]
    internal MovementAnimationScript movementAnimationScript;
    
    [SerializeField]
    internal FacingScript facingScript;

    [SerializeField]
    internal AccelerationStateScript accelerationStateScript;

    [SerializeField] Collider playerCollider;

    [SerializeField]
    internal CrouchScript crouchScript;

    [SerializeField]
    internal SlidingScript slidingScript;

    [SerializeField]
    public Rigidbody rb;

    [SerializeField]
    internal float xSpeed;

    [SerializeField]
    internal float ySpeed;

    [SerializeField]
    internal float inertia;

    [SerializeField]
    internal float vInertia;

    [SerializeField]
    internal float forceTolerance;

    [SerializeField]
    public SkinnedMeshRenderer mesh;

    [SerializeField]
    public Material[] testMats;


    //Status
    [SerializeField]
    internal bool wallHanging;
    [SerializeField]
    internal bool wallJumping;
    [SerializeField]
    internal bool jumpRising;
    [SerializeField]
    internal bool fullJumping;

    void Awake()
    {
        rb.freezeRotation = true;
        playerCollider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }



    void FixedUpdate()
    {
        xSpeed = rb.velocity.x;
        ySpeed = rb.velocity.y;
        inertia = Mathf.Abs(rb.velocity.x);
        vInertia = Mathf.Abs(rb.velocity.y);
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Trigger trigger!");
        if (TriggerWithPlayer != null) TriggerWithPlayer(collider);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Trigger!");
        if (CollideWithPlayer != null) CollideWithPlayer(collision);
    }

    public void SetCharActive(bool condition)
    {
        if(condition == true)
        {
            gameObject.SetActive(true);
        }
        if(condition == false)
        {
            gameObject.SetActive(false);
        }
    }
}
