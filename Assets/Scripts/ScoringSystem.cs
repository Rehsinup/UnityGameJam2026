using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ScoreStep
{
    public float time;          // Temps en secondes
    public int pointsPerSecond; // Points gagnés après ce temps
}

public class ScoringSystem : MonoBehaviour
{
    [Header("Score")]
    public int score = 0;

    [Header("Score progression")]
    public int defaultPointsPerSecond = 10;
    public List<ScoreStep> scoreSteps;

    [SerializeField] private int currentPointsPerSecond;

    void Start()
    {
        currentPointsPerSecond = defaultPointsPerSecond;
        StartCoroutine(ScoreRoutine());
    }

    IEnumerator ScoreRoutine()
    {
        while (true)
        {
            UpdatePointsPerSecond();

            score += currentPointsPerSecond;
            Debug.Log($"Score: {score} (+{currentPointsPerSecond}/s)");

            yield return new WaitForSeconds(1f);
        }
    }

    void UpdatePointsPerSecond()
    {
        float elapsedTime = Time.timeSinceLevelLoad;

        for (int i = 0; i < scoreSteps.Count; i++)
        {
            if (elapsedTime >= scoreSteps[i].time)
            {
                currentPointsPerSecond = scoreSteps[i].pointsPerSecond;
            }
        }
    }
}
