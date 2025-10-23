using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager3 : MonoBehaviour
{
  [Header("Score UI")]
  public Text scoreText;
  public Text rewardText;
  public Slider rewardProgressSlider;
  public GameObject rewardPanel;

  [Header("Mejoras UI")]
  public Text velocidadText;
  public Text gananciasText;
  public Text respawnText;
  public GameObject mejorasPanel;

  [Header("Game Over UI")]
  public GameObject gameOverPanel;
  public Text gameOverText;

  private Coroutine rewardCoroutine;

  void Start()
  {
    // Inicializar UI de score (igual que UIManager2)
    if (scoreText != null)
      scoreText.text = "Puntuación: 0";

    if (rewardProgressSlider != null)
    {
      rewardProgressSlider.minValue = 0;
      rewardProgressSlider.maxValue = 100;
      rewardProgressSlider.value = 0;
    }

    if (rewardPanel != null)
      rewardPanel.SetActive(false);

    if (rewardText != null)
      rewardText.text = "";

    // 👇 NUEVO: Inicializar Game Over UI
    if (gameOverPanel != null)
      gameOverPanel.SetActive(false);

    if (gameOverText != null)
      gameOverText.text = "";

    ActualizarMejoras(1f, 1f, 1f);

    if (mejorasPanel != null)
      mejorasPanel.SetActive(true);
  }

  // 👇 NUEVO: Método para mostrar Game Over
  public void ShowGameOver(string message = "GAME OVER\nTodos los humanoides alcanzaron sus escudos")
  {
    if (gameOverPanel != null && gameOverText != null)
    {
      gameOverText.text = message;
      gameOverPanel.SetActive(true);
      Debug.Log("UI: Mostrando Game Over");
    }
    else
    {
      Debug.LogWarning("UI: No se encontraron componentes de Game Over UI");
    }
  }

  // 👇 NUEVO: Método para ocultar Game Over (si necesitas reiniciar)
  public void HideGameOver()
  {
    if (gameOverPanel != null)
      gameOverPanel.SetActive(false);

    if (gameOverText != null)
      gameOverText.text = "";
  }

  // 👇 MÉTODOS EXISTENTES (igual que UIManager2)
  public void UpdateScore(int score)
  {
    if (scoreText != null)
    {
      scoreText.text = $"Puntuación: {score}";
    }
  }

  public void UpdateRewardProgress(int current, int total)
  {
    if (rewardProgressSlider != null)
    {
      rewardProgressSlider.value = current;
    }
  }

  public void ShowReward(string rewardMessage)
  {
    if (rewardText != null && rewardPanel != null)
    {
      rewardText.text = rewardMessage;
      rewardPanel.SetActive(true);

      if (rewardCoroutine != null)
        StopCoroutine(rewardCoroutine);

      rewardCoroutine = StartCoroutine(HideRewardAfterDelay(3f));
    }
  }

  IEnumerator HideRewardAfterDelay(float delay)
  {
    yield return new WaitForSeconds(delay);

    if (rewardPanel != null)
      rewardPanel.SetActive(false);

    if (rewardText != null)
      rewardText.text = "";
  }

  public void ActualizarMejoras(float velocidad, float ganancias, float respawn)
  {
    if (velocidadText != null)
      velocidadText.text = $"Velocidad x{velocidad:F2}";

    if (gananciasText != null)
      gananciasText.text = $"Ganancias x{ganancias:F2}";

    if (respawnText != null)
      respawnText.text = $"Respawn x{respawn:F2}";
  }
}