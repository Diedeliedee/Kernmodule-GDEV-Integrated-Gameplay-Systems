using UnityEngine;
using UnityEngine.UI;

public class CraftingQueueObject
{
    public CraftingRecipe Recipe { get; set; }

    public float PassedTime;

    private readonly GameObject connectedGameObject;

    public CraftingQueueObject(CraftingRecipe _recipe, RectTransform _craftingQueueUIParent, GameObject _craftingQueueUIPrefab)
    {
        Recipe = _recipe;

        connectedGameObject = Object.Instantiate(_craftingQueueUIPrefab, _craftingQueueUIParent);
        connectedGameObject.transform.GetChild(0).GetComponent<Image>().sprite = _recipe.Output.Item.Image;
    }

    public void Destroy()
    {
        Object.Destroy(connectedGameObject);
    }
}
