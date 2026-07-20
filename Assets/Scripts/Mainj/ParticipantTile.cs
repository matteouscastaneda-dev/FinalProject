using UnityEngine;
using TMPro;

public class ParticipantTile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameLabel;

    /// <summary>
    /// Updates the tile's displayed student name.
    /// </summary>
    public void SetName(string studentName)
    {
        nameLabel.text = studentName;
    }
}