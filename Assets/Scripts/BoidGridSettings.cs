using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoidGridSettings", menuName = "Boids/Boid Grid Settings", order = 1)]
public class BoidGridSettings : ScriptableObject {
    public int numCellsX = 10;
    public int numCellsY = 10;
    public int numCellsZ = 10;

    public int cellSize = 5;
}