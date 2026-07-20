using System.Collections.Generic;
using UnityEngine;

public class ParticipantGrid : MonoBehaviour
{
    [SerializeField] private List<ParticipantTile> tiles = new List<ParticipantTile>();

    private void OnEnable()
    {
        GameManager.OnAttendanceChanged += HandleAttendanceChanged;
        if (GameManager.Instance != null)
        {
            RefreshGrid();
        }
    }

    private void OnDisable()
    {
        GameManager.OnAttendanceChanged -= HandleAttendanceChanged;
    }

    /// <summary>
    /// Handler for the attendance changed event. Refreshes all tiles.
    /// </summary>
    private void HandleAttendanceChanged(int newCount)
    {
        RefreshGrid();
    }

    /// <summary>
    /// Reads the current active rostee
    /// updates each tiles state
    /// </summary>
    private void RefreshGrid()
    {
        AttendanceManager attendance = GameManager.Instance.Attendance;
        IReadOnlyList<Student> students = attendance.ActiveStudents;

        for (int i = 0; i < tiles.Count; i++)
        {
            ParticipantTile tile = tiles[i];
            if (i < students.Count)
            {
                tile.gameObject.SetActive(true);
                tile.SetName(students[i].studentName);
            }
            else
            {
                tile.gameObject.SetActive(false);
            }
        }
    }
}