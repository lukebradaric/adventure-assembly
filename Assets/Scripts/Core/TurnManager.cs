using System;
using System.Collections;
using TinyTools.AutoLoad;
using UnityEngine;

[AutoLoad]
public class TurnManager : MonoBehaviour
{
    public const float TurnInterval = 0.5f;

    public static event Action TurnUpdate;
    public static event Action EnemyTurnUpdate;

    public static int CurrentTurn { get; private set; } = 0;

    private bool _defaultTurn = true;

    private void Start()
    {
        StartCoroutine(TurnIntervalCoroutine());
        CurrentTurn = 0;
    }

    private IEnumerator TurnIntervalCoroutine()
    {
        yield return new WaitForSeconds(TurnInterval / 2);

        if (_defaultTurn)
        {
            TurnUpdate?.Invoke();
            CurrentTurn++;
        }
        else
        {
            EnemyTurnUpdate?.Invoke();
        }

        _defaultTurn = !_defaultTurn;

        StartCoroutine(TurnIntervalCoroutine());
    }
}
