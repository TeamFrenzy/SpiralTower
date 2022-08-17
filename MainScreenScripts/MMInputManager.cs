using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;

public class MMInputManager : Singleton<MMInputManager>
{
    #region Events
    public delegate void StartTouchPrimary(Vector2 position, float time);
    public event StartTouchPrimary OnStartTouchPrimary;
    public delegate void EndTouchPrimary(Vector2 position, float time);
    public event EndTouchPrimary OnEndTouchPrimary;
    public delegate void TapPrimary(Vector2 position, float time);
    public event TapPrimary OnTapPrimary;
    public delegate void HoldPrimary(Vector2 position, float time);
    public event HoldPrimary OnHoldPrimary;
    public delegate void StartTouchSecondary(Vector2 position, float time);
    public event StartTouchSecondary OnStartTouchSecondary;
    public delegate void EndTouchSecondary(Vector2 position, float time);
    public event EndTouchSecondary OnEndTouchSecondary;
    #endregion

    private InputActionsB playerMenuControls;
    public GameObject zoneTest;

    public Camera cam;
    public float camSpeed;

    public Transform settingsView;
    public Transform creditsView;
    public Transform startScreenView;
    public Transform loadScreenView;
    public Transform demoView;

    public Transform settingsViewAlt;
    public Transform creditsViewAlt;
    public Transform startScreenViewAlt;
    public Transform loadScreenViewAlt;
    public Transform demoViewAlt;

    public GameObject titleLogoObject;
    public GameObject touchScreenObject;

    public GameObject loadScreen;
    public GameObject settingsScreen;
    public GameObject creditsScreen;
    public GameObject demoScreen;

    public GameObject startSphere;
    public GameObject settingsSphere;
    public GameObject creditsSphere;
    public GameObject demoSphere;

    public GameObject demoFront;
    public GameObject demoBack;

    public GameObject demoFrontAlt;
    public GameObject demoBackAlt;

    public bool isInIntro = true;
    public float isInIntroMaxTimer;
    public float isInIntroCurrentTimer = 1f;
    public bool isInTouchScreen = false;
    public bool isInActive = false;

    public AudioSource tapPlayer;
    public AudioClip tapTrack;

    private void Start()
    {
        isInIntroCurrentTimer = isInIntroMaxTimer;
        titleLogoObject.SetActive(true);
        //StartCoroutine(startStuff());
    }

    private void Update()
    {
        if(playerMenuControls==null)
        {
            Debug.Log("Iteration");
            playerMenuControls = new InputActionsB();
            playerMenuControls.Touch.PrimaryTap.started += ctx =>
            {
                if (ctx.interaction is SlowTapInteraction)
                {
                    StartTouchPrimaryMethod(ctx);
                }
            };
            //SlowTapInteraction
            playerMenuControls.Touch.PrimaryTap.performed += ctx =>
            {
                Debug.Log("Primary Tap Performed");
                tapPlayer.PlayOneShot(tapTrack);
                if (ctx.interaction is SlowTapInteraction)
                {
                    EndTouchPrimaryMethod(ctx);
                }
                else
                {
                    Debug.Log("InTapMethod");
                    TapPrimaryMethod(ctx);
                }
            };

            playerMenuControls.Touch.PrimaryTap.canceled += ctx =>
            {
                if (ctx.interaction is SlowTapInteraction)
                {
                    EndTouchPrimaryMethod(ctx);
                }
            };

            playerMenuControls.Touch.SecondaryContact.started += ctx =>
            {
                StartTouchSecondaryMethod(ctx);

            };

            playerMenuControls.Enable();
            OnTapPrimary += Select;

            playerMenuControls.Touch.SecondaryContact.canceled += ctx => EndTouchSecondaryMethod(ctx);
        }
        
        if (isInIntroCurrentTimer > 0f)
        {
            isInIntroCurrentTimer = isInIntroCurrentTimer - Time.deltaTime;
            if (isInIntroCurrentTimer <= 0f)
            {
                isInIntro = false;
                isInTouchScreen = true;
                touchScreenObject.SetActive(true);
            }
        }
    }

    private void StartTouchPrimaryMethod(InputAction.CallbackContext context)
    {
        if (OnStartTouchPrimary != null) OnStartTouchPrimary(PrimaryPosition(), (float)context.startTime);
    }

    private void EndTouchPrimaryMethod(InputAction.CallbackContext context)
    {
        if (OnEndTouchPrimary != null) OnEndTouchPrimary(PrimaryPosition(), (float)context.time);
    }

