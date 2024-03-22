using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float flySpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //dodaj do wspó³rzêdnych wartoœæ x=1, y=0, z=0 pomno¿one przez czas
        //mierzony w sekundach od ostatniej klatki
        //transform.position += new Vector3(1, 0, 0) * Time.deltaTime;

        //prezentacja dzia³ania wyg³adzonego sterowania (emualcja joystika)
        //Debug.Log(Input.GetAxis("Vertical"));

        //sterowanie prêdkoœci¹
        //stworz nowy wektor przesuniêcia o wartoœci 1 do przodu
        Vector3 movement = transform.forward;
        //pomnó¿ go przez czas od ostatniej klatki
        movement *= Time.deltaTime;
        //pomnó¿ go przez "wychylenie joystika"
        movement *= Input.GetAxis("Vertical");
        //pomnó¿ przez prêdkoœæ lotu
        movement *= flySpeed;
        //dodaj ruch do obiektu
        //zmiana na fizyke
        // --- transform.position += movement;

        //komponent fizyki wewn¹trz gracza
        Rigidbody rb = GetComponent<Rigidbody>();
        //dodaj si³e - do przodu statku w trybie zmiany prêdkoœci
        rb.AddForce(movement, ForceMode.VelocityChange);


        //obrót
        //modyfikuj oœ "Y" obiektu player
        Vector3 rotation = Vector3.up;
        //przemnó¿ przez czas
        rotation *= Time.deltaTime;
        //przemnó¿ przez klawiaturê
        rotation *= Input.GetAxis("Horizontal");
        //pomnó¿ przez prêdkoœæ obrotu
        rotation *= rotationSpeed;
        //dodaj obrót do obiektu
        //nie mo¿emy u¿yæ += poniewa¿ unity u¿ywa Quaternionów do zapisu rotacji
        transform.Rotate(rotation);

    }

    private void OnCollisionEnter(Collision collision)
    {
        //uruchamia siê automatycznie jeœli zetkniemy sie z innym coliderem

        //sprawdz czy dotknêliœmy asteroidy
        if (collision.collider.transform.CompareTag("Asteroid"))
        {
            Debug.Log("Boom!");
            //pauza
            Time.timeScale = 0;
        }
    }
}