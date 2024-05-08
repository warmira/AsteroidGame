using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 10;
    public GameObject BulletPrefab;

    private Rigidbody rb;
    private Vector2 controlls;


    private Transform gun;
    private bool fireButtonDown = false;

    public float rotationSpeed = 100f;
    public float flySpeed = 5f;
    //odniesienie do menadzera poziomu
    GameObject levelManagerObject;
    //stan os�on w procentach (1=100%)
    float shieldCapacity = 1;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        gun = transform.Find("ProjectileSpawn");

        levelManagerObject = GameObject.Find("LevelManager");
    }

    // Update is called once per frame
    void Update()
    {
        float v, h;
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        //if(v != 0 && h != 0)
        controlls = new Vector2(h, v);

        //dodaj do wsp�rz�dnych warto�� x=1, y=0, z=0 pomno�one przez czas
        //mierzony w sekundach od ostatniej klatki
        //transform.position += new Vector3(1, 0, 0) * Time.deltaTime;

        //prezentacja dzia�ania wyg�adzonego sterowania (emualcja joystika)
        //Debug.Log(Input.GetAxis("Vertical"));

        //sterowanie pr�dko�ci�
        //stworz nowy wektor przesuni�cia o warto�ci 1 do przodu
        Vector3 movement = transform.forward;
        //pomn� go przez czas od ostatniej klatki
        movement *= Time.deltaTime;
        //pomn� go przez "wychylenie joystika"
        movement *= Input.GetAxis("Vertical");
        //pomn� przez pr�dko�� lotu
        movement *= flySpeed;
        //dodaj ruch do obiektu
        //zmiana na fizyke
        // --- transform.position += movement;

        //komponent fizyki wewn�trz gracza
        Rigidbody rb = GetComponent<Rigidbody>();
        //dodaj si�e - do przodu statku w trybie zmiany pr�dko�ci
        rb.AddForce(movement, ForceMode.VelocityChange);


        //obr�t
        //modyfikuj o� "Y" obiektu player
        Vector3 rotation = Vector3.up;
        //przemn� przez czas
        rotation *= Time.deltaTime;
        //przemn� przez klawiatur�
        rotation *= Input.GetAxis("Horizontal");
        //pomn� przez pr�dko�� obrotu
        rotation *= rotationSpeed;
        //dodaj obr�t do obiektu
        //nie mo�emy u�y� += poniewa� unity u�ywa Quaternion�w do zapisu rotacji
        transform.Rotate(rotation);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        UpdateUI();
    }

    void Fixed()
    {


    }

    void Shoot()
    {
        Debug.Log("strzal");


        GameObject bullet = Instantiate(BulletPrefab, gun.position, Quaternion.identity);

        var bulletRb = bullet.GetComponent<Rigidbody>();

        bulletRb.AddForce(gun.forward * 15, ForceMode.Impulse);

        Debug.Log("strzal 2");
        Debug.Log(bullet.gameObject.name);
    }

    private void UpdateUI()
    {
        //metoda wykonuje wszystko zwi�zane z aktualizacj� interfejsu u�ytkownika

        //wyciagnij z menadzera poziomu pozycje wyjscia
        Vector3 target = levelManagerObject.GetComponent<LevelManager>().exitPosition;
        //obroc znacznik w strone wyjscia
        transform.Find("NavUI").Find("TargetMarker").LookAt(target);
        //zmien ilosc procentwo widoczna w interfejsie
        //TODO: poprawi� wy�wietlanie stanu os�on!
        TextMeshProUGUI shieldText =
            GameObject.Find("Canvas").transform.Find("ShieldCapacityText").GetComponent<TextMeshProUGUI>();
        shieldText.text = " Shield: " + (shieldCapacity * 100).ToString() + "%";

        //sprawdzamy czy poziom si� zako�czy� i czy musimy wy�wietli� ekran ko�cowy
        if (levelManagerObject.GetComponent<LevelManager>().levelComplete)
        {
            //znajdz canvas (interfejs), znajdz w nim ekran konca poziomu i go w��cz
            GameObject.Find("Canvas").transform.Find("LevelCompleteScreen").gameObject.SetActive(true);
        }
        //sprawdzamy czy poziom si� zako�czy� i czy musimy wy�wietli� ekran ko�cowy
        if (levelManagerObject.GetComponent<LevelManager>().levelFailed)
        {
            //znajdz canvas (interfejs), znajdz w nim ekran konca poziomu i go w��cz
            GameObject.Find("Canvas").transform.Find("GameOverScreen").gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //uruchamia si� automatycznie je�li zetkniemy sie z innym coliderem

        //sprawdz czy dotkn�li�my asteroidy
        if (collision.collider.transform.CompareTag("Asteroid"))
        {

            //transform asteroidy
            Transform asteroid = collision.collider.transform;
            //policz wektor wed�ug kt�rego odepchniemy asteroide
            Vector3 shieldForce = asteroid.position - transform.position;
            //popchnij asteroide
            asteroid.GetComponent<Rigidbody>().AddForce(shieldForce * 5, ForceMode.Impulse);
            shieldCapacity -= 0.25f;
            if (shieldCapacity <= 0)
            {
                //poinformuj level manager, �e gra si� sko�czy�a bo nie mamy os�on
                levelManagerObject.GetComponent<LevelManager>().OnFailure();

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //je�eli dotkniemy znacnzika ko�ca poziomu to ustaw w levelmanager flag�,
        //�e poziom jest uko�czony
        if (other.transform.CompareTag("LevelExit"))
        {
            //wywo�aj dla LevelManager metod� zako�czenia poziomu
            levelManagerObject.GetComponent<LevelManager>().OnSuccess();
        }
    }

}