using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private BagController _bag;

    public void ShowBag()
    {
        if (_bag.GetItemsCount() != 0 && !_bag.gameObject.activeSelf)
        {
            _bag.Show();
        } 
        else
        {
            _bag.Hide();
        }
    }
}
