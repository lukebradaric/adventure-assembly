using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TinyTools.ScriptableVariables;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private IntScriptableVariable _score;
    [SerializeField] private IntScriptableVariable _highScore;
    private void Awake()
    {
        _score.Value = 0;
        _scoreText.text = "Score: " + _score.Value.ToString();
    }
    public void UpdateScore()
    {
        _scoreText.text = "Score: " + _score.Value.ToString();
        if(_score.Value > _highScore.Value)
        {
            _highScore.Value = _score.Value;
        }
    }
}
