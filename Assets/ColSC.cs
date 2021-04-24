using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColSC //: MonoBehaviour
{

}

public class CoroutineCls {
	public Vector3 ToPos = new Vector3();
	public Vector3 ToRot = new Vector3();
	public Vector3 UnitIncPos = new Vector3();
	public bool fFrontSide = false;
	bool fMoving = false;
	public bool fWithCard = false;
	public int MyID;
	public int MyOderInCard = 0;
	public int MyOrderInPlace = 0;
	public int MyPlace = 0;
	public int MyPlayer;
	int NUnit;
	int iCor;
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public bool FMoving() {
		return fMoving;
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void InitCor(int myp,int myid,Vector3 topos) {
		MyPlayer = myp;
		MyID = myid;
		ToPos = topos;
		iCor = 1;
		fMoving = true;
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void SetCor(ref GameObject aug) {
		Vector3 FrmPos = new Vector3();
		iCor = 20;//iCor = 1;//
		ToPos = Main. Deck[ MyPlayer ]. DefPos. CalcPos( MyPlace , MyOrderInPlace, MyOderInCard, fWithCard );
		ToRot = Main. Deck[ MyPlayer ]. DefPos. CalcRot( fFrontSide );
		FrmPos = aug. transform. position;
		NUnit = ( iCor * ( iCor + 1 ) ) / 2;
		UnitIncPos = ( ToPos - FrmPos ) / NUnit;
		fMoving = true;
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void StepCor(ref GameObject aug) {
		if ( fMoving ) {
			Vector3 tpos = new Vector3();
			Vector3 trot = new Vector3();
			if ( iCor <= 1 ) {
				fMoving = false;
				//POS
				tpos = aug. transform. position;
				tpos. x = ToPos. x;
				tpos. y = ToPos. y;
				tpos. z = ToPos. z;
				aug. transform. position = tpos;
				//ROT
				trot = aug. transform. eulerAngles;
				trot. x = ToRot. x;
				trot. y = ToRot. y;
				trot. z = ToRot. z;
				aug. transform. eulerAngles = trot;
			} else {
				//POS
				tpos = aug. transform. position + UnitIncPos * iCor--;
				aug. transform. position = tpos;
				//ROT
				
				// aug. transform. eulerAngles = trot;
			}
			//Debug. Log( "CorMoving " + MyPlayer + "-" + MyID );
		}
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
}



// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
public class DefPosCls {
	int MyPlayer;
	
	const float CardW = 0.67f;
	const float CardH = 0.83f;
	const float CardT = 0.01f;

	// bool[] InbChild = new bool[ Main. NMaxPlace ];
	bool[] TextDmg = new bool[ Main. NMaxPlace ];
	int[] NMaxClm = new int[ Main. NMaxPlace ];
	Vector3[] Ofs = new Vector3[ Main. NMaxPlace ];
	Vector3[] OfsEdit = new Vector3[ Main. NMaxPlayer ];
	Vector3[] OfsClm = new Vector3[ Main. NMaxPlace ];
	Vector3[] Pos = new Vector3[ Main. NMaxPlace ];
	bool[] ToBackSide = new bool[ Main. NMaxPlace ];
	bool[] ToFrontSide = new bool[ Main. NMaxPlace ];

	const int CDeck = Main. CDeck;
	const int CHand = Main. CHand;
	const int CBatle = Main. CBatle;
	const int CBench = Main. CBench;
	const int CTrush = Main. CTrush;
	const int CStudium = Main. CStudium;
	const int CSide = Main. CSide;
	const int CLost = Main. CLost;
	const int CWithCard = Main. CWithCard;
	const int CTmp1 = Main. CTmp1;
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public bool CalcBorF( bool bf , int plc , bool f ) {
		if ( ToBackSide[ plc ] ) {
			return false;//BACK SIDE
		} else if ( ToFrontSide[ plc ] ) {
			return true;//FOR SIDE
		}
		return bf;
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public Vector3 CalcPos(int Plc, int OdrP, int OdrC, bool fWith) {
		int wcOfs;
		Vector3 R = new Vector3();
		int Row = (int)Mathf. Ceil( OdrP / NMaxClm[ Plc ] );
		int SubOdr = OdrP % NMaxClm[ Plc ];
		if( fWith ) {
			wcOfs = OdrC + 1;
		} else {
			wcOfs = 0;
		}
		R = Pos[ Plc ] + SubOdr * Ofs[ Plc ] + Row * OfsClm[ Plc ] + wcOfs * Ofs[ CWithCard ];
		return R;
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public Vector3 CalcRot( bool fWith ) {
		Vector3	R = new Vector3();
		R. x = 0;
		R. y = 180;
		if( MyPlayer == 1 ) { R. y = 0; }
		if ( fWith ) {
			R. z = 0;
		} else { 
			R. z = 180;
		}
		return R;
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public Vector3 CalcPosEdit( int ply , int od) {
		int nmax = 6;
		int x = od % nmax;
		int y = (int)Mathf. Ceil( od / nmax );
		Vector3 R = new Vector3();
		R = SetVecPow( x , 0 , -y ) + OfsEdit[ ply ];
		return R;
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public Vector3  CalcPosCardCls(ref CardInfoCls aug) {
		return CalcPos(aug. MyPlace, aug. MyOrderInPlace, 0, aug. WithCard );
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public string DmgText( int plc, bool fWith ) {
		if( fWith ) {
			return "";
		} else if( TextDmg[ plc ] ) {
			return "0";
		}
		return "";
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void InitDefPosCls(int player) {
		MyPlayer = player;

		for ( int ipl = 0; ipl < Main. NMaxPlace; ipl++ ) {
			Pos[ ipl ] = new Vector3();
			Ofs[ ipl ] = new Vector3();
			OfsClm[ ipl ] = new Vector3();
		}
		OfsEdit[ 0 ] = new Vector3();
		OfsEdit[ 1 ] = new Vector3();

		//DECK
		Pos[ CDeck ] = SetVecPow( 3.5 , 0 , -1 );
		Ofs[ CDeck ] = SetVecPow( 0 , 1 , 0 );
		OfsClm[ CDeck ] = SetVecPow( 0 , 0 , 0 );
		// InbChild[ CDeck ] = false;
		NMaxClm[ CDeck ] = 60;
		TextDmg[ CDeck ] = false;
		ToBackSide[ CDeck ] = true;
		ToFrontSide[ CDeck ] = false;
		//
		Pos[ CHand ] = SetVecPow( -3 , 0 , -4 );
		Ofs[ CHand ] = SetVecPow( 0.8 , -1 , 0 );
		OfsClm[ CHand ] = SetVecPow( 0 , -1 , 0.5 );
		// InbChild[ CHand ] = false;
		NMaxClm[ CHand ] = 10;
		TextDmg[ CHand ] = false;
		ToBackSide[ CHand ] = false;
		ToFrontSide[ CHand ] = true;
		//
		Pos[ CBatle ] = SetVecPow( 0 , 0 , -1 );
		Ofs[ CBatle ] = SetVecPow( 1 , 0 , 0 );
		OfsClm[ CBatle ] = SetVecPow( 0 , 0 , 0 );
		// InbChild[ CBatle ] = false;
		NMaxClm[ CBatle ] = 60;
		TextDmg[ CBatle ] = true;
		ToBackSide[ CBatle ] = false;
		ToFrontSide[ CBatle ] = true;
		//
		Pos[ CBench ] = SetVecPow( -2 , 0 , -2.5 );
		Ofs[ CBench ] = SetVecPow( 1 , 0 , 0 );
		OfsClm[ CBench ] = SetVecPow( 0 , 0 , 0 );
		// InbChild[ CBench ] = false;
		NMaxClm[ CBench ] = 60;
		TextDmg[ CBench ] = true;
		ToBackSide[ CBench ] = false;
		ToFrontSide[ CBench ] = true;
		//
		Pos[ CTrush ] = SetVecPow( 3.5 , 0 , -2 );
		Ofs[ CTrush ] = SetVecPow( 0 , 1 , 0 );
		OfsClm[ CTrush ] = SetVecPow( 0 , 0 , 0 );
		// InbChild[ CTrush ] = false;
		NMaxClm[ CTrush ] = 60;
		TextDmg[ CTrush ] = false;
		ToBackSide[ CTrush ] = false;
		ToFrontSide[ CTrush ] = true;
		//
		Pos[ CStudium ] = SetVecPow( -1.5 , 0 , 0 );
		Ofs[ CStudium ] = SetVecPow( 0.2 , 0 , 0 );
		OfsClm[ CStudium ] = SetVecPow( 0 , 0 , 0 );
		// InbChild[ CStudium ] = false;
		NMaxClm[ CStudium ] = 60;
		TextDmg[ CStudium ] = false;
		ToBackSide[ CStudium ] = false;
		ToFrontSide[ CStudium ] = true;
		//
		Pos[ CSide ] = SetVecPow( -3.5 , 0 , -1 );
		Ofs[ CSide ] = SetVecPow( 0.2 , 0 , -0.2 );
		OfsClm[ CSide ] = SetVecPow( 0 , 0 , -0.8 );
		// InbChild[ CSide ] = false;
		NMaxClm[ CSide ] = 2;
		TextDmg[ CSide ] = false;
		ToBackSide[ CSide ] = true;
		ToFrontSide[ CSide ] = false;
		//
		Pos[ CLost ] = SetVecPow( 1.5 , 0 , -1 );
		Ofs[ CLost ] = SetVecPow( 0 , 1 , 0 );
		OfsClm[ CLost ] = SetVecPow( 0 , 0 , 0 );
		// InbChild[ CLost ] = false;
		NMaxClm[ CLost ] = 60;
		TextDmg[ CLost ] = false;
		ToBackSide[ CLost ] = false;
		ToFrontSide[ CLost ] = true;
		//
		Pos[ CWithCard ] = SetVecPow( 0 , 0 , 0 );
		Ofs[ CWithCard ] = SetVecPow( 0 , -1 , 0.2 );
		OfsClm[ CWithCard ] = SetVecPow( 0 , 0 , 0 );
		// InbChild[ CWithCard ] = false;
		NMaxClm[ CWithCard ] = 60;
		TextDmg[ CWithCard ] = false;
		ToBackSide[ CWithCard ] = false;
		ToFrontSide[ CWithCard ] = true;
		//
		Pos[ CTmp1 ] = SetVecPow( 5 , 0 , -1 );
		Ofs[ CTmp1 ] = SetVecPow( 0.5 , 1 , 0 );
		OfsClm[ CTmp1 ] = SetVecPow( 0 , 0 , 0 );
		// InbChild[ CTmp1 ] = false;
		NMaxClm[ CTmp1 ] = 60;
		TextDmg[ CTmp1 ] = true;
		ToBackSide[ CTmp1 ] = false;
		ToFrontSide[ CTmp1 ] = false;

		//EDIT
		OfsEdit[ 0 ] = SetVecPow( -7 , 0 , 4.5 );
		OfsEdit[ 1 ] = SetVecPow( 1 , 0 , 4.5 );
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public Vector3 SetVecPow(double x,double y,double z) {
		Vector3 R = new Vector3();
		R. x = (float)x * CardW;
		R. y = (float)y * CardT;
		R. z = (float)z * CardH;
		if ( MyPlayer == 1 ) {
			R. x = -R. x;
			R. z = -R. z;
		}
		return R;
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK

}
// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
