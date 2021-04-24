using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine. UI;
using TMPro;

public class DmgSC : MonoBehaviour
{
    // int MyPlayer = Main. ActivePlayer;
    // int MyID = Main. iGlv;
    public Text DmgText;
    bool FDmgCheck = false;
    // Start is called before the first frame update
    void Start(){ 
    }
    // Update is called once per frame
    void Update(){
        if ( FDmgCheck ) {
            if ( ! Main. FDmgInput ) {
                FDmgCheck = false;
                DmgText. text = Main. DmgInput;
            }
        }
     }
    //
    public void ButtonPush() {
        Debug. Log("Text Click");
        Main. FDmgInput = true;
     FDmgCheck = true;
    }
}
