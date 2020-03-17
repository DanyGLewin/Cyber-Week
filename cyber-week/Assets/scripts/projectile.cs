using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class projectile : MonoBehaviour
{
   
    [SerializeField] Vector3 movementvector= new Vector3(0.1f,0,0); // to understand = new Vector3(0, 60f, 0);
    Vector3 startingPos;
    [SerializeField] float xBorder=-30f; 


    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + movementvector*Time.deltaTime;
        if (transform.position.x <= xBorder);
        {
            print(transform.position);
            transform.position = startingPos;
        }

    }
}
