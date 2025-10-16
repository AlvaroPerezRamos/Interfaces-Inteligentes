using UnityEngine;

public class ZoneEffect : MonoBehaviour
{
    public enum ZoneType { Color, Damage } // Tipos de zona
    public ZoneType type = ZoneType.Color;

    [Header("Zona de color")]
    public Color zoneColor = Color.red;

    [Header("Zona de daño")]
    public int damageAmount = 10;

    void Start()
    {
        // Hacer el cubo invisible si tiene un Renderer
        var rend = GetComponent<Renderer>();
        if (rend != null)
            rend.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player == null) return;

        switch (type)
        {
            case ZoneType.Color:
                player.ChangeColor(zoneColor);
                Debug.Log("Entró en zona de color: " + name);
                break;

            case ZoneType.Damage:
                player.AddDamage(damageAmount);
                Debug.Log("Entró en zona de daño: " + name + " - Daño: " + damageAmount);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player == null) return;

        if (type == ZoneType.Color)
        {
            player.ResetColor();
            Debug.Log("Salió de zona de color: " + name);
        }
    }
}
