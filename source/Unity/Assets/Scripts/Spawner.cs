using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    // List of prefabs to spawn from
    [SerializeField]
    private List<GameObject> prefabs = new List<GameObject>();
    
    void Start() {
        SpawnRandom();
    }

    // Spawns a random prefab at the spawner's position and rotation
    public void SpawnRandom()
    {
        if (prefabs == null || prefabs.Count == 0)
            return;

        int index = Random.Range(0, prefabs.Count);
        GameObject prefabToSpawn = Instantiate(prefabs[index], transform.position, transform.rotation);
        //Set the parent of the spawned object

        prefabToSpawn.transform.SetParent(transform);
    }
}
