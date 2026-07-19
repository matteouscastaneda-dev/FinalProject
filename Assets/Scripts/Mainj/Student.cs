using UnityEngine;

[System.Serializable]
public class Student
{
    public string studentName;

    /// <summary>
    /// Sets a new student with a name
    /// </summary>
    public Student(string name)
    {
        studentName = name;
    }
}