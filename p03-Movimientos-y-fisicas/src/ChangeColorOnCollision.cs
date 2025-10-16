using UnityEngine;

public class ChangeColorOnCollision : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisión con: " + collision.gameObject.name);
        rend.material.color = Random.ColorHSV(); // Cambia a color aleatorio
    }

    void OnCollisionExit(Collision collision)
    {
        rend.material.color = originalColor; // Revierte al salir
    }
}
