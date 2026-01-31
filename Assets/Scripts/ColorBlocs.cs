using System.Drawing;
using UnityEngine;

public class ColorBloc : MonoBehaviour
{
    [Tooltip("0 = Rouge, 1 = Vert, 2 = Bleu, 3 = Jaune")]
    public int ColorIndex;
    private Material material;
    [SerializeField] private GameObject Cubes;
    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player == null) return;

        if (player.MaskIndex == ColorIndex)
        {
            Debug.Log(" Bonne couleur");
            gameObject.SetActive(false);
            material.SetColor("_Basecolor", UnityEngine.Color.white);
        }
        else
        {
            Debug.Log(" Mauvaise couleur");
        }
    }
}
