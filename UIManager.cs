using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {
   
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Image livesImage;
    [SerializeField]
    private Sprite[] livesSprites;

    [SerializeField]
    private Text gameOverText;

    [SerializeField]
    private Text restartText;

    private GameManager gameManager;
    void Start() {
       
        scoreText.text = "Score: " + 0;
        gameOverText.gameObject.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void updateScore(int playerScore){
        scoreText.text = "Score: " + playerScore;
    }
    public void updateLives(int currentLives) {
        livesImage.sprite = livesSprites[currentLives];
        if(currentLives == 0) {
            gameManager.gameOver();
            gameOverText.gameObject.SetActive(true);
            restartText.gameObject.SetActive(true);
            StartCoroutine(gameOverFlickerRoutine());
        }
    }
    IEnumerator gameOverFlickerRoutine() {
        while(true) {
            gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
