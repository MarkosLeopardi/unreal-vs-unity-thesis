using UnityEngine;
using System.Collections;
using System.Collections.Generic;

    /// <summary>
    /// PathNode represents a waypoint in the scene for AI navigation.
    /// Each PathNode can reference other PathNodes, allowing for dynamic path selection.
    /// </summary>
public class PathNode : MonoBehaviour
{
    // List of connected PathNodes that this node can lead to.
    // Assign these in the inspector to create a network of waypoints.
    [SerializeField] private List<PathNode> pathNodes = new List<PathNode>();

    /// <summary>
    /// Returns a random PathNode from the list of connected nodes.
    /// Used by AI agents to select the next waypoint.
    /// </summary>
    /// <returns>
    /// A randomly selected PathNode from the pathNodes list,
    /// or null if there are no connected nodes.
    /// </returns>
    public PathNode GetRandomPathNode() {
        // If there are no connected nodes, return null
        if (pathNodes == null || pathNodes.Count == 0)
            return null;

        // Select a random index within the bounds of the pathNodes list
        int index = Random.Range(0, pathNodes.Count);

        // Return the randomly selected PathNode
        return pathNodes[index];
    }
}
