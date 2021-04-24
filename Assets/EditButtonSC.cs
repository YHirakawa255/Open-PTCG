using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditButtonSC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //
    public void EditToggle () {
        Debug. Log("EditButton");
        Main. FFirstFrame = true;
        if( Main. FMode ) {
            Main. FMode = false;
        } else {
            Main. FMode = true;
        }
    }
}
