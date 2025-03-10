using UnityEngine;

public class HMovementController : MonoBehaviour
{
    [SerializeField] public float speed = 2f;
    public float maxSizeFactor = 2.4f; 
    public float minSizeFactor = 1f; 
    public AudioClip gangnam; // Sonido en loop

    public float maxVolume = 1f; // Volumen máximo del sonido
    public float minVolume = 0.2f; // Volumen mínimo del sonido

    private Rigidbody2D rb;
    private Vector3 originalScale;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;

        // Agregar un AudioSource si no existe
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = gangnam;
        audioSource.loop = true;  // 🔄 Mantiene el sonido encendido en todo momento
        audioSource.playOnAwake = true;
        audioSource.volume = minVolume; // Empezar con volumen bajo
        audioSource.Play();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");   

        Vector2 movement = new Vector2(horizontal, vertical) * speed;
        rb.linearVelocity = movement; // Corrección: `velocity`, no `linearVelocity`

        if (vertical < 0) // Si el jugador baja, crece
        {
            if (transform.localScale.x < originalScale.x * maxSizeFactor && transform.localScale.y < originalScale.y * maxSizeFactor)
            {
                transform.localScale *= 1.01f; // Crece
            }
        }
        else if (vertical > 0) // Si el jugador sube, se reduce
        {
            if (transform.localScale.x > originalScale.x * minSizeFactor && transform.localScale.y > originalScale.y * minSizeFactor)
            {
                transform.localScale *= 0.99f; // Se encoge
            }
        }

        // 🔊 Ajustar el volumen del sonido según el tamaño del personaje
        float sizeFactor = (transform.localScale.x - originalScale.x * minSizeFactor) / 
                           (originalScale.x * maxSizeFactor - originalScale.x * minSizeFactor);
        
        audioSource.volume = Mathf.Lerp(minVolume, maxVolume, sizeFactor);
    }
}
