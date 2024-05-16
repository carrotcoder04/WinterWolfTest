using JetBrains.Annotations;
using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardManager : Singleton<BoardManager>
{
    public GameObject cell1;
    public GameObject cell2;
    public Dictionary<Vector2Int, Gem> gemManager;
    public GameObject coin;
    void Start()
    {
        gemManager = new Dictionary<Vector2Int, Gem>();
        CreateCells();
    }
    void CreateCells()
    {
        for(int i=0;i<6;i++)
        {
            for(int j=0;j<6;j++)
            {
                if((i+j)%2==0)
                {
                    GameObject tile = Instantiate(cell1,new Vector3(i*1.25f,j*1.25f,0),Quaternion.identity,transform);
                }
                else
                {
                    GameObject tile = Instantiate(cell2, new Vector3(i * 1.25f, j * 1.25f, 0), Quaternion.identity, transform);
                }
                gemManager.Add(new Vector2Int(i,j), null);
            }
        }
    }
    void CheckGem(int x, int y, Gem gem)
    {
        Vector2Int left = new Vector2Int(x, y - 1);
        Vector2Int right = new Vector2Int(x, y + 1);
        Vector2Int top = new Vector2Int(x + 1, y);
        Vector2Int down = new Vector2Int(x - 1, y);
        Vector3 disappearGem = Vector3.zero;
        bool hasGem = false;
        if (gemManager.ContainsKey(left) && gemManager[left] != null)
        {
            if (gemManager[left].index == gem.index)
            {
                hasGem = true;
                disappearGem = gemManager[left].transform.position;
                gemManager[left].gameObject.SetActive(false);
                gemManager[left] = null;
            }
        }
        if (gemManager.ContainsKey(right) && gemManager[right] != null)
        {
            if (gemManager[right].index == gem.index)
            {
                hasGem = true;
                disappearGem = gemManager[right].transform.position;
                gemManager[right].gameObject.SetActive(false);
                gemManager[right] = null;
            }
        }
        if (gemManager.ContainsKey(top) && gemManager[top] != null)
        {
            if (gemManager[top].index == gem.index)
            {
                hasGem = true;
                disappearGem = gemManager[top].transform.position;
                gemManager[top].gameObject.SetActive(false);
                gemManager[top] = null;
            }
        }
        if (gemManager.ContainsKey(down) && gemManager[down] != null)
        {
            if (gemManager[down].index == gem.index)
            {
                hasGem = true;
                disappearGem = gemManager[down].transform.position;
                gemManager[down].gameObject.SetActive(false);
                gemManager[down] = null;
            }
        }
        if (hasGem)
        {
            gem.gameObject.SetActive(false);
            gemManager[new Vector2Int(x, y)] = null;
            Vector3 coinSpawn = (gem.transform.position + disappearGem)/2f;
            coin.transform.position = coinSpawn;
            coin.SetActive(true);
            int rand = Random.Range(0, 6);
            GemManager.Instance.CreateGem(rand);
            GemManager.Instance.CreateGem(rand);
        }
    }
    public bool CheckCell(int x,int y)
    {
        if(!gemManager.ContainsKey(new Vector2Int(x,y))) return false;
        if (gemManager[new Vector2Int(x,y)] == null)
        {
            return true;
        }
        return false;
    }
    public void PlaceGem(int xTarget, int yTarget,Gem gem)
    {
        int x = (int)(gem.lastPosition.x/1.25f);
        int y = (int)(gem.lastPosition.y/1.25f);
        if (gem.lastPosition.x/1.25f - x > 0.5f) x++;
        if (gem.lastPosition.y/1.25f - y > 0.5f) y++;
        Vector2Int lastKey = new Vector2Int(x, y);
        if (gemManager.ContainsKey(lastKey))
        {
            gemManager[lastKey] = null;
        }
        Vector2Int point = new Vector2Int(xTarget, yTarget);
        gemManager[point] = gem;
        gem.lastPosition = new Vector3(xTarget*1.25f,yTarget*1.25f,0);
        gem.transform.position = gem.lastPosition;
        CheckGem(xTarget,yTarget,gem);
    }
}
