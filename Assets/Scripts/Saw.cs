using UnityEngine;

public class Saw : MonoBehaviour
{
    public float speed = 100f;

    public enum RotationDirection
    {
        Horaire,
        AntiHoraire
    }

    public RotationDirection direction = RotationDirection.Horaire;

    void Update()
    {
        float dir = direction == RotationDirection.Horaire ? -1f : 1f;
        transform.Rotate(0f, 0f, dir * speed * Time.deltaTime);
    }
}
