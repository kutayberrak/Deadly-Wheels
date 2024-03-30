using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public Transform spawnPointList;
    public GameObject zombiePrefab;

    void Start()
    {
        foreach (Transform spawnPoint in spawnPointList)
        {
            Instantiate(zombiePrefab, spawnPoint.position, Quaternion.Euler(0, -90, 0));
        }
    }
}