using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManagerScript : Singleton<TowerManagerScript>
{
    //Room Data
    [System.Serializable]
    public class RoomID
    {
        public string roomName;
        public int id;
        public Room room;
        public string tiene;
        public string condiciones;
    }

    [System.Serializable]
    public class SectorID
    {
        public int id;
        public Sector sector;
    }

    [System.Serializable]
    public class RoomData
    {
        public string objectName;
        public string tiene;
        public string condiciones;
    }

    [SerializeField]
    public RoomID[] rooms;

    [SerializeField]
    public SectorID[] sectors;

    [SerializeField]
    public RoomData[] roomDatas;

    public Dictionary<int, Room> roomDictionary;

    public Dictionary<int, Sector> sectorDictionary;

    public Dictionary<string, RoomData> roomDataDictionary;

    public float currentRoom;

    public float currentSector;

    // Start is called before the first frame update
    void Awake()
    {
        /*
        roomDictionary = new Dictionary<int, Room>();
       // Debug.Log("Paso 1:");
        for(int i=0; i<rooms.Length; i++)
        {
            rooms[i].room.location = rooms[i].room.roomObject.transform.localPosition;
            //Debug.Log(rooms[i].id);
            roomDictionary.Add(rooms[i].id, rooms[i].room);
        }
        */

        //Este es el que importa
        
        sectorDictionary = new Dictionary<int, Sector>();
        for (int i = 0; i < sectors.Length; i++)
        {
            sectors[i].sector.location = sectors[i].sector.sectorObject.transform.localPosition;
            sectorDictionary.Add(sectors[i].id, sectors[i].sector);
        }



        roomDataDictionary = new Dictionary<string, RoomData>();
        for (int i = 0; i < roomDatas.Length; i++)
        {
            roomDataDictionary.Add(roomDatas[i].objectName, roomDatas[i]);
        }


        /*
        for(int i=0;i<roomDictionary.Count;i++)
        {
            Debug.Log("Name: " + roomDictionary[i].id);
            Debug.Log("Location: " + roomDictionary[i].location);
        }
        */
    }

    public void PresentChoices()
    {
        //Se highlightean los cuartos a los que se puede acceder desde el cuarto actual. Si no se esta en ningun cuarto, se highlightean las posibles entradas.
        for (int i = 0; i < roomDictionary.Count; i++)
        {

        }
    }

    public void ChooseRoom()
    {
        //
    }

    public void ChooseSection()
    {
        //Para mas adelante, cuando se implementen las secciones.
    }

}
