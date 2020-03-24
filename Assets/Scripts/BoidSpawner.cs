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

        /*
        // Get the smallest dimension to make sure boids won't spawn out of the grid, independently of its' dimensions
        float minDimension = Mathf.Min(_boidGrid.gridSettings.numCellsX,
            Mathf.Min(_boidGrid.gridSettings.numCellsY, _boidGrid.gridSettings.numCellsZ));

        float spawnRadiusX = transformPos.x +
                             minDimension * _boidGrid.gridSettings.cellSize / 3f;
        float spawnRadiusY = transformPos.y +
                             minDimension * _boidGrid.gridSettings.cellSize / 3f;
        float spawnRadiusZ = transformPos.z +
                             minDimension * _boidGrid.gridSettings.cellSize / 3f;
                             */

        for (int i = 0; i < numSpawns; i++) {
            /*
            Vector3 spawnPos = new Vector3(Random.Range(transformPos.x, spawnRadiusX),
                Random.Range(transformPos.y, spawnRadiusY),
                Random.Range(transformPos.z, spawnRadiusZ));
                */

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