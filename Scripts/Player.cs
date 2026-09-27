using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float velocidadEmpuje = 200f;
    public float velocidadRotacion = 120f;
    
    public GameObject spawner, bulletPrefab;

    private Rigidbody rigid;

    public float xBorderLimit = 10f; 
    public float yBorderLimit = 6f;

    public static int SCORE = 0;

    public GameObject panel;

    public ObjectPool miPool;

    void Start()
    {
        SCORE = 0;
        Time.timeScale = 1;
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                pause();
            }
            else
            {
                resume();
            }
        }

        if (Time.timeScale == 0) return;

        float rotacion = Input.GetAxis("Horizontal") * Time.deltaTime;
        float empuje = Input.GetAxis("Vertical") * Time.deltaTime;

        Vector3 direccionEmpuje = transform.right;

        rigid.AddForce(direccionEmpuje * empuje * velocidadEmpuje);

        transform.Rotate(Vector3.forward, -rotacion * velocidadRotacion);

        var newPos = transform.position;
        
        if (newPos.x > xBorderLimit)
            newPos.x = -xBorderLimit + 1;
        else if (newPos.x < -xBorderLimit)
            newPos.x = xBorderLimit - 1;
        else if (newPos.y > yBorderLimit)
            newPos.y = -yBorderLimit + 1;
        else if (newPos.y < -yBorderLimit)
            newPos.y = yBorderLimit - 1;

        transform.position = newPos;

        if (Input.GetKeyDown(KeyCode.Space))
{
    if (miPool != null && spawner != null)
    {
        GameObject bullet = miPool.GetPooledObject();   
        if (bullet != null) 
        {
            bullet.transform.position = spawner.transform.position;
            bullet.transform.rotation = spawner.transform.rotation;

            Bullet balaScript = bullet.GetComponent<Bullet>();
            if (balaScript != null)
            {
                balaScript.targetVector = transform.right;
            }

            bullet.SetActive(true); 
        }
    }
}
}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("He colisionado con otra cosa...");
        }
    }

    public void pause()
    {
        Time.timeScale = 0;
        panel.SetActive(true);
    }

    public void resume()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }
}
