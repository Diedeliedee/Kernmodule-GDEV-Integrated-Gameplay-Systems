using UnityEngine;

public interface IInteractionManager : IService
{
    public void Subscribe(IInteractable _element, RectTransform _key);

    public void Unsubscribe(RectTransform _key);

    public Vector2 CanvasToScreenPoint(Vector2 _point);

    public Vector2 ScreenToCanvasPoint(Vector2 _point);
}

