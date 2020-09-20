using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class Spacemine : MonoBehaviour
{

    private float _mineSpeed = 1.5f;


    //DO NOT TOUCH THE COLLIDERS OR YOU BREAK DA CODE
    [SerializeField]
    private Collider2D _actualCollider;
    [SerializeField]
    private Collider2D _endCollider;


    public GameObject Light1;
    public GameObject Light2;

    [SerializeField]
    private Animator _Activation;


    void Start()
    {
        _Activation = GetComponent<Animator>();
    }


    void Update()
    {
        Move();
    }


    public void IsActivated()
    {
        DisableLights();
        _mineSpeed = -2;
        transform.localScale = new Vector2(1.5f, 1.5f);
        _actualCollider.enabled = false;
        _endCollider.enabled = true;
        StartCoroutine(EndOfEndCollider());
        _Activation.SetTrigger("Activation");
        Destroy(this.gameObject, 3.2f);
    }


    //Turns off the Lights when it detonates
    public void DisableLights()
    {
        Destroy(Light1);
        Destroy(Light2);
    }
    

    public void Move()
    {
        transform.Translate(Vector3.up * _mineSpeed * Time.deltaTime);

        if (transform.position.y >= 6)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator EndOfEndCollider()
    {
        yield return new WaitForSeconds(1.5f);
        _endCollider.enabled = false;
    }



}

