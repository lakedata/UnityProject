using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float dropSpeed = 1e-25f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.down * this.dropSpeed * Time.deltaTime);


        if (transform.position.y < -4.0f)
        {
            Destroy(gameObject);
        }
    }
}
