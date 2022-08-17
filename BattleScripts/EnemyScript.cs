using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private string[] attacks;
    [SerializeField] private Animator animator;

    private TestInput testInputActions;

    private void Awake()
    {
        testInputActions = new TestInput();
    }

    private void OnEnable()
    {
        testInputActions.Enable();
      //  testInputActions.TestPlayer.XUp.started += TestAttack;
    }

    private void OnDisable()
    {
        testInputActions.Disable();
       // testInputActions.TestPlayer.XUp.started -= TestAttack;
    }

    /*
    void TestAttack(InputAction.CallbackContext obj)
    {
        Debug.Log("Attacking");
        animator.Play("Spear_AttackFromRun");
    }
    */

    private void Update()
    {
        transform.LookAt(target);
    }
}
