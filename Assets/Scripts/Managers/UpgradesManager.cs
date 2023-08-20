using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradesMenu;

    private void Start()
    {
        upgradesMenu.SetActive(false);
        GameManager.instance.upgradesManager = this;
    }

    public void ActivateUpgradesMenu()
    {
        upgradesMenu.SetActive(true);
    }
}
