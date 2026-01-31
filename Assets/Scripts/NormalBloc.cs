using UnityEngine;

public class DamageBlock : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health == null) return;

        health.TakeDamage(damage);
    }
}
