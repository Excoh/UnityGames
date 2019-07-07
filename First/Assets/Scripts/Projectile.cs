using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float timeToLive;
    public float speed;
    private void Start()
    {
        Destroy(gameObject, timeToLive);
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}
