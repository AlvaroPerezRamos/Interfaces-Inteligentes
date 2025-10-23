using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ComportamientoHumanoide2 : MonoBehaviour
{
  public Notificador3 notificador;
  public float speed = 2.5f;
  public float rotSpeed = 4f;
  public GameObject shield1;
  public GameObject shield2;

  private bool moveToShield1 = false;
  private bool moveToShield2 = false;
  private Animator anim;
  private Renderer humanoidRenderer;
  private Color originalColor;
  private bool colorChanged = false;

  void Start()
  {
    anim = GetComponent<Animator>();
    anim.applyRootMotion = false;

    humanoidRenderer = GetComponentInChildren<Renderer>();
    if (humanoidRenderer != null)
      originalColor = humanoidRenderer.material.color;

    if (notificador == null)
    {
      Debug.LogError($"{name} no tiene asignado el Notificador3 en el inspector.");
      return;
    }

    // Suscribirse según el tag del humanoide
    if (CompareTag("Humanoide_Tipo1"))
    {
      notificador.OnTriggerHumanoide2 += OnHumanoide2Touched;
    }
    else if (CompareTag("Humanoide_Tipo2"))
    {
      notificador.OnTriggerHumanoide1 += OnHumanoide1Touched;
    }
  }

  void Update()
  {
    // Verificar si el escudo objetivo aún existe y está activo
    if (moveToShield1 && (shield1 == null || !shield1.activeInHierarchy))
    {
      moveToShield1 = false;
      if (anim != null)
        anim.SetBool("isWalking", false);
      Debug.Log($"{name} se detuvo - escudo tipo 1 no disponible");
    }

    if (moveToShield2 && (shield2 == null || !shield2.activeInHierarchy))
    {
      moveToShield2 = false;
      if (anim != null)
        anim.SetBool("isWalking", false);
      Debug.Log($"{name} se detuvo - escudo tipo 2 no disponible");
    }

    // Movimiento normal solo si el escudo está disponible
    if (moveToShield1 && shield1 != null && shield1.activeInHierarchy)
      MoveTowards(shield1.transform);

    if (moveToShield2 && shield2 != null && shield2.activeInHierarchy)
      MoveTowards(shield2.transform);
  }

  void MoveTowards(Transform target)
  {
    Vector3 direction = target.position - transform.position;
    direction.y = 0;

    if (direction.magnitude > 0.1f)
    {
      Quaternion lookRot = Quaternion.LookRotation(direction);
      transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * rotSpeed);

      transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);

      if (anim != null)
        anim.SetBool("isWalking", true);
    }
    else
    {
      if (anim != null)
        anim.SetBool("isWalking", false);
    }
  }

  void OnHumanoide1Touched()
  {
    Debug.Log($"{name} (tipo 2) moviéndose hacia escudo tipo 2");
    moveToShield2 = true;
    moveToShield1 = false;
  }

  void OnHumanoide2Touched()
  {
    Debug.Log($"{name} (tipo 1) moviéndose hacia escudo tipo 1");
    moveToShield1 = true;
    moveToShield2 = false;
  }

  // Método para reaccionar cuando reaparezcan los escudos
  public void OnShieldRespawned(GameObject respawnedShield)
  {
    // Si es humanoide tipo 1 y reaparece SU escudo tipo 1 asignado, reanudar movimiento
    if (CompareTag("Humanoide_Tipo1") && respawnedShield.CompareTag("Shield_Tipo1"))
    {
      if (shield1 == respawnedShield)
      {
        moveToShield1 = true;
        Debug.Log($"{name} reanuda movimiento hacia escudo tipo 1");
      }
    }
    // Si es humanoide tipo 2 y reaparece SU escudo tipo 2 asignado, reanudar movimiento
    else if (CompareTag("Humanoide_Tipo2") && respawnedShield.CompareTag("Shield_Tipo2"))
    {
      if (shield2 == respawnedShield)
      {
        moveToShield2 = true;
        Debug.Log($"{name} reanuda movimiento hacia escudo tipo 2");
      }
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Shield_Tipo1") && !colorChanged)
    {
      CambiarColor(Color.green);
      colorChanged = true;
      Debug.Log($"{name} cambió de color PERMANENTE al tocar escudo tipo 1");
    }
    if (other.CompareTag("Shield_Tipo2") && !colorChanged)
    {
      CambiarColor(Color.red);
      colorChanged = true;
      Debug.Log($"{name} cambió de color PERMANENTE al tocar escudo tipo 2");
    }
  }

  void CambiarColor(Color nuevoColor)
  {
    if (humanoidRenderer != null)
    {
      Material m = new Material(humanoidRenderer.material);
      m.color = nuevoColor;
      humanoidRenderer.material = m;
    }
  }

  // Métodos públicos para control externo
  public void StopMovement()
  {
    moveToShield1 = false;
    moveToShield2 = false;
    if (anim != null)
      anim.SetBool("isWalking", false);
  }

  public void ResumeMovementToShield1()
  {
    if (shield1 != null && shield1.activeInHierarchy)
    {
      moveToShield1 = true;
      moveToShield2 = false;
    }
  }

  public void ResumeMovementToShield2()
  {
    if (shield2 != null && shield2.activeInHierarchy)
    {
      moveToShield2 = true;
      moveToShield1 = false;
    }
  }

  public void ResetColor()
  {
    if (humanoidRenderer != null)
    {
      Material m = new Material(humanoidRenderer.material);
      m.color = originalColor;
      humanoidRenderer.material = m;
      colorChanged = false;
    }
  }

  // Para limpiar suscripciones
  void OnDestroy()
  {
    if (notificador != null)
    {
      if (CompareTag("Humanoide_Tipo1"))
      {
        notificador.OnTriggerHumanoide2 -= OnHumanoide2Touched;
      }
      else if (CompareTag("Humanoide_Tipo2"))
      {
        notificador.OnTriggerHumanoide1 -= OnHumanoide1Touched;
      }
    }
  }
}