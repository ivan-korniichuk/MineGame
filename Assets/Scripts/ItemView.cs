using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _info;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;

    private Item _item;

    public Item Item => _item;

    public event UnityAction<Item> ButtonClicked;
    public event UnityAction Emptied;

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
        CryptoExchange.PriceChanged -= OnPriceChanged;
        _item.CountChanged -= OnCountChanged;
    }

    private void OnButtonClick()
    {
        ButtonClicked?.Invoke(_item);
    }

    public void OnCountChanged(int count)
    {
        if (count <= 0)
        {
            Destroy(gameObject);
            Emptied?.Invoke();
            return;
        }
        _info.text = count.ToString();
    }

    private void OnPriceChanged(float price)
    {
        _info.text = (_item.Cost * CryptoExchange.Price / 100000000 + 5).ToString(format: "F1") + '$';
    }

    public void Render(Item item, bool isStoreItem = false)
    {
        _item = item;

        _button.onClick.AddListener(OnButtonClick);
        _label.text = _item.Label;
        _icon.sprite = _item.Icon;
        if (isStoreItem)
        {
            CryptoExchange.PriceChanged += OnPriceChanged;
            OnPriceChanged(CryptoExchange.AvPrice);
        }
        else
        {
            _item.CountChanged += OnCountChanged;
            OnCountChanged(_item.Count);
        }
    }
}
