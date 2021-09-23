using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float forceMultiplier = 5;
    public Rigidbody enemyRB;
    public Transform player;
    public GameObject bullet;
    public float bulletSpeed = 20;

    GameObject _bullet;
    bool exists = false;
    
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    void Update()
    {
        enemyRB.AddForce((player.position - this.transform.position).normalized * forceMultiplier);
        if(transform.position.y < -5)
        {
            Destroy(gameObject);
        }
        if (exists)
        {
            _bullet.GetComponent<Rigidbody>().AddForce((transform.position - _bullet.transform.position).normalized * bulletSpeed);
            _bullet.transform.LookAt(this.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _bullet)
        {
            Destroy(other.gameObject);
            exists = false;
            Destroy(gameObject);
            
        }
    }

    public void GettingShot()
    {
        _bullet = Instantiate(bullet, player.transform.position + (transform.position - player.transform.position).normalized, Quaternion.LookRotation((transform.position - player.transform.position), transform.up)) as GameObject;
        exists = true;
    }
}
