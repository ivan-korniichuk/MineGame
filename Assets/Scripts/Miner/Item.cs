using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private int _count = 1;
    [SerializeField] private int _cost;
    [SerializeField] protected int Id;

    public int ItemId => Id;
    public Sprite Icon => _icon;
    public string Label => _label;
    public int Count => _count;
    public int Cost => _cost;
    public event UnityAction<int> CountChanged;

    public Item(Item item)
    {
        _label = item.Label;
        Id = item.Id;
        _icon = item.Icon;
        _count = item.Count;
        _cost = item.Cost;
    }

    public bool TryAddItem(Item item)
    {
        if (item.Id == Id)
        {
            _count++;
            CountChanged?.Invoke(_count);
            return true;
        }

        return false;
    }

    public Item TryTakeItem(int id)
    {
        if (id == Id && _count > 0)
        {
            _count--;
            CountChanged?.Invoke(_count);
            return this;
        }

        return null;
    }
}