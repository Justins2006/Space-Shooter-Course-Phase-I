using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _enemySpeed = 6.0f;

    private Player _player;
    private Spacemine _spaceMine;

    private UIManager _uiManager;


    public GameObject enemyLaser;

    private AudioSource _audioSource;

    public bool isDying;

    private float _fireRate = 5.0f;
    private float _canFire = -1.0f;

    private Shake shake;

    [SerializeField]
    private Animator _OnEnemyDeath;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();

        if (_player == null)
        {
            Debug.Log("The Player is NULL!!!");
        }

        _OnEnemyDeath = GetComponent<Animator>();

        if (_OnEnemyDeath == null)
        {
            Debug.LogError("The Animator is NULL!!!");
        }
    }

    void Update()
    {
        CalculateMovement();
        Lasers();


    }
   
    void Lasers()
    {
        if (Time.time > _canFire && isDying == false)
        {
            _fireRate = Random.Range(6f, 8f);
            _canFire = Time.time + _fireRate;
            GameObject _enemyLaser = Instantiate(enemyLaser, transform.position - new Vector3(0, 0.3f, 0), Quaternion.identity);

            Laser[] lasers = _enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemy();
            }

        }

    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
        if (transform.position.y <= -10)
        {
            float randomX = Random.Range(-7.5f, 7.5f);
            float randomY = Random.Range(7, 12);
            transform.position = new Vector3(randomX, randomY, 0);
        }
    }

    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        PolygonCollider2D _destroyCollider = GetComponentInChildren<PolygonCollider2D>();

        
        Debug.Log(gameObject.name + " hit: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            isDying = true;
            shake.CamShake();
            _destroyCollider.enabled = false;
            _player.Damage();
            _OnEnemyDeath.SetTrigger("OnEnemyDeath");
            _enemySpeed = 5;
            _destroyCollider.enabled = false;
            _audioSource.Play();
            Destroy(gameObject, 4f);
        }


        if (other.CompareTag("Laser"))
        {
            isDying = true;
            shake.CamShake();
            _destroyCollider.enabled = false;
            Destroy(other.gameObject);
            _uiManager.AddEnemyCount();
            if (_player != null)
            {
                _player.AddScore(100);

            }
            _OnEnemyDeath.SetTrigger("OnEnemyDeath");
            _enemySpeed = 5;
            _destroyCollider.enabled = false;
            _audioSource.Play();
            Destroy(gameObject, 3f);

        }

        if(other.CompareTag("Spacemine"))
        {
            _spaceMine = GameObject.FindGameObjectWithTag("Spacemine").GetComponent<Spacemine>();
            isDying = true;
            shake.CamShake();
            _destroyCollider.enabled = false;
            _spaceMine.IsActivated();
            if (_player != null)
            {
                _player.AddScore(150);
            }
            _OnEnemyDeath.SetTrigger("OnEnemyDeath");
            _enemySpeed = 5; 
            _audioSource.Play();
            Destroy(gameObject, 3f);
            
        }
    }

    

}
