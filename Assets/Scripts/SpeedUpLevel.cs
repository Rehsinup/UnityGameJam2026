using UnityEngine;
using System.Collections;

public class SpeedUpLevel : MonoBehaviour
{
    [SerializeField] private float acceleration = 1f;

    [Tooltip("Multiplicateur (> 1)")]
    public float tauxAcceleration = 1.1f;

    public float interval = 10f;

    public float Acceleration => acceleration;

    private void Start()
    {
        StartCoroutine(AccelerationRoutine());
    }

    private IEnumerator AccelerationRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            acceleration *= tauxAcceleration;
        }
    }
}
