using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAsteroid : MonoBehaviour
{

    //model zawieraj¹cy trzy kostki
    GameObject model;
    //wylosowana rotacja/s
    Vector3 rotation = Vector3.one;

    public GameObject explosionEffect;

    // Start is called before the first frame update
    //gracz
    GameObject player;
    //prędkość podążania za graczem
    public float walkSpeed = 1f;
    //odwolanie do levelManager
    GameObject levelManager;

    // Start is called before the first frame update
    void Start()
    {
        model = transform.Find("DynamicAsteroid").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        levelManager = GameObject.Find("LevelManager");
        foreach (Transform cube in model.transform)
        {
            //u¿yj wbudowanego random.rotation
            cube.rotation = Random.rotation;

            //losowa liczba
            float scale = Random.Range(0.9f, 1.1f);

            //przeskaluj
            cube.localScale = new Vector3(scale, scale, scale);

        }

        //wylosuj jednorazowo rotacje/s naszej asteroidy
        rotation.x = Random.value;
        rotation.y = Random.value;
        rotation.z = Random.value;
        rotation *= Random.Range(10, 20);

    }

    // Update is called once per frame
    void Update()
    {
        model.transform.Rotate(rotation * Time.deltaTime);
        //patrz się na gracza
        transform.LookAt(player.transform.position);
        //idz do przodu
        transform.position += transform.forward * Time.deltaTime * walkSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}