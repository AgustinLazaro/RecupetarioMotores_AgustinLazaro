using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private float speed = 100f;
    [SerializeField] private float lifeTime = 2f;
    void OnEnable()
    {
        Invoke("DesactivarBala", lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void DesactivarBala()
    {
        gameObject.SetActive(false);
    }
    void OnDisable()
    {
        CancelInvoke();
    }
}