using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public class BoidAlignment : MonoBehaviour {
    private Boid boid;
    private Boid[] boids;

    // Start is called before the first frame update
    void Start(){
        boid = GetComponent<Boid>();
        boids = FindObjectsOfType<Boid>();
    }

    // Update is called once per frame
    void Update(){
        Vector3 perceivedVelocity = Vector3.zero;
        var found = 0;

        foreach (var b in boids.Where(b => b != this.boid)) {
            var dist = b.transform.position - boid.transform.position;

            if (dist.magnitude <= boid.awarenessRadius) {
                perceivedVelocity += b.velocity;
                found++;
            }
        }

        if (found > 0) {
            perceivedVelocity /= found;
            boid.velocity += Vector3.Lerp(boid.velocity, perceivedVelocity, Time.deltaTime);
        }
    }
}