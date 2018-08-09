using UnityEngine;
using System.Collections;

public enum MusicStyle{
	Eletro = 0,
	Rock = 1,
	Pop= 2,
	PopGaga = 3,
	Samba = 4,
	Classic = 5,
	Reggae= 6,
	Rap = 7,
	Latina = 8,
	AnimeShounen = 9,
//	DingoBells = 10,
	Lenght = 10
}

public struct Skin{
	public MusicStyle musicStyle;
	public string skinName;
	public int id;
	public bool isBand;
	public bool isClothChanger;
	public int bandN;
	public int styleId;
}

public class GD : MonoBehaviour {

    public static GD s;
	[Header ("MAIN VARS")]
	public int GD_PW_SIGHT_TIME;
	public int GD_PW_HEARTH_TIME;
	public int GD_ROULLETE_WAIT_MINUTES;
	public int GD_COIN_CHANCE = 25;

	[Space (10)]

	public int JUKEBOX_PRICE = 50, JUKEBOX_FTU_PRICE = 5;

	public int[] SCENERY_FLOOR_VALUES;

	[Space (10)]

	public float FOLLOWER_DELAY = 0.7f, FOLLOWER_DELAY_BASE = 0.1f;



	public int FTU_NEWBIE_SCORE = 5;
	public int FTU_MATCHES_TO_UNLOCK_PW = 2;
	public int FTU_MATCHES_TO_UNLOCK_GIFT = 4;
	public int CUR_MATCHES_TO_UNLOCK_STUFF = 0;


    public int GD_WITH_PW_TIME;
    public int GD_WITHOUT_PW_TIME;

    public int GD_JUMPS_PW_BAR_FULL;

    public int GD_GIFT_WAIT_MINUTES;

    public float GlowInTime = 0.15f;
	public float GlowStaticTime = 0f;
	public float GlowOutTime = 0.83f;

    public bool AnalyticsLive = false;

	public int N_MUSIC = 6;
	public int N_SKINS = 9;
	public int SKINS_PER_MUSIC = 3;

	public bool[] musicStyleAllowed;
	public Skin[] skins;
	int curId = 0;

    // Use this for initialization
    void Awake() {
        s = this;
		InitMusicStylesData ();
    }

	void InitMusicStylesData(){
		int skinsArraySize = 0;
		for (int i = 0; i < (int)MusicStyle.Lenght; i++) {
			skinsArraySize += SKINS_PER_MUSIC;
		}
		skins = new Skin[skinsArraySize]; // TBD REMOTE VARIABLE
		Debug.Log(" SKINS: " + skins.Length);

		// ELETRO SKINS = 1
		NewSkin("One More Try", MusicStyle.Eletro, 1); // 0
		NewSkin("Electro Robot", MusicStyle.Eletro, 2); // 1
		NewSkin("Interstella", MusicStyle.Eletro, 3, true, 4); // 2

		// ROCK SKINS = 2
		NewSkin("Guitar Solist", MusicStyle.Rock, 1); // 3
		NewSkin("Rock'n'Roll", MusicStyle.Rock, 2); // 4
		NewSkin("Rock Band", MusicStyle.Rock, 3, true, 3); // 5

		// POP SKINS = 3
		NewSkin("Thriller Man", MusicStyle.Pop, 1); // 7
		NewSkin("Pop King", MusicStyle.Pop, 2); // 6
		NewSkin("Disco", MusicStyle.Pop, 3, true, 5); // 8

		// POPSTARS = 4
		NewSkin("Umbrella", MusicStyle.PopGaga, 1);
		NewSkin("Classic Popstar", MusicStyle.PopGaga, 2);
		NewSkin("Lady Pop", MusicStyle.PopGaga, 3, false, 0, true);

		// SAMBA = 5
		NewSkin("Carnaval", MusicStyle.Samba, 1);
		NewSkin("Rei Momo", MusicStyle.Samba, 2);
		NewSkin("Xaranga", MusicStyle.Samba, 3, true, 3);

		// CLASSIC = 6
		NewSkin("Maestro", MusicStyle.Classic, 1);
		NewSkin("Sympohist", MusicStyle.Classic, 2);
		NewSkin("Orquestra", MusicStyle.Classic, 3, true, 3);

		// REGGAE = 7
		NewSkin("The Jammer", MusicStyle.Reggae, 1);
		NewSkin("Rasta", MusicStyle.Reggae, 2);
		NewSkin("Reggae Family", MusicStyle.Reggae, 3, true, 3);

		// RAP = 8
		NewSkin("Jing Bling", MusicStyle.Rap, 1);
		NewSkin("The Rhymer", MusicStyle.Rap, 2);
		NewSkin("Ma Man", MusicStyle.Rap, 3, true, 3);

		// LATINA = 9
		NewSkin("Waka Waka", MusicStyle.Latina, 1);
		NewSkin("La Vida Loka", MusicStyle.Latina, 2);
		NewSkin("Muchachos", MusicStyle.Latina, 3, true, 3);



//		for (int i = 0; i < MusicStyle.Lenght; i++) {
//			skins [i] = new Skin[3];
//		}

//		skins [0][0]


			/*public enum MusicStyle{
	Eletro = 0,
	Rock = 1,
	Pop= 2,
	PopGaga = 3,
	Reggae= 4,
	Rap = 5,
	Samba = 6,
	Latina = 7,
	Classic = 8,
	DingoBells = 9,
	Lenght = 10*/

	}

