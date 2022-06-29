using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [SerializeField] private List<Item> _storeItems;
    [SerializeField] private ItemView _itemTemplate;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _itemContainer;

    private void Start()
    {
        foreach (var item in _storeItems)
        {
            RenderItem(item);
        }
    }

    public bool RenderItem(Item item)
    {
        if (ItemList.GetItemById(item.ItemId) != null)
        {
            var itemView = Instantiate(_itemTemplate, _itemContainer.transform);
            itemView.ButtonClicked += OnSellButtonClicked;
            itemView.Render(item, true);
            return true;
        }
        else
        {
            Debug.Log("error ID: " + item.ItemId);
        }
        return false;
    }

    private void OnSellButtonClicked(Item item)
    {
        TrySellItem(ItemList.GetItemById(item.ItemId));
    }

    private void TrySellItem(Item item)
    {
        float price = item.Cost * CryptoExchange.Price / 100000000 + 5;
        if (price <= _player.Money)
        {
            _player.BuyItem(item, price);
        }
    }
}
