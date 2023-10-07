using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class UnlockedRecipeObject
{
    public CraftingRecipe Recipe { get; set; }

    private readonly GameObject connectedGameObject;
    private readonly Image isCraftableImage;
    private readonly Sprite check;
    private readonly Sprite cross;

    public UnlockedRecipeObject(CraftingRecipe _recipe, RectTransform _recipeUIParent, GameObject _recipeUIPrefab, UnityAction onClick, Sprite _check, Sprite _cross)
    {
        Recipe = _recipe;
        check = _check;
        cross = _cross;

        connectedGameObject = Object.Instantiate(_recipeUIPrefab, _recipeUIParent);
        connectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text = _recipe.Output.Item.ItemName;
        connectedGameObject.transform.GetChild(0).GetComponent<Image>().sprite = _recipe.Output.Item.Image;
        connectedGameObject.GetComponent<Button>().onClick.AddListener(onClick);

        isCraftableImage = connectedGameObject.transform.GetChild(2).GetComponent<Image>();
    }

    public void SetIsCraftable(bool _isCraftable)
    {
        if (_isCraftable) { isCraftableImage.sprite = check; }
        else { isCraftableImage.sprite = cross; }
    }
}

