using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _enemyPowerup;
    [SerializeField] private GameObject _asteroidContainer;
    [SerializeField] private GameObject _ammoCollectible;
    [SerializeField] private GameObject _ammoContainer;
    [SerializeField] private GameObject _healthCollectible;
    [SerializeField] private GameObject _healthContainer;
    [SerializeField] private GameObject _SpaceMineCollectible;
    [SerializeField] private GameObject _SpaceMineContainer;
    [SerializeField]
    private GameObject _Asteroid;
    private bool _stopSpawning = false;
    private int enemynum = 0;

    void Start()
    {
        StartCoroutine(AmmoSpawn());
        StartCoroutine(HealthSpawn());
        StartCoroutine("EnemySpawn");
        StartCoroutine("EnemyPowerupSpawn");
        StartCoroutine("AsteroidSpawn");
        StartCoroutine("SpaceMineSpawn");
    }

    IEnumerator AsteroidSpawn()
    {
        yield return new WaitForSeconds(15.0f);
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(15.0f, 25.0f));
            int spawnAmount = Random.Range(7, 12);
            for (int i = 0; i < spawnAmount; i++)
            {
                Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(7, 30), 0);
                GameObject Asteroid = Instantiate(_Asteroid, posToSpawn, Quaternion.identity);
                Asteroid.transform.parent = _asteroidContainer.transform;
            }
        }
    }

    IEnumerator EnemySpawn()
    {
        yield return new WaitForSeconds(5.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(7, 12), 0);
            GameObject newEnemy = Instantiate(_enemy, posToSpawn, Quaternion.identity);
            newEnemy.name = "Enemy #" + enemynum;
            enemynum++;
            if(newEnemy != null && _enemyContainer != null)
                    newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(2.0f, 4.0f));
        }
    }

    IEnumerator EnemyPowerupSpawn()
    {
        if (_enemyPowerup != null)
        {
            yield return new WaitForSeconds(20.0f);
            while (_stopSpawning == false)
            {
                yield return new WaitForSeconds(Random.Range(10.0f, 25.0f));
                Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(7, 12), 0);
                GameObject enemyPowerup = Instantiate(_enemyPowerup, posToSpawn, Quaternion.identity);
                if (enemyPowerup != null && _enemyContainer != null)
                    enemyPowerup.transform.parent = _enemyContainer.transform;
            }
        }
    }

    public void OnPlayerDeath()
    {
        Debug.Log("Player is dead says the spawnmanager");
        _stopSpawning = true;
    }
    IEnumerator AmmoSpawn()
    {
        yield return new WaitForSeconds(7);
        while(_stopSpawning == false)
        {
            yield return new WaitForSeconds(5);
            Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(7, 12), 0);
            GameObject ammo = Instantiate(_ammoCollectible, posToSpawn, Quaternion.identity);
            ammo.transform.parent = _ammoContainer.transform;
            yield return new WaitForSeconds(Random.Range(6, 9));
        }
    }

    //Spawns the Health powerup, this script doesn't say what the health powerup is supposed to do! Go to Powerups.cs which is on the powerup itself aswell!
    IEnumerator HealthSpawn()
    {
        yield return new WaitForSeconds(15);
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(0,3));
            Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(7, 12), 0);
            GameObject health = Instantiate(_healthCollectible, posToSpawn, Quaternion.identity);
            health.transform.parent = _healthContainer.transform;
            yield return new WaitForSeconds(Random.Range(7, 14));
        }
    }

    IEnumerator SpaceMineSpawn()
    {
        yield return new WaitForSeconds(17);
        while(_stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(1, 4));
            Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(7, 12), 0);
            GameObject spacemine = Instantiate(_SpaceMineCollectible, posToSpawn, Quaternion.identity);
            spacemine.transform.parent = _SpaceMineContainer.transform;
            yield return new WaitForSeconds(Random.Range(10, 12));
        }
    } 
}