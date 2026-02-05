using UnityEngine;
using System.Collections;

public class SpeedUpLevel : MonoBehaviour
{
    [SerializeField] private float acceleration = 1f;

    ProceduralMove move;

    [Tooltip("Multiplicateur (> 1)")]
    public float tauxAcceleration = 1.1f;

    public float interval = 10f;

    public float Acceleration => acceleration;

    private void Start()
    {
        StartCoroutine(AccelerationRoutine());
        move = FindAnyObjectByType<ProceduralMove>();
    }

    private IEnumerator AccelerationRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            acceleration *= tauxAcceleration;
            FEUR();
            Finder();
        }
    }

    private void FEUR()
    {
        if (move != null)
        {
            move.moveSpeed *= acceleration;
        }
    }

    private void Finder()
    {
        move = FindAnyObjectByType<ProceduralMove>();
    }
}
