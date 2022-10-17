using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    private int _score;
    private TMP_Text _scoreText;

    private void Start()
    {
        _scoreText = GetComponent<TMP_Text>();
        _scoreText.text = "Start";
    }

    public void IncreaseScore(int amountToIncrease)
    {
        _score += amountToIncrease;
        _scoreText.text = _score.ToString();
        //Debug.Log("Score is now: " + _score);
    }
}
