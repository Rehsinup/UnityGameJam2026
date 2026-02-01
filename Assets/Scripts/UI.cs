using UnityEngine;
using TMPro;


public class UI : MonoBehaviour
{
    public Health playerHealth;
    public TMP_Text healthText;



    private void Update()
    {
        if (playerHealth == null) return;

        healthText.text = "Vie : " + playerHealth.currentHealth;
    }
}
