using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInfoCls {
	public bool FrontSide;
	public int MyID;
	public int MyOderInCard;
	public int MyOrderInPlace;
	public int MyParentCard;
	public int MyPlace;
	public int MyPlayer;
	public bool WithCard;
	public double RandNum;
	public string TextDmg = "";
	public Vector3 ToPos = new Vector3();
	public CoroutineCls Cor = new CoroutineCls();
	public SubPlaceCls SubPlace = new SubPlaceCls();

	public GameObject CardPfb;// = new GameObject();
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void DebugCinf() {
		//Debug. Log( "Card:" + MyPlayer + "-" + MyID + " in " + MyPlace + "-" + MyOrderInPlace );
	}
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void InitCardCls( int player , int id ) {
		MyID = id;
		MyPlayer = player;
		WithCard = false;
		Cor. InitCor( MyPlayer,MyID,ToVector3( MyID * 0.1 , 0 , MyPlayer ) );
		SubPlace. InitSubPlace( MyPlayer, MyID );
		//CardPfb = Main. CardsPfb[ MyPlace , MyID ];
		// SetPos();
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void Reflesh() {
		SetCor();
		SubPlace. MyPlace = MyPlace;
		SubPlace. MyCardOder = MyOrderInPlace;
		SubPlace. Reflesh();
		// Debug. Log("Card:"+MyID+", in "+MyPlace+"-"+MyOrderInPlace+" , with : "+WithCard+"="+MyParentCard+"-"+MyOderInCard);
		SetCor();
		Cor. SetCor(ref Main.CardsPfb[MyPlayer,MyID]);
		//Debug. Log( "Reflesh Card "+MyID+"Place:"+MyPlace+"-"+MyOrderInPlace );
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void Set( int plc , int odp , int pcd , int odc , bool f) {
		string S = "plyer" + MyPlayer + " p:" + MyPlace + "-"+MyOrderInPlace;
		if(WithCard){
			S += "'s "+MyParentCard+"-"+MyOderInCard;
		}
		MyOderInCard = odc;
		MyOrderInPlace = odp;
		MyParentCard = pcd;
		MyPlace = plc;
		WithCard = f;
		FrontSide = Main. Deck[ MyPlayer ]. DefPos. CalcBorF( FrontSide , plc , WithCard );
		Main. TextControl[ MyPlayer , MyID ] = Main. Deck[ MyPlayer ]. DefPos. DmgText( MyPlace , WithCard );
		S += " => " + MyPlace + "-" + MyOrderInPlace;
		if(WithCard){
			S += "'s "+MyParentCard+"-"+MyOderInCard;
		}
		Debug. Log(S);
		SetSub();
		SetCor();
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void SetCor() {
		Cor. fFrontSide = FrontSide;
		Cor. fWithCard = WithCard;
		Cor. MyID = MyID;
		Cor. MyOderInCard = MyOderInCard;
		Cor. MyOrderInPlace = MyOrderInPlace;
		Cor. MyPlace = MyPlace;
		Cor. MyPlayer = MyPlayer;
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void SetSub() {
		SubPlace. MyPlace = MyPlace;
		SubPlace. MyCardOder = MyOrderInPlace;
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void SetToPos() {
		ToPos = Main. Deck[ MyPlayer ]. DefPos. CalcPos( MyPlace , MyOrderInPlace, MyOderInCard, WithCard );
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	Vector3 ToVector3( double x , double y , double z ) {
		Vector3 R = new Vector3();
		R. x = (float)x;
		R. y = (float)y;
		R. z = (float)z;
		return R;
	}
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
}





public class UndoControlCls {
	public bool FMoved = false;
	public int[] NCardInPlace = new int[ Main. NMaxPlace ];
	public int[] Oder = new int[ Main. NMaxCard ];
	public int[] Place = new int[ Main. NMaxCard ];
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
}
// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK





public class TextureManageClass {
	public bool Exist = false;
	public Texture TextureData;
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void Load ( string aug ) {
		Exist = true;
		TextureData = (Texture) Resources. Load( aug );
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
}
