using UnityEngine;

public class ColorBloc : MonoBehaviour
{
    [Tooltip("Couleur du cube")]
    public Color blocColor;

    private Material material;
    public int damage = 1;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.color = blocColor;
    }

    void Update()
    {
        DitherCubes();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player == null) return;

        if (player.CurrentMaskColor != material.color)
        {
            Health health = player.GetComponent<Health>();
            if (health != null)
                health.TakeDamage(player, damage);
        }
    }

    private void DitherCubes()
    {
        bool anyPlayerHasColor = false;
        foreach (var player in PlayerCharacter.AllPlayers)
        {
            if (player.CurrentMaskColor == material.color)
            {
                anyPlayerHasColor = true;
                break;
            }
        }

        Color c = material.color;
        c.a = anyPlayerHasColor ? 0.1f : 1f;
        material.SetColor("_BaseColor", c);
    }
}
