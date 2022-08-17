using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;

public class CTInputManager : Singleton<CTInputManager>
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

    private InputActionsB playerMenuControls;
    public GameObject zoneTest;

    public Camera cam;
    public float camSpeed;

    private void Awake()
    {
        playerMenuControls = new InputActionsB();
    }

    private void Start()
    {

        playerMenuControls.Touch.PrimaryTap.started += ctx =>
        {
            if (ctx.interaction is SlowTapInteraction)
            {
                StartTouchPrimaryMethod(ctx);
            }
        };
        //SlowTapInteraction
        playerMenuControls.Touch.PrimaryTap.performed += ctx =>
        {
            if (ctx.interaction is SlowTapInteraction)
            {
                EndTouchPrimaryMethod(ctx);
            }
            else
            {
                Debug.Log("InCTTapMethod");
                TapPrimaryMethod(ctx);
            }
        };

        playerMenuControls.Touch.PrimaryTap.canceled += ctx =>
        {
            if (ctx.interaction is SlowTapInteraction)
            {
                EndTouchPrimaryMethod(ctx);
            }
        };

        playerMenuControls.Touch.SecondaryContact.started += ctx =>
        {
            StartTouchSecondaryMethod(ctx);

        };
        playerMenuControls.Touch.SecondaryContact.canceled += ctx => EndTouchSecondaryMethod(ctx);
    }

    private void OnEnable()
    {
        playerMenuControls.Enable();
    }

    private void OnDisable()
    {
        playerMenuControls.Disable();
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
        return playerMenuControls.Touch.PrimaryPosition.ReadValue<Vector2>();
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
        return playerMenuControls.Touch.SecondaryPosition.ReadValue<Vector2>();
    }

    //Mecanismo de subdialogo: un dialogo alternativo que piensa el personaje mientras dice otra cosa.
    
}