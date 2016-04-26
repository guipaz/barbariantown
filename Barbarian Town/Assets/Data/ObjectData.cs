using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    public int x;
    public int y;
    public List<Sprite> sprites;
    public bool passable;
    public bool selectable;
    public Type type;

    public enum Type
    {
        Ground,
        Resource,
        Building,
        Item,
        Barbarian,
        Enemy,
        NPC
    }
}
