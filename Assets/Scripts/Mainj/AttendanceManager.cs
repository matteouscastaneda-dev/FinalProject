using System.Collections.Generic;

public class AttendanceManager
{
    private readonly List<Student> activeStudents = new List<Student>();

    public int ActiveCount { get { return activeStudents.Count; } }
    public bool IsEmpty { get { return activeStudents.Count == 0; } }
    public IReadOnlyList<Student> ActiveStudents { get { return activeStudents; } }

    /// <summary>
    /// Populates the class
    /// </summary>
    public void PopulateClass(int startingCount)
    {
        activeStudents.Clear();
        for (int i = 0; i < startingCount; i++)
        {
            activeStudents.Add(new Student("Student " + (i + 1)));
        }
    }

    /// <summary>
    /// Removes the most recently added active student 
    /// </summary>
    public Student RemoveOneStudent()
    {
        if (activeStudents.Count == 0)
        {
            return null;
        }

        int lastIndex = activeStudents.Count - 1;
        Student leaving = activeStudents[lastIndex];
        activeStudents.RemoveAt(lastIndex);
        return leaving;
    }
}