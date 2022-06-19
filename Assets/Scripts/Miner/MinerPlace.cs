using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerPlace : MonoBehaviour
{
    private static int _minerPlaceIds = 0;

    public GameObject Item { get; private set; }
    public bool IsRenderItem { get; private set; } = true;
    public int Id { get; private set; }

    public static int GetMinerPlaceId()
    {
        return _minerPlaceIds++;
    }

    private void Awake()
    {
        Id = GetMinerPlaceId();
    }

    public void Render(Item item, Material renderMaterial)
    {
        Item = Instantiate(item.GameObject, transform.position, Quaternion.identity);
        Item.GetComponent<Miner>().enabled = false;
        Item.GetComponent<Miner>().Init(item.Id, Id);
        IsRenderItem = true;

        foreach (var meshRenderer in Item.GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderer.material = renderMaterial;
        }
    }

    public void SetMiner(Player player, Material renderMaterial)
    {
        Item.GetComponent<Miner>().enabled = true;
        Item.GetComponent<Miner>().Init(player);
        IsRenderItem = false;

        foreach (var meshRenderer in Item.GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderer.material = renderMaterial;
        }
    }

    public GameObject DeleteItem()
    {
        IsRenderItem = true;
        Destroy(Item);
        return Item;
    }
}
