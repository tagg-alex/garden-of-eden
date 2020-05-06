using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 500;
    [SerializeField] int scoreValue = 150;

    
    //[SerializeField]
    float shotCounter;
    [Header("Shooting")]
    [SerializeField] float minTimeBetweenShots = 0.3f;
    [SerializeField] float maxTimeBetweenSHots = 2f;
    [SerializeField] GameObject enemyProjectile;
    [SerializeField] float xVel = 0f;
    [SerializeField] float yVel = 4f;
    [SerializeField] GameObject explosionStuff;
    [SerializeField] float explosionDuration;

    [Header("Audio")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0,1)] float deathVolume = 0.7f;
    [SerializeField] AudioClip enemyShotSFX;
    [SerializeField] [Range(0, 1)] float enemyShotVolume = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenSHots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();

    }

    private void CountDownAndShoot() { 
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            enemyFire();
            AudioSource.PlayClipAtPoint(enemyShotSFX, Camera.main.transform.position, enemyShotVolume);
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenSHots);
        }
    }

    private void enemyFire()
    {
        GameObject slime = Instantiate(enemyProjectile,
               transform.position,
               Quaternion.identity) as GameObject;
        slime.GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, -yVel);
        //yield return new WaitForSeconds(projectileFiringPeriod);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.Damage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(explosionStuff, transform.position, transform.rotation);
        Destroy(explosion, explosionDuration);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathVolume);
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
    }
}
