using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private bool _activeTouchScreen = false;
    [SerializeField] private bool _alwaysActive = false;

    public bool SetActive(bool active)
    {
        if (!active && _alwaysActive)
            return _activeTouchScreen;

        this.gameObject.SetActive(active);

        return _activeTouchScreen;
    }
}
