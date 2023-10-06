using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SlotElement
{
    private readonly Tile connectedTile = null;

    private readonly Image itemImage = null;
    private readonly TextMeshProUGUI itemText = null;

    public SlotElement(RectTransform _transform, Tile _connectedTile)
    {
        connectedTile = _connectedTile;

        itemImage = _transform.GetChild(0).GetComponent<Image>();
        itemText = _transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        connectedTile.OnAltered += OnTileAltered;
    }

    private void OnTileAltered(ItemStack _stack)
    {
        itemImage.sprite = _stack.Item.Image;
        itemText.text = _stack.Amount.ToString();
    }
}
