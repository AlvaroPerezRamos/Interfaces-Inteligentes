/*
  ScoreManager.cs
  Gestiona la puntuación del jugador, actualiza la UI y otorga recompensas por hitos.
*/

using UnityEngine;

public class ScoreManager3 : MonoBehaviour
{
  public int score = 0;
  private UIManager2 uiManager;
  private int nextRewardThreshold = 100;
  private GameManager gameManager;


  void Start()
  {
    uiManager = FindFirstObjectByType<UIManager2>();
    gameManager = FindFirstObjectByType<GameManager>();
    UpdateUI();
  }

  public void AddPoints(int points)
  {
    float puntosConMultiplier = points * gameManager.GetGananciasMultiplier();
    int puntosFinales = Mathf.RoundToInt(puntosConMultiplier);

    int oldScore = score;
    score += puntosFinales;
    Debug.Log($"Puntos base: +{points} × {gameManager.GetGananciasMultiplier():F2} = +{puntosFinales}. Total: {score}");
    CheckForReward(oldScore);
    UpdateUI();
  }

  void CheckForReward(int oldScore)
  {
    int oldThresholds = oldScore / 100;
    int newThresholds = score / 100;

    if (newThresholds > oldThresholds)
    {
      int rewardsEarned = newThresholds - oldThresholds;

      for (int i = 0; i < rewardsEarned; i++)
      {
        int rewardThreshold = (oldThresholds + i + 1) * 100;
        GrantReward(rewardThreshold);
      }
    }
  }

  void GrantReward(int threshold)
  {
    Debug.Log($"¡RECOMPENSA OBTENIDA! {threshold} puntos - Mejora aleatoria aplicada");

    // Aplicar mejora aleatoria
    if (gameManager != null)
    {
      gameManager.AplicarMejoraAleatoria();
    }

    if (uiManager != null)
    {
      uiManager.ShowReward($"¡{threshold} puntos!\nMejora aplicada");
    }

    nextRewardThreshold = threshold + 100;
  }
  void UpdateUI()
  {
    if (uiManager != null)
    {
      uiManager.UpdateScore(score);
      int progress = score % 100;
      uiManager.UpdateRewardProgress(progress, 100);
    }
  }

  public void ResetScore()
  {
    score = 0;
    Debug.Log("Puntuación reiniciada a 0");
    nextRewardThreshold = 100;
    UpdateUI();
  }
  public (int current, int total) GetRewardProgress()
  {
    return (score % 100, 100);
  }
}