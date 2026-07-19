using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ParticipantTile : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI nameLabel;

    [Header("Colors")]
    [SerializeField] private Color activeColor = new Color(0.35f, 0.55f, 0.9f, 1f);
    [SerializeField] private Color absentColor = new Color(0.12f, 0.12f, 0.14f, 1f);

    public void SetActive(bool active)
    {
        if (active)
        {
            background.color = activeColor;
            nameLabel.enabled = true;
        }
        else
        {
            background.color = absentColor;
            nameLabel.enabled = false;
        }
    }

    /// <summary>
    /// Updates the student name
    /// </summary>
    public void SetName(string studentName)
    {
        nameLabel.text = studentName;
    }
}