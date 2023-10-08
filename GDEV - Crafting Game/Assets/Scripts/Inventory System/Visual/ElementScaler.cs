using UnityEngine;

public class ElementScaler
{
    public readonly RectTransform Transform = null;
    public readonly float Recovery = 0f;

    private float scale = 1f;

    public ElementScaler(RectTransform _transform, float _recovery)
    {
        Transform = _transform;
        Recovery = _recovery;
    }

    public void Scale(float _scale)
    {
        scale = _scale;
        Transform.localScale = Vector3.one * _scale;
    }

    public void Tick(float _deltaTime)
    {
        Scale(Mathf.Lerp(scale, 1f, _deltaTime / Recovery));
    }
}
