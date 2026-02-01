using UnityEngine;

public class SpeedUpLevel : MonoBehaviour
{
    BallMovement meteor;


    public float acceleration = 1f;

    [Tooltip("Multiplicateur (> 1)")]
    [SerializeField] float tauxAcceleration = 1.1f;

    [SerializeField] float interval = 10f;



    private void Start()
    {
        InvokeRepeating(nameof(IncreaseAcceleration), interval, interval);

        meteor = FindFirstObjectByType<BallMovement>();
    }

    private void Update()
    {
        Debug.Log(acceleration);
    }

    private void IncreaseAcceleration()
    {
        acceleration *= tauxAcceleration;
        meteor.speed *= acceleration;
    }
}
