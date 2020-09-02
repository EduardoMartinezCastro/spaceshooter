using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject container;

    [SerializeField]
    private GameObject[] powerUps;
    private bool stopSpawning = false;
    public void startSpawning(){
        StartCoroutine(spawnRoutine());
        StartCoroutine(spawnPowerUpRoutine());
    }


    IEnumerator spawnRoutine() {
        yield return new WaitForSeconds(3.0f);
        while(!stopSpawning) {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject nw = Instantiate(enemyPrefab,posToSpawn,Quaternion.identity);
            nw.transform.parent = container.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator spawnPowerUpRoutine() {
        yield return new WaitForSeconds(3.0f);
        while(!stopSpawning) {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPU = Random.Range(0, 3);
            
            Instantiate(powerUps[randomPU], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void onPlayerDeath() {
        stopSpawning = true;
    }
}
