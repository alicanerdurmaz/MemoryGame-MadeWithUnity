using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour {

    public int scoreCount = 0;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public void UpdateScore()
    {
        scoreCount += 1;
        scoreText.text = "" + scoreCount;
    }
}
