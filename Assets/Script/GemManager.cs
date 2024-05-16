using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : Singleton<GemManager>
{
    string[] gemPool = new string[6] { "Blue", "Green", "Orange", "Pink", "Red", "Yellow" };
    Dictionary<Vector2Int, bool> hasGem = new Dictionary<Vector2Int, bool>();
    void Start()
    {
        Invoke(nameof(InitGem), 0.1f);
    }

    void InitGem()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                CreateGem(i);
            }
        }
    }
    public void SetFalse(Vector3 point)
    {
        int x = (int)(point.x / 1.25f);
        int y = (int)(point.y / 1.25f);
        Vector2Int key = new Vector2Int(x, y);
        if(hasGem.ContainsKey(key))
        {
            hasGem[key] = false;
        }
    }
    public void CreateGem(int i)
    {
        int x = Random.Range(0, 6);
        int y = Random.Range(-2, -6);
        while (hasGem.ContainsKey(new Vector2Int(x, y)) && hasGem[new Vector2Int(x, y)])
        {
            x = Random.Range(0, 6);
            y = Random.Range(-2, -6);
        }
        hasGem[new Vector2Int(x, y)] = true;
        Gem gem = EasyObjectPool.instance.GetObjectFromPool(gemPool[i], new Vector3(x * 1.25f, y * 1.25f), Quaternion.identity).GetComponent<Gem>();
        gem.lastPosition = new Vector3(x * 1.25f, y * 1.25f);
    }
}
