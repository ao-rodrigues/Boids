using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewBoidSettings", menuName = "Boids/Boid Settings", order = 1)]
public class BoidSettings : ScriptableObject {
    // How much the boid wants to avoid collisions
    public float separationWeight = 1f;
    
    // How much the boid wants to match its velocity with its flockmates
    public float alignmentWeight = 1f;
    
    // How much the boid wants stay near the center of the flock
    public float cohesionWeight = 1f;

    public float maxSteerForce = 3f;
    
    [FormerlySerializedAs("maxVelocity")] 
    public float maxSpeed = 5f;
    public float awarenessRadius = 5f;
    public float minDistance = 0.4f;
    public float alignmentRate = 0.01f;

    [Header("Collision Settings")] 
    public LayerMask obstacleMask;
    public float boundsRadius = .3f;
    public float collisionAwarenessRadius = 5;
    public float avoidCollisionWeight = 10;
}