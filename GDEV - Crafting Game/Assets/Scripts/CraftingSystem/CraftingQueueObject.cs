using UnityEngine;
using UnityEngine.UI;

public class CraftingQueueObject : BaseInteractable
{
    public CraftingRecipe Recipe { get; set; }

    public float PassedTime;

    private readonly GameObject connectedGameObject;
    private readonly Image crossImage;
    private readonly System.Action<CraftingQueueObject> onClick;

    public CraftingQueueObject(CraftingRecipe _recipe, RectTransform _craftingQueueUIParent, GameObject _craftingQueueUIPrefab, System.Action<CraftingQueueObject> _onClick)
    {
        Recipe = _recipe;
        onClick = _onClick;

        connectedGameObject = Object.Instantiate(_craftingQueueUIPrefab, _craftingQueueUIParent);
        connectedGameObject.transform.GetChild(0).GetComponent<Image>().sprite = _recipe.Output.Item.Image;
        crossImage = connectedGameObject.transform.GetChild(1).GetComponent<Image>();
        Setup((RectTransform)connectedGameObject.transform);
    }

    public void Destroy()
    {
        DisconnectInteractable();
        Object.Destroy(connectedGameObject);
    }

    public override void OnEnter(Vector2 _mousePos)
    {
        crossImage.enabled = true;
    }

    public override void OnExit(Vector2 _mousePos)
    {
        crossImage.enabled = false;
    }

    public override void OnClick(Vector2 _mousePos)
    {
        onClick(this);
    }
}
