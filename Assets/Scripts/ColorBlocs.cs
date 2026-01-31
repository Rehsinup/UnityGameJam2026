using System.Drawing;
using UnityEngine;

public class ColorBloc : MonoBehaviour
{
    [Tooltip("0 = Rouge, 1 = Vert, 2 = Bleu, 3 = Jaune")]
    public int ColorIndex;

    private Material material;

    [SerializeField] private GameObject Cubes;

    public PlayerCharacter player;
    public Health health;

    public int damage = 1;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
        player = FindAnyObjectByType<PlayerCharacter>();
        health = FindAnyObjectByType<Health>();
    }

    void Update()
    {
        DitherCubes();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (player == null) return;

        if (player.MaskIndex == ColorIndex)
        {
            Debug.Log("Bonne couleur");
        }
        else if (player.MaskIndex != ColorIndex)
        {
            Debug.Log("Mauvaise couleur");
            if ( other.CompareTag("Player"))
                health.TakeDamage(damage);
                gameObject.SetActive(false);
        }
    }


    private void DitherCubes()
    {
        if (player.MaskIndex == ColorIndex)
        {
            Debug.Log("Dither On");
            UnityEngine.Color c = material.GetColor("_BaseColor");
            c.a = 0.1f;
            material.SetColor("_BaseColor", c);
        }
        else
        {
            Debug.Log("Dither Off");
            UnityEngine.Color c = material.GetColor("_BaseColor");
            c.a = 1f;
            material.SetColor("_BaseColor", c);
        }
          

    }

}
