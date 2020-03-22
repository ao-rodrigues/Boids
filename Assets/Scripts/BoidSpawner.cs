using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour {

    public GameObject boidPrefab;
    public float spawnRadius = 10;
    public int numSpawns;
    
    void Awake(){
        for (int i = 0; i < numSpawns; i++) {
            Instantiate(boidPrefab, transform.position + Random.insideUnitSphere * spawnRadius, Random.rotation);
        }
    }
}