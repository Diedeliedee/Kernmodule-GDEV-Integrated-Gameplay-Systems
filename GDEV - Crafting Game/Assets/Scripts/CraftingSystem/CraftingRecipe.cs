using UnityEngine;

[CreateAssetMenu(fileName = "New Crafting Recipe", menuName = "Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    [SerializeField] private ItemStack[] input;
    [SerializeField] private ItemStack[] output;
    [SerializeField] private int duration;

    public ItemStack[] Input => input;
    public ItemStack[] Output => output;
    public int Duration => duration;
}

