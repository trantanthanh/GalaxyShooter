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
    // Start is called before the first frame update
    void Start()
    {
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
