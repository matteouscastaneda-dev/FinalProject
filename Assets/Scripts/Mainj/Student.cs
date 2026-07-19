using UnityEngine;

[System.Serializable]
public class Student
{
    public string studentName;
    public EvaluationTier rating;

    /// <summary>
    /// Default for new students
    /// </summary>
    public Student(string name)
    {
        studentName = name;
        rating = EvaluationTier.Passing;
    }
}

/// <summary>
/// Reaction bundle for a rating
/// </summary>
[System.Serializable]
public class StudentReaction
{
    public Sprite portrait;
    public string chatLine;
    public AudioClip sfx;
}