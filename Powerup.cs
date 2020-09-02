using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.5f;

    [SerializeField]
    private float powerUpId;
    [SerializeField]
    private AudioClip clip;
   
    void Update(){
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if(transform.position.y < -4.5f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            Player p = collision.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(clip, transform.position);
            if(p != null) {

                if(powerUpId == 0) {
                    p.tripleShotActive();
                }else if(powerUpId == 1) {
                    p.speedBoostActive();
                }else if(powerUpId == 2) {
                    p.shieldActive();
                }
            }
            Destroy(this.gameObject);
        }
    }
}
