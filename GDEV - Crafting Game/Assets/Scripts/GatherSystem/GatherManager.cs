using System.Collections.Generic;
using UnityEngine;

public class GatherManager : BaseUpdatable, IGatherManager
{
    private IInventory inventory;

    private readonly List<GatherComponent> gatherComponents;
    private readonly GatherInfo gatherInfo;

    public GatherManager(GatherComponent _baseGatherComponent) 
    {
        gatherComponents = new List<GatherComponent>();
        gatherInfo = new GatherInfo();

        AddGatherComponent(_baseGatherComponent);
    }

    public override void OnStart()
    {
        inventory = ServiceLocator.Instance.Get<IInventory>();
    }

    public override void OnFixedUpdate()
    {
        List<ItemStack> itemStacks = new();

        foreach (KeyValuePair<ItemData, GatherChance> gatherItem in gatherInfo.gatherItems)
        {
            float randomPercentage = Random.Range(0.0f, 100.0f);

            if (randomPercentage < gatherItem.Value.gatherChancePercentage)
            {
                int randomStackSize = Random.Range(0, gatherItem.Value.maxStackSize + 1);
                itemStacks.Add(new(gatherItem.Key, randomStackSize));
            }
        }

        inventory.Add(itemStacks.ToArray());
    }

    public void AddGatherComponent(GatherComponent _gatherComponent)
    {
        gatherComponents.Add(_gatherComponent);

        gatherInfo.gatherItems.Clear();
        foreach (GatherComponent component in gatherComponents)
        {
            gatherInfo.Decorate(component);
        }
    }
}
