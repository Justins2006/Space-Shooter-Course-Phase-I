using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 20f;

    [SerializeField]
    private GameObject _explosionPrefab;

 
    public Player _player;

    private Spacemine _spaceMine;

    public bool _shieldActive;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
        if(transform.position.y <= -10f)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            Player _player = playerObj.GetComponent<Player>();
            _shieldActive = _player.shieldActive;

            if (_shieldActive)
            {
                _player.ShieldPowerupActive(false);
            }

                PolygonCollider2D playerCollider = _player.GetComponent<PolygonCollider2D>();
                Instantiate(_explosionPrefab, _player.transform.position, Quaternion.identity);
                _player.Damage(true);
         
        }

        if (other.CompareTag("Spacemine"))
        {
            Shake shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
            _spaceMine = GameObject.FindGameObjectWithTag("Spacemine").GetComponent<Spacemine>();
            shake.CamShake();
            _spaceMine.IsActivated();
            if (_player != null)
            {
                _player.AddScore(150);
            }
            Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
            Destroy(gameObject);

        }

        if (other.tag == "Enemy")
        {
            GameObject enemy = other.gameObject;
            Instantiate(_explosionPrefab, enemy.transform.position, Quaternion.identity);

            if(enemy != null)
                Destroy(other.transform.parent.gameObject);

            Destroy(other.transform.gameObject);
        }
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
        }
    }

}