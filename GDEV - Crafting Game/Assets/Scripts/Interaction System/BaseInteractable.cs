using UnityEngine;

public abstract class BaseInteractable : IInteractable
{
    protected RectTransform element = null;
    protected CanvasTranslator translator = null;

    public BaseInteractable() { }
    public BaseInteractable(RectTransform _element) => Setup(_element);

    public void Setup(RectTransform _element)
    {
        var interactionManager = ServiceLocator.Instance.Get<InteractionManager>();

        element = _element;
        translator = interactionManager.Translator;

        interactionManager.Subscribe(this, _element);
    }

    public virtual void OnExit(Vector2 _mousePos) { }

    public virtual void OnEnter(Vector2 _mousePos) { }

    public virtual void OnClick(Vector2 _mousePos) { }

    public virtual void OnRelease(Vector2 _mousePos) { }

    /// <summary>
    /// Caution! Only works when a RectTransform's pivot is in the middle for now!
    /// </summary>
    /// <returns>Whether the mouse position overlaps with the RectTransform of this interactable.</returns>
    public bool Overlaps(Vector2 _mousePos)
    {
        var pos = element.position;
        var size = translator.CanvasToScreenPoint(element.sizeDelta);

        var overlapX = _mousePos.x >= pos.x - (size.x / 2) && _mousePos.x <= pos.x + (size.x / 2);
        var overlapY = _mousePos.y >= pos.y - (size.y / 2) && _mousePos.y <= pos.y + (size.y / 2);

        return overlapX && overlapY;
    }
}
