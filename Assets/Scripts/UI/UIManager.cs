using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textScore;
    int currentScore;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        AddScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
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
