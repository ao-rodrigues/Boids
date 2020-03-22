using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour {
    public BoidGrid _boidGrid;
    public GameObject boidPrefab;
    public Transform boidsParent;
    public int numSpawns;

    void Awake(){
        //_boidGrid = GetComponent<BoidGrid>();
        var transformPos = transform.position;

        for (int i = 0; i < numSpawns; i++) {
            Vector3 spawnPos = new Vector3(Random.Range(transformPos.x, _boidGrid.gridSettings.numCellsX),
                Random.Range(transformPos.y, _boidGrid.gridSettings.numCellsY),
                Random.Range(transformPos.z, _boidGrid.gridSettings.numCellsZ));

            var newBoid = Instantiate(boidPrefab, spawnPos, Random.rotation);
            newBoid.transform.parent = boidsParent;
        }
    }
}