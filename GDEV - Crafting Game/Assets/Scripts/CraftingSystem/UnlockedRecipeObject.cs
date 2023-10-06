using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class UnlockedRecipeObject
{
    public CraftingRecipe Recipe { get; set; }

    private readonly GameObject connectedGameObject;
    private Image isCraftableImage;
    private Sprite check;
    private Sprite cross;

    public UnlockedRecipeObject(CraftingRecipe _recipe, RectTransform _recipeUIParent, GameObject _recipeUIPrefab, UnityAction onClick, Sprite _check, Sprite _cross)
    {
        Recipe = _recipe;

        connectedGameObject = Object.Instantiate(_recipeUIPrefab, _recipeUIParent);
        connectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text = _recipe.name;
        connectedGameObject.transform.GetChild(0).GetComponent<Image>().sprite = _recipe.Output.Item.Image;
        connectedGameObject.GetComponent<Button>().onClick.AddListener(onClick);
        isCraftableImage = connectedGameObject.transform.GetChild(2).GetComponent<Image>();
        check = _check;
        cross = _cross;
    }

    public void SetIsCraftable(bool _isCraftable)
    {
        if (_isCraftable) { isCraftableImage.sprite = check; }
        else { isCraftableImage.sprite = cross; }
    }
}

