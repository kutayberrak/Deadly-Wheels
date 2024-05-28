using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public Transform zombieSpawnPointList;
    public Transform barrelSpawnPointList;
    public Transform boltSpawnPointList;
    public GameObject zombiePrefab;
    public GameObject barrelPrefab;
    public GameObject boltPrefab;

    public void SpawnZombies()
    {
        foreach (Transform spawnPoint in zombieSpawnPointList)
        {
            Instantiate(zombiePrefab, spawnPoint.position, Quaternion.Euler(0, -90, 0));
        }

        foreach (Transform spawnPoint in barrelSpawnPointList)
        {
            Instantiate(barrelPrefab, spawnPoint.position, Quaternion.Euler(0, 0, 0));
        }

        foreach (Transform spawnPoint in boltSpawnPointList)
        {
            Instantiate(boltPrefab, spawnPoint.position, Quaternion.Euler(0, -90, 0));
        }
    }

    public void DestroyAllPrefabs()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] barrels = GameObject.FindGameObjectsWithTag("Barrel");
        GameObject[] bolts = GameObject.FindGameObjectsWithTag("Bolt");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        foreach (GameObject barrel in barrels)
        {
            Destroy(barrel);
        }

        foreach (GameObject bolt in bolts)
        {
            Destroy(bolt);
        }
    }
}