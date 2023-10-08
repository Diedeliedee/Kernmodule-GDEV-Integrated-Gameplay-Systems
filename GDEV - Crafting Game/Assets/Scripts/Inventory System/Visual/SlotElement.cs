using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotElement : BaseInteractable
{
    private readonly Tile connectedTile = null;
    private readonly GridSettings settings = null;

    private readonly ElementScaler imageScaler = null;
    private readonly ElementScaler countScaler = null;

    private readonly Image itemImage = null;
    private readonly TextMeshProUGUI itemText = null;

    public SlotElement(RectTransform _transform, Tile _connectedTile, GridSettings _settings) : base(_transform)
    {
        connectedTile = _connectedTile;
        settings = _settings;

        imageScaler = new ElementScaler(_transform.GetChild(0).GetComponent<RectTransform>(), settings.LerpSpeed);
        countScaler = new ElementScaler(imageScaler.Transform.GetChild(0).GetComponent<RectTransform>(), settings.LerpSpeed);

        itemImage = imageScaler.Transform.GetComponent<Image>();
        itemText = countScaler.Transform.GetComponent<TextMeshProUGUI>();

        connectedTile.OnAltered += OnTileAltered;
        connectedTile.OnTypeChanged += OnTypeChanged;
        connectedTile.OnValueChanged += OnValueChanged;

        itemImage.enabled = false;
        itemText.enabled = false;
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
            itemImage.sprite = _stack.Item.Image;
            itemText.text = _stack.Amount.ToString();
            itemImage.enabled = true;
            itemText.enabled = true;
        }
    }

    private void OnTypeChanged(ItemData _type)
    {
        imageScaler.Scale(settings.SizeMultiplier);
    }

    private void OnValueChanged(int _amount)
    {
        countScaler.Scale(settings.SizeMultiplier);
    }

    public void Tick(float _deltaTime)
    {
        imageScaler.Tick(_deltaTime);
        countScaler.Tick(_deltaTime);
    }
}
