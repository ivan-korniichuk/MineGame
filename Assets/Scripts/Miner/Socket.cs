using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : Item
{
    [SerializeField] private Miner _miner;

    public Socket(Item item) : base(item)
    {
    }
}
