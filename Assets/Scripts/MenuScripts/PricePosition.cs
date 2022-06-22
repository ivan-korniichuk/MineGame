using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PricePosition : MonoBehaviour
{
    private float _price;
    private int _day;

    public float Price => _price;
    public int Day => _day;

    /*    public PricePosition(float price, int day)
        {
            _price = price;
            _day = day;
        }*/

    public void Init(float price, int day)
    {
        _price = price;
        _day = day;
    }

    public void ChangePrice(float price)
    {
        _price = price;
    }
}