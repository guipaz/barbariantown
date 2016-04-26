using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Instantiator
{
    public static ObjectData Create(string name, Vector2? pos = null, int? order = null)
    {
        GameObject prefab = Resources.Load<GameObject>("prefabs/" + name);
        if (prefab == null)
            return null;

        GameObject obj = GameObject.Instantiate(prefab);
        if (pos != null)
            obj.transform.position = (Vector2)pos;
        if (order != null)
            obj.GetComponentInChildren<SpriteRenderer>().sortingOrder = (int)order;

        ObjectData data = obj.GetComponent<ObjectData>();
        data.x = (int)((Vector2)pos).x;
        data.y = (int)((Vector2)pos).y;
        if (data.sprites != null && data.sprites.Count > 0)
            obj.GetComponentInChildren<SpriteRenderer>().sprite = data.sprites[Global.random.Next(0, data.sprites.Count)];

        return data;
    }
}