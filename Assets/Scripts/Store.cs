using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [SerializeField] private List<Item> _storeItems;
    [SerializeField] private ItemView _itemTemplate;
    [SerializeField] private Player _player;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private GameObject _store;

    private bool _opened = false;

    private void Start()
    {
        foreach (var item in _storeItems)
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
    }

    public bool RenderItem(Item item)
    {
        if (ItemList.GetItemById(item.Id) != null)
        {
            var itemView = Instantiate(_itemTemplate, _itemContainer.transform);
            itemView.Render(item, true);
            return true;
        }
        else
        {
            Debug.Log("error ID: " + item.Id);
        }
        return false;
    }
}
