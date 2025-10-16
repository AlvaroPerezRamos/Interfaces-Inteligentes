using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    private Rigidbody rb;

    [Header("Color y daño")]
    private Renderer rend;
    private Color originalColor;
    public int damage = 0;

    void Start()
    {
        // Referencias
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();

        rb.freezeRotation = true;
        originalColor = rend.material.color;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal"); // A/D o Flechas
        float v = Input.GetAxis("Vertical");   // W/S o Flechas

        Vector3 direction = new Vector3(h, 0, v).normalized;

        // Movimiento controlado con física
        Vector3 move = direction * speed;
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
    }

    // Métodos públicos para las zonas trigger
    public void ChangeColor(Color newColor)
    {
        rend.material.color = newColor;
    }

    public void ResetColor()
    {
        rend.material.color = originalColor;
    }

    public void AddDamage(int amount)
    {
        damage += amount;
        Debug.Log("Daño actual del jugador: " + damage);
    }
}
