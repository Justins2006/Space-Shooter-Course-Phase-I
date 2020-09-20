using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForSpawn : MonoBehaviour
{


    void Start()
    {

        GameObject.Find("SpawnManager").GetComponent<SpawnManager>().enabled = false;
        GameObject.Find("Enemy").GetComponent<Enemy>().enabled = false;
       // GameObject.Find("Player").GetComponent<Player>().enabled = false;
        StartCoroutine(WaitForEnemiesToSpawn());
    }

    IEnumerator WaitForEnemiesToSpawn()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Waitforseconds is over!");
        GameObject.Find("SpawnManager").GetComponent<SpawnManager>().enabled = true;
        //GameObject.Find("Player").GetComponent<Player>().enabled = true;
        GameObject.Find("Enemy").GetComponent<Enemy>().enabled = true;
    }
}
