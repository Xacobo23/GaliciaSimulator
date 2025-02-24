using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float velocidad = 5f;       // Velocidad en horizontal
    public float factorVelocidadY = 0.7f; // Factor para reducir la velocidad en vertical

    private Rigidbody2D rb;
    private Vector2 movimiento;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtiene el SpriteRenderer
    }

    void Update()
    {
        // Captura la entrada del jugador (WASD o flechas)
        float movX = Input.GetAxisRaw("Horizontal");
        float movY = Input.GetAxisRaw("Vertical") * factorVelocidadY; // Reducir velocidad en Y

        // Normaliza el movimiento para evitar mayor velocidad en diagonal
        movimiento = new Vector2(movX, movY).normalized;

        // Voltear el sprite sin afectar la física
        if (movX > 0)
            spriteRenderer.flipX = false; // Mirando a la derecha
        else if (movX < 0)
            spriteRenderer.flipX = true;  // Mirando a la izquierda
    }

    void FixedUpdate()
    {
        // Aplicar movimiento
        rb.linearVelocity = new Vector2(movimiento.x * velocidad, movimiento.y * velocidad);
    }
}
