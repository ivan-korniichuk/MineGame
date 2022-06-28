using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitcoinView : ValueView
{
    [SerializeField] private Player _player;
    [SerializeField] private string[] _roundNames = { "uBTC", "mBTC", "cBTC", "dBTC", "BTC" };
    [SerializeField] private int[] _roundCounts =   { 1, 1000, 10000, 100000, 1000000 };

    private void OnEnable()
    {
        _player.BitcoinsChanged += OnBitcoinsChanged;
    }

    private void OnDisable()
    {
        _player.BitcoinsChanged -= OnBitcoinsChanged;
    }

    private void OnBitcoinsChanged(float bitcoins)
    {
        int i = 0;

        while (i + 1 <= _roundNames.Length && i + 1 <= _roundCounts.Length && bitcoins >= _roundCounts[i])
        {
            i++;
        }

        if(i > 0)
            i--;

        bitcoins /= (_roundCounts[i] * 100f);

        OnValueChanged(bitcoins.ToString(format: "F2") + _roundNames[i]);
    }
}
