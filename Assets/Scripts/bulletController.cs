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

    void OnEnable()
    {
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
