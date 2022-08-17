using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LobbyManager : Singleton<LobbyManager>
{
    [SerializeField]
    private Camera cam;
    public float camSpeed;

    [SerializeField]
    private Transform mainLobbyT;
    [SerializeField]
    private Transform screenOverviewT;
    [SerializeField]
    private Transform middleScreenT;
    [SerializeField]
    private Transform leftScreenT;
    [SerializeField]
    private Transform rightScreenT;
    [SerializeField]
    private Transform towerViewT;

    private InputManagerB inputManager;

    private Vector3 previousPosition;

    private Coroutine coroutine;

    //La idea seria usar un enum pero bue
    public bool isInMainLobby = true;
    public bool isInScreenOverview = false;
    public bool isInMiddleScreen = false;
    public bool isInLeftScreen = false;
    public bool isInRightScreen = false;
    public bool isInTowerView = false;

    public bool isInTransition = false;

    public AudioSource bgMusicPlayer;
    public AudioClip bgMusic;

    public AudioSource bgRainPlayer;
    public AudioClip bgRain;

    // Start is called before the first frame update
    private void Awake()
    {
        inputManager = InputManagerB.Instance;
    }

    private void Start()
    {
        inputManager.DisableMainScreenFunctions();
        bgMusicPlayer.PlayOneShot(bgMusic);
        bgRainPlayer.PlayOneShot(bgRain);
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
        inputManager.OnStartTouchPrimary += LobbySwipeStart;
        inputManager.OnEndTouchPrimary += LobbySwipeEnd;
    }

    public void DisableFunctions()
    {
        inputManager.OnStartTouchPrimary -= LobbySwipeStart;
        inputManager.OnEndTouchPrimary -= LobbySwipeEnd;
    }

    public void LobbySwipeStart(Vector2 position, float time)
    {
        previousPosition = cam.ScreenToViewportPoint(position);
        Debug.Log("Swipe Started");
    }

    public void LobbySwipeEnd(Vector2 position, float time)
    {
        Vector3 direction = previousPosition - cam.ScreenToViewportPoint(inputManager.PrimaryPosition());
        int result = 0;

        Debug.Log("Crude Direction: " + direction);

        if(!inputManager.isScrolling)
        {
            if (direction.y < -0.2f)
            {
                //Down
                result = 1;
            }
            else if (direction.y > 0.2f)
            {
                //Up
                result = 2;
            }
            else if (direction.x > 0.2f)
            {
                //Right
                result = 3;
            }
            else if (direction.x < -0.2f)
            {
                //Left
                result = 4;
            }
            SetDirection(result);
        }
    }

    public void SetDirection(int result)
    {
        Debug.Log("Direction is " + result);
        if(isInMainLobby)
        {
            if(result == 2)
            {
                StartCoroutine(FocusCamera(2));
            }
            else if(result == 4)
            {
                StartCoroutine(FocusCamera(4));
            }
        }
        else if(isInScreenOverview)
        {
            if (result == 1)
            {
                StartCoroutine(FocusCamera(1));
            }
            else if (result == 2)
            {
                StartCoroutine(FocusCamera(2));
            }
            else if (result == 3)
            {
                StartCoroutine(FocusCamera(3));
            }
            else if (result == 4)
            {
                StartCoroutine(FocusCamera(4));
            }
        }
        else if(isInLeftScreen)
        {
            if (result == 1)
            {
                StartCoroutine(FocusCamera(1));
            }
        }
        else if (isInMiddleScreen)
        {
            if (result == 1)
            {
                StartCoroutine(FocusCamera(1));
            }
        }
        else if (isInRightScreen)
        {
            if (result == 1)
            {
                StartCoroutine(FocusCamera(1));
            }
        }
    }

    public IEnumerator FocusCamera(int location)
    {
        Debug.Log("Method Start");
        if (location == 1)
        {
            if (isInScreenOverview)
            {
                isInScreenOverview = false;
                isInTransition = true;
                while (cam.transform.position != mainLobbyT.position)
                {
                    Vector3 tempPos = Vector3.MoveTowards(cam.transform.position, mainLobbyT.position, Time.deltaTime * 500f * camSpeed);
                    Quaternion tempRot = Quaternion.RotateTowards(cam.transform.rotation, mainLobbyT.rotation, 25f * Time.deltaTime);

                    cam.transform.SetPositionAndRotation(tempPos, tempRot);
                    yield return null;
                }
                isInMainLobby = true;
                isInTransition = false;
                yield return null;
            }
            else if (isInLeftScreen)
            {
                isInLeftScreen = false;
                isInTransition = true;
                while (cam.transform.position != screenOverviewT.position)
                {
                    Vector3 tempPos = Vector3.MoveTowards(cam.transform.position, screenOverviewT.position, Time.deltaTime * 100f * camSpeed);
                    Quaternion tempRot = Quaternion.RotateTowards(cam.transform.rotation, screenOverviewT.rotation, 60f * Time.deltaTime);

                    cam.transform.SetPositionAndRotation(tempPos, tempRot);
                    yield return null;
                }
                inputManager.DisableSelectFunctions();
                isInScreenOverview = true;
                isInTransition = false;
                yield return null;
            }
            else if (isInMiddleScreen)
            {
                isInMiddleScreen = false;
                isInTransition = true;
                while (cam.transform.position != screenOverviewT.position)
                {
                    Vector3 tempPos = Vector3.MoveTowards(cam.transform.position, screenOverviewT.position, Time.deltaTime * 100f * camSpeed);
                    Quaternion tempRot = Quaternion.RotateTowards(cam.transform.rotation, screenOverviewT.rotation, 15f * Time.deltaTime);

                    cam.transform.SetPositionAndRotation(tempPos, tempRot);
                    yield return null;
                }
                inputManager.DisableSelectFunctions();
                isInScreenOverview = true;
                isInTransition = false;
                yield return null;
            }
            else if (isInRightScreen)
            {
                isInRightScreen = false;
                isInTransition = true;
                while (cam.transform.position != screenOverviewT.position)
                {
                    Vector3 tempPos = Vector3.MoveTowards(cam.transform.position, screenOverviewT.position, Time.deltaTime * 100f * camSpeed);
                    Quaternion tempRot = Quaternion.RotateTowards(cam.transform.rotation, screenOverviewT.rotation, 60f * Time.deltaTime);

                    cam.transform.SetPositionAndRotation(tempPos, tempRot);
                    yield return null;
                }
                inputManager.DisableSelectFunctions();
                isInScreenOverview = true;
                isInTransition = false;
                yield return null;
            }

            yield return null;
        }
        else if (location == 2)
        {
            if (isInScreenOverview)
            {
                isInScreenOverview = false;
                isInTransition = true;
                while (cam.transform.position != middleScreenT.position)
                {
                    Vector3 tempPos = Vector3.MoveTowards(cam.transform.position, middleScreenT.position, Time.deltaTime * 100f * camSpeed);
                    Quaternion tempRot = Quaternion.RotateTowards(cam.transform.rotation, middleScreenT.rotation, 15f * Time.deltaTime);

                    cam.transform.SetPositionAndRotation(tempPos, tempRot);
                    yield return null;
                }
                isInMiddleScreen = true;
                inputManager.EnableSelectFunctions();
                isInTransition = false;
                yield return null;
            }
            else if (isInMainLobby)
            {
                isInMainLobby = false;
                isInTransition = true;
                while (cam.transform.position != towerViewT.position)
                {
                    Vector3 tempPos = Vector3.MoveTowards(cam.transform.position, towerViewT.position, Time.deltaTime * 200f * camSpeed);
                    Quaternion tempRot = Quaternion.RotateTowards(cam.transform.rotation, towerViewT.rotation, 15f * Time.deltaTime);

                    cam.transform.SetPositionAndRotation(tempPos, tempRot);
                    yield return null;
                }
                isInTowerView = true;
                isInTransition = false;
                inputManager.EnableMainScreenFunctions();
                yield return null;
            }
            yield return null;
        }
        else if (location == 3)
        {
            if (isInScreenOverview)
            {
                isInScreenOverview = false;
                isInTransition = true;
                while (cam.transform.position != rightScreenT.position)
                {
                    Vector3 tempPos = Vector3.MoveTowards(cam.transform.position, rightScreenT.position, Time.deltaTime * 100f * camSpeed);
                    Quaternion tempRot = Quaternion.RotateTowards(cam.transform.rotation, rightScreenT.rotation, 60f * Time.deltaTime);

                    cam.transform.SetPositionAndRotation(tempPos, tempRot);
                    yield return null;
                }
                inputManager.EnableSelectFunctions();
                isInRightScreen = true;
                isInTransition = false;
                yield return null;
            }
            yield return null;
        }
        else if (location == 4)
        {
            if (isInMainLobby)
            {
                isInMainLobby = false;
                isInTransition = true;
                while (cam.transform.position != screenOverviewT.position)
                {
                    Vector3 tempPos = Vector3.MoveTowards(cam.transform.position, screenOverviewT.position, Time.deltaTime * 500f * camSpeed);
                    Quaternion tempRot = Quaternion.RotateTowards(cam.transform.rotation, screenOverviewT.rotation, 25f * Time.deltaTime);

                    cam.transform.SetPositionAndRotation(tempPos, tempRot);
                    yield return null;
                }
                isInScreenOverview = true;
                isInTransition = false;
                yield return null;
            }
            else if (isInScreenOverview)
            {
                isInScreenOverview = false;
                isInTransition = true;
                while (cam.transform.position != leftScreenT.position)
                {
                    Vector3 tempPos = Vector3.MoveTowards(cam.transform.position, leftScreenT.position, Time.deltaTime * 100f * camSpeed);
                    Quaternion tempRot = Quaternion.RotateTowards(cam.transform.rotation, leftScreenT.rotation, 60f * Time.deltaTime);

                    cam.transform.SetPositionAndRotation(tempPos, tempRot);
                    yield return null;
                }
                isInLeftScreen = true;
                inputManager.EnableSelectFunctions();
                isInTransition = false;
                yield return null;
            }
            yield return null;
        }
        else if (location == 5)
        {
            Debug.Log("In Transition 2");
            if (isInTowerView)
            {
                Debug.Log("In Transition 1");
                isInTowerView = false;
                isInTransition = true;
                while (cam.transform.position != mainLobbyT.position)
                {
                    Vector3 tempPos = Vector3.MoveTowards(cam.transform.position, mainLobbyT.position, Time.deltaTime * 100f * camSpeed);
                    Quaternion tempRot = Quaternion.RotateTowards(cam.transform.rotation, mainLobbyT.rotation, 60f * Time.deltaTime);

                    cam.transform.SetPositionAndRotation(tempPos, tempRot);
                    yield return null;
                }
                inputManager.DisableSelectFunctions();
                inputManager.DisableMainScreenFunctions();
                isInMainLobby = true;
                isInTransition = false;
                yield return null;
            }
        }
        yield return null;
    }
}
