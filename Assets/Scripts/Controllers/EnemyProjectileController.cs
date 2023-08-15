using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    float speed;
    Vector3 targetDirection;

    public float damage;
    public SpriteRenderer spriteRenderer;

    public void Setup(float speed, float damage, Vector3 targetDirection, Sprite sprite)
    {
        this.speed = speed;
        this.damage = damage;
        this.targetDirection = targetDirection;
        if (sprite != null) spriteRenderer.sprite = sprite;
    }

    void Update()
    {
        transform.position += targetDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && collision.isTrigger)
        {
            Debug.Log("bullet");
            collision.gameObject.GetComponent<PlayerController>().ChangeHealth(-damage);
            gameObject.SetActive(false);
        }
    }
}
