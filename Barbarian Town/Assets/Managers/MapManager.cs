using UnityEngine;
using System.Collections;
using System;

public class MapManager : MonoBehaviour
{
    public Map map { get; private set; }

    public void Build(Map map)
    {
        this.map = map;

        for (int y = 0; y < map.height; y++)
        {
            for (int x = 0; x < map.width; x++)
            {
                Vector2 pos = new Vector2(x, map.height - y - 1);
                for (int h = 0; h < 5; h++)
                {
                    Instantiator.Create(map.canvas[x, y][h], pos, h);
                }
            }
        }
    }

    public void CreateBarbarians(int v)
    {
        
    }
}
