using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBullet : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D m_rb;
    public float timeToDestroy;
    private GameManager m_gm;


    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, timeToDestroy);
        m_gm = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        m_rb.velocity = Vector2.up * speed;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (m_gm != null && m_gm.enemyIsMoving)
            {
                collision.GetComponent<Enemy>().TakeDamage(0);
            }
            else
            {
                collision.GetComponent<Enemy>().TakeDamage(1);

                Destroy(gameObject);

            }
            
        }
    }
}
