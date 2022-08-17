using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchManager : MonoBehaviour
{
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera cineCam;

    [SerializeField]
    private float cameraSpeed = 4f;

    private InputManagerB inputManager;

    private Coroutine zoomCoroutine;
    private Transform cameraTransform;

    public Vector2 primaryTouch;
    public Vector2 secondaryTouch;

    public float inThreshold;
    public float outThreshold;

    private void Awake()
    {
        inputManager = InputManagerB.Instance;

       // controls = new InputActionsB();

        cameraTransform = Camera.main.transform;
        primaryTouch = Vector2.zero;
        secondaryTouch = Vector2.zero;
    }

    private void OnEnable()
    {
        //controls.Enable();
        EnableFunctions();
    }

    private void OnDisable()
    {
        //controls.Disable();
        DisableFunctions();
    }

    public void EnableFunctions()
    {
        inputManager.OnStartTouchSecondary += ZoomStart;
        inputManager.OnEndTouchSecondary += ZoomEnd;
    }

    public void DisableFunctions()
    {
        inputManager.OnStartTouchSecondary -= ZoomStart;
        inputManager.OnEndTouchSecondary -= ZoomEnd;

    }


    /*
    private void Start()
    {
        controls.Touch.SecondaryContact.started += _ => ZoomStart();
        controls.Touch.SecondaryContact.canceled += _ => ZoomEnd();
    }
    */



    /*
    private void Update()
    {
        primaryTouch = inputManager.PrimaryPosition();
        secondaryTouch = inputManager.SecondaryPosition();
    }
    */



    public void ZoomStart(Vector2 position, float time)
    {
        inputManager.pinchZooming = true;

        Debug.Log("Routine Started");
        zoomCoroutine = StartCoroutine(ZoomDetection());
    }
    
    /*
    private void ZoomStart()
    {
        zoomCoroutine = StartCoroutine(ZoomDetection());
    }
    */

    
    public void ZoomEnd(Vector2 position, float time)
    {
        Debug.Log("Routine Ending");
        StopCoroutine(zoomCoroutine);
        Debug.Log("Routine Ended");
        inputManager.zDistance = cameraTransform.position.z;
        inputManager.pinchZooming = false;
    }
    
    /*
    private void ZoomEnd()
    {
        StopCoroutine(zoomCoroutine);
    }
    */

    IEnumerator ZoomDetection()
    {
        float previousDistance = 0f, distance = 0f;
        while (true)
        {
            if (!inputManager.focusing && !inputManager.unfocusing)
            {
                //distance = Vector2.Distance(controls.Touch.PrimaryPosition.ReadValue<Vector2>(), controls.Touch.SecondaryPosition.ReadValue<Vector2>());


                distance = Vector2.Distance(inputManager.PrimaryPosition(), inputManager.SecondaryPosition());
                //Detection
                //Zoom Out
                if ((distance < previousDistance) && (cineCam.transform.position.z > outThreshold))
                {
                    Debug.Log("Zoom Out!: " + distance);

                    Vector3 targetPosition = cineCam.transform.position;
                    //inputManager.zDistance -= 1;
                    targetPosition.z -= 1;
                    //targetPosition.z = inputManager.zDistance;
                    cineCam.transform.position = Vector3.Slerp(cineCam.transform.position, targetPosition, Time.deltaTime * cameraSpeed);
                }
                //Zoom In
                else if ((distance > previousDistance) && (cineCam.transform.position.z < inThreshold))
                {
                    Debug.Log("Zoom In!: " + distance);
                    Vector3 targetPosition = cineCam.transform.position;
                    //inputManager.zDistance += 1;
                    targetPosition.z += 1;
                    //targetPosition.z = inputManager.zDistance;
                    cineCam.transform.position = Vector3.Slerp(cineCam.transform.position, targetPosition, Time.deltaTime * cameraSpeed);
                }
                //Keep track of previous distance for next loop
                previousDistance = distance;
                yield return null;
            }
        }
    }
}
