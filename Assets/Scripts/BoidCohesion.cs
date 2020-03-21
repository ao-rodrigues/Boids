using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public class BoidCohesion : MonoBehaviour {

    private Boid boid;
    
    // Start is called before the first frame update
    void Start(){
        boid = GetComponent<Boid>();
    }

    // Update is called once per frame
    void Update(){
    }
}