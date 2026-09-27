using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{   
    public float velocidad = 10f;
    public float tiempoVida = 3f;
    public Vector3 targetVector;

    private void OnEnable()
    {
        Invoke(nameof(DesactivarBala), tiempoVida);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(DesactivarBala));
    }

    private void DesactivarBala()
    {
        gameObject.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   

    }

    // Update is called once per frame
    void Update()
    {   
        
        transform.position += targetVector * velocidad * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision){



        if(collision.gameObject.tag == "Enemy"){

            Collider enemyCollider = collision.gameObject.GetComponent<Collider>();
            
            if (enemyCollider != null && enemyCollider.enabled) // Hago esto para que dos balas dando a un meteorito no consigan 2 puntos, tan solo 1
            {
                 enemyCollider.enabled = false;
                 IncreaseScore();
                 Destroy(collision.gameObject);
                 gameObject.SetActive(false);
            }
            else{
                 gameObject.SetActive(false);
            }

           

        }
    }

    private void IncreaseScore(){

       Player.SCORE++;
       UpdateScoreText();

    }

    private void UpdateScoreText(){

        GameObject puntos = GameObject.FindGameObjectWithTag("UI");
            puntos.GetComponent<Text>().text = "Puntos : " + Player.SCORE;
      
    

    }
}
