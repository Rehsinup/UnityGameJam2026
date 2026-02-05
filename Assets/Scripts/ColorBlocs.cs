using UnityEngine;

public class ColorBloc : MonoBehaviour
{
    [Tooltip("Couleur du cube")]
    public Color blocColor;

    public int damage = 1;

    private Material material;
    private SpriteRenderer childSprite;

    void Start()
    {
        Initialize();
    }


    public void Initialize()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            material = rend.material;
            material.color = blocColor;
        }

        childSprite = GetComponentInChildren<SpriteRenderer>();

        DitherCubes();
    }


    void Update()
    {
        DitherCubes();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player == null) return;

        bool someoneHasGoodColor = false;

        foreach (var p in PlayerCharacter.AllPlayers)
        {
            if (ColorMatches(p.CurrentMaskColor, blocColor))
            {
                someoneHasGoodColor = true;
                break;
            }
        }

        if (!someoneHasGoodColor)
        {
            Health health = player.GetComponent<Health>();
            if (health != null)
                health.TakeDamage(player, damage);
        }

        gameObject.SetActive(false);
    }

    private void DitherCubes()
    {
        bool shouldBeTransparent = false;

        foreach (var player in PlayerCharacter.AllPlayers)
        {
            if (ColorMatches(player.CurrentMaskColor, blocColor))
            {
                shouldBeTransparent = true;
                break;
            }
        }

        float targetAlpha = shouldBeTransparent ? 0.1f : 1f;

        if (material != null)
        {
            Color c = material.color;
            c.a = targetAlpha;
            material.SetColor("_BaseColor", c);
        }

        if (childSprite != null)
        {
            Color sc = childSprite.color;
            sc.a = targetAlpha;
            childSprite.color = sc;
        }
    }

    private bool ColorMatches(Color a, Color b, float tolerance = 0.01f)
    {
        return Mathf.Abs(a.r - b.r) < tolerance &&
               Mathf.Abs(a.g - b.g) < tolerance &&
               Mathf.Abs(a.b - b.b) < tolerance &&
               Mathf.Abs(a.a - b.a) < tolerance;
    }
}
