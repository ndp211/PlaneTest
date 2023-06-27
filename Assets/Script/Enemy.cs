using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 5; 
    private int currentHealth;
    
    bool m_isDead;
    private GameManager m_gm;


    // Start is called before the first frame update
    void Start()
    {   
        currentHealth = maxHealth;
        m_gm = FindAnyObjectByType<GameManager>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            m_isDead = true;
            Destroy(gameObject);
            m_gm.Score++;
        }
    }


}
