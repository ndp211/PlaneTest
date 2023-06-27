using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyBulletPrefab;

    public float moveDuration = 5f;
    public float spacing = 0.5f;

    private float shootTimer = 3f;
    private const float shootTime = 3f;

    public bool enemyIsMoving;

    private int m_score;

    public int Score { get => m_score; set => m_score = value; }


    public static List<GameObject> allEnemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
            allEnemies.Add(e);

        if (shootTimer <= 0) Shoot();
        shootTimer -= Time.deltaTime;
    }

    private IEnumerator SpawnEnemies()
    {
        enemyIsMoving = true;
        // Spawn 16 enemies in a square formation
        GameObject[] enem = new GameObject[16];
        
        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                
                float x = col * spacing - (1.5f * spacing);
                float y = row * spacing - (1.5f * spacing) + 2f;

                Vector3 spawnPosition = new Vector3(Random.Range(-10f,10f), Random.Range(-10f,10f), 0f);
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                enem[row * col] = enemy;
                StartCoroutine(MoveEnemy(enem[row * col], new Vector3(x, y, 0f), 3f));
                
            }
        }

        yield return new WaitForSeconds(moveDuration);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //Rearrange enemies into a diamond shape formation
        for (int i = 0; i < 16; i++)
        {
            float x = 0f;
            float y = 0f;

            if (i < 1)
            {
                x = 0f;
                y = 4f;
            }
            else if (i < 5)
            {
                x = i * 0.75f - 1.875f;
                y = 3.5f;
            }
            else if (i < 11)
            {
                x = (i - 5) * 0.75f - 1.875f;
                y = 3f;
            }
            else if (i < 15)
            {
                x = (i - 10) * 0.75f - 1.875f;
                y = 2.5f;
            }
            else
            {
                x = 0f;
                y = 2f;
            }

            StartCoroutine(MoveEnemy(enemies[i], new Vector3(x, y, 0f), moveDuration));
        }

        yield return new WaitForSeconds(moveDuration);

        //Rearrange enemies into a triangle formation
        for (int i = 0; i < 16; i++)
        {
            float x = 0f;
            float y = 0f;

            if (i < 1)
            {
                x = 0f;
                y = 0.5f;
            }
            else if (i < 5)
            {
                x = 0 + 0.6f * i;
                y = 0.5f;
            }
            else if (i < 9)
            {
                x = 0f - 0.6f * (i - 4);
                y = 0.5f;
            }
            else if (i < 11)
            {
                x = 0f - 1.8f + 3.6f * (i - 9);
                y = 1.5f;
            }
            else if (i < 13)
            {
                x = 0f - 1.2f + 2.4f * (i - 11);
                y = 2.5f;
            }
            else if (i < 15)
            {
                x = 0 - 0.6f + 1.2f * (i - 13);
                y = 3.5f;
            }
            else
            {
                x = 0;
                y = 4.5f;
            }
            StartCoroutine(MoveEnemy(enemies[i], new Vector3(x, y, 0f), moveDuration));
        }

        yield return new WaitForSeconds(moveDuration);

        //Rearrange enemies into a rectangle formation
        for (int i = 0; i < 16; i++)
        {
            float x = 0f;
            float y = 0f;

            if (i < 7)
            {
                x = -2.4f + i * 0.8f;
                y = 1f;
            }
            else if (i < 9)
            {
                x = -2.4f + 4.8f * (i - 7f);
                y = 2f;
            }
            else
            {
                x = -2.4f + (i - 9) * 0.8f;
                y = 3f;
            }
            StartCoroutine(MoveEnemy(enemies[i], new Vector3(x, y, 0f), moveDuration));


        }
        yield return new WaitForSeconds(moveDuration);

        enemyIsMoving = false;

    }
    private IEnumerator MoveEnemy(GameObject enemy, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = enemy.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            enemy.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        enemy.transform.position = targetPosition;
    }
    private void Shoot()
    {
        Vector2 pos = allEnemies[Random.Range(0, allEnemies.Count)].transform.position;

        Instantiate(enemyBulletPrefab, pos, Quaternion.identity);

        shootTimer = shootTime;
    }

}







