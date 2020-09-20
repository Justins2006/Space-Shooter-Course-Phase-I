using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _enemyCount;
    [SerializeField]
    private TextMeshProUGUI _ammoCount;
    [SerializeField]
    private TextMeshProUGUI _mineCount;
    [SerializeField]
    private GameObject _gameOver;

    [SerializeField]
    private GameObject _UIInfo;


    [SerializeField]
    private Image _thirdLife;
    [SerializeField]
    private Image _secondLife;
    [SerializeField]
    private Image _firstLife;

    public Image _cooldownBar;

    [SerializeField]
    private GameObject _TripleShot1;
    [SerializeField]
    private GameObject _TripleShot2;
    [SerializeField]
    private GameObject _TripleShot3;
    [SerializeField]
    private GameObject _SingleShot;

    [SerializeField]
    private Image _ShieldPowerup;
    [SerializeField]
    private Image _SpeedPowerup;
    [SerializeField]
    private Image _TripleShotPowerup;

    private GameManager _gameManager;   

    public int enemyCount = 0000;

    public void Start()
    {

        _TripleShot1.SetActive(false);
        _TripleShot2.SetActive(false);
        _TripleShot3.SetActive(false);
        _SingleShot.SetActive(true);

        _scoreText.text = "000000";
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL!!!");
        }
    }

    public void ThrusterCooldown(float thrusterCharge)
    {
        _cooldownBar.fillAmount = thrusterCharge / 100;     
    }
    public void KillScore(int playerScore)
    {
        _scoreText.text = playerScore.ToString("D6");
    }
    public void AmmoShow(bool isEnabled = true, bool isNotEnabled = false)
    {
        _TripleShot1.SetActive(isEnabled);
        _TripleShot2.SetActive(isEnabled);
        _TripleShot3.SetActive(isEnabled);
        _SingleShot.SetActive(isNotEnabled);
    }
    public void MineCount(int mines)
    {
        _mineCount.text = mines.ToString("D1");
    }
    public void AmmoCount(int ammo)
    {
        _ammoCount.text = ammo.ToString("D2");
    }
    public void AddEnemyCount()
    {
        enemyCount += 0001;
        _enemyCount.text = enemyCount.ToString("D4");
    }
    public void UpdateShieldPowerup(bool isEnabled = true)
    {
        _ShieldPowerup.enabled = isEnabled;   
    }
    public void UpdateSpeedPowerup(bool isEnabled = true)
    {
        _SpeedPowerup.enabled = isEnabled;
        
    }
    public void UpdateTripleshotPowerup(bool isEnabled = true)
    {
        _TripleShotPowerup.enabled = isEnabled;
    }
    public void UpdateLives(int currentLives)
    {

        int _player = GameObject.Find("Player").GetComponentInChildren<Player>()._lives;
        if(_player == 3)
        {
            _firstLife.enabled = true;
            _secondLife.enabled = true;
            _thirdLife.enabled = true;
        }
        if (_player == 2)
        {
            _firstLife.enabled = true;
            _secondLife.enabled = true;
            _thirdLife.enabled = false;
        }
        if(_player == 1)
        {
            _firstLife.enabled = true;
            _secondLife.enabled = false;
            _thirdLife.enabled = false;
        }
        if(_player < 1)
        {
            _firstLife.enabled = false;
            _secondLife.enabled = false;
            _thirdLife.enabled = false;
        }
    }
    public void GameOver(bool isEnabled = true, bool isFalse = true)
    {
        _gameOver.SetActive(isEnabled);
        _UIInfo.SetActive(isFalse);
    }
}
