using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class Upgrade : SerializedScriptableObject
{
    [field: SerializeField]
    public int level { get; set; }
    [field: SerializeField]
    public string upgradeName { get; }

    [field: SerializeField]
    public string description { get; }
    [field: SerializeField]
    public Sprite sprite { get; }

    [field: SerializeField]
    public Upgrade nextUpgrade { get; }
    [field: SerializeField]
    public Upgrade requiredUpgrade { get; }

    [field: SerializeField]
    public UpgradeType upgradeType { get; }

    public abstract void Choose();
    protected abstract void LevelUp();
}
