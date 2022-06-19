using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _money;
    [SerializeField] private float _bitcoins;
    [SerializeField] private float _sellPrice;
    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private Storage _storage;

    private PlayerInput _playerInput;

    public event UnityAction<float> MoneyChanged;
    public event UnityAction<float> BitcoinsChanged;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Player.Swipe.performed += ctx => OnSwipe();
        _playerInput.Player.Touch.performed += ctx => OnTouch();

        MoneyChanged?.Invoke(_money);
        BitcoinsChanged?.Invoke(_bitcoins);
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnTouch()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(_playerInput.Player.Touch.ReadValue<Vector2>());
        Physics.Raycast(ray, out hit);

        if (hit.collider != null)
        {
            if(hit.collider.TryGetComponent<Miner>(out Miner miner))
            {
                _storage.TrySetItem(miner.PlaceId, miner.Id);
            }
        }
    }

    private void OnSwipe()
    {
        _cameraMover.OnSwipe(_playerInput.Player.Swipe.ReadValue<Vector2>());
    }

    public void AddBitcoins(float income)
    {
        _bitcoins += income;
        BitcoinsChanged?.Invoke(_bitcoins);
    }
}