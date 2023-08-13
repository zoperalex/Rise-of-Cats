using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    public Vector3 target;
    public float speed = 10f;
    public GameObject player;
    public float damage = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.position += target * speed * Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("ProjectileDespawn"))
        {
            gameObject.SetActive(false);
        }
    }
}
