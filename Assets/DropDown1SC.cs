using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine. UI;

public class DropDown1SC : MonoBehaviour
{
    [SerializeField] private Dropdown DD;
    // public fLoad = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DD. ClearOptions();
        DD. AddOptions( Main. DeckName );
        // DD. options.Add(new Dropdown.OptionData { text = "Item #1" });
        DD. RefreshShownValue();
        
    }
}
