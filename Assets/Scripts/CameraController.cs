using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //wsp�rz�dne gracza
    Transform player;
    //wysoko�� kamery
    public float cameraHeight = 10.0f;
    //pr�dko�� kamery - do u�ytku dla smoothdamp
    Vector3 cameraSpeed;
    //szybko�� wyg�adzania ruchu kamery - dla smoothdamp
    public float dampSpeed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        //pod��cz pozycj� gracza do lokalnej zmiennej korzystaj�c z jego taga
        //to nie jest zapisanie warto�ci jeden raz tylko referencja do obiektu
        //to znaczy, �e player zawsz� b�dzie zawiera� aktualn� pozycj� gracza
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //oblicz docelow� pozycj� kamery
        Vector3 targetPosition = player.position + Vector3.up * cameraHeight;

        //p�ynnie przesu� kamer� w kierunku gracza
        //funkcja Vector3.Lerp
        //p�ynnie przechodzi z pozycji pierwszego argumentu do pozycji drugiego w czasie trzeciego
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);

        //smoothdamp dzia�a jak spr�yna staraj�ca si� doci�gn�� kamer� do was
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref cameraSpeed, dampSpeed);
    }
}