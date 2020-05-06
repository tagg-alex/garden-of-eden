using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player")]
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] float padding = 1.0f;
    [SerializeField] int playerhealth = 300;

    [Header("Projectile")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float xVel = 0f;
    [SerializeField] float yVel = 10f;
    [SerializeField] float projectileFiringPeriod = 1.0f;
    [SerializeField] AudioClip splatSFX;
    [SerializeField] [Range(0, 1)] float playerShotVolume = 0.7f;
    [SerializeField] AudioClip playerDeathSFX;
    [SerializeField] [Range(0, 1)] float playerDeathVolume = 0.7f;


    Coroutine firingCoroutine;
    //Coroutine firingCoroutine;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    Rigidbody2D projectileRigidBody;
    AudioSource myAudioSource;

    void Start()
    {
        SetUpMoveBoundaries();
        myAudioSource = GetComponent<AudioSource>();

    }
    
    void Update()
    {
        Move();
        TriggerProjectile();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        playerhealth -= damageDealer.Damage();
        damageDealer.Hit();
        if (playerhealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(playerDeathSFX, Camera.main.transform.position, playerDeathVolume);
    }

    public int GetHealth()
    {
        return playerhealth;
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        //1 is the camera max in a given axis and 0 is it's minimum, for any camera size 
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }

    private void TriggerProjectile()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
           
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }
    
    IEnumerator FireContinuously()
    {
        while (true)
        {
            float xShoot = transform.position.x + 0.13f;
            GameObject projectile = Instantiate(projectilePrefab,
               new Vector3(xShoot, transform.position.y,0),
               Quaternion.identity) as GameObject;
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
            AudioSource.PlayClipAtPoint(splatSFX, Camera.main.transform.position, playerShotVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
            
        }
    }
    
}
 