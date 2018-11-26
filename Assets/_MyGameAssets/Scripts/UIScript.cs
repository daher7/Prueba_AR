using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour {

    [SerializeField] GameObject sphere;
    [SerializeField] float forceSphere = 100;

    public void ApretarIzquierda() {
        print("Me has apretado");
        sphere.GetComponent<Rigidbody>().AddRelativeForce(Vector3.left * forceSphere);
    }

    public void ApretarDerecha() {
        print("Me has apretado");
        sphere.GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * forceSphere);
    }
}
