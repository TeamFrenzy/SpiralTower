using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    //ID del jugador
    public int id;

    string test;

    //Objetos dentro del juego
    public GameObject[] prefabs;
    public Canvas mainCanvas;

    //Acumulador de objetos a eliminar
    List<GameObject> windowList;

    //Diccionario de escenas
    Dictionary<string, TextAsset> dictionary;

    //Array publico de escenas. Se inserta al diccionario. Para poder editar el diccionario desde el Editor
    public ScriptDict[] scriptDict;

    //Index de dialogo actual dentro de escena
    private int index = 0;

    //UI dentro del juego
    public TextMeshProUGUI dialogue;
    public TextMeshProUGUI charName;

    //Elementos de audio
    private AudioClip[] aclips;
    static AudioSource aSource;
    public AudioSource bgMusic;

    //Contenedor principal de json
    DiaFileList diaMain;

    //Velocidad de tipeo
    float typingSpeed = 0.05f;

    //Longitud de texto añadida por caracteres especiales que permiten eventos dentro del dialogo
    int extraLength = 0;

    //Lista de 'marks' actual del jugador. Determinan decisiones previas. Se acumulan en un solo string para ser facil de almacenar
    public string route;

    //Burbujas de Texto
    public GameObject[] bubblePrefabs;

    [SerializeField]
    private CTInputManager inputManager;

    private void Awake()
    {
        inputManager = CTInputManager.Instance;
    }
    private void OnEnable()
    {
        //controls.Enable();
        EnableFunctions();
    }

    private void OnDisable()
    {
        //controls.Disable();
        DisableFunctions();
    }

    public void EnableFunctions()
    {
        inputManager.OnTapPrimary += nextSentence;
    }

    public void DisableFunctions()
    {
        inputManager.OnTapPrimary -= nextSentence;

    }

    private void Start()
    {
        id = 1;
        windowList = new List<GameObject>();
        //dialogue.text = "";
        route = "start";
        //GetRouteCall();
        Debug.Log("Test: " + test);
        Debug.Log("Route: " + route);
        FillDictionary();
        SetMain();
        //SetListener(true);
        SetAudio();
        StartCoroutine(Type());
    }

    /*
    //Activa o desactiva el listener del fondo de pantalla que pasa al siguiente dialogo
    public void SetListener(bool active)
    {
        if (active)
        {
            Debug.Log("In Listener");

            blackBG.onClick.AddListener(nextSentence);
        }
        else
        {
            blackBG.onClick.RemoveAllListeners();
        }
    }
    */

    public void SetMain()
    {
        diaMain = JsonUtility.FromJson<DiaFileList>(dictionary["begin"].text);
    }

    public void SetAudio()
    {
        aSource = GetComponent<AudioSource>();
        aclips = new AudioClip[4];
        aclips[0] = Resources.Load<AudioClip>("clipUno");
        aclips[1] = Resources.Load<AudioClip>("clipCuatro");
        aclips[2] = Resources.Load<AudioClip>("clipTres");
        aclips[3] = Resources.Load<AudioClip>("05");
        bgMusic.PlayOneShot(aclips[3]);
    }
    //Transforma cadena de string de dialogo en chars y los ingresa uno por uno en el dialogo contemplando caracteres especiales para introducir eventos especiales
    IEnumerator Type()
    {
        GameObject tempBubble = Instantiate(bubblePrefabs[diaMain.diaList[index].textBubble.bubbleType], new Vector3(diaMain.diaList[index].textBubble.bubblePositionX, diaMain.diaList[index].textBubble.bubblePositionY, 0f), Quaternion.Euler(0f, 0f, 0f));
        tempBubble.transform.SetParent(mainCanvas.transform);
        tempBubble.transform.localPosition = new Vector3(diaMain.diaList[index].textBubble.bubblePositionX, diaMain.diaList[index].textBubble.bubblePositionY, 0f);
        tempBubble.GetComponent<RectTransform>().sizeDelta = new Vector2(diaMain.diaList[index].textBubble.bubbleSizeX, diaMain.diaList[index].textBubble.bubbleSizeY);
        tempBubble.transform.Find("BubbleText").GetComponent<RectTransform>().sizeDelta = new Vector2(tempBubble.GetComponent<RectTransform>().sizeDelta.x - 20f, tempBubble.GetComponent<RectTransform>().sizeDelta.y - 20f);


        tempBubble.transform.Find("DemoTextBubblePointer").GetComponent<RectTransform>().localPosition = new Vector3(tempBubble.transform.Find("DemoTextBubblePointer").GetComponent<RectTransform>().localPosition.x, tempBubble.transform.Find("DemoTextBubblePointer").GetComponent<RectTransform>().localPosition.y-((tempBubble.GetComponent<RectTransform>().sizeDelta.y/2)-50f), 0f);


        dialogue = tempBubble.GetComponentInChildren<TextMeshProUGUI>();
        dialogue.text = "";

        aSource.volume = 1f;
        typingSpeed = 0.05f;
       // dialogue.fontSize = 6.6f;
        int tempIndex = index;
        bool input = false;
        bool command = false;
        bool ctypebool = false;
        char commandType = ' ';
        string commandInput = "";
       // charName.text = diaMain.diaList[index].name;

        if (diaMain.diaList[index].special == 3)
        {
            bgMusic.Pause();
        }
        if (diaMain.diaList[index].special == 4)
        {
            bgMusic.UnPause();
        }

        foreach (char letter in diaMain.diaList[index].dialogue.ToCharArray())
        {
            if (letter == '[')
            {
                command = true;
                ctypebool = true;
                extraLength++;
            }
            else if (letter == ']')
            {
                float parInput = float.Parse(commandInput);
                command = false;
                commandInput = "";
                extraLength++;
                if (commandType == 'w')
                {
                    yield return new WaitForSeconds(parInput);
                }
                else if (commandType == 't')
                {
                    typingSpeed = parInput;
                }
                else if (commandType == 's')
                {
                    //Display sound number parInput
                }
            }
            else if ((command == true) && (ctypebool == true))
            {
                commandType = letter;
                ctypebool = false;
                extraLength++;
            }
            else if ((command == true) && (ctypebool == false))
            {
                commandInput += letter;
                extraLength++;
            }
            else if (letter == '<')
            {
                dialogue.text += letter;
                input = true;
            }
            else if (letter == '>')
            {
                dialogue.text += letter;
                input = false;
            }
            else if (input == true)
            {
                dialogue.text += letter;
            }
            else if (tempIndex == index)
            {
                if (diaMain.diaList[index].name == "Tamaki")
                {
                    aSource.PlayOneShot(aclips[0]);
                }
                else if (diaMain.diaList[index].name == "Mokyu")
                {
                    aSource.PlayOneShot(aclips[1]);
                }
                else if (diaMain.diaList[index].name == "7")
                {
                    aSource.PlayOneShot(aclips[2]);
                }
                dialogue.text += letter;
                yield return new WaitForSeconds(typingSpeed);

                if ((diaMain.diaList[index].interrupts == 1) && (dialogue.text.Length == diaMain.diaList[index].dialogue.Length))
                {
                    if (index != diaMain.diaList.Count - 1)
                    {
                        index++;
                    }
                    if (diaMain.diaList[index].addendum != 1)
                    {
                        dialogue.text = "";
                    }
                    StartCoroutine(Type());
                }

                if ((diaMain.diaList[index].addendum != 1) && (dialogue.text.Length == diaMain.diaList[index].dialogue.Length))
                {
                    break;
                }

                if ((diaMain.diaList[index].addendum == 1) && (dialogue.text.Length == diaMain.diaList[index - 1].dialogue.Length + diaMain.diaList[index].dialogue.Length))
                {
                    break;
                }
            }
        }

        //Si se cumplen condiciones para dos rutas diferentes se tiene que dar la opcion para elegir. Aveces el cambio es opcional y aveces no.
        if ((dialogue.text.Length + extraLength) == diaMain.diaList[index].dialogue.Length)
        {
            if (diaMain.diaList[index].choiceAmount >= 1)
            {
                List<string> possibleRoutes = new List<string>();
                List<string> possibleChoicesName = new List<string>();
                List<bool> possibleDivergences = new List<bool>();
                int possibleRoutesNumber = 0;

                for (int i = 0; i < diaMain.diaList[index].choiceAmount; i++)
                {
                    int comparable = 0;
                    for (int j = 0; j < diaMain.diaList[index].choices[i].conditionAmount; j++)
                    {
                        if (route.Contains(diaMain.diaList[index].choices[i].conditionList[j].condition))
                        {
                            comparable++;
                        }
                    }
                    if (comparable == diaMain.diaList[index].choices[i].conditionAmount)
                    {
                        possibleRoutes.Add(diaMain.diaList[index].choices[i].mark);
                        possibleChoicesName.Add(diaMain.diaList[index].choices[i].text);
                        possibleDivergences.Add(diaMain.diaList[index].choices[i].divergence);
                        possibleRoutesNumber++;
                    }
                }

                if (possibleRoutesNumber == 1)
                {
                    Save(possibleRoutes[0]);
                    if (possibleDivergences[0])
                    {
                        SetRouteEx(possibleRoutes[0]);
                    }
                    else
                    {
                        nextSentence(new Vector2(0f,0f), 0f);
                    }
                }
                else
                {
                    SpawnChoices(possibleRoutes, possibleDivergences, possibleChoicesName);
                }
            }
        }

    }

    public void nextSentence(Vector2 position, float time)
    {
        if (dialogue.text == "Fin!")
        {

        }
        else if (diaMain.diaList[index].interrupts == 1)
        {
            dialogue.text = diaMain.diaList[index].dialogue;

            if (index != diaMain.diaList.Count - 1)
            {
                index++;
            }

            if (diaMain.diaList[index].addendum != 1)
            {
                dialogue.text = "";
            }

            StartCoroutine(Type());
        }
        else if ((diaMain.diaList[index].addendum == 1) && ((dialogue.text.Length + extraLength) != (diaMain.diaList[index - 1].dialogue.Length + diaMain.diaList[index].dialogue.Length)))
        {
            aSource.volume = 0.4f;
            typingSpeed = 0.001f;
        }
        else if ((diaMain.diaList[index].addendum != 1) && ((dialogue.text.Length + extraLength) != diaMain.diaList[index].dialogue.Length))
        {
            aSource.volume = 0.4f;
            typingSpeed = 0.001f;
        }
        else if (index == diaMain.diaList.Count - 1)
        {
            dialogue.text = "";
            //charName.text = "";
            dialogue.text = "Fin!";
        }
        else if (index < diaMain.diaList.Count - 1)
        {
            extraLength = 0;
            if (index != diaMain.diaList.Count - 1)
            {
                PathFinder();
            }
            if (diaMain.diaList[index].addendum != 1)
            {
                dialogue.text = "";
            }
            StartCoroutine(Type());
        }
    }

    public void SpawnChoices(List<string> possibleChoices, List<bool> possibleDivergences, List<string> possibleChoicesName)
    {
        //blackBG.onClick.RemoveListener(nextSentence);
        float[] height = new float[4];

        if (possibleChoices.Count == 2)
        {
            height[0] = 75f;
            height[1] = -75f;
        }
        else if (possibleChoices.Count == 3)
        {
            height[0] = 100f;
            height[1] = 0f;
            height[2] = -100f;
        }
        else if (possibleChoices.Count == 4)
        {
            height[0] = 120f;
            height[1] = 60f;
            height[2] = -60f;
            height[3] = -120f;
        }
        for (int i = 0; i < possibleChoices.Count; i++)
        {
            GameObject oChoice = Instantiate(prefabs[0], new Vector3(0, height[i], 0), transform.rotation);
            oChoice.transform.SetParent(mainCanvas.transform, false);
            oChoice.GetComponentInChildren<TextMeshProUGUI>().text = possibleChoicesName[i];
            string tempString = possibleChoices[i];
            bool tempBool = possibleDivergences[i];
            oChoice.GetComponent<Button>().onClick.AddListener(() => Choice(tempString, tempBool));
            windowList.Add(oChoice);
        }
    }
    public void Save(string mark)
    {
        route += " " + mark;
        StartCoroutine(SaveRoute());
    }

    public void Choice(string markChoice, bool divergence)
    {
        Save(markChoice);
        for (int i = windowList.Count - 1; i >= 0; i--)
        {
            Destroy(windowList[i]);
        }
        windowList.Clear();
        if (divergence)
        {
            SetRoute(markChoice);
            dialogue.text = "";
            StartCoroutine(Type());
        }
        else
        {
            nextSentence(new Vector2(0f,0f), 0f);
        }
        //blackBG.onClick.AddListener(nextSentence);
    }

    public void PathFinder()
    {
        index++;
        if (diaMain.diaList[index].conditionAmount >= 1)
        {
            int comparable = 0;
            bool pathMaker = false;
            while (!pathMaker)
            {
                for (int i = 0; i < diaMain.diaList[index].conditionAmount; i++)
                {
                    if (route.Contains(diaMain.diaList[index].conditionList[i].condition))
                    {
                        comparable++;
                    }
                }

                if (comparable == diaMain.diaList[index].conditionAmount)
                {
                    pathMaker = true;
                }
                else
                {
                    index++;
                }
            }
        }
    }
    public void SetRoute(string mark)
    {
        index = 0;
        diaMain = JsonUtility.FromJson<DiaFileList>(dictionary[mark].text);
       // blackBG.onClick.AddListener(nextSentence);
    }

    public void SetRouteEx(string mark)
    {
        index = 0;
        diaMain = JsonUtility.FromJson<DiaFileList>(dictionary[mark].text);
        //blackBG.onClick.AddListener(Resume);
    }

    public void FillDictionary()
    {
        dictionary = new Dictionary<string, TextAsset>();
        for (int i = 0; i < scriptDict.Length; i++)
        {
            dictionary.Add(scriptDict[i].mark, scriptDict[i].text);
        }
    }
    public void Resume()
    {
        dialogue.text = "";
        StartCoroutine(Type());
       // blackBG.onClick.RemoveAllListeners();
       // blackBG.onClick.AddListener(nextSentence);
    }

    IEnumerator GetRoute(System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        Debug.Log("El ID es: " + id);

        //using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/getroute", form))
        yield return null;
    }

    //Guarda la ruta actual en el server
    IEnumerator SaveRoute()
    {
        yield return null;
    }

    void GetRouteCall()
    {
        StartCoroutine(GetRoute(returnValue =>
        {
            route = returnValue;
        }));
    }
}