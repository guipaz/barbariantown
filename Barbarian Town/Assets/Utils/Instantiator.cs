using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum ETile
{
    None,
    Grass,
    Tree,
    Bonfire,
}

public static class Instantiator
{
    static System.Random random = new System.Random();

    static List<string> grasses = new List<string>() { "grass1", "grass2", "grass3" };

    public static GameObject Create(ETile id, Vector2? pos = null, int? order = null)
    {
        string prefab = "";
        switch (id)
        {
            case ETile.Grass:
                prefab = "map/" + grasses[random.Next(0, grasses.Count)];
                break;
            case ETile.Tree:
                prefab = "map/tree";
                break;
            case ETile.Bonfire:
                prefab = "objects/bonfire";
                break;
        }

        if (prefab == "")
            return null;

        GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>("prefabs/" + prefab));
        if (pos != null)
            obj.transform.position = (Vector2)pos;
        if (order != null)
            obj.GetComponentInChildren<SpriteRenderer>().sortingOrder = (int)order;

        return obj;
    }
}