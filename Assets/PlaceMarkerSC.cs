using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMarkerSC : MonoBehaviour
{
    int MyPlayer = Main.ActivePlayer;
    int MyPlace = Main.iGlv;

    void Start(){
    }
    void Update(){
    }
    //KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    private void OnMouseOver() {
        if( Input.GetMouseButtonDown( 1 ) ) {
            Debug.Log("Place Marker Right Clicked !!!");
            Main.ClkRight = true;
            Main.ClkKind = 2;//Marker
            Main.ClkPlayer = MyPlayer;
            Main.ClkPlace = MyPlace;
            // Main.ClkID = MyID;
            Main.ClkF = true;
        }
    }



}
