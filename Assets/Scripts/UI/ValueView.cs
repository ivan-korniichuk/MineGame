using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValueView : MonoBehaviour
{
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
    }

    protected void OnValueChanged(string value)
    {
        _text.text = value.ToString();
    }
}
