using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    float vInput;

    public float powerupDuration;
    public Rigidbody playerRB;
    public float forceMultiplier = 50;
    public GameObject focalPoint;
    bool hasPowerup;
    public float powerupStrength = 10.0f;
    public GameObject powerupIndicator;
    public GameObject powerup2Indicator;

    public bool hasPowerup2;
    private Nullable<bool> differential = null;

    public bool isGrounded = true;
    public float knockBackForce = 1000;

    int shakerCharges = 0;
    public GameObject enemyWeapon;
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }


    void Update()
    {
        vInput = Input.GetAxis("Vertical");
        


        powerupIndicator.transform.position = (this.transform.position + new Vector3(0f, -0.5f, 0f));
        powerup2Indicator.transform.position = (this.transform.position + new Vector3(0f, -0.5f, 0f));

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && shakerCharges > 0)
        {
            StartCoroutine(EarthShaker());
            shakerCharges--;
        }
        
    }
    private void FixedUpdate()
    {
        playerRB.AddForce(focalPoint.transform.forward * forceMultiplier * vInput);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
            differential = true;
            StartCoroutine(PowerupRuntime(powerupIndicator));
        }
        if (other.CompareTag("Powerup2"))
        {
            Shooting();
            hasPowerup2 = true;
            Destroy(other.gameObject);
            powerup2Indicator.gameObject.SetActive(true);
            differential = false;
            StartCoroutine(PowerupRuntime(powerup2Indicator));
        }
        if (other.CompareTag("Powerup"))
        {
            shakerCharges++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("BossWeapon"))
        {
            playerRB.AddForce(Vector3.Cross((other.transform.position - transform.position), transform.up).normalized * knockBackForce, ForceMode.Impulse);
        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = -(transform.position - collision.gameObject.transform.position);
            enemyRB.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            Debug.Log(hasPowerup);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!isGrounded)
            {
                EnemyBehavior[] targets = FindObjectsOfType<EnemyBehavior>();

                foreach(EnemyBehavior enemy in targets)
                {
                    if((enemy.transform.position - transform.position).magnitude < 8)
                    {
                        Rigidbody _enemyRB = enemy.GetComponent<Rigidbody>();
                        _enemyRB.AddForce((_enemyRB.transform.position - transform.position).normalized / (_enemyRB.transform.position - transform.position).magnitude * knockBackForce , ForceMode.Impulse);
                        Debug.Log((_enemyRB.transform.position - transform.position).normalized / (_enemyRB.transform.position - transform.position).magnitude * knockBackForce);
                    }
                }

                isGrounded = true;
            }
        }
    }


    IEnumerator PowerupRuntime(GameObject powInd)
    {
        yield return new WaitForSeconds(5);
        powInd.SetActive(false);
        if ((bool)differential)
        {
            hasPowerup = false;
        }
        else
        {
            hasPowerup2 = false;
        }
    }



    void Shooting()
    {
        EnemyBehavior[] targets = FindObjectsOfType<EnemyBehavior>();
        foreach(EnemyBehavior k in targets)
        {
            k.GettingShot();
        }
    }

    IEnumerator EarthShaker()
    {
        {
            playerRB.AddForce(Vector3.up * 50, ForceMode.Impulse);
        }
        isGrounded = false;
        yield return new WaitForSeconds(0.3f);
        {
            playerRB.AddForce(Vector3.up * -80, ForceMode.Impulse);
        }
    }
}