using System;
using System.Collections;
using TinyTools.AutoLoad;
using UnityEngine;

[AutoLoad]
public class TurnManager : MonoBehaviour
{
    public static float TurnInterval { get; private set; } = 0.5f;

    public static event Action TurnUpdate;
    public static event Action EnemyTurnUpdate;

    private bool _defaultTurn = true;

    private void Start()
    {
        StartCoroutine(TurnIntervalCoroutine());
    }

    private IEnumerator TurnIntervalCoroutine()
    {
        yield return new WaitForSeconds(TurnInterval / 2);

        if (_defaultTurn)
            TurnUpdate?.Invoke();
        else
            EnemyTurnUpdate?.Invoke();

        _defaultTurn = !_defaultTurn;

        StartCoroutine(TurnIntervalCoroutine());
    }
}
