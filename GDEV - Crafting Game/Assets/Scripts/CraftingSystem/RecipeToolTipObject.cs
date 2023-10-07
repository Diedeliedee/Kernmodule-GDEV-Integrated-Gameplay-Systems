using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeToolTipObject : BaseUpdatable
{
    private readonly RectTransform toolTipTransform;
    private readonly RectTransform costListParent;
    private readonly TextMeshProUGUI durationText;
    private readonly TextMeshProUGUI headerText;
    private readonly GameObject costListItem;
    private readonly Vector3 toolTipOffset;

    public RecipeToolTipObject(RectTransform _toolTipTransform)
    {
        toolTipTransform = _toolTipTransform;
        toolTipOffset = new Vector3(10, 10, 0);

        headerText = toolTipTransform.GetChild(0).GetComponent<TextMeshProUGUI>();
        costListParent = (RectTransform)toolTipTransform.GetChild(1);
        durationText = toolTipTransform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        costListItem = Resources.Load<GameObject>("Crafting/ToolTipCostItem");
    }
    public override void OnUpdate()
    {
        toolTipTransform.position = Input.mousePosition + toolTipOffset;
    }

    public void SetActive(bool _value)
    {
        toolTipTransform.gameObject.SetActive(_value);
    }

    public void SetRecipe(CraftingRecipe _recipe)
    {
        durationText.text = _recipe.Duration.ToString();
        headerText.text = _recipe.Output.Item.ItemName;

        foreach (RectTransform child in costListParent)
        {
            Object.Destroy(child.gameObject);
        }

        foreach (ItemStack stack in _recipe.Input)
        {
            GameObject newCostListItem = Object.Instantiate(costListItem, costListParent);
            newCostListItem.transform.GetChild(0).GetComponent<Image>().sprite = stack.Item.Image;
            newCostListItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = stack.Amount.ToString();
        }
    }
}

