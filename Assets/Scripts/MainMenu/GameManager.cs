
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static event Action<int> OnAttendanceChanged;
    public static event Action<Student> OnStudentLeft;

    [Header("Attendance")]
    [SerializeField] private int startingAttendance = 12;

    [Header("Evaluation Tiers")]
    [SerializeField] private EvaluationTierEntry[] tierEntries;

    private AttendanceManager attendance;
    private Dictionary<EvaluationTier, EvaluationTierEntry> tierLookup;

    public AttendanceManager Attendance { get { return attendance; } }
    public int StartingAttendance { get { return startingAttendance; } }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        BuildTierDictionary();
        attendance = new AttendanceManager();
        attendance.PopulateClass(startingAttendance);
    }

    private void Start()
    {
        if (OnAttendanceChanged != null)
        {
            OnAttendanceChanged.Invoke(attendance.ActiveCount);
        }
    }

    /// <summary>
    /// Copies the Inspector entries into a Dictionary
    /// </summary>
    private void BuildTierDictionary()
    {
        tierLookup = new Dictionary<EvaluationTier, EvaluationTierEntry>();

        for (int i = 0; i < tierEntries.Length; i++)
        {
            EvaluationTierEntry entry = tierEntries[i];
            tierLookup[entry.tier] = entry;
        }
    }

    /// <summary>
    /// Removes one student from the roster
    /// OnStudentLeft with the leaving student
    /// OnAttendanceChanged with the new count
    /// </summary>
    public void LoseOneStudent()
    {
        Student leaving = attendance.RemoveOneStudent();

        if (leaving == null)
        {
            return;
        }

        if (OnStudentLeft != null)
        {
            OnStudentLeft.Invoke(leaving);
        }
        if (OnAttendanceChanged != null)
        {
            OnAttendanceChanged.Invoke(attendance.ActiveCount);
        }
    }

    /// <summary>
    /// Gets the final grade from how many students remain
    /// </summary>
    public EvaluationTier EvaluateFinalScore()
    {
    int count = attendance.ActiveCount;
    EvaluationTier bestTier = EvaluationTier.Failed;
    int bestThreshold = -1;

    foreach (var pair in tierLookup)
    {
        EvaluationTierEntry entry = pair.Value;
        if (count >= entry.minAttendance && entry.minAttendance > bestThreshold)
        {
            bestTier = pair.Key;
            bestThreshold = entry.minAttendance;
        }
    }
    return bestTier;
}

    /// <summary>
    /// Returns the display message for a given evaluation tier
    /// </summary>
    public string GetTierMessage(EvaluationTier tier)
    {
        if (!tierLookup.ContainsKey(tier))
        {
            return tier.ToString();
        }
        return tierLookup[tier].message;
    }

    /// <summary>
    /// Starts scene load with the loading screen
    /// </summary>
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    /// <summary>
    /// Shows the loading overlay 
    /// Waits for scene load
    /// then hides the overlay
    /// </summary>
    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        UIManager.Instance.ShowLoadingScreen();

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        while (!op.isDone)
        {
            yield return null;
        }

        UIManager.Instance.HideLoadingScreen();
    }
}

/// <summary>
/// Inspector data for one evaluation tier
/// </summary>
[System.Serializable]
public class EvaluationTierEntry
{
    public EvaluationTier tier;
    public int minAttendance;
    public string message;
}