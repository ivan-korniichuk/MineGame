using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyView : ValueView
{
    [SerializeField] private Player _player;
    [SerializeField] private string[] _roundNames = { "", "", "" };

    private void OnEnable()
    {
        _player.MoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _player.MoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(float money)
    {
        /*int i = 0;
        while(i + 1 < _roundNames.Length && money >= 1000f)
        {
            money /= 1000f;
            i++;
        }*/

        OnValueChanged(money.ToString(format: "F1") + '$');
    }
}
