using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    Transform player;
    //odleg�o�c od ko�ca poziomu
    public float levelExitDistance = 100;
    //punkt ko�ca poziomu
    public Vector3 exitPosition;
    public GameObject exitPrefab;
    //zmienna - flaga - oznaczaj�ca uko�czenie poziomu
    public bool levelComplete = false;
    //taka sama zmienna tylko je�li przegramy
    public bool levelFailed = false;
    // Start is called before the first frame update
    void Start()
    {
        //znajdz gracza
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //wylosuj pozycj� na kole o �rednicy 100 jednostek
        Vector2 spawnCircle = Random.insideUnitCircle; //losowa pozycja x,y wewn�trz ko�a o r=1
        //chcemy tylko pozycj� na okr�gu, a nie wewn�trz ko�a
        spawnCircle = spawnCircle.normalized; //pozycje x,y w odleg�o�ci 1 od �rodka
        spawnCircle *= levelExitDistance; //pozycja x,y w odleg�o�ci 100 od �rodka
        //konwertujemy do Vector3
        //podstawiamy: x=x, y=0, z=y
        exitPosition = new Vector3(spawnCircle.x, 0, spawnCircle.y);
        Instantiate(exitPrefab, exitPosition, Quaternion.identity);

        //wystartuj czas
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {

    }
    //funkcja jest utuchamiana kiedy dany poziom (level) jest zako�czony sukcesem
    public void OnSuccess()
    {
        //zatrzymaj fizyk� gry
        Time.timeScale = 0f;
        //ustaw flag� - poziom zako�czony
        levelComplete = true;
        //odegraj d�wi�k ko�ca poziomu
        Camera.main.transform.Find("LevelCompleteSound").GetComponent<AudioSource>().Play();
    }
    public void OnFailure()
    {
        //zatrzymaj fizyk�
        Time.timeScale = 0f;
        //ustaw flag�, �e nie uda�o si� uko�czy� poziomu
        levelFailed = true;
        //odgrywmay dzwiek przegranej
        Camera.main.transform.Find("GameOverSound").GetComponent<AudioSource>().Play();
    }
}