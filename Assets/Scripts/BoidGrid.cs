using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoidGrid : MonoBehaviour {
    
    public BoidGridSettings gridSettings;

    private LinkedList<Boid>[,,] _cells;

    // Used to compensate for the differences in word and local axis origins
    private Vector3 _axisDiff;

    private void Awake(){
        _cells = new LinkedList<Boid>[gridSettings.numCellsX, gridSettings.numCellsY, gridSettings.numCellsZ];
    }

    private void Start(){
        float offsetX = (gridSettings.numCellsX * gridSettings.cellSize) / 2f;
        float offsetY = (gridSettings.numCellsY * gridSettings.cellSize) / 2f;
        float offsetZ = (gridSettings.numCellsZ * gridSettings.cellSize) / 2f;

        Vector3 transformPos = transform.position;
        // Calculate difference between Unity's world axis origin and the grid's local axis origin
        _axisDiff = new Vector3(transformPos.x - offsetX, transformPos.y - offsetY, transformPos.z - offsetZ);
    }

    private Vector3 GetLocalPos(Vector3 worldPos){
        return worldPos + _axisDiff;
    }

    public void Add(Boid boid){
        Vector3 boidPos = GetLocalPos(boid.transform.position);

        int x = (int) (boidPos.x / gridSettings.cellSize);
        int y = (int) (boidPos.y / gridSettings.cellSize);
        int z = (int) (boidPos.z / gridSettings.cellSize);

        if (_cells[x, y, z] == null) {
            _cells[x, y, z] = new LinkedList<Boid>();
        }

        _cells[x, y, z].AddLast(boid);
    }

    public Boid[] GetBoidsInSameCell(Boid boid){
        Vector3 boidPos = GetLocalPos(boid.transform.position);

        int x = (int) (boidPos.x / gridSettings.cellSize);
        int y = (int) (boidPos.y / gridSettings.cellSize);
        int z = (int) (boidPos.z / gridSettings.cellSize);

        return _cells[x, y, z].ToArray();
    }

    public void Move(Boid boid, Vector3 oldPos){
        Vector3 localOldPos = GetLocalPos(oldPos);

        // Get old coords
        int oldX = (int) (localOldPos.x / gridSettings.cellSize);
        int oldY = (int) (localOldPos.y / gridSettings.cellSize);
        int oldZ = (int) (localOldPos.z / gridSettings.cellSize);

        // Get new coords
        var newPos = GetLocalPos(boid.transform.position);
        int newX = (int) (newPos.x / gridSettings.cellSize);
        int newY = (int) (newPos.y / gridSettings.cellSize);
        int newZ = (int) (newPos.z / gridSettings.cellSize);

        // Check if they've changed
        if (oldX == newX && oldY == newY && oldZ == newZ) return;

        // Remove boid from previous cell and add to new cell
        _cells[oldX, oldY, oldZ].Remove(boid);
        Add(boid);
    }


    private void OnDrawGizmosSelected(){
        if (_cells == null) return;

        Gizmos.color = Color.green;
        Vector3 origin = GetLocalPos(transform.position);

        for (int z = 0; z < _cells.GetLength(2); z++) {
            for (int y = 0; y < _cells.GetLength(1); y++) {
                for (int x = 0; x < _cells.GetLength(0); x++) {
                    Vector3 cellPos = new Vector3(origin.x + x * gridSettings.cellSize + gridSettings.cellSize / 2f,
                        origin.y + y * gridSettings.cellSize + gridSettings.cellSize / 2f,
                        origin.z + z * gridSettings.cellSize + gridSettings.cellSize / 2f);

                    Gizmos.DrawWireCube(cellPos,
                        new Vector3(gridSettings.cellSize, gridSettings.cellSize, gridSettings.cellSize));
                }
            }
        }
    }
}