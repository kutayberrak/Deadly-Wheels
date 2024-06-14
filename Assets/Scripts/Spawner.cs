using UnityEngine;
using DG.Tweening;

public class Spawner : MonoBehaviour
{
    public Transform zombieSpawnPointList;
    public Transform barrelSpawnPointList;
    public Transform boltSpawnPointList;
    public GameObject zombiePrefab;
    public GameObject barrelPrefab;
    public GameObject boltPrefab;

    private int spawnedZombieCount;
    public void SpawnPrefabs()
    {
        /*
        spawnedZombieCount = 0;
        foreach (Transform spawnPoint in zombieSpawnPointList)
        {
            Instantiate(zombiePrefab, spawnPoint.position, Quaternion.Euler(0, -90, 0));
            spawnedZombieCount += 1;
        }

        foreach (Transform spawnPoint in barrelSpawnPointList)
        {
            Instantiate(barrelPrefab, spawnPoint.position, Quaternion.Euler(0, 0, 0));
        }

        foreach (Transform spawnPoint in boltSpawnPointList)
        {
            Instantiate(boltPrefab, spawnPoint.position, Quaternion.Euler(0, -90, 0));
        }
        */
    }

    public int CalculateKilledZombies()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Enemy");
        int remainingZombies = zombies.Length;
        return spawnedZombieCount - remainingZombies;
    }

    public void DestroyAllPrefabs()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] barrels = GameObject.FindGameObjectsWithTag("Barrel");
        GameObject[] bolts = GameObject.FindGameObjectsWithTag("Bolt");

        foreach (GameObject enemy in zombies)
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
            DOTween.Kill(bolt.transform);
        }
    }
}