using System;
using System.Collections;
using TinyTools.AutoLoad;
using UnityEngine;

[AutoLoad]
public class TurnManager : MonoBehaviour
{
    public static float TurnInterval { get; private set; } = 0.5f;

    public static event Action TurnUpdate;

    private void Start()
    {
        StartCoroutine(TurnIntervalCoroutine());
    }

    private IEnumerator TurnIntervalCoroutine()
    {
        yield return new WaitForSeconds(TurnInterval);
        TurnUpdate?.Invoke();
        StartCoroutine(TurnIntervalCoroutine());
    }
}
