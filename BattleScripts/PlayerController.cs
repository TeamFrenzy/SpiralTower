using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private TestInput testInputActions;

    private void Awake()
    {
        testInputActions = new TestInput();
    }

    private void OnEnable()
    {
        testInputActions.Enable();
        testInputActions.TestPlayer.XUp.started += MoveXUp;
        testInputActions.TestPlayer.XDown.started += MoveXDown;
        testInputActions.TestPlayer.ZUp.started += MoveZUp;
        testInputActions.TestPlayer.ZDown.started += MoveZDown;
    }

    private void OnDisable()
    {
        testInputActions.Disable();
        testInputActions.TestPlayer.XUp.started -= MoveXUp;
        testInputActions.TestPlayer.XDown.started -= MoveXDown;
        testInputActions.TestPlayer.ZUp.started -= MoveZUp;
        testInputActions.TestPlayer.ZDown.started -= MoveZDown;
    }

    void MoveXUp(InputAction.CallbackContext obj)
    {
        Debug.Log("Moving X Up");
        transform.position = transform.position + new Vector3(1f, 0, 0);

    }
    void MoveXDown(InputAction.CallbackContext obj)
    {
        Debug.Log("Moving X Down");
        transform.position = transform.position + new Vector3(-1f, 0, 0);


    }
    void MoveZUp(InputAction.CallbackContext obj)
    {
        Debug.Log("Moving Z Up");
        transform.position = transform.position + new Vector3(0, 0, 1f);

    }
    void MoveZDown(InputAction.CallbackContext obj)
    {
        Debug.Log("Moving Z Down");
        transform.position = transform.position + new Vector3(0, 0, -1f);

    }
}
