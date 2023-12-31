﻿using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : IInteractionManager, IUpdatable
{
    private readonly CanvasTranslator translator = null;
    private readonly Dictionary<int, IInteractable> subscribedElements = new();

    private IInteractable clickedElement = null;
    private IInteractable hoveringElement = null;

    public InteractionManager(Canvas _canvas)
    {
        translator = new(_canvas);
    }

    public void OnFixedUpdate() { }

    public void OnStart() { }

    public void OnUpdate()
    {
        var mousePos = Input.mousePosition;

        //  Regulating hovering.
        if (hoveringElement == null && OverlapsWithElement(mousePos, out hoveringElement))
        {
            hoveringElement.OnEnter(mousePos);
        }
        else if (hoveringElement != null && !hoveringElement.Overlaps(mousePos))
        {
            hoveringElement.OnExit(mousePos);
            hoveringElement = null;
        }

        //  Regulating clicking.
        if (clickedElement == null && Input.GetMouseButtonDown(0) && OverlapsWithElement(mousePos, out clickedElement))
        {
            clickedElement.OnClick(mousePos);
        }
        else if (clickedElement != null && Input.GetMouseButtonUp(0))
        {
            clickedElement.OnRelease(mousePos);
            clickedElement = null;
        }
    }

    public void Subscribe(IInteractable _element, RectTransform _key)
    {
        if (subscribedElements.ContainsKey(_key.GetInstanceID()))
        {
            Debug.LogWarning($"Transform of {_key.gameObject.name} already subscribed in interaction registry.");
            return;
        }
        subscribedElements.Add(_key.GetInstanceID(), _element);
    }

    public void Unsubscribe(RectTransform _key)
    {
        if (subscribedElements[_key.GetInstanceID()] == clickedElement) { clickedElement = null; }
        if (subscribedElements[_key.GetInstanceID()] == hoveringElement) { hoveringElement = null; }

        if (!subscribedElements.ContainsKey(_key.GetInstanceID()))
        {
            Debug.LogWarning($"Transform of {_key.gameObject.name} not subscribed in interaction registry.");
            return;
        }
        subscribedElements.Remove(_key.GetInstanceID());
    }

    public Vector2 CanvasToScreenPoint(Vector2 _point) => translator.CanvasToScreenPoint(_point);

    public Vector2 ScreenToCanvasPoint(Vector2 _point) => translator.ScreenToCanvasPoint(_point);

    private bool OverlapsWithElement(Vector2 _mousePos, out IInteractable _interactable)
    {
        _interactable = null;
        foreach (var element in subscribedElements)
        {
            if (!element.Value.Overlaps(_mousePos)) continue;
            _interactable = element.Value;
            return true;
        }
        return false;
    }
}
