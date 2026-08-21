using UnityEngine;

public class AlphaChanger
{
    public void SetAlpha(Renderer renderer, float alpha)
    {
        Color color = renderer.material.color;
        color.a = alpha;
        renderer.material.color = color;
    }

    public void ResetAlpha(Renderer renderer)
    {
        SetAlpha(renderer, 1f);
    }
}