using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine. UI;
using System;
using System.Linq;
using TMPro;

public class Main : MonoBehaviour {
	// KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
	//TEST
	//OPTIONS
	public const int NMaxCard = 60;
    public const int NMaxPlace = 10;
    public const int NMaxPlayer = 2;
    public int PlaceTmp;
    public const int CDeck = 0;
    public const int CHand = 1;
    public const int CBatle = 2;
    public const int CBench = 3;
    public const int CTrush = 4;
    public const int CStudium = 5;
    public const int CSide = 6;
    public const int CLost = 7;
    public const int CWithCard = 8;
    public const int CTmp1 = 9;
    //KEY
    public int PlaceF;
    public int PlaceD;
    public int PlaceS;
    public int PlaceR;
    public int PlaceE;
    public int PlaceW;
    //CLICK
    public static bool ClkF = false;
    public static int ClkID = -1;
    public static bool ClkCardButtonUp = false;
    public static bool ClkCardButtonDown = false;
    public static int ClkKind = -1;//1:Card; 2:Marker
    public static bool ClkRight = false;
    //public static int ClkOder = -1;
    public static int ClkPlace = -1;
    public static int ClkPlayer = -1;
    public static bool ClkShift = false;
    public static bool ClkDrag = false;
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    //CONTROL
    public static int iGlv = 0;
    // public static int iGlv2 = 0;
    public static int ActivePlayer = 0;
    public static int ClickedCardID;
    public static int ClickedPlaceID;
    public static int[] DeckSelection = new int[2];
    public static string DmgInput = "";
    public Text DmgText;
    public static bool Shift = false;
    // public static bool FBenchMarkerMove = false;
    public static bool FCameraMove = false;
    public static bool FDoUndo = false;
    public static bool FDmgInput = false;
    public static bool FMode = false;
    public static bool FFirstFrame = true;
    public static bool[] FWithCard = new bool[ NMaxPlace ];
    public int NDeckData = 10;
    public static int[] PreBenchNum = new int[ 2 ];
    public static string[,] TextControl = new string[ NMaxPlayer, NMaxCard ];
    public static int[,] TextureControl = new int[ NMaxPlayer, NMaxCard ];

    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    //PREFAB
    Camera MainCam;
    public static GameObject[,] CardsPfb = new GameObject[ 2 , 60 ];
    public static GameObject[,] PlaceMarkerPfb = new GameObject[ 2 , 60 ];
    Vector3 tpos;
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    //CLASS
    public static DeckCls[] Deck = new DeckCls[ 2 ];
    public DeckDataCls[] DeckData = new DeckDataCls[10];
    public GameObject DmgInputUI;
    public static TextureManageClass[] AdTex = new TextureManageClass[ 1024 ];
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    //EDIT
    int XN = 10;
    int YN = 6;
    public GameObject CardButtonUp;
    public GameObject CardButtonDown;
    public int DeckEditSelection = 0;
    public static List<string> DeckName = new List<string>();
    [SerializeField] private Dropdown[] DeckSelectDD = new Dropdown[2];
    [SerializeField] private Dropdown DeckSelectDDEdit;
    // [SerializeField] private Dropdown DeckSelectDD1;
    // [SerializeField] private Dropdown DeckSelectDD2;
    public static int NTexList;
    int[] TexList = new int[1];
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK


    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void Start() {
        InitMain();
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void Update() {
        Ray MainRay = new Ray();
        RaycastHit RayHit = new RaycastHit();
        Camera MainCam = Camera. main;
        MainRay = Camera. main. ScreenPointToRay( Input. mousePosition );

        if( FMode ) {
            DeckEdit();
        } else {
            RefleshAll();
            ClickContextPcs();
            DmgInputShow();
            Undo();
            BenchMarkerSensor();
            CorAll();
            DeckSelectCheck();
        }
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void BenchMarkerSensor(){
        Vector3 ofs = new Vector3();
        ofs. y = -0.1f;
        int n = Deck[0]. Place[CBench]. NCardInside;
        if(PreBenchNum[0] != n ) {
            SetPosGO( ref PlaceMarkerPfb[ 0 , CBench ], Deck[ 0 ].DefPos. CalcPos( CBench , n, 0, false ) + ofs);
            PreBenchNum[0] = n;
        }
        n = Deck[1]. Place[CBench]. NCardInside;
        if(PreBenchNum[1] != n) {
            SetPosGO( ref PlaceMarkerPfb[ 1 , CBench ], Deck[ 1 ]. DefPos. CalcPos( CBench , n, 0, false ) + ofs);
            PreBenchNum[1] = n;
        }
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void CameraPlayerSet() {
        if( FCameraMove ) {
            if( ActivePlayer == 0 ) {
                CameraPos( 0 , 5 , -4 );
                CameraRot( 60 , 0 , 0 );
            } else if( ActivePlayer == 1 ){
                CameraPos( 0 , 5 , 4 );
                CameraRot( 60 , 180 , 0 );
            }
        }
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void CameraPos(double x, double y, double z) {
        Vector3 R = MainCam. transform. position;
        R. x = (float)x;    R. y = (float)y;  R. z = (float)z;
        MainCam. transform. position = R;
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void CameraRot(double x, double y, double z) {
        Vector3 R = MainCam. transform. eulerAngles;
        R. x = (float)x;    R. y = (float)y;  R. z = (float)z;
        MainCam. transform. eulerAngles = R;
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void CardClickPcs(){
        Debug. Log( "CardClickPcs" );
        Deck[ ClkPlayer ]. MoveCard( ClkID , Deck[ ClkPlayer ]. Cards[ ClkID ]. WithCard ,  CTmp1 , false );
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void CardRightClickPcs() {
        Debug.Log("Right Click Pcs");
        int toPlace = Deck[ ClkPlayer ]. Cards[ClkID]. MyPlace;
        int toCard = ClkID;
        if( FWithCard[ toPlace ] ) {//BATTLE BENCH CTMP1
            if( Deck[ ClkPlayer ]. Cards[ ClkID ]. WithCard ) {//CHILD CARD
                toCard = Deck[ ClkPlayer ]. Cards[ ClkID ]. MyParentCard;
            }
            Deck[ ClkPlayer ]. MoveAllCard( CTmp1 , false , toCard , true );
            // Deck[ ClkPlayer ]. MoveAllCardToCard( CTmp1 , toCard );
        }else{
            Deck[ ClkPlayer ]. MoveAllCard( CTmp1 , false , toPlace , false );
            // Deck[ ClkPlayer ]. MoveAllCardToPlace( CTmp1 , toPlace );
        }
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void CardShiftClickPcs() {
        // Deck[ ClkPlayer ]. MoveAllCardToPlace( CTmp1 , Deck[ClkPlayer].Cards[ClkID].MyPlace );
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void ClickContextPcs() {
        if ( ClkF ) {//SOMETHING CLICKED
            ClkF = false;
            ClkShift = Input. GetKey( KeyCode. LeftShift );
            //Debug. Log( 1 );
            if ( ClkDrag ) {
            }else{//Drag
                if ( ClkKind == 1 ) {//CardPfb
                    ClkKind = -1;
                    //Debug. Log( 11 );
                    if ( ClkRight ) {
                        ClkRight = false;
                        CardRightClickPcs();
                    } else { //LEFT CLICK
                        if ( ClkShift ) {
                            ClkShift = false;
                            CardShiftClickPcs();
                        } else {
                            CardClickPcs();
                        }
                    }
                } else if ( ClkKind == 2 ) {//MarkerPfb
                    ClkKind = -1;
                    //Debug. Log( 12 );
                    if ( ClkRight ) {
                        MarkerRightClickPcs();
                    } else {
                        MarkerClickPcs();
                    }
                }//ClkKind
            }//ClkDrag  
        }//ClkF
        if( Input. GetKeyDown( KeyCode. Return ) ) {
            // GameObject tmpGO = DmgInputUI. transform. Find("Text"). gameObject;
            // DmgInput = tmpGO. GetComponent<text>. text;
            DmgInput = DmgText. text;
            FDmgInput = false;
            DmgInputUI. gameObject. SetActive( false );
        }
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void ClsSet() {
        Deck[0] = new DeckCls();
        Deck[1] = new DeckCls();
        Deck[0]. InitDeckCls(0);
        Deck[1]. InitDeckCls(1);
        // for( int i1 = 0; i1 < NMaxPlayer; i1++ ) {
        //     DeckSelectDD[ i1 ] = new Dropdown
        // }
        for( int i1 = 0; i1 < NDeckData; i1++ ) {
            DeckData[ i1 ] = new DeckDataCls();
        }
        for( int i1 = 0; i1 < 1024; i1++ ) {
            AdTex[ i1 ] = new TextureManageClass();
        }
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void CorAll() {
        for ( int ip = 0; ip < NMaxPlayer; ip++ ) {
            for ( int id = 0; id < NMaxCard; id++ ) {
                Deck[ ip ]. Cards[ id ]. Cor. StepCor(ref CardsPfb[ip,id]);
            }
        }
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void DataLoad() {
        NDeckData = PlayerPrefs. GetInt( "NDeckData" , 10 );
        string s = "";
        string name = "";
        for ( int i1 = 0; i1 < NDeckData; i1++ ) {
            //DeckData
            name = "DeckData" + i1;
            DeckName. Add( name );
            DeckData[ i1 ]. Name = name;
            s = JsonUtility. ToJson( DeckData[ i1 ] );
            s = PlayerPrefs. GetString( name , s );
            JsonUtility. FromJsonOverwrite( s , DeckData[i1] );
            Debug.Log(s);
        }
        foreach (DeckDataCls Deck in DeckData ) {
            Deck. ResetTexture();
        }
        foreach ( Dropdown item in DeckSelectDD) {
            item. ClearOptions();
            item. AddOptions( DeckName );
        }
        DeckSelectDDEdit. ClearOptions();
        DeckSelectDDEdit. AddOptions( DeckName );
        // DeckSelectDDEdit. gameObject. SetActive( false );
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void DataSave() {
        PlayerPrefs. SetInt( "NDeckData" , NDeckData );
        string s = "";
        string name = "";
        for ( int i1 = 0; i1 < NDeckData; i1++ ) {
            name = "DeckData" + i1;
            s = JsonUtility. ToJson( DeckData[ i1 ] );
            PlayerPrefs. SetString( name , s );
            Debug.Log(s);
        }
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void DeckDataLoad() {
        DataLoad();
        for( int ip = 0; ip < NMaxPlayer; ip++) {
            // TextureControl[ ip , 0 ] = 506;
            // TextureControl[ ip , 1 ] = 506;
            // TextureControl[ ip , 2 ] = 506;
            // TextureControl[ ip , 3 ] = 506;
            // TextureControl[ ip , 4 ] = 508;
            // TextureControl[ ip , 5 ] = 508;
            // TextureControl[ ip , 6 ] = 508;
            // TextureControl[ ip , 7 ] = 508;
            // TextureControl[ ip , 8 ] = 507;
            // TextureControl[ ip , 9 ] = 507;
            // TextureControl[ ip , 10 ] = 507;
            // TextureControl[ ip , 11 ] = 507;
            // TextureControl[ ip , 12 ] = 505;
            // TextureControl[ ip , 13 ] = 505;
            // TextureControl[ ip , 14 ] = 505;
            // TextureControl[ ip , 15 ] = 505;
            // TextureControl[ ip , 16 ] = 509;
            // TextureControl[ ip , 17 ] = 509;
            // TextureControl[ ip , 18 ] = 501;
            // TextureControl[ ip , 19 ] = 501;
            // TextureControl[ ip , 20 ] = 21;
            // TextureControl[ ip , 21 ] = 21;
            // TextureControl[ ip , 22 ] = 21;
            // TextureControl[ ip , 23 ] = 21;
            // TextureControl[ ip , 24 ] = 21;
            // TextureControl[ ip , 25 ] = 21;
            // TextureControl[ ip , 26 ] = 21;
            // TextureControl[ ip , 27 ] = 101;
            // TextureControl[ ip , 28 ] = 101;
            // TextureControl[ ip , 29 ] = 101;
            // TextureControl[ ip , 30 ] = 101;
            // TextureControl[ ip , 31 ] = 102;
            // TextureControl[ ip , 32 ] = 102;
            // TextureControl[ ip , 33 ] = 102;
            // TextureControl[ ip , 34 ] = 102;
            // TextureControl[ ip , 35 ] = 103;
            // TextureControl[ ip , 36 ] = 103;
            // TextureControl[ ip , 37 ] = 103;
            // TextureControl[ ip , 38 ] = 105;
            // TextureControl[ ip , 39 ] = 105;
            // TextureControl[ ip , 40 ] = 105;
            // TextureControl[ ip , 41 ] = 105;
            // TextureControl[ ip , 42 ] = 106;
            // TextureControl[ ip , 43 ] = 106;
            // TextureControl[ ip , 44 ] = 106;
            // TextureControl[ ip , 45 ] = 106;
            // TextureControl[ ip , 46 ] = 201;
            // TextureControl[ ip , 47 ] = 201;
            // TextureControl[ ip , 48 ] = 201;
            // TextureControl[ ip , 49 ] = 201;
            // TextureControl[ ip , 50 ] = 203;
            // TextureControl[ ip , 51 ] = 202;
            // TextureControl[ ip , 52 ] = 202;
            // TextureControl[ ip , 53 ] = 504;
            // TextureControl[ ip , 54 ] = 504;
            // TextureControl[ ip , 55 ] = 504;
            // TextureControl[ ip , 56 ] = 504;
            // TextureControl[ ip , 57 ] = 504;
            // TextureControl[ ip , 58 ] = 504;
            // TextureControl[ ip , 59 ] = 504;
        }
        DeckData[ DeckSelection[ 0 ] ]. RefleshTexture( 0 );
        DeckData[ DeckSelection[ 1 ] ]. RefleshTexture( 1 );
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void DeckEdit() {
        if( FFirstFrame ) {
            DeckEditFirst();
            FFirstFrame = false;
        }
        if( ClkF ) {
            CardButtonUp. SetActive( true );
            CardButtonDown. SetActive( true );
            MoveCardButton();
        }
        if( ClkCardButtonUp ) {
            ClkCardButtonUp = false;
            DeckData[ DeckEditSelection ]. AddCard( TextureControl[ ClkPlayer , ClkID ] );
        }
        if( ClkCardButtonDown ) {
            ClkCardButtonDown = false;
            DeckData[ DeckEditSelection ]. RemoveCard( TextureControl[ ClkPlayer , ClkID ] );
        }
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void DeckEditFirst() {
        Vector3 V3 = new Vector3();
        Vector3[] offset = new Vector3[ 2 ];
        Quaternion Q = new Quaternion();
        const float CardW = 0.67f;
        const float CardH = 0.83f;
        CameraPos( 0 , 8 , 0 );
        CameraRot( 90 , 0 , 0 );
        //CARD MOVE
        if( FFirstFrame ) {
            FFirstFrame = false;
            for ( int iod = 0; iod < NMaxCard; iod++ ) {
                V3 = CardsPfb[ 0 , iod ]. transform. position;
                V3 = Deck[ 0 ]. DefPos. CalcPosEdit( 0 , iod );
                CardsPfb[ 0 , iod ]. transform. position = V3;
                V3 = CardsPfb[ 1 , iod ]. transform. position;
                V3 = Deck[ 0 ]. DefPos. CalcPosEdit( 1 , iod );
                CardsPfb[ 1 , iod ]. transform. position = V3;
            }
        }
        //EDIT DECK
        DeckData[ DeckEditSelection ]. RefleshTexture( 1 );
        DeckSelectDDEdit. gameObject. SetActive( true );
        //TEX LIST
        int i1, i2;
        int nmax = Mathf. Min( NMaxCard , NTexList );
        for( i1 = 0; i1 < nmax; i1++) {
            TextureControl[ 0 , i1 ] = TexList[ i1 ];
        }
        for( i1 = nmax; i1 < NMaxCard; i1++ ) {
            CardsPfb[ 0 , i1 ]. SetActive( false );
        }
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void DeckSelectCheck() {
        for ( int i1 = 0; i1 < NMaxPlayer; i1++ ) {
            if( DeckSelection[ i1 ] != DeckSelectDD[ i1 ]. value ) {
                DeckSelection[ i1 ] = DeckSelectDD[ i1 ]. value;
                DeckData[ DeckSelection[ i1 ] ]. RefleshTexture( i1 );
            }
        }
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void DmgInputShow() {
        if( FDmgInput ) {
            DmgInputUI. gameObject. SetActive( true );
        }
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void InitMain() {
        PrefabSet();
        ClsSet();
        OptionLoad();
        //PrefabSetPst();
        TextureLoad();
        TextureInit();
        SetPlaceMarker();
        // DataLoad();
        DeckDataLoad();
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void MarkerClickPcs() {
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void MarkerRightClickPcs() {
        Deck[ ClkPlayer ]. MoveAllCard( CTmp1 , false , ClkPlace , false );
        // Deck[ ClkPlayer ]. MoveAllCardToPlace( CTmp1 , ClkPlace );
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void MoveCardButton() {
        Vector3 offset1 = new Vector3();
        Vector3 offset2 = new Vector3();
        offset1 = Deck[ 0 ]. DefPos. SetVecPow( 0 , 1 , 0.7 );
        offset2 = Deck[ 0 ]. DefPos. SetVecPow( 0 , 1 , -0.7 );
        SetPosGO( ref CardButtonUp, Deck[ 0 ]. DefPos. CalcPosEdit( ClkPlayer , ClkID ) + offset1 );
        SetPosGO( ref CardButtonDown, Deck[ 0 ]. DefPos. CalcPosEdit( ClkPlayer , ClkID ) + offset2 );
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void TextureInit() {
        for ( int ip = 0; ip < NMaxPlayer; ip++ ) {
            for ( int ic = 0; ic < NMaxCard; ic++ ) {
                TextureControl[ ip , ic ] = 21;
            }
        }
        TextureListMaking();
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void TextureListMaking() {
        int count = 0;
        foreach ( TextureManageClass aug in AdTex) {
            if( aug. Exist ) {
                count++;
            }
        }
        NTexList = count;
        Debug.Log("Tex N = " + NTexList);
        Array. Resize( ref TexList , NTexList );//OK
        count = 0;
        for( int i1 = 0; i1 < 1024; i1++ ) {
            if( AdTex[ i1 ]. Exist ) {
                TexList[ count ] = i1;
                count++;
            }
        }
        //LINQ
        count = AdTex. Count( a => a. Exist );
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void TextureLoad() {
        AdTex[ 1015 ]. Load("EnergyFire");
        AdTex[ 21 ]. Load("twinene");
        // AdTex[ 22 ]. Load("twinene");
        // CardTexture[ 21 ] = (Texture) Resources. Load("twinene");
        AdTex[ 101 ]. Load("kuibo");
        AdTex[ 102 ]. Load("supabo");
        AdTex[ 103 ]. Load("misutore");
        AdTex[ 104 ]. Load("poketsu");
        AdTex[ 105 ]. Load("kaishu");
        AdTex[ 106 ]. Load("dato");
        AdTex[ 201 ]. Load("hakaken");
        AdTex[ 202 ]. Load("boss_order");
        AdTex[ 203 ]. Load("homika");
        AdTex[ 501 ]. Load("yareyu");
        AdTex[ 502 ]. Load("bariya");
        AdTex[ 503 ]. Load("mutwo");
        AdTex[ 504 ]. Load("purizira");
        AdTex[ 505 ]. Load("bariko");
        AdTex[ 506 ]. Load("horubi");
        AdTex[ 507 ]. Load("kodede");
        AdTex[ 508 ]. Load("pottodesu");
        AdTex[ 509 ]. Load("yabacha");
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void OptionLoad(){
        FWithCard[ CDeck ] = false;
        FWithCard[ CHand ] = false;
        FWithCard[ CBatle ] = true;
        FWithCard[ CBench ] = true;
        FWithCard[ CTrush ] = false;
        FWithCard[ CStudium ] = false;
        FWithCard[ CSide ] = false;
        FWithCard[ CLost ] = false;
        FWithCard[ CWithCard ] = false;
        FWithCard[ CTmp1 ] = true;
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void PrefabSet() {
		CardsPfb[ 0 , 0 ] = new GameObject();
		CardsPfb[ 0 , 0 ] = GameObject. Find( "Card" );
		PlaceMarkerPfb[ 0 , 0 ] = new GameObject();
		PlaceMarkerPfb[ 0 , 0 ] = GameObject. Find( "PlaceMarker" );
        DmgInputUI. gameObject. SetActive( false );
        // CardButtonUp = new GameObject();
        // CardButtonUp = GameObject. Find( "PlaneTryUp" );
        // CardButtonDown = new GameObject();
        // CardButtonDown = GameObject. Find( "PlaneTryDwn" );
        MainCam = Camera. main;

        ActivePlayer = 0;
        for ( iGlv = 1; iGlv < Main. NMaxCard; iGlv++ ) {
            CardsPfb[ ActivePlayer , iGlv ] = Instantiate( CardsPfb[ 0 , 0 ] );
        }
		for ( iGlv = 1; iGlv < Main. NMaxPlace; iGlv++ ) {
			PlaceMarkerPfb[ ActivePlayer , iGlv ] = Instantiate( PlaceMarkerPfb[ 0 , 0 ] );
		}
        ActivePlayer = 1;
         for ( iGlv = 0; iGlv < Main. NMaxCard; iGlv++ ) {
            CardsPfb[ ActivePlayer , iGlv ] = Instantiate( CardsPfb[ 0 , 0 ] );
        }
		for ( iGlv = 0; iGlv < Main. NMaxPlace; iGlv++ ) {
			PlaceMarkerPfb[ ActivePlayer , iGlv ] = Instantiate( PlaceMarkerPfb[ 0 , 0 ] );
		}
        ActivePlayer = 0;
	}
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void RefleshAll() {
        CameraPlayerSet();
        if( FFirstFrame ) {
            DataSave();
            FFirstFrame = false;
            //CARD PFB
            for ( int ply = 0; ply < NMaxPlayer; ply++ ) {
                for( int id = 0; id < NMaxCard; id++ ) {
                    CardsPfb[ ply , id ]. SetActive( true );
                }
                Deck[ ply ]. Reflesh();
            }
            //CARD TEXTURE
            for( int ply = 0; ply < NMaxPlayer; ply++ ) {
                DeckData[ DeckSelection[ ply ] ]. RefleshTexture( ply );
            }
            //EDIT
            DeckSelectDDEdit. gameObject. SetActive( false );
            CardButtonUp. SetActive( false );
            CardButtonDown. SetActive( false );
        }// FFirstFrame
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void SetPlaceMarker() {
        Vector3 ofs = new Vector3();
        ofs. y = -0.1f;
        for ( int player = 0; player < NMaxPlayer; player++ ) {
            for ( int ip = 0; ip < NMaxPlace; ip++ ) {
                SetPosGO( ref PlaceMarkerPfb[ player , ip ] , Deck[ player ]. DefPos. CalcPos( ip , 0 , 0 , false ) + ofs );
            }
        }
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void SetPos(ref GameObject pfb, double x , double y , double z ) {
        Vector3 tpos = new Vector3();
        tpos = pfb. transform. position;
        tpos. x = (float)x;    tpos. y = (float)y;    tpos. z = (float)z;
        pfb. transform. position = tpos;
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void SetPosGO(ref GameObject aug, Vector3 vec) {
        Vector3 tpos = aug. transform. position;
        tpos. x = vec. x;
        tpos. y = vec. y;
        tpos. z = vec. z;
        aug. transform. position = tpos;
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    public static void ShuffleDeck() {
        // Debug. Log("SHUFFLE");
        Deck[ ActivePlayer ]. Place[ CDeck ]. Shuffle();
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
    void Undo() {
        if ( FDoUndo ) {
            FDoUndo = false;
            Debug. Log( "Undo!!!" );
            Deck[ 0 ]. DoUndo();
            Deck[ 1 ]. DoUndo();
        }
    }
    // KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK








}