    private void TapPrimaryMethod(InputAction.CallbackContext context)
    {
        if (OnTapPrimary != null) OnTapPrimary(PrimaryPosition(), (float)context.startTime);
    }

    public Vector2 PrimaryPosition()
    {
        return playerMenuControls.Touch.PrimaryPosition.ReadValue<Vector2>();
    }

    private void StartTouchSecondaryMethod(InputAction.CallbackContext context)
    {
        if (OnStartTouchSecondary != null) OnStartTouchSecondary(SecondaryPosition(), (float)context.startTime);
    }

    private void EndTouchSecondaryMethod(InputAction.CallbackContext context)
    {
        if (OnEndTouchSecondary != null) OnEndTouchSecondary(SecondaryPosition(), (float)context.time);
    }

    public Vector2 SecondaryPosition()
    {
        return playerMenuControls.Touch.SecondaryPosition.ReadValue<Vector2>();
    }

    public void Select(Vector2 screenPosition, float time)
    {
        if (isInTouchScreen)
        {
            touchScreenObject.SetActive(false);
            EnableSpheres();
            isInTouchScreen = false;
        }
        else
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            Debug.DrawRay(ray.origin, ray.direction * 50000000, Color.blue, 120f);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Selected!");
                if (hit.collider.gameObject.name == "SettingsSphere")
                {
                    Debug.Log("Settings Selected!");
                    StartCoroutine(FocusCamera(1));
                    settingsScreen.SetActive(true);
                    DisableSpheres();
                }
                else if (hit.collider.gameObject.name == "StartSphere")
                {
                    Debug.Log("Start Selected!");
                    StartCoroutine(FocusCamera(4));
                    loadScreen.SetActive(true);
                    DisableSpheres();
                }
                else if (hit.collider.gameObject.name == "CreditsSphere")
                {
                    Debug.Log("Credits Selected!");
                    StartCoroutine(FocusCamera(2));
                    creditsScreen.SetActive(true);
                    DisableSpheres();
                }
                else if (hit.collider.gameObject.name == "DemoSphere")
                {
                    Debug.Log("Demo Selected!");
                    StartCoroutine(FocusCamera(3));
                    demoScreen.SetActive(true);
                    DisableSpheres();
                }
                else if (hit.collider.gameObject.name == "Back")
                {
                    Debug.Log("Back Selected!");
                    StartCoroutine(FocusCamera(5));
                    DisableScreens();
                    EnableSpheres();
                }
                else if (hit.collider.gameObject.name == "SIS CINEMATICA")
                {
                    SceneManager.LoadScene(1);
                }
                else if (hit.collider.gameObject.name == "SIS LOBBY")
                {
                    SceneManager.LoadScene(2);
                }
                else if (hit.collider.gameObject.name == "DEMO MINIJUEGO")
                {
                    SceneManager.LoadScene(3);
                }
                else if (hit.collider.gameObject.name == "SIS DIALOGO")
                {
                    SceneManager.LoadScene(4);
                }
                else if (hit.collider.gameObject.name == "SIS COMBATE")
                {
                    SceneManager.LoadScene(5);
                }
                else if (hit.collider.gameObject.name == "Siguiente")
                {
                    Debug.Log("Contacto");
                    demoFront.SetActive(false);
                    demoBack.SetActive(true);
                }
                else if (hit.collider.gameObject.name == "Atras")
                {
                    demoFront.SetActive(true);
                    demoBack.SetActive(false);
                }
                else if (hit.collider.gameObject.name == "SettingsAlt")
                {
                    Debug.Log("SettingsAlt Selected!");
                    cam.transform.position = settingsViewAlt.position;
                }
                else if (hit.collider.gameObject.name == "StartAlt")
                {
                    Debug.Log("StartAlt Selected!");
                    cam.transform.position = loadScreenViewAlt.position;
                }
                else if (hit.collider.gameObject.name == "CreditsAlt")
                {
                    Debug.Log("CreditsAlt Selected!");
                    cam.transform.position = creditsViewAlt.position;
                }
                else if (hit.collider.gameObject.name == "DemoAlt")
                {
                    Debug.Log("Demo Selected!");
                    cam.transform.position = demoViewAlt.position;
                }
                else if (hit.collider.gameObject.name == "BackAlt")
                {
                    Debug.Log("Back Selected!");
                    cam.transform.position = startScreenViewAlt.position;
                }
                else if (hit.collider.gameObject.name == "LightMode")
                {
                    Debug.Log("Back Selected!");
                    cam.transform.position = settingsViewAlt.position;
                    cam.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
                else if (hit.collider.gameObject.name == "DarkMode")
                {
                    Debug.Log("Back Selected!");
                    cam.transform.position = settingsView.position;
                    cam.transform.rotation = Quaternion.Euler(-14f, 68f, 0f);
                }
                else if (hit.collider.gameObject.name == "SiguienteAlt")
                {
                    Debug.Log("Contacto");
                    demoFrontAlt.SetActive(false);
                    demoBackAlt.SetActive(true);
                }
                else if (hit.collider.gameObject.name == "AtrasAlt")
                {
                    demoFrontAlt.SetActive(true);
                    demoBackAlt.SetActive(false);
                }
            }
        }
    }

    public void EnableSpheres()
    {
        settingsSphere.SetActive(true);
        startSphere.SetActive(true);
        creditsSphere.SetActive(true);
        demoSphere.SetActive(true);
    }

    public void DisableSpheres()
    {
        settingsSphere.SetActive(false);
        startSphere.SetActive(false);
        creditsSphere.SetActive(false);
        demoSphere.SetActive(false);
    }

    public void DisableScreens()
    {
        demoScreen.SetActive(false);
        loadScreen.SetActive(false);
        settingsScreen.SetActive(false);
        creditsScreen.SetActive(false);
    }

    public void Return()
    {
        StartCoroutine(FocusCamera(5));
    }

    IEnumerator FocusCamera(int location)
    {
        if (location == 1)
        {
            while (cam.transform.position != settingsView.position)
            {
                Vector3 tempPos = Vector3.MoveTowards(cam.transform.position, settingsView.position, Time.deltaTime * 500f * camSpeed);
                Vector3 tempRot = Vector3.MoveTowards(cam.transform.rotation.eulerAngles, settingsView.rotation.eulerAngles, Time.deltaTime * 20f * camSpeed);

                cam.transform.SetPositionAndRotation(tempPos, Quaternion.Euler(tempRot));
                yield return null;
            }
        }
        else if (location == 2)
        {
            while (cam.transform.position != creditsView.position)
            {
                Vector3 tempPos = Vector3.MoveTowards(cam.transform.position, creditsView.position, Time.deltaTime * 300f * camSpeed);
                Vector3 tempRot = Vector3.MoveTowards(cam.transform.rotation.eulerAngles, creditsView.rotation.eulerAngles, Time.deltaTime * 20f * camSpeed);

                cam.transform.SetPositionAndRotation(tempPos, Quaternion.Euler(tempRot));
                yield return null;
            }
        }
        else if (location == 3)
        {
            while (cam.transform.position != demoView.position)
            {
                Vector3 tempPos = Vector3.MoveTowards(cam.transform.position, demoView.position, Time.deltaTime * 400f * camSpeed);
                Vector3 tempRot = Vector3.MoveTowards(cam.transform.rotation.eulerAngles, demoView.rotation.eulerAngles, Time.deltaTime * 5f * camSpeed);

                cam.transform.SetPositionAndRotation(tempPos, Quaternion.Euler(tempRot));
                yield return null;
            }
        }
        else if (location == 4)
        {
            while (cam.transform.position != loadScreenView.position)
            {
                Vector3 tempPos = Vector3.MoveTowards(cam.transform.position, loadScreenView.position, Time.deltaTime * 500f * camSpeed);
                Vector3 tempRot = Vector3.MoveTowards(cam.transform.rotation.eulerAngles, loadScreenView.rotation.eulerAngles, Time.deltaTime * 20f * camSpeed);

                cam.transform.SetPositionAndRotation(tempPos, Quaternion.Euler(tempRot));
                yield return null;
            }
        }
        else if (location == 5)
        {
            while (cam.transform.position != startScreenView.position)
            {
                Vector3 tempPos = Vector3.MoveTowards(cam.transform.position, startScreenView.position, Time.deltaTime * 500f * camSpeed);
                Vector3 tempRot = Vector3.MoveTowards(cam.transform.rotation.eulerAngles, startScreenView.rotation.eulerAngles, Time.deltaTime * 25f * camSpeed);

                cam.transform.SetPositionAndRotation(tempPos, Quaternion.Euler(tempRot));
                yield return null;
            }
        }
    }

}