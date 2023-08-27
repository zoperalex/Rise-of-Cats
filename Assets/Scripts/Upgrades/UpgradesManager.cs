using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradesMenu;

    public List<Upgrade> statUpgradeList;
    public List<Upgrade> weaponUpgradeList;

    private List<Upgrade> availableStatUpgrades;
    private List<Upgrade> availableWeaponUpgrades;

    private List<Upgrade> currentAvailableUpgrades;

    private List<Upgrade> currentWeaponUpgrades;
    private List<Upgrade> currentStatUpgrades;

    private List<Upgrade> currentCards;

    private void Start()
    {
        GameManager.instance.upgradesManager = this;
        upgradesMenu.SetActive(false);
        availableStatUpgrades = new List<Upgrade>();
        availableWeaponUpgrades = new List<Upgrade>();
        currentAvailableUpgrades = new List<Upgrade>();
        currentCards = new List<Upgrade>();
        currentWeaponUpgrades = new List<Upgrade>();
        currentStatUpgrades = new List<Upgrade>();
        foreach (Upgrade u in statUpgradeList)
        {
            u.level = 0;
            availableStatUpgrades.Add(u);
        }
        foreach(Upgrade u in weaponUpgradeList)
        {
            u.level = 0;
            availableWeaponUpgrades.Add(u);
        }
    }

    public void ActivateUpgradesMenu()
    {
        foreach(Upgrade u in currentWeaponUpgrades)
        {
            if(u.level < 5) currentAvailableUpgrades.Add(u);
        }
        if(currentWeaponUpgrades.Count != 5)
        {
            foreach (Upgrade u in availableWeaponUpgrades)
            {
                currentAvailableUpgrades.Add(u);
            }
        }

        foreach (Upgrade u in currentStatUpgrades)
        {
            if (u.level < 5) currentAvailableUpgrades.Add(u);
        }
        if (currentStatUpgrades.Count != 5)
        {
            foreach (Upgrade u in availableStatUpgrades)
            {
                currentAvailableUpgrades.Add(u);
            }
        }

        if(currentAvailableUpgrades.Count >= 3)
        {
            for(int i=0; i<3; i++)
            {
                int upgrade = Random.Range(0, currentAvailableUpgrades.Count);
                SetUpgradeCard(upgradesMenu.transform.GetChild(i+1).gameObject, currentAvailableUpgrades[upgrade]);
                currentCards.Add(currentAvailableUpgrades[upgrade]);
                currentAvailableUpgrades.Remove(currentAvailableUpgrades[upgrade]);
            }
        }
        else if (currentAvailableUpgrades.Count == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                int upgrade = Random.Range(0, currentAvailableUpgrades.Count);
                SetUpgradeCard(upgradesMenu.transform.GetChild(i == 1? 1 : 3).gameObject, currentAvailableUpgrades[upgrade]);
                currentCards.Add(currentAvailableUpgrades[upgrade]);
                currentAvailableUpgrades.Remove(currentAvailableUpgrades[upgrade]);
                if (i == 0) currentCards.Add(null);
            }
        }
        else if(currentAvailableUpgrades.Count == 1)
        {
            SetUpgradeCard(upgradesMenu.transform.GetChild(2).gameObject, currentAvailableUpgrades[0]);
            currentCards.Add(null);
            currentCards.Add(currentAvailableUpgrades[0]);
            currentCards.Add(null);
            currentAvailableUpgrades.Remove(currentAvailableUpgrades[0]);
        }
        currentAvailableUpgrades = new List<Upgrade>();
        upgradesMenu.SetActive(true);
        return;
    }

    void SetUpgradeCard(GameObject card, Upgrade Upgrade)
    {
        card.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Upgrade.upgradeName;
        card.transform.GetChild(1).GetComponent<Image>().sprite = Upgrade.sprite;
        for(int i=2; i < 7; i++)
        {
            card.transform.GetChild(i).GetChild(0).gameObject.SetActive(Upgrade.level >= i-1);
        }
        card.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = Upgrade.description;
        card.SetActive(true);
    }

    public void OnClick(int card)
    {
        currentCards[card].Choose();
        if (currentCards[card].upgradeType.Equals(UpgradeType.WEAPON) && currentWeaponUpgrades.Find(x => x == currentCards[card]) != null) currentWeaponUpgrades.Add(currentCards[card]);
        else if(currentCards[card].upgradeType.Equals(UpgradeType.STAT) && currentStatUpgrades.Find(x => x == currentCards[card]) != null) currentStatUpgrades.Add(currentCards[card]);
        currentCards = new List<Upgrade>();
        DeactivateUpgradeMenu();
        Time.timeScale = 1;
        GameManager.instance.playerController.StartInvulnerabilityTimer();
    }

    void DeactivateUpgradeMenu()
    {
        for(int i = 1; i < 4; i++)
        {
            upgradesMenu.transform.GetChild(i).gameObject.SetActive(false);
        }
        upgradesMenu.SetActive(false);
    }
}
