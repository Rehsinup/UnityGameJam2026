using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Color color;
    private float speed;
    private bool vertical;
    public void Init(Color c, float s, bool moveVertical)
    {
        color = c;
        speed = s;
        vertical = moveVertical;
        GetComponent<Renderer>().material.color = color;
    }

    void Update()
    {
        if (vertical)
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        else
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            Renderer blockRenderer = other.GetComponent<Renderer>();
            if (blockRenderer)
            {
                Color blockColor = blockRenderer.material.color;
                if (blockColor != color)
                {
                    Destroy(gameObject);
                }
             
            }
        }
    }
}
