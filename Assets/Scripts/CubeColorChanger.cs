using UnityEngine;

public class CubeColorChanger
{
    public void SetColor(Cube cube)
    {
        if (cube.Renderer != null)
        {
            cube.Renderer.material.color = Random.ColorHSV();
        }
    }

    public void ResetColor(Cube cube)
    {
        if (cube.Renderer != null)
        {
            cube.Renderer.material.color = Color.gray;
        }
    }
}