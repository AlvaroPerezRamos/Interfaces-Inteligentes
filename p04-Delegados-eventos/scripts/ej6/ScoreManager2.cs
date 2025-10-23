/*
  ScoreManager.cs
  Gestiona la puntuación del jugador y actualiza la UI.
*/

using UnityEngine;

public class ScoreManager2 : MonoBehaviour
{
  public int score = 0;
  private UIManager uiManager;

  void Start()
  {
    uiManager = FindFirstObjectByType<UIManager>();
    UpdateUI();
  }

  public void AddPoints(int points)
  {
    int oldScore = score;
    score += points;
    Debug.Log($"Total: {score}");
    UpdateUI();
  }

  void UpdateUI()
  {
    if (uiManager != null)
    {
      uiManager.UpdateScore(score);
    }
  }
  public void ResetScore()
  {
    score = 0;
    Debug.Log("Puntuación reiniciada a 0");
    UpdateUI();
  }
}