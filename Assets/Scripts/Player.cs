using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _horizontalSpeed = 8.0f;
    private readonly float _speedMultiply = 1.5f;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _minePrefab;
    [SerializeField]
    private GameObject _TripleShot;
    [SerializeField]
    private GameObject _ShieldPrefab;

    public int ammoCount = 15;
    public int mineCount;

    [SerializeField]
    private GameObject _PlayersExplosion;

    [SerializeField]
    private GameObject _damagedRightEngine;
    [SerializeField]
    private GameObject _damagedLeftEngine;

    [SerializeField]
    private bool tripleShot = false;
    [SerializeField]
    private bool speedActive = false;

    public bool shieldActive = false;
    public int shieldTime;

    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = -1f;
    private float _canFireMine = -1f;
    private readonly float _mineFireRate = 9f;

    public int _lives = 3;

    private SpawnManager _spawnManager;
    [SerializeField]
    private BackGround _backGround;

    [SerializeField]
    private int _Score;

    private UIManager _uiManager;

 
    private AudioSource audioSource1;
    private AudioSource audioSource2;
    private AudioSource audioSource3;

    public float thrusterCharge = 100f;
    private bool _canBoost;
    private bool _thrusterActive;

    void Start()
    {
        audioSource1 = GameObject.Find("SingleLaser_Sound").GetComponent<AudioSource>();
        audioSource2 = GameObject.Find("TripleShot_Sound").GetComponent<AudioSource>();
        audioSource3 = GetComponent<AudioSource>();

        _canBoost = true;

        transform.position = new Vector3(0, -4.0f, 0);

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _backGround = GameObject.Find("Background").GetComponent<BackGround>();

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("The UIManager is null!");
        }

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL!!!");
        }

        if (_backGround == null)
        {
            Debug.LogError("The Background is NULL!!!");
        }

        if (audioSource1 == null || audioSource2 == null)
        {
            Debug.LogError("AudioSource is NULL!!!");
        }

    }

    void Update()
    {
        PlayerMovement();
        LaserSpawn();
        MineSpawn();
        AmmoShow();
        SpaceMinesShow();
        Magnet();
        Thrusters();
    }


    void Thrusters()
    {
        _uiManager.ThrusterCooldown(thrusterCharge);

        if (_lives == 3) { 
            if (_thrusterActive)
            {
                _horizontalSpeed = 10f;
            }
            else
            {
                _horizontalSpeed = 8f;
            }
        }
        if (_lives == 2)
        {
            if (_thrusterActive)
            {
                _horizontalSpeed = 9f;
            }
            else
            {
                _horizontalSpeed = 7f;
            }
        }
        if (_lives == 1)
        {
            if (_thrusterActive)
            {
                _horizontalSpeed = 8f;
            }
            else
            {
                _horizontalSpeed = 6f;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && _canBoost == true)
        {
            thrusterCharge -= 25f * Time.deltaTime;
            _thrusterActive = true;
            Debug.Log(thrusterCharge);
        }
        else
        {
            Recharge();
            _thrusterActive = false;
        }
        thrusterCharge = Mathf.Clamp(thrusterCharge, 0.0f, 100.0f);
        if (thrusterCharge == 0)
            _thrusterActive = false;
    }

    void Recharge()
    {
        if(thrusterCharge < 100)
        {
            thrusterCharge += 20f * Time.deltaTime;
            _canBoost = false;
        }
        else
        {
            _canBoost = true;
        }
    }

    void Magnet()
    {
        if (Input.GetKey(KeyCode.S))
        {
            GameObject[] pickUps = GameObject.FindGameObjectsWithTag("MovablePowerup");

            for (int i = 0; i < pickUps.Length; i++)
            {
                if(pickUps[i].transform.position.y <= 1.25f)
                pickUps[i].transform.position = Vector2.MoveTowards(pickUps[i].transform.position, this.transform.position, 0.015f);
            }
        }
    }
    void PlayerMovement()
    {
        //Player Movement Controls:

        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 horizontalDirection = new Vector3(horizontalInput, 0, 0);
            transform.Translate(horizontalDirection * _horizontalSpeed * Time.deltaTime);
        

        float playersXPosition = transform.position.x;
        float playersZPosition = transform.position.z;

        //position restrictions
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8.5f, 8.5f), Mathf.Clamp(transform.position.y, -4.7f, 4f), 0);

        if(transform.position.y <= -4.5f)
        {
            transform.position = new Vector3(playersXPosition, -4.5f, playersZPosition);

        }

        //Glitch debugging
        if(transform.position.x >= 8)
        {
#pragma warning disable IDE0059 // Unnecessary assignment of a value
            playersXPosition = 8f;
#pragma warning restore IDE0059 // Unnecessary assignment of a value
        }

    }

    void MineSpawn()
    {
        if (Input.GetMouseButtonDown(1) && Time.time > _canFireMine && mineCount > 0)
        {
            mineCount--;
            _canFireMine = Time.time + _mineFireRate;
            Instantiate(_minePrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
    void LaserSpawn() 
    {
        if (Input.GetMouseButtonDown(0) && Time.time > _canFire && ammoCount > 0 || Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire && ammoCount > 0 || 
            Input.GetMouseButton(0) && Time.time > _canFire && ammoCount > 0 || Input.GetKey(KeyCode.Space) && Time.time > _canFire && ammoCount > 0)
        {
            if (tripleShot == true)
            {
                ammoCount--;
                ammoCount--;
                ammoCount--;
                _canFire = Time.time + _fireRate;
                Instantiate(_TripleShot, transform.position + new Vector3(-0.729149f, 0, 0), Quaternion.identity);
                audioSource2.Play();
            }
            else
            {
                ammoCount--;
                _canFire = Time.time + _fireRate;
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                audioSource1.Play();
            }
            
        }
    }
    public void Damage(bool hitByAsteroid = false)
    {
        if (shieldActive == true && shieldTime == 4)
        {
            Color fadeAlpha = Color.white;
            fadeAlpha.a = 0.85f;
            _ShieldPrefab.GetComponent<SpriteRenderer>().color = fadeAlpha;
            Debug.Log("ShieldTime = 2");
            shieldTime -= 1;
        }
        else if (shieldActive == true && shieldTime == 3)
        {
            Debug.Log("ShieldTime = 1");
            Color fadeAlpha = Color.white;
            fadeAlpha.a = 0.6f;
            _ShieldPrefab.GetComponent<SpriteRenderer>().color = fadeAlpha;
            shieldTime -= 1;
        }
        else if (shieldActive == true && shieldTime == 2)
        {
            Debug.Log("ShieldTime = 0");
            shieldTime -= 1;
            _ShieldPrefab.SetActive(false);
            _uiManager.UpdateShieldPowerup(false);
        }
        else if (shieldActive == true && shieldTime == 1)
        {
            shieldTime = 0;
            shieldActive = false;
        }

        if (shieldActive == false && shieldTime == 0)
        {

            _lives -= 1;

            _uiManager.UpdateLives(_lives);

            if (_lives < 1)
            {
                _uiManager.GameOver(true, false);
                Instantiate(_PlayersExplosion, transform.position, Quaternion.identity);
                _spawnManager.OnPlayerDeath();
                _backGround.enabled = false;
                audioSource3.Play();
                if (audioSource3 == null)
                {
                    Debug.LogError("audioSource 3 = null!");
                }
                Destroy(this.gameObject);
            }
            EngineDamage();

        }

            if (hitByAsteroid == true)
            {
                _lives = 0;
                _uiManager.GameOver(true, false);
                Instantiate(_PlayersExplosion, transform.position, Quaternion.identity);
                _spawnManager.OnPlayerDeath();
                _backGround.enabled = false;
                audioSource3.Play();
                if (audioSource3 == null)
                {
                    Debug.LogError("audioSource 3 = null!");
                }
                Destroy(this.gameObject);
            }
        }
    public void AmmoAdd()
    {
        ammoCount = 15;
        AmmoShow();
    }
    public void SpaceMineAdd()
    {
        mineCount += 1;
        SpaceMinesShow();
    }
    public void HealthAdd()
            {
                if (_lives < 3)
                {
                    _lives += 1;
                }
                _uiManager.UpdateLives(_lives);
            }
    public void TripleShotActive()
            {
                tripleShot = true;
                _uiManager.UpdateTripleshotPowerup();
                _uiManager.AmmoShow(true, false);

                StartCoroutine(TripleShotCoolDown());
            }
    public void SpeedPowerupActive()
            {
                speedActive = true;
                _fireRate = 0.2f;
                if (speedActive == true)
                {
                    _uiManager.UpdateSpeedPowerup();
                }
                _horizontalSpeed *= _speedMultiply;
                if (_horizontalSpeed >= 8.0f)
                {
                    Debug.Log("You're going faster!");
                }
                StartCoroutine(SpeedCooldown());
            }
    public void ShieldPowerupActive(bool isEnabled = true)
            {
                Color fadeAlpha = Color.white;
                _ShieldPrefab.GetComponent<SpriteRenderer>().color = fadeAlpha;
                fadeAlpha.a = 1f;
                shieldTime = 4;
                shieldActive = isEnabled;
                _ShieldPrefab.SetActive(isEnabled);
                _uiManager.UpdateShieldPowerup(isEnabled);
            }

    IEnumerator TripleShotCoolDown()
            {
                yield return new WaitForSeconds(5.0f);
                tripleShot = false;
                _uiManager.AmmoShow(false, true);
                _uiManager.UpdateTripleshotPowerup(false);
            }
    IEnumerator SpeedCooldown()
            {
                yield return new WaitForSeconds(10.0f);
                speedActive = false;
                _fireRate = 0.4f;
                _horizontalSpeed = 8.0f;
                _uiManager.UpdateSpeedPowerup(false);
            }

    public void EngineDamage()
            {
                if (_lives == 3)
                {
                    _damagedLeftEngine.SetActive(false);
                    _damagedRightEngine.SetActive(false);
                }
                if (_lives == 2)
                {
                    _horizontalSpeed = 7;
                    _damagedRightEngine.SetActive(true);
                    _damagedLeftEngine.SetActive(false);
                }
                if (_lives == 1)
                {
                    _horizontalSpeed = 6;
                    _damagedLeftEngine.SetActive(true);
                }
            }
    public void AddScore(int points)
            {
                _Score += points;
                _uiManager.KillScore(_Score);
            }
    public void AmmoShow()
            {
                _uiManager.AmmoCount(ammoCount);
            }
    public void SpaceMinesShow()
            {
                _uiManager.MineCount(mineCount);
            }

    }










