using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[ExecuteAlways] // fonctionne aussi hors Play
public class BackgroundColorChanging : MonoBehaviour
{
    [Header("Volume")]
    public Volume volume;

    [Header("Hue Control")]
    [Range(0f, 1f)]
    public float hue = 0f;
    [SerializeField] private float duration;

    private ColorAdjustments colorAdjustments;

    private void Start()
    {
        hue = 0f;
    }

    void OnEnable()
    {
        if (volume == null)
            return;

        // Important : instancier le profile pour éviter de modifier l'asset
        if (volume.profile == volume.sharedProfile)
            volume.profile = Instantiate(volume.sharedProfile);

        volume.profile.TryGet(out colorAdjustments);
    }

    void Update()
    {
        if (colorAdjustments == null)
            return;

        hue += Time.deltaTime / duration;
        hue = Mathf.Repeat(hue, 0.99f);

        Color color = Color.HSVToRGB(hue, 0.35f, 1f);
        colorAdjustments.colorFilter.value = color;
    }
}
