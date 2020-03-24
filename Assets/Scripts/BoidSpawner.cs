using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoidGrid))]
public class BoidSpawner : MonoBehaviour {
    public GameObject boidPrefab;
    public Transform spawnParent;

    public float spawnRadius = 5f;
    public int numSpawns = 50;

    private BoidGrid _boidGrid;

    void Awake(){
        //_boidGrid = GetComponent<BoidGrid>();
        var transformPos = transform.position;

        for (int i = 0; i < numSpawns; i++) {
            Vector3 spawnPos = transformPos + Random.insideUnitSphere * spawnRadius;

            var newBoid = Instantiate(boidPrefab, spawnPos, Random.rotation);
            newBoid.transform.parent = spawnParent;
        }
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}