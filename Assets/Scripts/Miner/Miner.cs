using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    [SerializeField] private float _income;
    [SerializeField] private float _delay;
    [SerializeField] private float _randomDeltaDelay;
    [SerializeField] private Player _player;

    private float _timeAfterLastPayment = 0;

    public int Id { get; private set; }
    public int PlaceId { get; private set; }

    private void Update()
    {
        _timeAfterLastPayment += Time.deltaTime;

        if (_timeAfterLastPayment >= _delay)
        {
            _timeAfterLastPayment = Random.Range(-_randomDeltaDelay, _randomDeltaDelay);

            _player.AddBitcoins(_income);
        }
    }

/*    public void Init(int id, int placedId, Player player)
    {
        Id = id;
        PlaceId = placedId;
        _player = player;
    }*/
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
