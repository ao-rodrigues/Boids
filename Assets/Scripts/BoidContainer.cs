using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public class BoidContainer : MonoBehaviour {
    private Boid boid;
    public float containerRadius = 10;
    public float boundaryForce;
    
    // Start is called before the first frame update
    void Start(){
        boid = GetComponent<Boid>();
    }

    // Update is called once per frame
    void Update(){
        if (boid.transform.position.magnitude > containerRadius) {
            boid.velocity += transform.position.normalized * ((containerRadius - boid.transform.position.magnitude) * Time.deltaTime * boundaryForce);
        }
    }
}