using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textScore;
    int currentScore;

    [SerializeField] Image livesImage;
    [SerializeField] Sprite[] livesSprites;
    [SerializeField] TextMeshProUGUI textGameOver;
    [SerializeField] TextMeshProUGUI textRestartGame;
    [SerializeField] GameObject buttonRestart;
    [SerializeField] float timerFlickTextGameOver = 0.5f;

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ShowGameOver(false);
        currentScore = 0;
        AddScore(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateLives(int currentLives)
    {
        livesImage.sprite = livesSprites[currentLives];
    }

    public void ShowGameOver(bool isShow)
    {
        textGameOver.enabled = isShow;
        textRestartGame.enabled = isShow;
        buttonRestart.SetActive(isShow);
        if (isShow)
        {
            gameManager.IsGameOver = true;
            StartCoroutine(FlickTextGameOver());
        }
    }

    IEnumerator FlickTextGameOver()
    {
        while (true)
        {
            yield return new WaitForSeconds(timerFlickTextGameOver);
            textGameOver.enabled = !textGameOver.enabled;
        }
    }

    public void AddScore(int score)
    {
        if (textScore != null)
        {
            currentScore += score;
            textScore.text = $"Score : {currentScore}";
        }
    }
}
