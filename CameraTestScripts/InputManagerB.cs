using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[DefaultExecutionOrder(-1)]
public class InputManagerB : Singleton<InputManagerB>
{
    [SerializeField]
    internal TowerManagerScript towerManagerScript;
    [SerializeField]
    internal SelectManager selectManagerScript;
    [SerializeField]
    internal PinchManager pinchManagerScript;
    [SerializeField]
    internal RotateManager rotateManagerScript;
    [SerializeField]
    internal UpDownManager upDownManagerScript;

    public GameObject testPrefab;

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

    [SerializeField]
    public Camera currentCam;

    [SerializeField]
    public Cinemachine.CinemachineVirtualCamera cineCam;

    [SerializeField]
    public Cinemachine.CinemachineVirtualCamera cineCamSecondary;

    public GameObject towerObject;

    public float zDistance = -10f;
    public bool pinchZooming = false;
    public bool rotating = false;
    public bool highlighting = false;
    public bool focusing = false;
    public bool unfocusing = false;

    public bool startSwitch = false;

    public float fadeInSpeed;
    public float fadeOutSpeed;

    bool fadingIn;

    bool fadingOut;

    [SerializeField]
    private LobbyManager lobbyManager;

    public bool isScrolling = false;

    private void Awake()
    {

        lobbyManager = LobbyManager.Instance;
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
                selectManagerScript.selectedRoom = 9999;
                selectManagerScript.selectedSection = 9999;
                highlighting = false;

                StartTouchPrimaryMethod(ctx);
            }
        };
        //SlowTapInteraction
        playerControls.Touch.PrimaryTap.performed += ctx =>
        {
            if (ctx.interaction is SlowTapInteraction)
            {
                EndTouchPrimaryMethod(ctx);
            }
            else
            {
                TapPrimaryMethod(ctx);
            }
        };

        playerControls.Touch.PrimaryTap.canceled += ctx =>
        {
            if (ctx.interaction is SlowTapInteraction)
            {
                EndTouchPrimaryMethod(ctx);
            }
        };

        playerControls.Touch.SecondaryContact.started += ctx =>
        {
            selectManagerScript.selectedRoom = 9999;
            selectManagerScript.selectedSection = 9999;
            highlighting = false;

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

    public void DisableMainScreenFunctions()
    {
        Debug.Log("Disable All");
        rotateManagerScript.DisableFunctions();
        pinchManagerScript.DisableFunctions();
        selectManagerScript.DisableFunctions();
        upDownManagerScript.DisableFunctions();
    }

    public void EnableMainScreenFunctions()
    {
        Debug.Log("Enable All");
        rotateManagerScript.EnableFunctions();
        pinchManagerScript.EnableFunctions();
        selectManagerScript.EnableFunctions();
        upDownManagerScript.EnableFunctions();
    }

    public void DisableSelectFunctions()
    {
        Debug.Log("Select Disabled");
        selectManagerScript.DisableFunctions();
    }

    public void EnableSelectFunctions()
    {
        Debug.Log("Select Enabled");
        selectManagerScript.EnableFunctions();
    }
    private void Update()
    {
        if (highlighting)
        {
            if (transform.rotation.eulerAngles.y != towerManagerScript.sectorDictionary[selectManagerScript.selectedSection].showCasingAngle)
            {
                /*
                    Vector3 temp = Vector3.MoveTowards(towerObject.transform.rotation.eulerAngles, new Vector3(towerObject.transform.rotation.eulerAngles.x, towerManagerScript.sectorDictionary[selectManagerScript.selectedSection].showCasingAngle, towerObject.transform.rotation.eulerAngles.z), Time.deltaTime * selectManagerScript.highlightZoomRotateSpeed);
                    towerObject.transform.rotation = Quaternion.Euler(temp);
                */
                float newAngle = Mathf.MoveTowardsAngle(towerObject.transform.rotation.eulerAngles.y, towerManagerScript.sectorDictionary[selectManagerScript.selectedSection].showCasingAngle, Time.deltaTime * selectManagerScript.highlightZoomRotateSpeed);
                Vector3 newRotation = new Vector3(towerObject.transform.rotation.eulerAngles.x, newAngle, towerObject.transform.rotation.eulerAngles.z);
                towerObject.transform.rotation = Quaternion.Euler(newRotation);
            }

            if (towerObject.transform.position.y != towerManagerScript.sectorDictionary[selectManagerScript.selectedSection].showCasingHeight)
            {
                /*
                towerObject.transform.position = Vector3.MoveTowards(cineCam.transform.position, new Vector3(cineCam.transform.position.x, towerManagerScript.sectorDictionary[selectManagerScript.selectedSection].showCasingHeight, cineCam.transform.position.z), Time.deltaTime * selectManagerScript.highlightZoomHeightSpeed);
                */

                float newHeight = Mathf.MoveTowards(towerObject.transform.position.y, towerManagerScript.sectorDictionary[selectManagerScript.selectedSection].showCasingHeight, Time.deltaTime * selectManagerScript.highlightZoomHeightSpeed);
                Vector3 newPosition = new Vector3(towerObject.transform.position.x, newHeight, towerObject.transform.position.z);
                towerObject.transform.position = newPosition;


            }
            if ((towerObject.transform.rotation.eulerAngles.y == towerManagerScript.sectorDictionary[selectManagerScript.selectedSection].showCasingAngle) && (cam.transform.position.y == towerManagerScript.sectorDictionary[selectManagerScript.selectedSection].showCasingHeight))
            {
                Debug.Log("Fits");
                highlighting = false;
            }
        }

        if (focusing)
        {
            fadingIn = false;
            fadingOut = true;
            cineCam.transform.position = Vector3.MoveTowards(cineCam.transform.position, new Vector3(cineCam.transform.position.x, cineCam.transform.position.y, towerManagerScript.sectorDictionary[selectManagerScript.selectedSection].zoomSize), Time.deltaTime * selectManagerScript.zoomSpeed);
        }

        if (unfocusing)
        {
            fadingIn = true;
            fadingOut = false;

            focusing = false;
            cineCam.transform.position = Vector3.MoveTowards(cineCam.transform.position, new Vector3(cineCam.transform.position.x, cineCam.transform.position.y, selectManagerScript.cameraSavedDistance), Time.deltaTime * selectManagerScript.zoomSpeed);

            if (cineCam.transform.position.z == selectManagerScript.cameraSavedDistance)
            {
                unfocusing = false;
                Debug.Log("Unfitso");
            }
        }
    }
}