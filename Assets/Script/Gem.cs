using JetBrains.Annotations;
using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public Vector3 lastPosition;
    public int index;
    void OnDisable()
    {
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }
    void OnMouseDown()
    {
        StartCoroutine(MoveToMouse());
    }
    void OnMouseUp()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        StopAllCoroutines();
        int x = (int)(mousePos.x/1.25f);
        int y = (int)(mousePos.y/1.25f);
        if (mousePos.x/1.25f - x > 0.5f) x++;
        if (mousePos.y/1.25f - y > 0.5f) y++;
        if(BoardManager.Instance.CheckCell(x,y))
        {
            GemManager.Instance.SetFalse(lastPosition);
            BoardManager.Instance.PlaceGem(x, y, this);
        }
        else
        {
            transform.position = lastPosition;
        }
    }
    IEnumerator MoveToMouse()
    {
        while (true)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
            yield return null;
        }
    }
}
