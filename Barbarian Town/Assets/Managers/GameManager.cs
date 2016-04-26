using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    MapManager mapManager;
    JobManager jobManager;

    public int mapW;
    public int mapH;

    ObjectData _selectedObject;
    public ObjectData selectedObject
    {
        get
        {
            return _selectedObject;
        }
        set
        {
            SpriteRenderer[] renderer = null;

            // resets the last selected object's color
            if (_selectedObject != null)
            {
                renderer = _selectedObject.gameObject.GetComponentsInChildren<SpriteRenderer>();
                if (renderer != null)
                    foreach (SpriteRenderer r in renderer)
                        r.color = Color.white;
            }
            
            _selectedObject = value;

            // adjusts the color of the new selected object
            if (_selectedObject != null)
            {
                renderer = _selectedObject.gameObject.GetComponentsInChildren<SpriteRenderer>();
                if (renderer != null)
                    foreach (SpriteRenderer r in renderer)
                        r.color = Color.blue;
            }
        }
    }

    void Awake()
    {
        mapManager = GetComponent<MapManager>();
        jobManager = GetComponent<JobManager>();

        Global.gameManager = this;
        Global.mapManager = mapManager;
        Global.jobManager = jobManager;
    }

	void Start ()
    {
        mapManager.Create(mapW, mapH);
    }

    void Update()
    {
        foreach (ITickable tickable in mapManager.map.tickableObjects)
            tickable.Tick();
    }
}
