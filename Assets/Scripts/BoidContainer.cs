using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public class BoidContainer : MonoBehaviour {
    public float minDistToBoundary = 10;
    public float boundaryForce;
    public BoidGrid boidGrid;

    private Boid _boid;

    // Start is called before the first frame update
    void Start(){
        _boid = GetComponent<Boid>();
    }

    // Update is called once per frame
    void Update(){
        float containerRadius =
            Mathf.Min(boidGrid.gridSettings.numCellsX,
                Mathf.Min(boidGrid.gridSettings.numCellsY, boidGrid.gridSettings.numCellsZ)) *
            boidGrid.gridSettings.cellSize / 2f;

        if (_boid.transform.position.magnitude > containerRadius - minDistToBoundary) {
            _boid.velocity += transform.position.normalized *
                              ((containerRadius - _boid.transform.position.magnitude) * Time.deltaTime * boundaryForce);
        }
    }
}