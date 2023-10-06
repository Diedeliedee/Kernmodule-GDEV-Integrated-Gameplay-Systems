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
    private readonly Canvas canvas = null;
    private readonly Camera camera = null;
    private readonly EventSystem eventSystem = null;
    private readonly GraphicRaycaster raycaster = null;

    private readonly InteractionSettings settings = null;

    private Dictionary<int, IInteractable> subscribedElements = new();
    private IInteractable selectedElement = null;

    public InteractionManager(Canvas _canvas)
    {
        canvas = _canvas;
        camera = Camera.main;
        eventSystem = EventSystem.current;
        raycaster = canvas.GetComponent<GraphicRaycaster>();

        settings = Resources.Load<InteractionSettings>("Settings/Interaction");

        ServiceLocator.Instance.Add(this);
    }

    public void OnFixedUpdate() { }

    public void OnStart() { }

    public void OnUpdate()
    {
        PointerEventData pointerData;

        void ClickOnElement()
        {
            var resultList = new List<RaycastResult>();

            raycaster.Raycast(pointerData, resultList);
            foreach (var hit in resultList)
            {
                if (!subscribedElements.TryGetValue(hit.gameObject.transform.GetInstanceID(), out selectedElement)) continue;
                selectedElement.OnClick(Input.mousePosition);
            }
        }

        void ReleaseElement()
        {
            selectedElement.OnRelease(pointerData.position);
            selectedElement = null;
        }

        pointerData = new PointerEventData(eventSystem);
        pointerData.position = Input.mousePosition;

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
