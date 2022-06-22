using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CryptoExchange : MonoBehaviour
{
    [SerializeField] private List<PricePosition> _positions;
    [SerializeField] private GameObject _display;
    [SerializeField] private int _days = 8;
    [SerializeField] private float _maxPrice = 5000;
    [SerializeField] private float _limit = 50000;
    [SerializeField] private PricePosition _pricePositionTemplate;
    [SerializeField] private float _timePeriod = 5;

    private float _timeAfterLastUpdate = 0;
    private Vector2 _minAnchor = new Vector2(0, 0);
    private Vector2 _maxAnchor = new Vector2(1, 1);

    private void Awake()
    {
        if (_days < 2)
        {
            _days = 2;
            Debug.Log("error CryptoExchange days count");
        }
/*        _minAnchor = rectTransform.anchorMin;
        _maxAnchor = rectTransform.anchorMax;*/
    }

    private void Start()
    {
        for (int i = 0; i < _days; i++)
        {
            SetPricePosition(_maxPrice / 2, i);
        }

        for (int i = 0; i < _days; i++)
        {
            SetNewCycle();
        }
    }

    private void Update()
    {
        _timeAfterLastUpdate += Time.deltaTime;
        if (_timeAfterLastUpdate >= _timePeriod)
        {
            _timeAfterLastUpdate = 0;
            SetNewCycle();
        }
    }

    private void SetMaxPrice()
    {
        float avaragePrice = 0;
        float theBiggestPrice = 0;


        foreach (var position in _positions)
        {
            avaragePrice += position.Price;

            if(theBiggestPrice < position.Price)
            {
                theBiggestPrice = position.Price;
            }
        }
        if (_maxPrice>=_limit)
        {
            avaragePrice = avaragePrice / _days * 1.95f;
        }
        else
        {
            avaragePrice = avaragePrice / _days * 2.05f;
        }

        _maxPrice = Mathf.Clamp( avaragePrice, theBiggestPrice + 3000, int.MaxValue);

        SetPricePositions();
    }

    private void SetNewCycle()
    {
        for (int i = 0; i < _positions.Count - 1; i++)
        {
            _positions[i].ChangePrice(_positions[i + 1].Price);
        }

        SetNewRandomPrice(_positions[_positions.Count - 2], _positions[_positions.Count - 1]);
        SetMaxPrice();
        SetPricePositions();
    }

    private void SetNewRandomPrice(PricePosition previousPricePosition, PricePosition thisPricePosition)
    {
        float randomPriceChange = Random.Range(-0.2f * previousPricePosition.Price, 0.2f * (_maxPrice - previousPricePosition.Price));
        thisPricePosition.ChangePrice(Mathf.Clamp(previousPricePosition.Price + randomPriceChange, 0 , _maxPrice));
        Debug.Log(thisPricePosition.Price);

        SetPricePosition(thisPricePosition);
    }

    private void SetPricePosition(float price, int day)
    {
        PricePosition pricePosition = Instantiate(_pricePositionTemplate, _display.transform);
        pricePosition.Init(price, day);

        if (_maxPrice <= 0)
        {
            _maxPrice = 10000;
            Debug.Log("error CryptoExchange maxPrice");
        }

        SetPricePosition(pricePosition);
        _positions.Add(pricePosition);
    }

    private void SetPricePosition(PricePosition pricePosition)
    {
        Vector2 position = new Vector2(_minAnchor.x + (_maxAnchor.x - _minAnchor.x) / (_days - 1) * pricePosition.Day, _minAnchor.y + (_maxAnchor.y - _minAnchor.y) * pricePosition.Price / _maxPrice);

        pricePosition.GetComponent<RectTransform>().anchorMin = position;
        pricePosition.GetComponent<RectTransform>().anchorMax = position;
    }

    private void SetPricePositions()
    {
        foreach (var pricePosition in _positions)
        {
            Vector2 position = new Vector2(_minAnchor.x + (_maxAnchor.x - _minAnchor.x) / (_days - 1) * pricePosition.Day, _minAnchor.y + (_maxAnchor.y - _minAnchor.y) * pricePosition.Price / _maxPrice);

            pricePosition.GetComponent<RectTransform>().anchorMin = position;
            pricePosition.GetComponent<RectTransform>().anchorMax = position;
        }
    }
}
