using UnityEngine;

public class BoundsScript : MonoBehaviour
{
    [Header("Bounds Assignment")]
    public GameObject boundA;
    public GameObject boundB;
    public GameObject boundC;
    public GameObject boundD;

    [Header("Tree Settings")]
    public GameObject treePrefab;
    [Range(1, 500)]
    public int numberOfTrees = 100;

    void Start()
    {
        if (treePrefab == null || boundA == null || boundB == null || boundC == null || boundD == null)
        {
            Debug.LogError("Assign all 4 bounds and the tree prefab in the inspector.");
            return;
        }
        GameObject[] bounds = { boundA, boundB, boundC, boundD };
        foreach (GameObject bound in bounds)
        {
            Bounds areaBounds;
            Renderer rend = bound.GetComponent<Renderer>();
            if (rend != null)
            {
                areaBounds = rend.bounds;
            }
            else
            {
                Collider col = bound.GetComponent<Collider>();
                if (col != null)
                {
                    areaBounds = col.bounds;
                }
                else
                {
                    Debug.LogWarning($"Bound {bound.name} has no Renderer or Collider.");
                    continue;
                }
            }
            for (int i = 0; i < numberOfTrees; i++)
            {
                float rx = Random.Range(areaBounds.min.x, areaBounds.max.x);
                float rz = Random.Range(areaBounds.min.z, areaBounds.max.z);
                float y = areaBounds.center.y;
                Vector3 pos = new Vector3(rx, y, rz);
                Instantiate(treePrefab, pos, Quaternion.identity, transform);
            }
        }
    }
}
