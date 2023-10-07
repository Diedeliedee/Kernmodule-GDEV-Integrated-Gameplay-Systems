using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IInteractable
{
    public void OnEnter(Vector2 _mousePos);

    public void OnExit(Vector2 _mousePos);

    public void OnClick(Vector2 _mousePos);

    public void OnRelease(Vector2 _mousePos);

    public bool Overlaps(Vector2 _mousePos);
}
