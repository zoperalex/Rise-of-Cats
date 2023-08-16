using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Scriptables/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    public float health;
    public float speed;
    public float attackDamage;
    public float attackSpeed;
    public AttackType attackType;
    public float projectileSpeed;
    public Sprite projectileSprite;
    public Sprite sprite;
    public float attackRange;
    public ExpLootConfig expLoot;
}
