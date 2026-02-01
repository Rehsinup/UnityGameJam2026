using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(PlayerCharacter player, int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (player != null)
        {
            Vector3 pos = player.transform.position;
            player.transform.position = new Vector3(pos.x, pos.y - 1f, pos.z);
        }

        Debug.Log($"{player.name} Vie : {currentHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log($"{player.name} Game Over !");
        }
    }
}
