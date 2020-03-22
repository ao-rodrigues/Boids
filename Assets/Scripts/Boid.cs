using System.Linq;
using UnityEngine;

public class Boid : MonoBehaviour {

    #region Field Declarations

    [HideInInspector] public Vector3 velocity;

    public float maxVelocity = 5;
    public float awarenessRadius = 5;

    #endregion


    private void Start(){
        velocity = transform.forward * maxVelocity;
    }

    // Works like Update but it's directly called by BoidManager
    public void UpdateBoid(){
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