using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ComportamientoHumanoide : MonoBehaviour
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
    if (moveToShield1 && shield1 != null)
      MoveTowards(shield1.transform);

    if (moveToShield2 && shield2 != null)
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

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Shield_Tipo1"))
    {
      if (humanoidRenderer != null)
      {
        Material m = new Material(humanoidRenderer.material);
        m.color = Color.green;
        humanoidRenderer.material = m;
      }
      Debug.Log($"{name} cambió de color al tocar un escudo tipo 1");
    }
    if (other.CompareTag("Shield_Tipo2"))
    {
      if (humanoidRenderer != null)
      {
        Material m = new Material(humanoidRenderer.material);
        m.color = Color.red;
        humanoidRenderer.material = m;
      }
      Debug.Log($"{name} cambió de color al tocar un escudo tipo 2");
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Shield_Tipo1") || other.CompareTag("Shield_Tipo2"))
    {
      if (humanoidRenderer != null)
      {
        Material m = new Material(humanoidRenderer.material);
        m.color = originalColor;
        humanoidRenderer.material = m;
      }
      Debug.Log($"{name} volvió a su color original");
    }
  }
}