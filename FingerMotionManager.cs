using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerMotionManager : MonoBehaviour
{
    [SerializeField]
    internal TowerManagerScript towerManagerScript;

    public Camera cam;

    private InputManager inputManager;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

}
