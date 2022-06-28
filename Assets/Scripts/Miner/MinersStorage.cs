using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinersStorage : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Material _renderMaterial;
    [SerializeField] private Material _material;

    private static MinerPlace[] _minerPlaces;

    private void Awake()
    {
        _minerPlaces = GetComponentsInChildren<MinerPlace>();
    }

    public static MinerPlace GetMinerPlaceById(int id)
    {
        foreach (var minerPlace in _minerPlaces)
        {
            if (minerPlace.Id == id)
            {
                return minerPlace;
            }
        }
        return null;
    }

    public void Render(Item item)
    {
        DeleteRenderItems();

        foreach (var minerPlace in _minerPlaces)
        {
            if (minerPlace.IsRenderItem)
            {
                minerPlace.Render(item, _renderMaterial);
            }
        }
    }

    public void SetItem(int placeID)
    {
        foreach (var minerPlace in _minerPlaces)
        {
            if (minerPlace.Id == placeID)
            {
                minerPlace.SetMiner(_player, _material);
                return;
            }
        }
    }

    public static void DeleteRenderItems()
    {
        foreach (var minerPlace in _minerPlaces)
        {
            if (minerPlace.IsRenderItem)
            {
                minerPlace.DeleteItem(minerPlace.Id);
            }
        }
    }
}
