using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTrigger : MonoBehaviour
{
    [Header("Spawn Prefabs")]
    public GameObject[] prefabs;
    public float spawnHeight = 35f;

    private SpeedUpLevel speedUpLevel;
    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider other)

    {
        if (hasSpawned) return;

        if (other.CompareTag("Collider"))
        {
            hasSpawned = true; 
            SpawnPrefab();
        }
    }

    private void SpawnPrefab()
    {
        if (prefabs.Length == 0)
        {
            Debug.LogWarning("Aucun prefab assigné !");
            return;
        }

        int index = Random.Range(0, prefabs.Length);
        GameObject prefabToSpawn = prefabs[index];

        Vector3 spawnPos = new Vector3(0, spawnHeight, 0);
        GameObject spawned = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        ColorBloc bloc = spawned.GetComponent<ColorBloc>();
        if (bloc != null)
        {
            bloc.Initialize();
        }

        speedUpLevel = FindAnyObjectByType<SpeedUpLevel>();
        if (speedUpLevel != null)
        {
            ProceduralMove move = spawned.GetComponent<ProceduralMove>();
            if (move != null)
            {
                move.moveSpeed *= speedUpLevel.Acceleration;
            }
        }

        Debug.Log($"Spawned {prefabToSpawn.name} à {spawnPos}");
    }
}
