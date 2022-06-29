using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    [SerializeField] private List<Item> _deffaultItemsFromInspector;

    private static List<Item> _deffaultItems;

    private void Awake()
    {
        _deffaultItems = _deffaultItemsFromInspector;
    }

    public static Item GetItemById(int id)
    {
        foreach (var item in _deffaultItems)
        {
            if (item.ItemId == id)
            {
                return item;
            }
        }
        return null;
    }
}