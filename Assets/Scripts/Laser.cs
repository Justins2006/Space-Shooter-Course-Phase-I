using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private float _laserSpeed = 24.0f;
    private float _enemyLaserSpeed = 15.0f;
    private AudioSource explosion;
    public bool isEnemyLaser = false;
    private Shake shake;

    void Start()
    {
        explosion = GameObject.Find("SpawnManager").GetComponent<AudioSource>();
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
    }
    void Update()
    {
        if (isEnemyLaser == false)
        {
            MoveUp();
        }

        if(isEnemyLaser == true)
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        if (transform.position.y >= 6)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * _enemyLaserSpeed * Time.deltaTime);

        if (transform.position.y < -8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
            Destroy(this.transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isEnemyLaser == true)
        {
            shake.CamShake();
            Player player = other.GetComponent<Player>();
            explosion.Play();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.transform.parent.gameObject);
        }
    }

    public void AssignEnemy()
    {
        isEnemyLaser = true;
    }
}
