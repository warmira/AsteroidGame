using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //współrzędne gracza
    Transform player;
    //wysokość kamery
    public float cameraHeight = 10.0f;
    //prędkość kamery - do użytku dla smoothdamp
    Vector3 cameraSpeed;
    //szybkość wygładzania ruchu kamery - dla smoothdamp
    public float dampSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        //podłącz pozycję gracza do lokalnej zmiennej korzystając z jego taga
        //to nie jest zapisanie wartości jeden raz tylko referencja do obiektu
        //to znaczy, że player zawszę będzie zawierał aktualną pozycję gracza
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //oblicz docelową pozycję kamery
        Vector3 targetPosition = player.position + Vector3.up * cameraHeight;

        //płynnie przesuń kamerę w kierunku gracza
        //funkcja Vector3.Lerp
        //płynnie przechodzi z pozycji pierwszego argumentu do pozycji drugiego w czasie trzeciego
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);

        //smoothdamp działa jak sprężyna starająca się dociągnąć kamerę do was
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref cameraSpeed, dampSpeed);
    }
}