using UnityEngine;
using UnityEngine.UI;

public class CanvasTranslator
{
    private readonly CanvasScaler scaler = null;

    public Vector2 ReferenceResolution => scaler.referenceResolution;

    public CanvasTranslator(Canvas _canvas)
    {
        scaler = _canvas.GetComponent<CanvasScaler>();
    }

    public Vector2 CanvasToScreenPoint(Vector2 _point)
    {
        _point.x = (_point.x / scaler.referenceResolution.x) * Screen.width;
        _point.y = (_point.y / scaler.referenceResolution.y) * Screen.height;
        return _point;
    }

    public Vector2 ScreenToCanvasPoint(Vector2 _point)
    {
        _point.x *= scaler.referenceResolution.x / Screen.width;
        _point.y *= scaler.referenceResolution.y / Screen.height;
        return _point;
    }
}