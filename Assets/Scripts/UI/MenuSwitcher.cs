using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private Menu _menu;

    public void SetMenuScreen(Menu menu)
    {
        MinersStorage.DeleteRenderItems();
        if (_menu)
        {
            _menu.SetActive(false);
            if (_menu == menu)
            {
                _player.TouchScreenWork(true);
                _menu = null;
                return;
            }
        }

        _menu = menu;
        _player.TouchScreenWork(_menu.SetActive(true));
    }

    public void CloseMenu()
    {
        _menu.SetActive(false);
        _player.TouchScreenWork(true);
        _menu = null;
    }
}
