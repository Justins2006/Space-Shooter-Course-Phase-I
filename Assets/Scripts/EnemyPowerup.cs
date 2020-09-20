using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPowerup : MonoBehaviour
{

    private float _enemyPowerupSpeed = 4.0f;

    public GameObject Laser;

    private Shake shake;

    [SerializeField]
    private GameObject[] _myPowerups;

    [SerializeField]
    private Animator _OnEnemyDeath;

    private AudioSource _audioSource;
     
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _OnEnemyDeath = GetComponent<Animator>();
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();

        if (_OnEnemyDeath == null)
        {
            Debug.LogError("The Animator is NULL!!!");
        }
    }

    void Update()
    {

        transform.Translate(Vector3.down * _enemyPowerupSpeed * Time.deltaTime);
        if (transform.position.y <= -7f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CapsuleCollider2D CCollider = GetComponent<CapsuleCollider2D>();
        BoxCollider2D BCollider = GetComponent<BoxCollider2D>();


        Debug.Log("Hit: " + other.transform.name);


        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            shake.CamShake();

            if (player != null)
            {
                player.Damage();
            }
            BCollider.enabled = false;
            CCollider.enabled = false;
            _enemyPowerupSpeed = 3.0f;
            _OnEnemyDeath.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            Destroy(this.gameObject, 3.27f);
        }

        if (other.CompareTag("Laser"))
        {
            shake.CamShake();
            BCollider.enabled = false;
            CCollider.enabled = false;
            _enemyPowerupSpeed = 3.0f;
            _OnEnemyDeath.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            Destroy(other.gameObject);
            Destroy(this.gameObject, 3.27f);

            int selectedPowerup = Random.Range(0, 3);
            Instantiate(_myPowerups[selectedPowerup], transform.position, Quaternion.identity);
        }


        if (other.CompareTag("Spacemine"))
        {
            Player player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Player>();
            Spacemine _spaceMine = other.gameObject.transform.GetComponent<Spacemine>();
            shake.CamShake();
            BCollider.enabled = false;
            CCollider.enabled = false;
            _spaceMine.IsActivated();
            if (_spaceMine == null)
                Debug.LogError("EnemyPowerup line 95 code error");
            if (player != null)
            {
                player.AddScore(200);
            }
            _enemyPowerupSpeed = 3.0f;
            _audioSource.Play();
            _OnEnemyDeath.SetTrigger("OnEnemyDeath");
            Destroy(this.gameObject, 3.27f);

        }

    }
}
