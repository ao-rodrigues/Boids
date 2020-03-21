using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public class BoidCohesion : MonoBehaviour {
    private Boid boid;
    private Boid[] boids;

    // Start is called before the first frame update
    void Start(){
        boid = GetComponent<Boid>();
        boids = FindObjectsOfType<Boid>();
    }

    // Update is called once per frame
    void Update(){
        Vector3 perceivedCentre = Vector3.zero;
        var found = 0;

        foreach (var b in boids.Where(b => b != this.boid)) {
            var dist = b.transform.position - boid.transform.position;

            if (dist.magnitude <= boid.awarenessRadius) {
                perceivedCentre += dist;
                found++;
            }
        }

        if (found > 0) {
            perceivedCentre /= found;
            boid.velocity += Vector3.Lerp(Vector3.zero, perceivedCentre, perceivedCentre.magnitude / boid.awarenessRadius);
        }
    }
}