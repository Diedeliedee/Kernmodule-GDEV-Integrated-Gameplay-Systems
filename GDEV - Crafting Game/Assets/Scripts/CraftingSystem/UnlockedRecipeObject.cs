using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class UnlockedRecipeObject : BaseInteractable
{
    public CraftingRecipe Recipe { get; set; }

    private readonly GameObject connectedGameObject;
    private readonly Image isCraftableImage;
    private readonly Sprite check;
    private readonly Sprite cross;
    private readonly RecipeToolTipObject toolTip;

    public UnlockedRecipeObject
    (
        CraftingRecipe _recipe, 
        RectTransform _recipeUIParent, 
        GameObject _recipeUIPrefab, 
        UnityAction onClick, 
        RecipeToolTipObject _toolTip,
        Sprite _check, 
        Sprite _cross
    )
    {
        Recipe = _recipe;
        toolTip = _toolTip;
        check = _check;
        cross = _cross;

        connectedGameObject = Object.Instantiate(_recipeUIPrefab, _recipeUIParent);
        connectedGameObject.transform.GetChild(0).GetComponent<Image>().sprite = _recipe.Output.Item.Image;
        connectedGameObject.GetComponent<Button>().onClick.AddListener(onClick);

        isCraftableImage = connectedGameObject.transform.GetChild(1).GetComponent<Image>();

        Setup((RectTransform)connectedGameObject.transform);
    }

    public void SetIsCraftable(bool _isCraftable)
    {
        if (_isCraftable) { isCraftableImage.sprite = check; }
        else { isCraftableImage.sprite = cross; }
    }

    public override void OnEnter(Vector2 _mousePos)
    {
        toolTip.SetActive(true);
        toolTip.SetRecipe(Recipe);
    }

    public override void OnExit(Vector2 _mousePos)
    {
        toolTip.SetActive(false);
    }
}

