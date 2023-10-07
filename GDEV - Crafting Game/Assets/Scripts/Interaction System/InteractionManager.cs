using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InteractionManager : IService, IUpdatable
{
    private Dictionary<int, IInteractable> subscribedElements = new();
    private IInteractable selectedElement = null;

    public InteractionManager()
    {
        ServiceLocator.Instance.Add(this);
    }

    public void OnFixedUpdate() { }

    public void OnStart() { }

    public void OnUpdate()
    {
        void ClickOnElement()
        {
            foreach (var element in subscribedElements)
            {
                if (!element.Value.Overlaps(Input.mousePosition)) continue;
                element.Value.OnClick(Input.mousePosition);
                selectedElement = element.Value;
                return;
            }
        }

        void ReleaseElement()
        {
            selectedElement.OnRelease(Input.mousePosition);
            selectedElement = null;
        }

        if (selectedElement == null)
        {
            if (Input.GetMouseButtonDown(0)) { ClickOnElement(); }
        }
        else
        {
            if (Input.GetMouseButtonUp(0)) { ReleaseElement(); }
        }
    }

    public void Subscribe(IInteractable _element, RectTransform _key)
    {
        subscribedElements.Add(_key.GetInstanceID(), _element);
    }
}
