using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TinyTools.ScriptableVariables;
public class DisplayHighScore : MonoBehaviour
{
    [SerializeField] private TMP_Text _highScore;
    [SerializeField] private IntScriptableVariable _highScoreInt;

    private void Awake()
    {
        _highScore.text = "High Score: " + _highScoreInt.Value.ToString();
    }
}
