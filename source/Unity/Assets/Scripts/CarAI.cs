using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// CarAI controls a car GameObject using Unity's NavMeshAgent.
/// The car follows a path defined by PathNodes and stops if another vehicle is detected in front.
/// </summary>
public class CarAI : MonoBehaviour {
    // The maximum distance to check for vehicles in front of the car
    [SerializeField] private float detectionDistance = 10f;

    // Offset to adjust the raycast origin (so it starts above ground)
    [SerializeField] private Vector3 offset = new Vector3(0, 1.0f, 0);

    // Layer mask to filter out only vehicle objects for collision detection
    [SerializeField] private LayerMask vehicleLayer;

    // Reference to the current target PathNode (waypoint)
    [SerializeField] private PathNode targetNode;

    // Reference to the NavMeshAgent component used for movement
    private NavMeshAgent navMeshAgent;

    void Start() {
        // Ensure the NavMeshAgent component is attached to this GameObject
        if (GetComponent<NavMeshAgent>() == null) {
            Debug.LogError("NavMeshAgent component is missing on this GameObject.");
            return;
        } else {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
    }

    void Update() {
        // Check if there is a vehicle in front using raycasting
        if(CheckForVehicleInFront()) {
            // If a vehicle is detected in front, stop the NavMeshAgent to avoid collision
            navMeshAgent.isStopped = true;   
            Debug.Log("Vehicle detected in front, stopping the NavMeshAgent.");
        }
        else {
            // If no vehicle is detected, continue moving towards the target node
            if (targetNode != null) {
                // Resume movement if previously stopped
                if (navMeshAgent.isStopped == true) {
                    navMeshAgent.isStopped = false;
                }
                // Set the destination to the current target node's position
                navMeshAgent.SetDestination(targetNode.transform.position);
            }

            // Check if the NavMeshAgent has reached the target node
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
                // Get a new random PathNode from the current node's connections
                PathNode newTargetNode = targetNode.GetRandomPathNode();
                if (newTargetNode != null) {
                    targetNode = newTargetNode; // Update the target node to the new one
                } else {
                    Debug.LogWarning("No valid PathNode found.");
                }                
            }
        }
    }

    // Draw the ray in the Scene view for debugging purposes
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + offset, transform.forward * detectionDistance);
    }

    /// <summary>
    /// Checks for vehicles in front of the car using a raycast.
    /// Returns true if another vehicle is detected within detectionDistance.
    /// </summary>
    /// <returns>True if a vehicle is detected in front, false otherwise.</returns>
    bool CheckForVehicleInFront() {
        Ray ray = new Ray(transform.position + offset, transform.forward);
        RaycastHit hit;

        // Perform the raycast using the vehicleLayer mask to detect only vehicles
        if (Physics.Raycast(ray, out hit, detectionDistance, vehicleLayer)) {
            return true; // Vehicle detected
        }
        
        return false; // No vehicle detected
    }
}
