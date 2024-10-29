using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

using Random = UnityEngine.Random;

public class ItemGenerator : MonoBehaviour
{
    public GameObject carrotPrefab;
    public GameObject bombPrefab;
    float delta = 0;
    float span = 1.0f;
    float speed = 0.5f;
    int ratio = 2;
    public void SetParameter(float span, float speed, int ratio)
    {
        this.span = span; //积己 加档
        this.speed = speed; //倡窍 加档
        this.ratio = ratio; //积己厚啦
    }

    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject item;
            int dice = Random.Range(1, 11);
            if (dice <= ratio)
            {
                item = Instantiate(bombPrefab);
            }
            else
            {
                item = Instantiate(carrotPrefab);
            }
            float x = Random.Range(-5, 5); 
            item.transform.position = new Vector3(x, 4, 0); 
            item.GetComponent<ItemController>().dropSpeed = this.speed;
        }
    }

}
