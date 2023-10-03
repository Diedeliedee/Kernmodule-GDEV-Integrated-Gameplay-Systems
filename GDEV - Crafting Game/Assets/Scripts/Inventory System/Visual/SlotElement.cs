using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class SlotElement : BaseInteractable
{
    private Tile connectedTile = null;

    public SlotElement(RectTransform _transform, Tile _connectedTile) : base(_transform)
    {
        connectedTile = _connectedTile;
    }

    public override void OnClick(Vector2 _mousePos)
    {
        Debug.Log("HALLOOO!!!");
    }
}
