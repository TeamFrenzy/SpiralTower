using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTouch : MonoBehaviour
{

    private InputManager inputManager;
    private Camera cameraMain;

    private void Awake()
    {
        inputManager = InputManager.Instance;
        cameraMain = Camera.main;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += Move;
    }

    private void OnDisable()
    {
        inputManager.OnEndTouch -= Move;
    }

    public void Move(Vector2 screenPosition, float time)
    {
        Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, 40f);
        Debug.Log("screenCoordinates: " + screenCoordinates);
        Vector3 worldCoordinates = cameraMain.ScreenToWorldPoint(screenCoordinates);
        worldCoordinates.z = 40f;
        Debug.Log("worldCoordinates: " + worldCoordinates);
        transform.position = screenCoordinates;
    }
}

/*RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(touchControls.Touch.TouchPosition.ReadValue<Vector3>());
        Debug.Log("Log: " + touchControls.Touch.TouchPosition.ReadValue<Vector3>());
        Debug.DrawRay(touchControls.Touch.TouchPosition.ReadValue<Vector3>(), Vector3.forward, Color.green, 60, false);

        // && hit.collider.tag == draggingTag
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Touching!");
            roomNumber = int.Parse(hit.transform.gameObject.name);
        }
        */