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
    [SerializeField] private Button _button;
    [SerializeField] private MinersStorage _minersStorage;
    [SerializeField] private GameObject _store;

    private bool _opened = false;
    private List<ItemView> _itemViews = new List<ItemView>();

    private void Start()
    {
        foreach (var item in _inventory)
        {
            RenderItem(item);
        }
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _opened = !_opened;
        _store.SetActive(_opened);

        if (!_opened)
        {
            _minersStorage.DeleteRenderItems();
        }
    }

    public bool RenderItem(Item item)
    {
        if (ItemList.GetItemById(item.Id) != null)
        {
            var itemView = Instantiate(_itemTemplate, _itemContainer.transform);
            itemView.Emptied += DeleteEmptyItems;
            itemView.RenderButtonClicked += MinersRender;
            itemView.Render(item);
            _itemViews.Add(itemView);
            return true;
        }
        else
        {
            Debug.Log("error ID: " + item.Id);
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
                _itemViews[i].RenderButtonClicked -= MinersRender;
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
                _minersStorage.DeleteRenderItems();
                return;
            }
        }
    }

    private void MinersRender(Item item)
    {
        _minersStorage.Render(item);
    }

    public void TryTakeItem(int placeId)
    {
        var item = MinersStorage.GetMinerPlaceById(placeId).DeleteItem(placeId);

        if (item != null)
        {
            AddItem(ItemList.GetItemById(item.GetComponent<Miner>().Id));
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
