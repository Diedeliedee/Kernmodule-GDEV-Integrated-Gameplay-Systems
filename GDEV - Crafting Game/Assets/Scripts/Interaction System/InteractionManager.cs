using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InteractionManager : IService, IUpdatable
{
    private readonly Camera camera = null;
    private readonly InteractionSettings settings = null;

    private Dictionary<int, IInteractable> subscribedElements = new();
    private IInteractable selectedElement = null;

    public InteractionManager()
    {
        camera = Camera.main;
        settings = Resources.Load<InteractionSettings>("Settings/Interaction");
    }

    public void OnFixedUpdate() { }

    public void OnStart() { }

    public void OnUpdate()
    {

        if (selectedElement == null)
        {
            //  Return if the mouse button hasn't been pressed.
            if (!Input.GetMouseButtonDown(0)) return;

            //  Cache variables.
            var mousePos = Input.mousePosition;
            var ray = camera.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y, 1f));

            //  Return if the raycast has not hit any interactable transform, or if attached interactable is not present.
            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, settings.InteractionMask, QueryTriggerInteraction.Collide)) return;
            if (!subscribedElements.TryGetValue(hit.transform.GetInstanceID(), out selectedElement)) return;

            //  If all checks pass, call on click.
            selectedElement.OnClick(Input.mousePosition);
        }
        else
        {
            if (!Input.GetMouseButtonUp(0)) return;

            selectedElement.OnRelease(Input.mousePosition);
        }
    }

    public void Subscribe(IInteractable _element, RectTransform _key)
    {
        subscribedElements.Add(_key.GetInstanceID(), _element);
    }
}
