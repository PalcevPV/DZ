using TMPro;
using UnityEngine;

public class StatisticsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _createdText;
    [SerializeField] private TMP_Text _spawnedText;
    [SerializeField] private TMP_Text _activeText;

    public void SetStatistics(int created, int spawned, int active)
    {
        _createdText.text = $"Created:{created}";
        _spawnedText.text = $"Spawned:{spawned}";
        _activeText.text = $"Active:{active}";
    }
}
