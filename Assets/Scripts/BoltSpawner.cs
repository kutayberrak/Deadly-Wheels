using UnityEngine;

public class BoltSpawner : MonoBehaviour
{
    public Transform spawnPointList;
    public GameObject boltPrefab;

    void Start()
    {
        foreach (Transform spawnPoint in spawnPointList)
        {
            Instantiate(boltPrefab, spawnPoint.position, Quaternion.Euler(0, -90, 0));
        }
    }
}
