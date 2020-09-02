using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 8.0f;

    private bool isEnemyLaser = false;
    // Update is called once per frame
    void Update()
    {
        if(!isEnemyLaser) {
            moveUp();
        } else {
            moveDown();
        }
    }

    void moveUp() {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if(transform.position.y > 8) {
            if(transform.parent != null) {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    void moveDown() {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if(transform.position.y < -8f) {
            if(transform.parent != null) {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void assignEnemuLaser() {
        isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag=="Player" && isEnemyLaser) {
            Player p = collision.GetComponent<Player>();
            p.damage();
        }
    }
}
