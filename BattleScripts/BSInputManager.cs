using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[DefaultExecutionOrder(-1)]
public class BSInputManager : Singleton<BSInputManager>
{

    #region Events
    public delegate void StartTouchPrimary(Vector2 position, float time);
    public event StartTouchPrimary OnStartTouchPrimary;
    public delegate void EndTouchPrimary(Vector2 position, float time);
    public event EndTouchPrimary OnEndTouchPrimary;
    public delegate void TapPrimary(Vector2 position, float time);
    public event TapPrimary OnTapPrimary;
    public delegate void HoldPrimary(Vector2 position, float time);
    public event HoldPrimary OnHoldPrimary;
    public delegate void StartTouchSecondary(Vector2 position, float time);
    public event StartTouchSecondary OnStartTouchSecondary;
    public delegate void EndTouchSecondary(Vector2 position, float time);
    public event EndTouchSecondary OnEndTouchSecondary;
    #endregion

    private InputActionsB playerControls;

    [SerializeField]
    public Camera cam;

    private void Awake()
    {
        playerControls = new InputActionsB();
        cam = Camera.main;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        playerControls.Touch.PrimaryTap.started += ctx =>
        {
            if (ctx.interaction is SlowTapInteraction)
            {
                StartTouchPrimaryMethod(ctx);
            }
        };

        //SlowTapInteraction
        playerControls.Touch.PrimaryTap.performed += ctx =>
        {
            if (ctx.interaction is SlowTapInteraction)
            {
                EndTouchPrimaryMethod(ctx);
                // Debug.Log("FireHeld Performed");
            }
            else
            {
                TapPrimaryMethod(ctx);
                // Debug.Log("FirePerformed");
            }
        };

        playerControls.Touch.PrimaryTap.canceled += ctx =>
        {
            if (ctx.interaction is SlowTapInteraction)
            {
                EndTouchPrimaryMethod(ctx);
                //  Debug.Log("FireHeld Cancelled");
            }
        };

        playerControls.Touch.SecondaryContact.started += ctx =>
        {
            Debug.Log("Secondary Touch Started");
            StartTouchSecondaryMethod(ctx);
        };
        playerControls.Touch.SecondaryContact.canceled += ctx => EndTouchSecondaryMethod(ctx);
    }
    private void StartTouchPrimaryMethod(InputAction.CallbackContext context)
    {
        if (OnStartTouchPrimary != null) OnStartTouchPrimary(PrimaryPosition(), (float)context.startTime);
    }

    private void EndTouchPrimaryMethod(InputAction.CallbackContext context)
    {
        if (OnEndTouchPrimary != null) OnEndTouchPrimary(PrimaryPosition(), (float)context.time);
    }

    private void TapPrimaryMethod(InputAction.CallbackContext context)
    {
        if (OnTapPrimary != null) OnTapPrimary(PrimaryPosition(), (float)context.startTime);
    }

    public Vector2 PrimaryPosition()
    {
        return playerControls.Touch.PrimaryPosition.ReadValue<Vector2>();
    }

    private void StartTouchSecondaryMethod(InputAction.CallbackContext context)
    {
        if (OnStartTouchSecondary != null) OnStartTouchSecondary(SecondaryPosition(), (float)context.startTime);
    }

    private void EndTouchSecondaryMethod(InputAction.CallbackContext context)
    {
        if (OnEndTouchSecondary != null) OnEndTouchSecondary(SecondaryPosition(), (float)context.time);
    }

    public Vector2 SecondaryPosition()
    {
        return playerControls.Touch.SecondaryPosition.ReadValue<Vector2>();
    }
}
