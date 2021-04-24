using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckDataSC : MonoBehaviour
{
}

public class DeckDataCls {
    public string Name = "";
    public int[] TexID = new int[Main. NMaxCard];
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    public void AddCard( int id ){
        int nmax = Main. NMaxCard;
        int i1, i2;
        for( i1 = 0; i1 < nmax; i1++) {
            if( TexID[ i1 ] >= id ) {
                break;
            }
        }
        i1 = Mathf. Min( i1 , nmax - 1 );
        for( i2 = nmax - 1; i2 > i1; i2-- ) {
            TexID[ i2 ] = TexID[ i2 -1 ];
        }
        TexID[ i1 ] = id;
        RefleshTexture( 1 );
    }
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    public void RemoveCard ( int id ) {
        int nmax = Main. NMaxCard;
        int i1, i2;
        for( i1 = 0; i1 < nmax; i1++ ) {
            if( TexID[ i1 ] == id ) {
                break;
            }
        }
        for( i2 = i1; i2 < nmax - 1; i2++ ) {
            TexID[ i2 ] = TexID[ i2 + 1 ];
        }
        TexID[ nmax -1 ] = 1023;
        RefleshTexture( 1 );
    }
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    public void RefleshTexture( int p ) {
        for( int i1 = 0 ; i1 < Main. NMaxCard; i1++ ) {
            Main. TextureControl[ p , i1 ] = TexID[ i1 ];
        }
    }
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    public void ResetTexture() {
        for( int i1 = 0; i1 < Main. NMaxCard; i1++ ) {
            if ( TexID[ i1 ] == 0 ) {
                TexID[ i1 ] = 1023;
            }
        }
    }
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
}



public class DeckCaseCls {
    public DeckDataCls[] DeckData = new DeckDataCls[10];
    public int NData = 10;
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    public void InitDeckCase() {
        for( int i1 = 0; i1 < NData; i1++ ) {
            DeckData[ i1 ] = new DeckDataCls();
        }
    }
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
}
