using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 3f;
    [SerializeField]
    private GameObject explosionPrefab;
    private SpawnManager sm;

    private void Start() {
        sm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        
    }
    void Update() {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Laser") {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            sm.startSpawning();
            Destroy(this.gameObject,0.15f);
        }
    }
}
