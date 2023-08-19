using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    float speed;
    Vector3 targetDir;
    private float damage;
    public SpriteRenderer spriteRenderer;

    public void Setup(float speed, float damage, Vector3 targetDir, Sprite sprite)
    {
        this.speed= speed;
        this.damage= damage;
        this.targetDir= targetDir;
        if(sprite!=null) spriteRenderer.sprite = sprite;
    }

    void Update()
    {
        transform.position += targetDir * speed * Time.deltaTime;
    }

    private void OnEnable()
    {
        StartCoroutine(StartTimer());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
