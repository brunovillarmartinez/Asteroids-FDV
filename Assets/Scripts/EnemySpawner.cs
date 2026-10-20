using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;

    public float spawnRatePerMinute = 30;
    public float spawnRateIncrement = 1f;

    private float spawnTime = 0;

    public float limite;

    public float tiempoVida = 3f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnTime){

            spawnTime = Time.time + 60/spawnRatePerMinute;

            spawnRatePerMinute += spawnRateIncrement;

            float rand = Random.Range(-limite, limite);

            Vector2 spawnPosition = new Vector2(rand, 8f);

            GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

            Destroy(meteor, tiempoVida);
        }
    }

   
}
