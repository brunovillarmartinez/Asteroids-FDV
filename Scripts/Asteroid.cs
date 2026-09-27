using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject miniAsteroidPrefab; 
    public float anguloGrados = 45f;       
    public float velocidadMini = 5f;       
    public bool esMini = false;          

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.name.Contains("Bullet"))
        {

            if (!esMini && miniAsteroidPrefab != null)
            {
                Vector3 dirBala = Vector3.up; 
                Bullet bala = collision.gameObject.GetComponent<Bullet>();

                if (bala != null && bala.targetVector != Vector3.zero)
                {
                    dirBala = bala.targetVector.normalized;
                }
                else
                {
                    dirBala = (transform.position - collision.transform.position).normalized;
                }

                float rad = anguloGrados * Mathf.Deg2Rad;

                Vector3 dirIzquierda = new Vector3(
                    dirBala.x * Mathf.Cos(rad) - dirBala.y * Mathf.Sin(rad), 
                    dirBala.x * Mathf.Sin(rad) + dirBala.y * Mathf.Cos(rad), 
                    0f
                );

                Vector3 dirDerecha = new Vector3(
                    dirBala.x * Mathf.Cos(rad) + dirBala.y * Mathf.Sin(rad), 
                    -dirBala.x * Mathf.Sin(rad) + dirBala.y * Mathf.Cos(rad), 
                    0f
                );

                CrearMiniAsteroide(dirIzquierda);
                CrearMiniAsteroide(dirDerecha);
            }

            Destroy(gameObject);
        }
    }

    private void CrearMiniAsteroide(Vector3 direccion)
    {
        GameObject mini = Instantiate(miniAsteroidPrefab, transform.position, Quaternion.identity);

        Rigidbody nuevo = mini.GetComponent<Rigidbody>();
        if (nuevo != null)
        {
            nuevo.linearVelocity = direccion * velocidadMini;
        }
    }
}