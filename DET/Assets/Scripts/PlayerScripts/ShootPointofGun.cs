using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPointofGun : MonoBehaviour
{
    public int damage = 30;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    float timer;

    public ParticleSystem flash;
    public ParticleSystem hitEffect;
    GameObject player;
    PlayerHealth playerHealth;
    LineRenderer gunLine;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    Ray shootRay;

    public GameObject BloodSplash;
   // public GameObject ZombieRig;
    

     void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
     
        playerHealth = player.GetComponent<PlayerHealth>();
        gunLight = GetComponent<Light>();
        gunLine = GetComponent<LineRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime; 
        
        if(Input.GetButtonDown("Fire1") && playerHealth.currentHealth > 0 && timer >= timeBetweenBullets )
        {
            flash.Emit(1);
            Shoot();
        }
        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
        
    }
    void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    
    void Shoot()
    {
        timer = 0f;
        gunLight.enabled = true;
        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
       
        
        
        // play shootingSound 
        FindObjectOfType<AudioManager>().play("shootingSound");

        RaycastHit hitInfo;
        bool ishitted = Physics.Raycast(transform.position, transform.forward, out hitInfo, range);
        
        hitEffect.transform.position = hitInfo.point;
        hitEffect.transform.forward = hitInfo.normal;
        hitEffect.Emit(1);


            if (ishitted)
            {
                    if (hitInfo.collider.tag == "zombie")
                    {
                        Instantiate(BloodSplash, hitInfo.point, Quaternion.identity);
                    }
                Debug.Log(hitInfo.collider.name);
            
                ZombieHealth zombiehealth = hitInfo.transform.GetComponent<ZombieHealth>();
                if ( zombiehealth != null)
                {
                    zombiehealth.TakeDamage(damage, hitInfo.point);
                   
                }
                gunLine.SetPosition (1, hitInfo.point);
                Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
            }
            else
            {
                gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
            }
        
    }
}
