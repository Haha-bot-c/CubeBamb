using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CubeColorChanger : MonoBehaviour
{
    private Color _originalColor;
    private Renderer _cubeRenderer;

    private void Start()
    {
        _cubeRenderer = GetComponent<Renderer>();
        _originalColor = _cubeRenderer.material.color;
    }

    public void ChangeColor()
    {
        Color randomColor = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);
        _cubeRenderer.material.color = randomColor;
    }

    public void ReturnColor()
    {
        _cubeRenderer.material.color = _originalColor;
    }
}
