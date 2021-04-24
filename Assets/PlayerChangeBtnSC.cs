using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeBtnSC : MonoBehaviour
{
    void Start() {

    }
    void Update() {

    }

    public void ChangePlayer() {
        if( Main. ActivePlayer == 0 ) {
            Main. ActivePlayer = 1;
        } else {
            Main. ActivePlayer = 0;
        }
        Main. FCameraMove = true;
    }
}
