using UnityEngine;
using System.Collections;

public class BallSpawner : MonoBehaviour
{
    [Header("Spawn settings")]
    public GameObject ballPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 1f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnBall();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnBall()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
    }
}