	void NewSkin(string name, MusicStyle music, int styleId, bool isBand = false, int bandQuantity = 0, bool clothChanger = false){
		skins[curId].skinName = name;
		skins[curId].musicStyle = music;
		skins[curId].isBand = isBand;
		skins [curId].bandN = bandQuantity;
		skins [curId].isClothChanger = clothChanger;
		skins [curId].id = curId;
		skins [curId].styleId = styleId;

		curId++;
	}


	void DefineAvailableSongs(){
		// FAZER CODIGO AQUI QUE USA VARIAVEL REMOTA
	}

    // Update is called once per frame
    void Update () {
	
	}

	public string GetStyleName(MusicStyle style){
		if (style == MusicStyle.Classic)
			return "Classic";
		else if (style == MusicStyle.Eletro)
			return "Eletronic";
		else if (style == MusicStyle.Latina)
			return "Latina";
		else if (style == MusicStyle.Reggae)
			return "Reggae";
		else if (style == MusicStyle.Pop)
			return "Classic Pop";
		else if (style == MusicStyle.Rap)
			return "Rap";
		else if (style == MusicStyle.PopGaga)
			return "Modern Pop";
		else if (style == MusicStyle.Rock)
			return "Rock";
		else if (style == MusicStyle.Samba)
			return "Samba";
		else if (style == MusicStyle.AnimeShounen)
			return "Anime Shounen";
//		else if (style == MusicStyle.DingoBells)
//			return "Dingo Bells";
		else return "";
	}

	public string GetMusicNameForFMOD(MusicStyle style){
		if (style == MusicStyle.Classic)
			return "music_classic";
		else if (style == MusicStyle.Eletro)
			return "music_electronic";
		else if (style == MusicStyle.Latina)
			return "music_latina";
		else if (style == MusicStyle.Reggae)
			return "music_reggae";
		else if (style == MusicStyle.Pop)
			return "music_pop";
		else if (style == MusicStyle.Rap)
			return "music_rap";
		else if (style == MusicStyle.PopGaga)
			return "music_modern_pop";
		else if (style == MusicStyle.Rock)
			return "music_rock";
		else if (style == MusicStyle.Samba)
			return "music_samba";
		else if (style == MusicStyle.AnimeShounen)
			return "music_anime_shounen";
		//		else if (style == MusicStyle.DingoBells)
		//			return "Dingo Bells";
		else return "";
	}

}
