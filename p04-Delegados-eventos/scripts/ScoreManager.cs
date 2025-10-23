/*
  ScoreManager.cs
  Gestiona la puntuación del jugador.
*/

using UnityEngine;

public class ScoreManager : MonoBehaviour
{
  public int score = 0;

  public void AddPoints(int points)
  {
    int oldScore = score;
    score += points;
    Debug.Log($"Puntos base: +{points} × {gameManager.GetGananciasMultiplier():F2} = +{puntosFinales}. Total: {score}");
  }

  public void ResetScore()
  {
    score = 0;
    Debug.Log("Puntuación reiniciada a 0");
  }
  
}