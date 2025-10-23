/*
  UIManager.cs
  Gestiona la interfaz de usuario, actualiza la puntuación, muestra recompensas y mejora estadísticas.
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
  [Header("Score UI")]
  public Text scoreText;
  
  void Start()
  {
    // Inicializar UI de score
    if (scoreText != null)
      scoreText.text = "Puntuación: 0";
  }

  public void UpdateScore(int score)
  {
    if (scoreText != null)
    {
      scoreText.text = $"Puntuación: {score}";
    }
  }
}