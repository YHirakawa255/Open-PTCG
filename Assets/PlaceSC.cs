using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCls {
	int MyPlace;
	int MyPlayer;
	public int NCardInside;
	int[] IDsufOrder = new int[ Main. NMaxCard ];
	float[] Rnd = new float[ Main. NMaxCard ];
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void AddCard( ref CardInfoCls Cinf) {
		Cinf. Set( MyPlace , NCardInside , -1 , 0 , false );
		IDsufOrder[ NCardInside++ ] = Cinf. MyID;
		Reflesh();
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void DebugPlace() {
		string S = "" + (char)10;
		S += "Debug Place " + MyPlayer + "-" + MyPlace +" has "+NCardInside+""+ (char)10;
		for ( int od = 0; od < Main. NMaxCard; od++ ) {
			S += "od:" + od + " = " + IDsufOrder[ od ] + (char)10;
		}
		Debug. Log( S );
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public int GetId( int od ) {
		return IDsufOrder[ od ];
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public int GetIdEnd() {
		int tmp = IDsufOrder[ NCardInside - 1 ];
		// Debug. Log( "GetIdEnd:" + tmp );
		return tmp;
	}
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void InitPlaceCls(int player, int place) {
		MyPlace = place;
		MyPlayer = player;
		for ( int od = 0; od < Main. NMaxCard; od++ ) {
			IDsufOrder[ od ] = -1;
		}
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void Reflesh() {
		for ( int od = 0; od < NCardInside; od++ ) {
			// Debug. Log("Reflesh Sub Place OD:" + od);
			Main. Deck[ MyPlayer ]. Cards[ IDsufOrder[ od ] ]. Reflesh();
		}
		// DebugPlace();
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public bool RemoveCardinfo(ref CardInfoCls aug) {
		// Debug.Log("RemoveCardInfo Place N:"+NCardInside);
		for ( int od = 0; od < NCardInside; od++ ) {
			if ( IDsufOrder[ od ] == aug. MyID ) {
				NCardInside--;
				for ( int od2 = od; od2 < NCardInside; od2++ ) {
					IDsufOrder[ od2 ] = IDsufOrder[ od2 + 1 ];
					Main. Deck[ MyPlayer ]. Cards[ IDsufOrder[ od2 ] ]. MyOrderInPlace = od2;
					//Main. Deck[ MyPlayer ]. Cards[ IDsufOrder[ od2 ] ]. Reflesh();
				}
				IDsufOrder[ NCardInside ] = -1;
				Reflesh();
				// Debug.Log(NCardInside);
				return true;
			}
			// Debug.Log(NCardInside);
		}
		return false;
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void ResetAll() {
		for ( int od = 0; od < NCardInside; od++ ) {
			IDsufOrder[ od ] = -1;
		}
		NCardInside = 0;
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void Shuffle() {
		DebugPlace();
		Rnd = new float[ Main. NMaxCard ];
		for ( int i1 = 0; i1 < NCardInside; i1++ ) {
			Rnd[ i1 ] = UnityEngine. Random. Range( -1.0f , -0.1f );
		}
		Array. Sort( Rnd , IDsufOrder );
		Reflesh();
		DebugPlace();
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void UndoSet( int id , int od ) {
		IDsufOrder[ od ] = id;
	}
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK

}





public class SubPlaceCls{
	public int MyPlayer;
	public int MyPlace;
	public int MyCardOder;
	public int MyCardId;
	public int NCardInside;
	int[] IDsufOrder = new int[ Main. NMaxCard ];
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void AddCard( ref CardInfoCls Cinf ) {
		Cinf. Set( MyPlace , MyCardOder , MyCardId , NCardInside , true );
		IDsufOrder[ NCardInside++ ] = Cinf. MyID;
		Reflesh();
		DebugPlace();
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void DebugPlace() {
		string S = "" + (char)10;
		S += "Debug Place " + MyPlayer + "-" + MyPlace + "'s " + MyCardId +" has "+NCardInside+""+ (char)10;
		for ( int od = 0; od < NCardInside; od++ ) {
			S += "od:" + od + " = " + IDsufOrder[ od ] + (char)10;
		}
		Debug. Log( S );
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public int GetId( int od ) {
		return IDsufOrder[ od ];
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void InitSubPlace(int ply, int cdid){
		MyPlayer = ply;
		MyCardId = cdid;
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void Reflesh(){
		// Debug. Log("Sub Place "+NCardInside+" inside");
		for( int od = 0; od < NCardInside; od++) {
			Main. Deck[ MyPlayer ]. Cards[ IDsufOrder[ od ] ]. MyOrderInPlace = MyCardOder;
			Main. Deck[ MyPlayer ]. Cards[ IDsufOrder[ od ] ]. MyPlace = MyPlace;
			Main. Deck[ MyPlayer ]. Cards[ IDsufOrder[ od ] ]. Reflesh(); // !!!
		}
		// Debug. Log("Sub Place "+NCardInside+" inside");
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public bool RemoveCardinfo(ref CardInfoCls aug){
		for( int od = 0; od < NCardInside; od++ ){
			if( IDsufOrder[ od ] == aug. MyID ) {
				NCardInside--;
				for( int od2 = od; od2 < NCardInside; od2++ ) {
					IDsufOrder[ od2 ] = IDsufOrder[ od2 + 1 ];
					Main. Deck[ MyPlayer ]. Cards[ IDsufOrder[ od2 ] ]. MyOderInCard = od2;
				}
				IDsufOrder[ NCardInside ] = -1;
				Reflesh();
				DebugPlace();
				return true;
			}
		}
		return false;
	}

}