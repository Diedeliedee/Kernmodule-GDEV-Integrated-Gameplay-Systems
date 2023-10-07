using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SlotElement : BaseInteractable
{
    private readonly Tile connectedTile = null;

    private readonly Image itemImage = null;
    private readonly TextMeshProUGUI itemText = null;

    public SlotElement(RectTransform _transform, Tile _connectedTile) : base(_transform)
    {
        connectedTile = _connectedTile;

        itemImage = _transform.GetChild(0).GetComponent<Image>();
        itemText = itemImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        connectedTile.OnAltered += OnTileAltered;

        itemImage.enabled = false;
        itemText.enabled = false;
    }

    public override void OnClick(Vector2 _mousePos)
    {
        Debug.Log($"Hallooo! Je hebt geklikt op {element.gameObject.name}.");
    }

    private void OnTileAltered(ItemStack _stack)
    {
        if (_stack.Amount <= 0)
        {
            itemImage.enabled = false;
            itemText.enabled = false;
        }
        else
        {
            itemImage.enabled = true;
            itemText.enabled = true;

            itemImage.sprite = _stack.Item.Image;
            itemText.text = _stack.Amount.ToString();
        }
    }
}
