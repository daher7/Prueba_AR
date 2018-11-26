using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonScript : MonoBehaviour {

    private const string PARAM_ANIMADO = "animado";
    [SerializeField] Animator hamburguerAnimator;

    public void Animar() {
        hamburguerAnimator.SetBool(PARAM_ANIMADO, true);
    }
}
