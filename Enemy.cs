using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    [SerializeField]
    private float speed = 4.0f;

    private Player player;
    private Animator anim;
    private AudioSource audioSource;
    private bool hasBeenDestroyed = false;
    private float fireRate = 3.0f;
    private float canFire = -1f;

    [SerializeField]
    private GameObject laserPrefab;
    
    private void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); 
    }
    void Update()
    {
        move();
        if(Time.time > canFire) {
            fireRate = Random.Range(3f, 7f);
            canFire = Time.time + fireRate;
            GameObject el = Instantiate(laserPrefab,transform.position, Quaternion.identity);
            Laser[] lasers = el.GetComponentsInChildren<Laser>();
            lasers[0].assignEnemuLaser();
            lasers[1].assignEnemuLaser();
        }
    }

    void move() {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if(transform.position.y < -5.5f) {
            //Destroy(this.gameObject);
            transform.position = new Vector3(Random.Range(-8f, 8f), 7, 0);

        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(!hasBeenDestroyed) {
            if(other.tag == "Player") {
                Player p = other.transform.GetComponent<Player>();
                if(p != null)
                    p.damage();
                speed /= 2;
                anim.SetTrigger("onEnemyDeath");
                audioSource.Play();
                Destroy(this.gameObject, 2.6f);
                hasBeenDestroyed = true;

            }

            if(other.tag == "Laser") {
                Destroy(other.gameObject);
                if(player != null) {
                    player.addScore(10);
                }
                speed /= 2;
                anim.SetTrigger("onEnemyDeath");
                audioSource.Play();
                Destroy(this.gameObject, 2.6f);
                hasBeenDestroyed = true;
            }
        }
    }
}
