using System;
using UnityEngine;


public class Health : MonoBehaviour
{

    [Header("Health")]
    public int maxHealth = 3;
    public int currentHealth;

    public int damage = 1;


    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        PlayerCharacter player = GetComponent<PlayerCharacter>();
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        player.transform.position = new Vector3(0, player.transform.position.y-1,-1);

        Debug.Log(" Vie : " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Game Over");
        }
    }

}
