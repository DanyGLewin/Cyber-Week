using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectMovement3 : MonoBehaviour
{
    float cycles;
    [SerializeField] Vector3 movementvector; // to understand = new Vector3(0, 60f, 0);
    [Range(-1, 1)] [SerializeField] float movementfactor;
    [SerializeField] float period= 3f;
    Vector3 startingPos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period != 0)
           cycles = Time.time / period; //grows continually from 0
        else
            cycles = 0f;
        const float tau = Mathf.PI * 2f; //6.28
        float rawsinwave = Mathf.Sin(tau * cycles);// goes from -1 to 1
        movementfactor = rawsinwave / 2f ; // moves between -0.5 and 0.5 values 
        Vector3 offset = movementvector * movementfactor;
        transform.position = startingPos+offset;
       
    }
}
