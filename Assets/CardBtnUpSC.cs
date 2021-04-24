using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBtnUpSC : MonoBehaviour
{
    private void OnMouseUp() {
        Main. ClkCardButtonUp = true;
        Debug. Log("Up Button !!!");
    }
}
