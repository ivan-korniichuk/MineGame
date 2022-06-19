using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _count;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;

    private Item _item;

    public Item Item => _item;

    public event UnityAction<Item> RenderButtonClicked;
    public event UnityAction Emptied;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
        _item.CountChanged += OnCountChanged;
    }

    private void OnDisable()
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
        _count.text = count.ToString();
    }

    public void Render(Item item)
    {
        _item = item;

        _label.text = _item.Label;
        _count.text = _item.Count.ToString();
        _icon.sprite = _item.Icon;
    }
}
