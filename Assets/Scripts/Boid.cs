using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {
    [HideInInspector]
    public Vector3 velocity;
    
    //public Vector3 generalDirection = new Vector3(1, 0, 0);
    public float maxVelocity = 5;
    public float awarenessRadius = 5;

    private void Start(){
        velocity = transform.forward * maxVelocity;
    }

    // Update is called once per frame
    void Update(){
        if (velocity.magnitude > maxVelocity) {
            velocity = velocity.normalized * maxVelocity;
        }
        
        transform.position += velocity * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(velocity);
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, awarenessRadius);
    }
}