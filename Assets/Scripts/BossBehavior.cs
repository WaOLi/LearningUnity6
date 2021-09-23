using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class BossBehavior : MonoBehaviour
{
    public float rotationSpeed = 100;
    public GameObject player;
    public GameObject weapon;
    public float forceMultiplier;
    public GameObject lifeBar;
    public Slider bossLife;
    public int life = 10;
    public GameObject misc_xD;

    private void Start()
    {
        bossLife.maxValue = life;
        bossLife.value = life;
        player = FindObjectOfType<PlayerControlls>().gameObject;
    }
    void Update()
    {
        lifeBar.transform.position = transform.position + new Vector3(0f, 2f, 0f);
        if(bossLife.value <= 0)
        {
            Destroy(misc_xD);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        weapon.transform.position = transform.position;
        this.GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position).normalized * forceMultiplier);
    }

    IEnumerator BatSpin()
    {
        float angle = 0;
        while(angle < 400)
        {
            weapon.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
            angle += Time.deltaTime * rotationSpeed;
            yield return null;
        }
        weapon.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            weapon.SetActive(true);
            StartCoroutine(BatSpin());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject == player.gameObject) && !player.GetComponent<PlayerControlls>().isGrounded)
        {
            bossLife.value -= 4;
        }
    }
}
