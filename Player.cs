using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour {
   
    [SerializeField]
    private float speed = 4.0f;

    [SerializeField]
    private float speedMultiplier = 2f;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleShotPrefab;
    [SerializeField]
    private float fireRate = 0.2f;
    private float canFire = -1f;
    [SerializeField]
    private int lives = 3;

    private SpawnManager sm;
    [SerializeField]
    private bool isTripleShotActive = false;

    [SerializeField]
    private bool isSpeedBoostActive = false;
    private bool isShieldActive = false;

    [SerializeField]
    private GameObject shieldVisualizer;
    [SerializeField]
    private GameObject rightEngine;
    [SerializeField]
    private GameObject leftEngine;

    private int score = 0;
    private UIManager uiManager;

    [SerializeField]
    private AudioClip laserSound;

    [SerializeField]
    private GameObject explosionPrefab;
 

    private AudioSource audioSource;
    void Start(){
        transform.position = new Vector3(0, 0, 0);
        sm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = laserSound;
    }


    void Update(){
        calculateMovement();
        fireLaser();
    }

    private void fireLaser() {

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)) && Time.time > canFire) {
            canFire = Time.time + fireRate;
            if(isTripleShotActive) {
                Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
            } else {
                Instantiate(laserPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            }
            audioSource.Play();
        }
        
    }
    private void calculateMovement() {

        float horizontalInput =  Input.GetAxis("Horizontal");
        float verticalInput =  Input.GetAxis("Vertical");
       
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * speed * Time.deltaTime);
       
        if(transform.position.y >= 0) {
            transform.position = new Vector3(transform.position.x, 0, 0);

        } else if(transform.position.y <= -3.8f) {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);

        }

        if(transform.position.x > 11.3f) {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        } else if(transform.position.x < -11.3f) {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    public void damage() {
        
        if(isShieldActive) {
            isShieldActive = false;
            shieldVisualizer.SetActive(false);
            return;
        }
            
        lives--;
        if(lives == 2) {
            leftEngine.SetActive(true);
        }else if(lives == 1) {
            rightEngine.SetActive(true);
        }
        uiManager.updateLives(lives);
        if(lives < 1) {
            sm.onPlayerDeath();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        
    }

    public void tripleShotActive() {
        isTripleShotActive = true;
        StartCoroutine(tripleShotPowerDownRoutine());

    }
    public void speedBoostActive() {
        isSpeedBoostActive = true;
        speed *= speedMultiplier;
        StartCoroutine(speedBoostPowerDownRoutine());
    }

    public void shieldActive() {
        isShieldActive = true;
        shieldVisualizer.gameObject.SetActive(true);
    }
    IEnumerator tripleShotPowerDownRoutine() {
        yield return new WaitForSeconds(5.0f);
        isTripleShotActive = false;
    }

    IEnumerator speedBoostPowerDownRoutine() {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
        speed /= speedMultiplier;
    }

    public void addScore(int points) {
        score += points;
        uiManager.updateScore(score);
    }
}
