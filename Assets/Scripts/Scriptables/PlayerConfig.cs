using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptables/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public int startingExpGoal;
    public float health;
    public float speed;
    public int attackDamage;
    public float attackSpeed;
    public AttackType attackType;
    public float projectileSpeed;
    public Sprite projectileSprite;
}
