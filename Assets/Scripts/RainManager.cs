using UnityEngine;

public class RainManager : MonoBehaviour
{
    public GameObject rainPrefab; // Le prefab pour les �l�ments de pluie
    float spawnRate = 0.1f; // Taux de g�n�ration des �l�ments (en secondes)
    Vector3 spawnAreaMin = new Vector3(-14.5f, 59.1f, 60.4f); // Coin inf�rieur gauche de la zone de g�n�ration
    Vector3 spawnAreaMax = new Vector3(93.5f, 69.6f, 93.2f); // Coin sup�rieur droit de la zone de g�n�ration

    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnRainElement();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnRainElement()
    {
        float spawnX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float spawnY = spawnAreaMax.y; // Toujours en haut
        float spawnZ = Random.Range(spawnAreaMin.z, spawnAreaMax.z); // Profondeur si n�cessaire
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

        Instantiate(rainPrefab, spawnPosition, Quaternion.identity);
    }
}
