using UnityEngine;

public interface IInteractionManager : IService
{
    void Subscribe(IInteractable _element, RectTransform _key);
    void Unsubscribe(RectTransform _key);
}

