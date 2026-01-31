using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProceduralTrigger : MonoBehaviour
{

    public GameObject prefab;
    public float spawnHeight = 35f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collider"))
        {
            Instantiate(prefab, new Vector3( 0, spawnHeight, 0), Quaternion.identity);
        }
    }
}

