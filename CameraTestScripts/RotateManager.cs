using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateManager : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 previousPosition;

    private InputManagerB inputManager;

    private Coroutine coroutine;

    public float rotationSpeed;

    public GameObject targetObject;

    private void Awake()
    {
        //currentEulerAngles = cam.transform.eulerAngles;
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
        inputManager.OnStartTouchPrimary += SwipeStart;
        inputManager.OnEndTouchPrimary += SwipeEnd;
    }

    public void DisableFunctions()
    {
        inputManager.OnStartTouchPrimary -= SwipeStart;
        inputManager.OnEndTouchPrimary -= SwipeEnd;

    }

    public void SwipeStart(Vector2 position, float time)
    {
        previousPosition = cam.ScreenToViewportPoint(position);
        coroutine = StartCoroutine(Trail());
    }

    private IEnumerator Trail()
    {
        
        while (true)
        {
            if (!inputManager.pinchZooming && !inputManager.focusing && !inputManager.unfocusing)
            {
                Vector3 direction = previousPosition - cam.ScreenToViewportPoint(inputManager.PrimaryPosition());

                // cam.transform.position = new Vector3(0, 10, 0);

                if (direction.x > 0)
                {
                    //cam.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
                    targetObject.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
                }
                else if (direction.x < 0)
                {
                    // cam.transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
                    targetObject.transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
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

    public void SwipeEnd(Vector2 position, float time)
    {
       StopCoroutine(coroutine);
    }
}

/*
 * 
 * 

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(0))
        {
            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

            cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
            cam.transform.Translate(new Vector3(0, 0, -1));

            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);

        }
    }
*/