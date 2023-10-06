using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractable : IInteractable
{
    protected RectTransform element = null;

    public BaseInteractable(RectTransform _element)
    {
        element = _element;
        ServiceLocator.Instance.Get<InteractionManager>().Subscribe(this, _element);
    }

    public virtual void OnClick(Vector2 _mousePos) { }

    public virtual void OnRelease(Vector2 _mousePos) { }
}
