using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject friendlyBulletPrefab;
    public Transform shootingPoint;

    public float atkRate = 0.1f;
    private float m_speed = 3;

   
    private bool m_isDead = false;
  
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        float xDir = Input.GetAxis("Horizontal");
        float yDir = Input.GetAxis("Vertical");
        if ((xDir < 0 && transform.position.x <= -2.5f) || (xDir > 0 && transform.position.x >= 2.5f))
            return;
        if ((yDir < 0 && transform.position.y <= -4.5f) || (yDir > 0 && transform.position.y >= 4.5f))
            return;
        transform.position += Vector3.right * m_speed * xDir * Time.deltaTime;
        transform.position += Vector3.up * m_speed * yDir * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();
    }

    private void Shoot()
    {
        if (friendlyBulletPrefab && shootingPoint)
        {
            Instantiate(friendlyBulletPrefab, shootingPoint.position, Quaternion.identity);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("EnemyBullet"))
        {
            m_isDead = true;
        }
    }
}
