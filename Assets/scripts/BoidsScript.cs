using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsScript : MonoBehaviour
{
    public float Speed;
    public Transform prefab;
    public int amount;
    public Transform[] flock;
    Vector3 pos1, pos2, pos3;
    // Start is called before the first frame update
    void Start()
    {
        flock = new Transform[amount];
        // spawn 
        for (int i = 0; i < amount; i++)
        {

            flock[i] = Instantiate(prefab, new Vector3(Random.Range(0, 5), Random.Range(0, 5), Random.Range(0, 5)), Quaternion.identity);
          
        }
        foreach (Transform boid in flock)
        {
            boid.GetComponent<Rigidbody>().velocity = transform.forward * Speed;
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

            boid.GetComponent<Rigidbody>().velocity = boid.GetComponent<Rigidbody>().velocity + seperation(boid) + alignment(boid) + cohesion(boid);
          // als je ze niet heeb en weer wil laten schommelen en ze statietsch op een positie wil laten flokken haal dit weg uit de lijn hierboven  (boid.GetComponent<Rigidbody>().velocity = transform.forward * Speed);
          
        }
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
                if (dist <= 2)
                {
                    c = c + (t.position - boid.position);
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
                c = c + boid.position;
            }
        }
        c = c / 100;

                return (c - boid.position)/100;
    }



    Vector3 cohesion(Transform boid)
    {
        Vector3 c = new Vector3(0, 0, 0);


        foreach (Transform t in flock)
        {
            if (t != boid)
            {
                c = c + boid.GetComponent<Rigidbody>().velocity;
            }
        } 
        c = c / (amount - 1); 

        return (c - boid.GetComponent<Rigidbody>().velocity) /100;
    }
}
