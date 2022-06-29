using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{
    [SerializeField] private List<Item> _inventory;
    [SerializeField] private ItemView _itemTemplate;
    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private MinersStorage _minersStorage;

    private List<ItemView> _itemViews = new List<ItemView>();

    private void Start()
    {
        foreach (var item in _inventory)
        {
            RenderItem(item);
        }
    }

    public bool RenderItem(Item item)
    {
        if (ItemList.GetItemById(item.ItemId) != null)
        {
            var itemView = Instantiate(_itemTemplate, _itemContainer.transform);
            itemView.Emptied += DeleteEmptyItems;
            itemView.ButtonClicked += OnRenderButtonClicked;
            itemView.Render(item);
            _itemViews.Add(itemView);
            return true;
        }
        else
        {
            Debug.Log("error ID: " + item.ItemId);
        }
        return false;
    }

    private void DeleteEmptyItems()
    {
        for (int i = 0; i < _itemViews.Count; i++)
        {
            if (_itemViews[i].Item.Count <= 0)
            {
                _itemViews[i].Emptied -= DeleteEmptyItems;
                _itemViews[i].ButtonClicked -= OnRenderButtonClicked;
                _itemViews.RemoveAt(i);
            }
        }
        for (int i = 0; i < _inventory.Count; i++)
        {
            if(_inventory[i].Count <= 0)
            {
                _inventory.RemoveAt(i);
            }
        }
    }

    public void TrySetItem(int placeID, int itemId)
    {
        foreach (Item item in _inventory)
        {
            if (item.TryTakeItem(itemId) != null)
            {
                _minersStorage.SetItem(placeID);
                MinersStorage.DeleteRenderItems();
                return;
            }
        }
    }

    private void OnRenderButtonClicked(Item item)
    {
        _minersStorage.Render(item);
    }

    public void TryTakeItem(int placeId)
    {
        var item = MinersStorage.GetMinerPlaceById(placeId).DeleteItem(placeId);

        if (item != null)
        {
            AddItem(ItemList.GetItemById(item.GetComponent<Miner>().ItemId));
        }
    }

    public void AddItem(Item newItem)
    {
        foreach (var item in _inventory)
        {
            if (item.TryAddItem(newItem))
            {
                return;
            }
        }

        if (RenderItem(newItem))
        {
            _inventory.Add(newItem);
        }
    }
}
