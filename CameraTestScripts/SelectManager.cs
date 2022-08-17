using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    private InputManagerB inputManager;
    private LobbyManager lobbyManager;
    private TowerManagerScript towerManager;

    [SerializeField]
    private Camera cam;

    public int selectedRoom;
    public int selectedSection;
    public GameObject selectedSectionObject;

    public int focusedRoom;
    public int focusedSection;

    public float cameraSavedDistance;

    //Highlight
    public float highlightZoomMaxDuration;
    public float highlightZoomCurrentDuration;
    public float highlightZoomRotateSpeed;
    public float highlightZoomHeightSpeed;

    //Zoom
    public float zoomMaxDuration;
    public float zoomCurrentDuration;
    public float zoomSpeed;
    public float zoomDepth;

    //UnZoom
    public float unZoomMaxDuration;
    public float unZoomCurrentDuration;
    public float unZoomSpeed;
    public float fadeOutSpeed;
    public float fadeInSpeed;
    GameObject selectedObject;

    //Move Variables
    private Vector3 previousPosition;
    public Coroutine coroutine;
    public float moveSpeed;
    GameObject scrollerObject;
    public GameObject dummy;

    //MiddleScreenVariables
    public GameObject listList;
    public GameObject createList;
    public GameObject recycleList;

    public TextMeshProUGUI tieneText;
    public TextMeshProUGUI condicionesText;

    private void Awake()
    {
        inputManager = InputManagerB.Instance;
        lobbyManager = LobbyManager.Instance;
        towerManager = TowerManagerScript.Instance;
    }

    private void OnEnable()
    {
        // inputManager.OnEndTouchPrimary += Select;
        EnableFunctions();
    }

    private void OnDisable()
    {
        // inputManager.OnEndTouchPrimary -= Select;
        DisableFunctions();
    }

    public void EnableFunctions()
    {
        inputManager.OnTapPrimary += Select;
        inputManager.OnStartTouchPrimary += Move;
        inputManager.OnEndTouchPrimary += EndMove;
    }

    public void DisableFunctions()
    {
        inputManager.OnTapPrimary -= Select;
        inputManager.OnStartTouchPrimary -= Move;
        inputManager.OnEndTouchPrimary -= EndMove;
    }
    
    public void Move(Vector2 position, float time)
    {
        Debug.Log("Move Attempt");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(position);
        Debug.DrawRay(ray.origin, ray.direction * 50000000, Color.red, 120f);
        if (Physics.Raycast(ray, out hit))
        {
            inputManager.isScrolling = true;
            Debug.Log("(Move) Name: " + hit.transform.gameObject.name);
            if(hit.transform.gameObject.tag == "Scroller")
            {
                scrollerObject = hit.transform.gameObject;
                previousPosition = cam.ScreenToViewportPoint(position);
                Debug.Log("Coroutine Started");
                coroutine = StartCoroutine(UpDownUI());
            }
        }
    }

    public void EndMove(Vector2 screenPosition, float time)
    {
        if(coroutine!=null)
        {
            StopCoroutine(coroutine);
        }
        scrollerObject = dummy;
        inputManager.isScrolling = false;
    }

    private IEnumerator UpDownUI()
    {
        while (true)
        {
            if (!inputManager.pinchZooming && !inputManager.focusing && !inputManager.unfocusing)
            {
                Vector3 direction = previousPosition - cam.ScreenToViewportPoint(inputManager.PrimaryPosition());

                if (direction.y < 0)
                {
                    scrollerObject.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * moveSpeed);
                }
                else if (direction.y > 0)
                {
                    scrollerObject.transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * moveSpeed);
                }
                previousPosition = cam.ScreenToViewportPoint(inputManager.PrimaryPosition());
            }
            yield return null;

        }
    }

    public void Select(Vector2 screenPosition, float time)
    {
        Debug.Log("Select Attempt");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Debug.DrawRay(ray.origin, ray.direction * 50000000, Color.blue, 120f);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Selected object: " + hit.transform.gameObject.name);
            selectedObject = hit.transform.gameObject;
            if (!inputManager.focusing)
            {
                Debug.Log("In Not Focusing");
                if (hit.transform.gameObject.tag == "Section")
                {
                    int sectionNumber = int.Parse(hit.transform.gameObject.name);

                    if (sectionNumber != selectedSection)
                    {
                        selectedSection = sectionNumber;
                        selectedSectionObject = hit.transform.gameObject;
                        inputManager.highlighting = true;
                    }
                    else if (sectionNumber == selectedSection)
                    {
                        if (inputManager.unfocusing)
                        {
                            inputManager.unfocusing = false;
                        }
                        focusedSection = sectionNumber;
                        inputManager.focusing = true;
                        //StartCoroutine(FadeObject.FadeOutObject(selectedObject, fadeOutSpeed));
                        StartCoroutine(FadeObject.FadeOutObjectTwo(selectedObject, fadeOutSpeed));
                        hit.transform.gameObject.GetComponent<MeshCollider>().enabled = false;
                        EnableRooms(focusedSection);
                    }
                }
                else if(hit.transform.gameObject.tag == "Scroller")
                {
                    Debug.Log("Name: " + hit.transform.gameObject.name);
                }
                else if (hit.transform.gameObject.tag == "Button")
                {
                    if(hit.transform.gameObject.name == "ListButton")
                    {
                        listList.SetActive(true);
                        createList.SetActive(false);
                        recycleList.SetActive(false);
                    }
                    else if (hit.transform.gameObject.name == "CreateButton")
                    {
                        listList.SetActive(false);
                        createList.SetActive(true);
                        recycleList.SetActive(false);
                    }
                    else if (hit.transform.gameObject.name == "RecycleButton")
                    {
                        listList.SetActive(false);
                        createList.SetActive(false);
                        recycleList.SetActive(true);
                    }
                    else if (hit.transform.gameObject.name == "StartButton")
                    {
                        Debug.Log("Start!!");
                    }
                    else if (hit.transform.gameObject.name == "GoBackButton")
                    {
                        Debug.Log("InGoBackButton");
                        StartCoroutine(lobbyManager.FocusCamera(5));
                        Debug.Log("In after Lobby Focus Camera");
                    }
                }
            }
            else if (inputManager.focusing)
            {
                if (hit.transform.gameObject.tag == "Room")
                {
                   Debug.Log("Room: " + hit.transform.gameObject.name);
                    Debug.Log("Tiene: " + towerManager.roomDataDictionary[hit.transform.gameObject.name].tiene);
                    tieneText.text = towerManager.roomDataDictionary[hit.transform.gameObject.name].tiene;
                    Debug.Log("Condiciones: " + towerManager.roomDataDictionary[hit.transform.gameObject.name].condiciones);
                    condicionesText.text = towerManager.roomDataDictionary[hit.transform.gameObject.name].condiciones;
                }
            }
        }
        else if(inputManager.focusing)
        {
            Debug.Log("InFocusOut");
            inputManager.focusing = false;
            inputManager.unfocusing = true;
            selectedSectionObject.GetComponent<MeshCollider>().enabled = true;
            StartCoroutine(FadeObject.FadeInObject(selectedObject, fadeInSpeed));
           // selectedSectionObject = null;
            DisableRooms(focusedSection);
        }
    }

    void EnableRooms(int focusSection)
    {
        Debug.Log("InEnableRooms");
        for (int i = 0; i < inputManager.towerManagerScript.sectorDictionary[focusSection].sectorRooms.Length; i++)
        {
            Debug.Log("The Room Name: " + inputManager.towerManagerScript.sectorDictionary[focusSection].sectorRooms[i].roomObject.name);
            inputManager.towerManagerScript.sectorDictionary[focusSection].sectorRooms[i].roomObject.GetComponent<MeshCollider>().enabled = true;
        }
    }

    void DisableRooms(int focusSection)
    {
        Debug.Log("InDisableRooms");
        for (int i = 0; i < inputManager.towerManagerScript.sectorDictionary[focusSection].sectorRooms.Length; i++)
        {
            inputManager.towerManagerScript.sectorDictionary[focusSection].sectorRooms[i].roomObject.GetComponent<MeshCollider>().enabled = false;
        }
    }
}