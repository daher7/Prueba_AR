using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TresEnRayaScript : MonoBehaviour {

    public Camera camara;
    public GameObject prefabFichaPlayer;
    public GameObject prefabFichaRival;
    bool turnoPlayer = true;
    // Inicialmente todas las celdas estan vacias
    int[] celdas = { -1, -1, -1, -1, -1, -1, -1, -1, -1 };
    int numTurnos = 1;
    public Text textoGanador;

    void Start() {
        Time.timeScale = 1;
    }
    /*
    void Update() {
        if (numTurnos > 9) {
            return;
        }

        if (turnoPlayer && Input.GetMouseButtonDown(0)) {

            Ray ray = camara.ScreenPointToRay(Input.mousePosition);
            RaycastHit rch;
            if (Physics.Raycast(ray, out rch)) {
                int indice = int.Parse(rch.transform.gameObject.name.Substring(5, 1));
                if (celdas[indice] != -1) {
                    return;
                }
                GeneracionFichaYCambioDeTurno(rch, indice);
            }
        }

    }
    */

    private void Update() {
        if (numTurnos > 9) {
            return;
        }
        if (turnoPlayer && (Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began)){
            Ray ray = camara.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit rch;
            if (Physics.Raycast(ray, out rch)) {
                int indice = int.Parse(rch.transform.gameObject.name.Substring(5, 1));
                if (celdas[indice] != -1) {
                    return;
                }
                GeneracionFichaYCambioDeTurno(rch, indice);
            }
        }
    }

    private void GeneracionFichaYCambioDeTurno(RaycastHit rch, int indice) {
        // Generamos la ficha
        GameObject ficha = Instantiate(prefabFichaPlayer, rch.transform);
        ficha.transform.Translate(Vector3.up * 0.1f);
        // Para decir que esta el player
        celdas[indice] = 0;
        turnoPlayer = false;
        numTurnos++;
        ComprobarTresEnRaya();
        if (numTurnos < 9) {
            Invoke("TurnoRival", 1);
        }
    }

    void TurnoRival() {
        int pos;
        do {
            pos = Random.Range(0, 8);
        } while (celdas[pos] != -1);
        GameObject casilla = GameObject.Find("Celda" + pos);
        GameObject ficha = Instantiate(prefabFichaRival, casilla.transform);
        ficha.transform.Translate(Vector3.up * 0.1f);
        // Para decir que esta el player
        celdas[pos] = 1;
        turnoPlayer = true;
        numTurnos++;
        ComprobarTresEnRaya();
    }

    void ComprobarTresEnRaya() {
        bool hayTresEnRaya = false;
        int ganador = -1;
        if (celdas[0] != -1 && celdas[0] == celdas[1] && celdas[0] == celdas[2]) {
            hayTresEnRaya = true;
            ganador = celdas[0];
        } else if (celdas[3] != -1 && celdas[3] == celdas[4] && celdas[3] == celdas[5]) {
            hayTresEnRaya = true;
            ganador = celdas[3];
        } else if (celdas[6] != -1 && celdas[6] == celdas[7] && celdas[6] == celdas[8]) {
            hayTresEnRaya = true;
            ganador = celdas[6];
        } else if (celdas[0] != -1 && celdas[0] == celdas[3] && celdas[0] == celdas[6]) {
            hayTresEnRaya = true;
            ganador = celdas[0];
        } else if (celdas[1] != -1 && celdas[1] == celdas[4] && celdas[1] == celdas[7]) {
            hayTresEnRaya = true;
            ganador = celdas[1];
        } else if (celdas[2] != -1 && celdas[2] == celdas[5] && celdas[2] == celdas[8]) {
            hayTresEnRaya = true;
            ganador = celdas[2];
        } else if (celdas[0] != -1 && celdas[0] == celdas[4] && celdas[0] == celdas[8]) {
            hayTresEnRaya = true;
            ganador = celdas[0];
        } else if (celdas[2] != -1 && celdas[2] == celdas[4] && celdas[2] == celdas[6]) {
            hayTresEnRaya = true;
            ganador = celdas[2];
        }
        if (hayTresEnRaya) {
            if (ganador == 0) {
                textoGanador.text = "HAS GANADO";
                print("HAS GANADO");
            } else {
                print("HAS PERDIDO");
                textoGanador.text = "ERES MU MALO";
            }
            // Para que se pare el juego
            Time.timeScale = 0;
        } else if (numTurnos > 9) {
            print("EMPATE");
            textoGanador.text = "HAS HECHO LA LOGSE, ¿NO?";
        }
    }

}
