using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownManager : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera cineCam;

    [SerializeField]
    private Transform towerTransform;

    private Vector3 previousPosition;

    private InputManagerB inputManager;

    private Coroutine coroutine;

    public float moveSpeed;

    public float upThreshold;
    public float downThreshold;

    private void Awake()
    {
        inputManager = InputManagerB.Instance;
    }

    private void OnEnable()
    {
        EnableFunctions();
    }

    private void OnDisable()
    {
        DisableFunctions();
    }

    public void EnableFunctions()
    {
        inputManager.OnStartTouchPrimary += SwipeUpDownStart;
        inputManager.OnEndTouchPrimary += SwipeUpDownEnd;
    }

    public void DisableFunctions()
    {
        inputManager.OnStartTouchPrimary -= SwipeUpDownStart;
        inputManager.OnEndTouchPrimary -= SwipeUpDownEnd;
    }

    public void SwipeUpDownStart(Vector2 position, float time)
    {
        previousPosition = cam.ScreenToViewportPoint(position);
        Debug.Log("Coroutine Started");

        if (!inputManager.pinchZooming && !inputManager.focusing && !inputManager.unfocusing)
        {
            coroutine = StartCoroutine(UpDownTrail());
        }
    }

    private IEnumerator UpDownTrail()
    {

        while (true)
        {
            if (!inputManager.pinchZooming && !inputManager.focusing && !inputManager.unfocusing)
            {
                Vector3 direction = previousPosition - cam.ScreenToViewportPoint(inputManager.PrimaryPosition());

                // cam.transform.position = new Vector3(0, 10, 0);

                if ((direction.y < 0) && (towerTransform.transform.position.y<upThreshold))
                {
                    //cam.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
                    //cineCam.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * moveSpeed);
                    towerTransform.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * moveSpeed);
                }
                else if ((direction.y > 0) && (towerTransform.transform.position.y > downThreshold))
                {
                    // cam.transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
                    //cineCam.transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * moveSpeed);
                    towerTransform.transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * moveSpeed);
                }

                //  cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
                // cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
                //cam.transform.Rotate(0f, -direction.x *rotationSpeed * Time.deltaTime, 0f);

                // cam.transform.Translate(new Vector3(0, 0, inputManager.zDistance));

                previousPosition = cam.ScreenToViewportPoint(inputManager.PrimaryPosition());
            }
            yield return null;

        }
    }

    public void SwipeUpDownEnd(Vector2 position, float time)
    {
        StopCoroutine(coroutine);
    }
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownManager : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera cineCam;

    [SerializeField]
    private Transform towerTransform;

    private Vector3 previousPosition;

    private InputManagerB inputManager;

    private Coroutine coroutine;

    public float moveSpeed;

    public float upThreshold;
    public float downThreshold;

    private void Awake()
    {
        inputManager = InputManagerB.Instance;
    }

    private void OnEnable()
    {
        EnableFunctions();
    }

    private void OnDisable()
    {
        DisableFunctions();
    }

    public void EnableFunctions()
    {
        inputManager.OnStartTouchPrimary += SwipeUpDownStart;
        inputManager.OnEndTouchPrimary += SwipeUpDownEnd;
    }

    public void DisableFunctions()
    {
        inputManager.OnStartTouchPrimary -= SwipeUpDownStart;
        inputManager.OnEndTouchPrimary -= SwipeUpDownEnd;
    }

    public void SwipeUpDownStart(Vector2 position, float time)
    {
        previousPosition = cam.ScreenToViewportPoint(position);
        Debug.Log("Coroutine Started");

        if (!inputManager.pinchZooming && !inputManager.focusing && !inputManager.unfocusing)
        {
            coroutine = StartCoroutine(UpDownTrail());
        }
    }

    private IEnumerator UpDownTrail()
    {

        while (true)
        {
            if (!inputManager.pinchZooming && !inputManager.focusing && !inputManager.unfocusing)
            {
                Vector3 direction = previousPosition - cam.ScreenToViewportPoint(inputManager.PrimaryPosition());

                // cam.transform.position = new Vector3(0, 10, 0);

                if ((direction.y > 0) && (cam.transform.position.y < upThreshold))
                {
                    //cam.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
                    cineCam.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * moveSpeed);
                }
                else if ((direction.y < 0) && (cam.transform.position.y > downThreshold))
                {
                    // cam.transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
                    cineCam.transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * moveSpeed);
                }

                //  cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
                // cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
                //cam.transform.Rotate(0f, -direction.x *rotationSpeed * Time.deltaTime, 0f);

                // cam.transform.Translate(new Vector3(0, 0, inputManager.zDistance));

                previousPosition = cam.ScreenToViewportPoint(inputManager.PrimaryPosition());
            }
            yield return null;

        }
    }

    public void SwipeUpDownEnd(Vector2 position, float time)
    {
        StopCoroutine(coroutine);
    }
}
*/