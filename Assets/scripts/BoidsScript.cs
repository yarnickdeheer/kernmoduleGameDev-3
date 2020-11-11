using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsScript : MonoBehaviour
{
    public float Speed;
    public Transform prefab;
    public int amount;
    public Transform[] flock;
    public Transform Cam;
    Vector3 pos1, pos2, pos3;
    // Start is called before the first frame update
    void Start()
    {
        flock = new Transform[amount];
        // spawn 
        for (int i = 0; i < amount; i++)
        {

            flock[i] = Instantiate(prefab, new Vector3(Random.Range(0, 5), Random.Range(0, 5), Random.Range(0, 5)), Quaternion.identity).transform;
          
        }
        foreach (Transform boid in flock)
        {
           // boid.GetComponent<Boids>().velocity = transform.forward * Speed;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        //check the rule
       
        foreach (Transform boid in flock)
        {
            pos1 = seperation(boid);
            pos2 = alignment(boid);
            pos3 = cohesion(boid);
            //Debug.Log("seperation " + pos1);
           // Debug.Log("alignment " + pos2);
          //  Debug.Log("cohesion " + pos3);


            boid.GetComponent<Boids>().velocity += (pos1 + pos2 + pos3) * Time.deltaTime * 0.5f;
          
        }
        Vector3 temp = flock[5].position;
        Cam.position = new Vector3(temp.x, temp.y,temp.z - 100);
    }
    // seperation,alignment, cohesion
    Vector3 seperation(Transform boid)
    {
        Vector3 c = new Vector3(0,0,0);
        foreach (Transform t in flock)
        {
            if (t != boid)
            {
                float dist = Vector3.Distance(t.position, boid.position);
                if (dist < 2)
                {
                    c = c - (t.position - boid.position);
                 //   Debug.Log("seperation transform t " + t.position);
                }
            }

        }
        return c;
    }
    Vector3 alignment(Transform boid)
    {
        Vector3 c = new Vector3(0, 0, 0);
        foreach (Transform t in flock)
        {
            if (t != boid)
            {
                c = c + t.position;
                //Debug.Log("cohesion transform t " + t.position);
            }
        }
        c = c / (amount-1);
//
       // Debug.Log(c);
                return (c - boid.position)/2;
    }



    Vector3 cohesion(Transform boid)
    {
        Vector3 c = new Vector3(0, 0, 0);

         
        foreach (Transform t in flock)
        {
            if (t != boid)
            {
                c = c + t.GetComponent<Boids>().velocity;
            }
           // Debug.Log("cohesion transform t " + t.position);
        } 
        c = c / (amount - 1);
       // Debug.Log("cohesion " + c);
        return (c - boid.GetComponent<Boids>().velocity) /2f;
    }
} 
