using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;

    private int _score;
    private int _maxScore;

    public void UpdateUI()
    {
        _scoreText.text = "Score: " + _score + " / " + _maxScore;
    }

    public void SetMaxScore(int value)
    {
        _maxScore = value;
        UpdateUI();

        Debug.Log($"Max Score: {_maxScore}");
    }

    public void AddScore(int value)
    {
        _score += value;
        UpdateUI();
    }

    private void Start()
    {
        _score = 0;
        //$_maxScore = 0;
        UpdateUI();
    }
}
