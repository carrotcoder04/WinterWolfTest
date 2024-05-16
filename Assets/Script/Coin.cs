using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    float time = 0f;
    void Update()
    {
        if(time < 1f)
        {
            time += Time.deltaTime;
            transform.position += (new Vector3(0, 0.8f) * Time.deltaTime);
        }
        else
        {
            time = 0f;
            gameObject.SetActive(false);
        }
    }
}
