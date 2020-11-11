using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boids : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 lastpos;
    // Start is called before the first frame update
    void Start()
    {
        lastpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = velocity.normalized;
        //velocity += transform.position;
      
        transform.position += velocity ;
    }
} 
