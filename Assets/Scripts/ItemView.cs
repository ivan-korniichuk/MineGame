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

    public event UnityAction<Item> RenderButtonClicked;
    public event UnityAction Emptied;

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
        _item.CountChanged -= OnCountChanged;
    }

    private void OnButtonClick()
    {
        RenderButtonClicked?.Invoke(_item);
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

/**/
    public void Render(Item item)
    {
        _item = item;

        _button.onClick.AddListener(OnButtonClick);
        _item.CountChanged += OnCountChanged;

        _label.text = _item.Label;
        _info.text = _item.Count.ToString();
        _icon.sprite = _item.Icon;
    }
/**/

    public void Render(Item item, bool isStoreItem)
    {
        if (isStoreItem)
        {
            _item = item;

            _button.onClick.AddListener(OnButtonClick);
            _item.CountChanged += OnCountChanged;

            _label.text = _item.Label;
            _info.text = _item.Cost.ToString() + '$';
            _icon.sprite = _item.Icon;
        }
    }
}
