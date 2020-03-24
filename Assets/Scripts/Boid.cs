using System.Linq;
using UnityEngine;

public class Boid : MonoBehaviour {
    #region Field Declarations
    
    [HideInInspector] 
    public Vector3 velocity;

    private BoidSettings _settings;

    private Vector3 _acceleration;
    
    private Vector3 _perceivedCentre;
    private Vector3 _perceivedVelocity;
    private Vector3 _displacement;
    
    #endregion

    public void Initialize(BoidSettings settings){
        _settings = settings;
        velocity = transform.forward * settings.maxSpeed;
    }

    public void SetForces(Vector3 perceivedCentre, Vector3 perceivedVelocity, Vector3 displacement){
        _perceivedCentre = perceivedCentre;
        _perceivedVelocity = perceivedVelocity;
        _displacement = displacement;
    }

    // Works like Update but it's directly called by BoidManager as soon as we have the computation results
    public void UpdateBoid(){
        _acceleration = Vector3.zero;
        
        // Add acceleration forces
        _acceleration += SteerTowards(_perceivedCentre) * _settings.cohesionWeight;
        _acceleration += SteerTowards(_perceivedVelocity) * _settings.alignmentWeight;
        _acceleration += SteerTowards(_displacement) * _settings.separationWeight;
        
        if (HeadedForCollision()) {
            Vector3 clearDir = FindClearDirection();
            _acceleration += SteerTowards(clearDir) * _settings.avoidCollisionWeight;
        }

        velocity += _acceleration * Time.deltaTime;
        Vector3.ClampMagnitude(velocity, _settings.maxSpeed);
        
        transform.position += velocity * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(velocity);
    }

    private bool HeadedForCollision(){
        var cachedTransform = transform;
        
        return Physics.SphereCast(cachedTransform.position, _settings.boundsRadius, cachedTransform.forward, out _,
            _settings.collisionAwarenessRadius, _settings.obstacleMask);
    }

    private Vector3 FindClearDirection(){
        Vector3[] pointsInSphere = BoidUtils.PointsInSphere;
        Transform cachedTransform = transform;

        foreach (var point in pointsInSphere) {
            Vector3 dir = cachedTransform.TransformDirection(point);
            Ray ray = new Ray(cachedTransform.position, dir);

            if (!Physics.SphereCast(ray, _settings.boundsRadius, _settings.collisionAwarenessRadius,
                _settings.obstacleMask)) {
                return dir;
            }
        }

        return cachedTransform.forward;
    }

    private Vector3 SteerTowards(Vector3 dir){
        // Multiply the normalized dir by the maxVelocity to bring it to the same scale as current velocity
        // Subtract current velocity to get the actual increase/decrease in velocity
        Vector3 velocityDiff = dir.normalized * _settings.maxSpeed - velocity;

        // Clamp it to allow for smooth changes in acceleration
        return Vector3.ClampMagnitude(velocityDiff, _settings.maxSteerForce);
    }
}