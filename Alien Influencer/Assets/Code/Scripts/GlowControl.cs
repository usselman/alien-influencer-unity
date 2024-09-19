using UnityEngine;

public class GlowControl : MonoBehaviour
{
    public float highlightMultiply = 1.5f;
    private Color originalColor;
    private Material material;

    void Start()
    {

        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;

        originalColor = material.GetColor("_EmissionColor");
        Glow();
    }

    public void Glow()
    {
        material.SetColor("_EmissionColor", originalColor * highlightMultiply);
        DynamicGI.SetEmissive(GetComponent<Renderer>(), originalColor * highlightMultiply);
    }

    public void NoGlow()
    {
        material.SetColor("_EmissionColor", originalColor);
        DynamicGI.SetEmissive(GetComponent<Renderer>(), originalColor);
    }
}
