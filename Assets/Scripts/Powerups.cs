using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    private readonly float _powerupSpeed = 4.0f;
    [SerializeField]
    private int _PowerupID;
    
    private AudioSource _audioSource;
    private int _mineCount;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _audioSource = GameObject.Find("Powerup_Sound").GetComponent<AudioSource>();
        if(_player != null)
            _mineCount = GameObject.Find("Player").GetComponent<Player>().mineCount;
        
    }
    private void Update()
    {
        transform.Translate(Vector3.down * _powerupSpeed * Time.deltaTime);

        if(transform.position.y < -8.0f)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            
            if (player != null)
            {
                _audioSource.Play();

                switch (_PowerupID)
                {
                    case 0:
                        Debug.Log("TripleShot Collected!");
                        player.TripleShotActive();
                        break;
                    case 1:
                        Debug.Log("Speed Collected!");
                        player.SpeedPowerupActive();
                        break;
                    case 2:
                        Debug.Log("Shield Collected!");
                        player.ShieldPowerupActive();
                        break;
                    case 3:
                        Debug.Log("Ammo Collected!");
                        player.AmmoAdd();
                        break;
                    case 4:
                        Debug.Log("Health Colllected!");
                        player.HealthAdd();
                        player.EngineDamage();
                        break;
                    case 5:
                        if (_mineCount < 6)
                        {
                            Debug.Log("Spacemines Collected!");
                            player.SpaceMineAdd();
                        }
                        break;
                    default:
                        break;
                }

            }
            Destroy(this.gameObject);
        }
    }

}
