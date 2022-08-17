using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    //3
    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;

    private Camera mainCamera;

   // public Touch activeTouch;

    private void Awake()
    {
        mainCamera = Camera.main;
        EnhancedTouchSupport.Enable();
    }

    /*
    private void Update()
    {
        if (Touch.activeFingers.Count == 1)
        {
            activeTouch = Touch.activeFingers[0].currentTouch;
            //Debug.Log($"Phase: {activeTouch.phase} | Position: {activeTouch.screenPosition}");
        }
    }
    */

    private void OnEnable()
    {
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += FingerUp;
    }

    private void OnDisable()
    {
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp -= FingerUp;
    }

    private void FingerDown(Finger finger)
    {
        //Debug.Log("It's Triggering in: " + finger.screenPosition);
        //
        Debug.Log("Primary In: " + PrimaryPosition());
        //if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(mainCamera, finger.screenPosition), (float)Touch.activeFingers[0].currentTouch.startTime);
        if (OnStartTouch != null) OnStartTouch(PrimaryPosition(), (float)Touch.activeFingers[0].currentTouch.startTime);
    }

    private void FingerUp(Finger finger)
    {
       // Debug.Log("It's not Triggering in: " + finger.screenPosition);
        Debug.Log("Primary Out: " + PrimaryPosition());
        //if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(mainCamera, finger.screenPosition), (float)Touch.activeFingers[0].currentTouch.time);
        if (OnEndTouch != null) OnEndTouch(PrimaryPosition(), (float)Touch.activeFingers[0].currentTouch.time);
    }
    
    public Vector2 PrimaryPosition()
    {
        //
        //Vector3 pos = new Vector3(UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0].screenPosition.x, UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0].screenPosition.y, cameraEventDistance);
        Vector3 pos = Touch.activeFingers[0].currentTouch.screenPosition;
        return Utils.ScreenToWorld(mainCamera, pos);
    }
}
