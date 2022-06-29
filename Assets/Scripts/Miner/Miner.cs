using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Item
{
    [SerializeField] private float _income;
    [SerializeField] private float _delay;
    [SerializeField] private float _randomDeltaDelay;
    [SerializeField] private Player _player;
    [SerializeField] private Core _core;

    private float _timeAfterLastPayment = 0;

    public int PlaceId { get; private set; }

    public Miner(Item item) : base(item)
    {
    }

    private void Update()
    {
        _timeAfterLastPayment += Time.deltaTime;

        if (_timeAfterLastPayment >= _delay)
        {
            _timeAfterLastPayment = Random.Range(-_randomDeltaDelay, _randomDeltaDelay);

            _player.AddBitcoins(_income);
        }
    }

    public void Init(int id, int placedId)
    {
        Id = id;
        PlaceId = placedId;
    }
    public void Init(Player player)
    {
        _player = player;
    }
}
