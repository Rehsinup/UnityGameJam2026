using UnityEngine;
using UnityEngine.UI;

public class PressSpace : MonoBehaviour
{
    public Image image;

    [Header("Scale")]
    public float minScale = 0.8f;
    public float maxScale = 1.2f;

    [Header("Alpha")]
    public float minAlpha = 0.3f;
    public float maxAlpha = 1f;

    [Header("Speed")]
    public float speed = 2f;

    private Vector3 baseScale;

    void Start()
    {
        baseScale = transform.localScale;
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);

        // SCALE
        float scale = Mathf.Lerp(minScale, maxScale, t);
        transform.localScale = baseScale * scale;

        // ALPHA
        Color c = image.color;
        c.a = Mathf.Lerp(minAlpha, maxAlpha, t);
        image.color = c;
    }
}
