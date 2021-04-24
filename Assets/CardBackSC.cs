using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBackSC : MonoBehaviour
{
    int MyPlayer = Main. ActivePlayer;
    int MyID = Main. iGlv;
	SubPlaceCls ChildsPlace = new SubPlaceCls();
	int MyTexId = -1;

    void Start(){
	}
    void Update(){
		// GetComponent<Renderer>(). material. mainTexture = Main. CardTexture[0];
		// TextureCheck();
    }
    //KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    public void InitCardSC() {
		// GetComponent<Renderer>(). material. mainTexture = Main. CardTexture[0];
    }
    //KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void TextureCheck() {
		if ( MyTexId != Main. TextureControl [ MyPlayer , MyID ] ) {
			MyTexId = Main. TextureControl [ MyPlayer , MyID ];
			GetComponent<Renderer>(). material. mainTexture = Main. AdTex[ MyTexId ]. TextureData;
		}
	}
    //KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	private void OnMouseUp() {
		Main. ClkDrag = false;
		Main. ClkRight = false;
		Main. ClkKind = 1;
		Main. ClkPlayer = MyPlayer;
		Main. ClkID = MyID;
		// Debug. Log( "!!! ClickUp !!!" );//OK
		Main. ClkF = true;
	}
	//KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	private void OnMouseOver() {
		if ( Input. GetMouseButtonDown( 1 ) ) {
			//Debug. Log( "!!! Right Click !!!" );//OK
			Main. ClkRight = true;
			Main. ClkKind = 1;
			Main. ClkPlayer = MyPlayer;
			Main. ClkID = MyID;
			Main. ClkF = true;
		}
	}
	//KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	private void OnMouseDrag() {
		Main. ClkDrag = true;
		// Debug. Log( "Drug" );
		// Debug. Log( "DraDra" );
	}
	//KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
}
