using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackController : MonoBehaviour
{
    private float speed;
    private Vector3 targetDir;
    private float damage;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public void Setup(float speed, Vector3 targetDir, float damage, Sprite sprite)
    {
        this.speed = speed;
        this.targetDir = targetDir;
        this.damage = damage;
        if(sprite!=null)
        {
            spriteRenderer.sprite = sprite;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(StartTimer());
    }

    void Update()
    {
        transform.position += targetDir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
        }
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }
}
