using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float speed = 100f; // Velocidad a la que viaja la bala
    [SerializeField] private float lifeTime = 2f;  // Cuánto tiempo vive antes de desaparecer

    void Start()
    {
        // Apenas la bala aparece en el mundo, le decimos que se destruya después de 'lifeTime' segundos
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Movemos la bala hacia adelante constantemente en cada frame
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
