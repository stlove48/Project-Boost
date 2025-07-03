using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction<int> ScoreChanged;

    public static void OnScoreChanged(int changeAmount) => ScoreChanged?.Invoke(changeAmount);
}
