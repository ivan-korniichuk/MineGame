using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
            if (item.Id == id)
            {
                return new Item(item);
            }
        }
        return null;
    }
}

[System.Serializable]
public class Item
{
    [SerializeField] private string _label;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private int _id;
    [SerializeField] private int _count = 1;
    [SerializeField] private int _cost;

    public int Id => _id;
    public Sprite Icon => _icon;
    public string Label => _label;
    public int Count => _count;
    public int Cost => _cost;
    public GameObject GameObject => _gameObject;
    public event UnityAction<int> CountChanged;

    public Item(Item item)
    {
        _label = item.Label;
        _id = item.Id;
        _icon = item.Icon;
        _gameObject = item.GameObject;
        _count = item.Count;
        _cost = item.Cost;
    }

    public bool TryAddItem(Item item)
    {
        if(item.Id == _id)
        {
            _count++;
            CountChanged?.Invoke(_count);
            return true;
        }

        return false;
    }

    public Item TryTakeItem(int id)
    {
        if (id == _id && _count > 0)
        {
            _count--;
            CountChanged?.Invoke(_count);
            return this;
        }

        return null;
    }

}
