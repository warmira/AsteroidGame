using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        //dodaj do wsp�rz�dnych warto�� x=1, y=0, z=0, po�o�oneprzez czas 
        //mierzymy w sekundach od ostatniej klatki
        transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
    }
}