using System.Collections;
using System.Collections.Generic;
using UnityEngine;



	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
public class DeckCls{
	int MyPlayer;
	public CardInfoCls[] Cards = new CardInfoCls[Main. NMaxCard];
	public PlaceCls[] Place = new PlaceCls[Main. NMaxPlace];
	public DefPosCls DefPos = new DefPosCls();
	public UndoControlCls Undo = new UndoControlCls();
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void DebugAllCards() {
		string S = "" + (char)10;
		S += "Deck " + MyPlayer + "s All Cards List" + (char)10;
		for ( int id = 0; id < Main. NMaxCard; id++ ) {
			S += "id:" + id + " is " + Cards[ id ]. MyPlace + " of " + Cards[ id ]. MyOrderInPlace + (char)10;
		}
		Debug. Log( S );
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void DoUndo() {
		Undo. FMoved = false;
		for ( int id = 0; id < Main. NMaxCard; id++ ) {
			Place[ Undo. Place[ id ] ]. UndoSet( id , Undo. Oder[ id ] );
			Cards[ id ]. Set( Undo. Place[ id ] , Undo. Oder[ id ] , -1 , 0 , false);
			Cards[ id ]. Reflesh();
		}
		for ( int plc = 0; plc < Main. NMaxPlace; plc++ ) {
			Place[ plc ]. NCardInside = Undo. NCardInPlace[ plc ];
			for ( int od = Place[ plc ]. NCardInside; od < Main. NMaxPlace; od++ ) {
				Place[ plc ]. UndoSet( -1 , od );
				Place[ plc ]. Reflesh();
			}
		}
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void InitDeckCls(int player) {
		int i;
		//DECK
		MyPlayer = player;
		//PLACE
		for (i = 0 ; i < Main. NMaxPlace ; i++)  {
			Place[i] = new PlaceCls();
			Place[i]. InitPlaceCls(MyPlayer,i);
		}
		//CARD
		for (i = 0 ; i < Main. NMaxCard ; i++) {
			Cards[i] = new CardInfoCls();
			Cards[i]. InitCardCls(MyPlayer,i);
		}
		//DEFPOS
		DefPos. InitDefPosCls(MyPlayer);
		//CARD SET INITIAL PLACE
		InitSetPlace(Main.CDeck);
		SetUndoMem();
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void InitSetPlace( int CPlace ) {
		Place[ CPlace ]. DebugPlace();
		for ( int id = 0; id < Main. NMaxCard; id++ ) {
			//SetPlace( ref Cards[ id ] , CPlace );
			Place[ CPlace ]. AddCard( ref Cards[ id ] );
		}
		Place[ CPlace ]. Reflesh();
		Place[ CPlace ]. DebugPlace();
		DebugAllCards();
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void InitSetPlaceMarker( int CPlace ) {
		for ( int ip = 0; ip < Main. NMaxPlace; ip++ ) ;
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void MoveAllCard ( int frm , bool fInCard , int to , bool fToCard ) {
		// Debug.Log("Move All Card");
		Undo. FMoved = false;
		int n; // = Place[ frmPC ]. NCardInside;
		if ( fInCard ) {
			n = Cards[ frm ]. SubPlace. NCardInside;
			// Debug.Log("A : "+n);
			for (int i1 = 0; i1 < n; i1++ ) {
				MoveCard ( Cards[ frm ]. SubPlace. GetId( 0 ) , true , to , fToCard );
			}
		} else {
			n = Place[ frm ]. NCardInside;
			// Debug.Log("B : "+n);
			for (int i1 = 0; i1 < n; i1++ ) {
				MoveCard ( Place[ frm ]. GetId( 0 ) , false , to , fToCard);//!!!
			}
		}
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void MoveCard( int Cid , bool fInCard , int toId , bool fToCard) {
		// Debug.Log("Move Card"+Cid+fInCard+"=>"+toId+fToCard);
		if( Undo. FMoved == false ) {
			SetUndoMem();
		}
		//REMOVE
		bool fWithPre = Cards[ Cid ]. WithCard;//!!!
		if ( fWithPre ) {
			int parentId = Cards[ Cid ]. MyParentCard;
			Cards[ parentId ]. SubPlace. RemoveCardinfo( ref Cards[ Cid ] );
			// Cards[ parentId ]. SubPlace. Reflesh();
			// Cards[ parentId ]. SubPlace. DebugPlace();
		} else {
			int placeId = Cards[ Cid ]. MyPlace;
			Place[ placeId ]. RemoveCardinfo( ref Cards[ Cid ] );
			// Place[ placeId ]. Reflesh();
			// Place[ placeId ]. DebugPlace();
		}
		//SET
		if ( fToCard ) {
			Cards[ toId ]. SubPlace. AddCard( ref Cards[ Cid ] );
		} else {
			Place[ toId ]. AddCard( ref Cards[ Cid ] );
		}
		// CHILD CARDS
		if ( Cards[ Cid ]. SubPlace. NCardInside > 0 ) {
			Debug. Log("route 000");
			if( fToCard ) {
				Debug. Log("route 001");
				MoveAllCard( Cid , true , toId , fToCard );
			}else if( ! Main. FWithCard[ toId ] ) {
				Debug. Log("route 002");
				MoveAllCard( Cid , true , toId , fToCard );
			}
		}
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void Reflesh() {
		for( int plc = 0; plc < Main. NMaxPlace; plc++ ) {
			Place[ plc ]. Reflesh();
		}
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	public void SetUndoMem() {
		Debug. Log( "Set Undo" );
		Undo. FMoved = true;
		for ( int plc = 0; plc < Main. NMaxPlace; plc++ ) {
			Undo. NCardInPlace[ plc ] = Place[ plc ]. NCardInside;
		}
		for ( int id = 0; id < Main. NMaxCard; id++ ) {
			Undo. Oder[ id ] = Cards[ id ]. MyOrderInPlace;
			Undo. Place[ id ] = Cards[ id ]. MyPlace;
		}
	}
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
}
// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK






