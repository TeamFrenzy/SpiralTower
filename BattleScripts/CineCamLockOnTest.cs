using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineCamLockOnTest : MonoBehaviour
{
    [Header("Framing")]
    [SerializeField] private Transform _playerCharacterHeadTransform = null;
    [SerializeField] private Transform _playerCharacterBodyTransform = null;

    /*
    [Header("Distance")]
    [SerializeField] private float _defaultDistance = 5f;
    [SerializeField] private float _minDistance = 0f;
    [SerializeField] private float _maxDistance = 10f;

    [Header("Rotation")]
    [SerializeField] private float _defaultVerticalAngle = 20f;
    [SerializeField] [Range(-90, 90)] private float _minVerticalAngle = -90;
    [SerializeField] [Range(-90, 90)] private float _maxVerticalAngle = 90;
    */

    [Header("Lock On")]
   // [SerializeField] private ITargetable _target;
    [SerializeField] private ITargetable _facingTarget;
    [SerializeField] private GameObject testLockOnTarget;
    [SerializeField] private GameObject testLockOnFacingTarget;

    /*
    private void OnValidate()
    {
        _defaultDistance = Mathf.Clamp(_defaultDistance, _minDistance, _maxDistance);
        _defaultVerticalAngle = Mathf.Clamp(_defaultVerticalAngle, _minVerticalAngle, _maxVerticalAngle);
    }
    */

    private void Start()
    {
      //  _target = testLockOnTarget.GetComponent<ITargetable>();
        _facingTarget = testLockOnFacingTarget.GetComponent<ITargetable>();
    }
    void Update()
    {
        Vector3 targetPosition = new Vector3(_facingTarget.TargetTransform.position.x, transform.position.y, _facingTarget.TargetTransform.position.z);
        _playerCharacterHeadTransform.LookAt(_facingTarget.TargetTransform);
        _playerCharacterBodyTransform.LookAt(targetPosition);
    }
}


/*
 * using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineCamLockOnTest : MonoBehaviour
{
    [Header("Framing")]
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _camera = null;
    [SerializeField] private Transform _followTransform = null;
    [SerializeField] private Transform _playerCharacterTransform = null;
    [SerializeField] private Transform _playerCharacterHeadTransform = null;
    [SerializeField] Vector3 _framingNormal = Vector3.zero;

    [Header("Distance")]
    [SerializeField] private float _defaultDistance = 5f;
    [SerializeField] private float _minDistance = 0f;
    [SerializeField] private float _maxDistance = 10f;

    [Header("Rotation")]
    [SerializeField] private float _rotationSharpness = 25f;
    [SerializeField] private float _defaultVerticalAngle = 20f;
    [SerializeField] [Range(-90, 90)] private float _minVerticalAngle = -90;
    [SerializeField] [Range(-90, 90)] private float _maxVerticalAngle = 90;

    [Header("Obstructions")]
    [SerializeField] private float _checkRadius = 0.2f;
    [SerializeField] private LayerMask _obstructionLayers = -1;
    private List<Collider> _ignoreColliders = new List<Collider>();

    [Header("Lock On")]
    [SerializeField] private Vector3 _lockOnFraming = Vector3.zero;
    [SerializeField, Range(1, 179)] private float _lockOnFOV = 40;
    [SerializeField] private ITargetable _target;
    [SerializeField] private ITargetable _facingTarget;
    [SerializeField] private ITargetable banana;
    [SerializeField] private GameObject testLockOnTarget;
    [SerializeField] private GameObject testLockOnFacingTarget;

    [Header("FramingTest")]
    [SerializeField] private Vector3 _framing;
    [SerializeField] private Vector3 _focusPosition;
    [SerializeField] private float _fov;
    [SerializeField] private Vector3 _camToTarget;
    [SerializeField] private Vector3 _planarCamToTarget;
    [SerializeField] private Quaternion _lookRotation;


    public bool LockedOn { get => _lockedOn; }

    //public ITargetable Target { get => _target; }
    public Vector3 CameraPlanarDirection { get => _planarDirection; }

    //Privates
    [Header("Privates")]
    public float _fovNormal;
    public float _framingLerp;
    public Vector3 _planarDirection; //Cameras forward on the x,z plane
    public float _targetDistance;
    public Vector3 _targetPosition;
    public Quaternion _targetRotation;
    public float _targetVerticalAngle;

    public Vector3 _newPosition;
    public Quaternion _newRotation;

    private bool _lockedOn;
    //private ITargetable _target;

    private void OnValidate()
    {
        _defaultDistance = Mathf.Clamp(_defaultDistance, _minDistance, _maxDistance);
        _defaultVerticalAngle = Mathf.Clamp(_defaultVerticalAngle, _minVerticalAngle, _maxVerticalAngle);
    }

    private void Start()
    {
        _target = testLockOnTarget.GetComponent<ITargetable>();
        _facingTarget = testLockOnFacingTarget.GetComponent<ITargetable>();
        /*
        //Ignore the player colliders
        _ignoreColliders.AddRange(GetComponentsInChildren<Collider>());

        //important
        _fovNormal = _camera.m_Lens.FieldOfView;
        _planarDirection = _followTransform.forward;

        //Calculate Targets
        _targetDistance = _defaultDistance;
        _targetVerticalAngle = _defaultVerticalAngle;
        _targetRotation = Quaternion.LookRotation(_planarDirection) * Quaternion.Euler(_targetVerticalAngle, 0, 0);
        _targetPosition = _followTransform.position = (_targetRotation * Vector3.forward) * _targetDistance;s
    }


    // Update is called once per frame
    void Update()
{
    // _newRotation = Quaternion.Slerp(_camera.transform.rotation, _targetRotation, Time.deltaTime * _rotationSharpness);
    _followTransform.LookAt(_target.TargetTransform);
    //_playerCharacterTransform.LookAt(_target.TargetTransform);
    _playerCharacterHeadTransform.LookAt(_facingTarget.TargetTransform);
    /*
    //Framing + Fov
    _framing = Vector3.Lerp(_framingNormal, _lockOnFraming, _framingLerp);
    _focusPosition = _followTransform.position + _followTransform.TransformDirection(_framing);
    _fov = Mathf.Lerp(_fovNormal, _lockOnFOV, _framingLerp);
    _camera.m_Lens.FieldOfView = _fov;
    _camToTarget = _target.TargetTransform.position - _camera.transform.position;
    _planarCamToTarget = Vector3.ProjectOnPlane(_camToTarget, Vector3.up);
    _lookRotation = Quaternion.LookRotation(_camToTarget, Vector3.up);

    _framingLerp = Mathf.Clamp01(_framingLerp + Time.deltaTime * 4);

    _planarDirection = _planarCamToTarget != Vector3.zero ? _planarCamToTarget.normalized : _planarDirection;
    // _targetDistance = Mathf.Clamp(_targetDistance + _zoom, _minDistance, _maxDistance);
    //_targetVerticalAngle = Mathf.Clamp(_lookRotation.eulerAngles.x, _minVerticalAngle, _maxVerticalAngle);
    _targetVerticalAngle = _lookRotation.eulerAngles.x;


    //Final targets
    _targetRotation = Quaternion.LookRotation(_planarDirection) * Quaternion.Euler(_targetVerticalAngle, 0, 0);

    //Handle Smoothing
    _newRotation = Quaternion.Slerp(_camera.transform.rotation, _targetRotation, Time.deltaTime * _rotationSharpness);

    //Apply
   // _camera.transform.rotation = _newRotation;
}
    
}
*/