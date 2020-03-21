using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public class BoidSeparation : MonoBehaviour {
    public float minDistance = 0.25f;

    private Boid boid;
    private Boid[] boids;
    
    // Start is called before the first frame update
    void Start(){
        boid = GetComponent<Boid>();
        boids = FindObjectsOfType<Boid>();
    }

    // Update is called once per frame
    void Update(){
        Vector3 displacement = Vector3.zero;
        
        foreach (var b in boids.Where(b => b != this.boid)) {
            var dist = b.transform.position - boid.transform.position;

            if (dist.magnitude < minDistance) {
                displacement -= dist;
            }
        }

        boid.velocity += displacement;
    }
}