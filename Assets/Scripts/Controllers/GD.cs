using UnityEngine;
using System.Collections;

public enum MusicStyle{
	Eletro = 0,
	Rock = 1,
	Pop= 2,
	PopGaga = 3,
	Samba = 4,
	Classic = 5,
	Rap = 6,
	Latina = 7,
	Reggae = 8,
	AnimeShounen = 9,
	DingoBells = 10,
	Lenght = 11
}

public struct Skin{
	public MusicStyle musicStyle;
	public string skinName;
	public int id;
	public bool isBand;
	public bool isClothChanger;
	public int bandN;
	public int styleId;
	public SkinRarity rarity;
	public bool isReleased;
}

public enum SkinRarity{common, uncommon, rare, epic, realBand}

public class GD : MonoBehaviour {

    public static GD s;
	[Header ("POWER UPS")]
	public int GD_PW_SIGHT_TIME;
	public int GD_PW_HEARTH_TIME;
	public int GD_PW_CHANCE_SUPER_JUMP = 20;
	public int GD_PW_CHANCE_SHIELD = 40;
	public int GD_PW_CHANCE_VISION = 40;
    public int GD_PW_SUPER_JUMP_FLOORS = 5;

	[Space (10)]
	[Header ("METAGAME ECONOMY")]
	public int GD_COIN_CHANCE = 25;
	public int GD_COIN_CHANCE_INC = 1;
	public int JUKEBOX_PRICE = 50, JUKEBOX_FTU_PRICE = 5;
	public int GD_ROULLETE_WAIT_MINUTES;
	[Space (5)]

	public int GD_DROP_CHANCE_COMMON = 75;
	public int GD_DROP_CHANCE_UNCOMMON = 20 , GD_DROP_CHANCE_RARE = 5;

	[Header ("STAGE FLOOR")]

	public int[] SCENERY_FLOOR_VALUES;

	[Space (7)]

	[Header ("GEM VALUES")]
	public int GEM_PACK_S_VALUE = 15;
	public int GEM_PACK_M_VALUE = 35;
	public int GEM_PACK_L_VALUE = 75;
	public int GEM_PACK_XL_VALUE = 300;


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

	[Header ("MAIN Ns")]
	public int N_MUSIC = 6;
	public int N_SKINS = 9;
	public int N_STYLES = 9;
	public int SKINS_PER_MUSIC = 3;

	public int SPECIAL_OFFER_DURATION_HOURS = 24;

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
		NewSkin("Interstelar", MusicStyle.Eletro, 3, true, 4, false, SkinRarity.rare); // 2

		// ROCK SKINS = 2
		NewSkin("Guitar Solist", MusicStyle.Rock, 1); // 3
		NewSkin("Rock'n'Roll", MusicStyle.Rock, 2); // 4
		NewSkin("Rock Band", MusicStyle.Rock, 3, true, 3, false, SkinRarity.uncommon); // 5

		// POP SKINS = 3
		NewSkin("Thriller Man", MusicStyle.Pop, 1); // 7
		NewSkin("Pop King", MusicStyle.Pop, 2); // 6
		NewSkin("Disco", MusicStyle.Pop, 3, true, 5, false, SkinRarity.rare); // 8

		// POPSTARS = 4
		NewSkin("Umbrella", MusicStyle.PopGaga, 1);
		NewSkin("Classic Popstar", MusicStyle.PopGaga, 2);
		NewSkin("Lady Pop", MusicStyle.PopGaga, 3, false, 0, true, SkinRarity.rare);

		// SAMBA = 5
		NewSkin("Carnaval", MusicStyle.Samba, 1);
		NewSkin("Rei Momo", MusicStyle.Samba, 2);
		NewSkin("Xaranga", MusicStyle.Samba, 3, true, 3, false , SkinRarity.uncommon);

		// CLASSIC = 6
		NewSkin("Maestro", MusicStyle.Classic, 1);
		NewSkin("Symponist", MusicStyle.Classic, 2);
		NewSkin("Orquestra", MusicStyle.Classic, 3, true, 3, false, SkinRarity.uncommon);

		// RAP = 7
		NewSkin("Jing Bling", MusicStyle.Rap, 1);
		NewSkin("Rap Boy", MusicStyle.Rap, 2);
//		NewSkin("Jump This Way", MusicStyle.Rap, 3, true, 3, false, SkinRarity.uncommon);
		NewSkin("Jump This Way", MusicStyle.DingoBells, 3, true, 3, false, SkinRarity.uncommon);

		// LATINA = 8
		NewSkin("Waka Waka", MusicStyle.Latina, 1);
		NewSkin("Vida Loka", MusicStyle.Latina, 2);
		NewSkin("Mariachis", MusicStyle.Latina, 3, true, 3, false, SkinRarity.uncommon);

		// REGGAE = 9
		NewSkin("The Jammer", MusicStyle.Reggae, 1);
		NewSkin("Rasta", MusicStyle.Reggae, 2);
		NewSkin("Reggae Family", MusicStyle.Reggae, 3, true, 3, false, SkinRarity.uncommon);


		// ANIME SHONEN = 10
		NewSkin("Ora Ora Ora", MusicStyle.AnimeShounen, 1);
		NewSkin("Not a Ninja", MusicStyle.AnimeShounen, 2);
		NewSkin("Revived Franchise", MusicStyle.AnimeShounen, 3, true, 3, false, SkinRarity.uncommon);

		// DINGO BELLS = 11
//		NewSkin("Dingo Bells", MusicStyle.DingoBells, 3, true, 3, false, SkinRarity.rare);

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

	void NewSkin(string name, MusicStyle music, int styleId, bool isBand = false, int bandQuantity = 0, bool clothChanger = false, SkinRarity rarity = SkinRarity.common){
		skins[curId].skinName = name;
		skins[curId].musicStyle = music;
		skins[curId].isBand = isBand;
		skins [curId].bandN = bandQuantity;
		skins [curId].isClothChanger = clothChanger;
		skins [curId].id = curId;
		skins [curId].styleId = styleId;
		skins [curId].rarity = rarity;

		curId++;
	}


	void DefineAvailableSongs(){
		// FAZER CODIGO AQUI QUE USA VARIAVEL REMOTA
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
//			return "music_funk";
		else if (style == MusicStyle.Reggae)
			return "music_reggae";
		else if (style == MusicStyle.Pop)
			return "music_pop";
		else if (style == MusicStyle.Rap)
			return "music_dingo_bells_2";
//			return "music_rap";
//			return "music_funk";
//			return "music_ty_hands_on";
		else if (style == MusicStyle.PopGaga)
			return "music_modern_pop";
		else if (style == MusicStyle.Rock)
			return "music_rock";
		else if (style == MusicStyle.Samba)
			return "music_samba";
		else if (style == MusicStyle.AnimeShounen)
			return "music_anime_shounen";
		else if (style == MusicStyle.DingoBells)
			return "music_dingo_bells_2";

		else return "";
	}

}
