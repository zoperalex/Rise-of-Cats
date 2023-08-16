using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceController : MonoBehaviour
{
    public int expAmount;
    public SpriteRenderer spriteRenderer;

    public void Setup(int expAmount, Sprite sprite = null)
    {
        this.expAmount = expAmount;
        if(sprite != null ) spriteRenderer.sprite = sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Player") && collision.isTrigger)
        {
            collision.gameObject.GetComponent<PlayerController>().AddExp(expAmount);
            gameObject.SetActive(false);
        }
    }
}
