using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    MapManager mapManager;

    public int mapW;
    public int mapH;

    void Awake()
    {
        mapManager = GetComponent<MapManager>();
    }

	void Start ()
    {
        mapManager.Build(MapGenerator.CreateMap(mapW, mapH));
    }
}
