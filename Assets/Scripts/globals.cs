using UnityEngine;
using System.Collections;


public enum GameScreen{
	MainMenu,
	Gameplay,
	Store,
	LevelEnd,
	SpinDisk,
	RewardNotes,
	RewardCharacter
}

public class globals : MonoBehaviour {
	public static globals s;

	public GameScreen curGameScreen;
	public GameScreen previousGameScreen;

	public int pwSuperJumpCollected = 0, pwVisionCollected, pwShieldCollected = 0;

	public int NOTES_COLLECTED = 0 ;
	public int NOTES_COLLECTED_JUKEBOX = 0;
	public float TIME_TO_ALLOW_TAP = 0.4f;

	public bool AT_STORE = false;

    public bool ALERT_BALL = false;
    public int GAME_OVER = 0;
    public bool CAN_RESTART = false;
    public bool GAME_STARTED = false;

    public float SPK_SCALE = 0.7f;

    public int BALL_ID = 0;
    public float BALL_Y = -8f;
    public float BALL_X = -4f;
    public float CUR_BALL_SPEED;
    public int BALL_FLOOR = 0;
    public bool BALL_GROUNDED = true;
    public float BALL_SPEED_X = 4f;
    public float BALL_SPEED_Y;
	public float CAMERA_SPEED = 0;
	public float HOLE_SPK_DIST = 1.35f;
	[HideInInspector] public float LIMIT_LEFT = -4.8f;
    [HideInInspector] public float LIMIT_RIGHT = 4.8f;
	[HideInInspector] public float SCREEN_MID =  0;
	[HideInInspector] public float FLOOR_HEIGHT = 4f;
    [HideInInspector] public float SLOT = 1f;
	[HideInInspector] public float BASE_Y = -7.5f;
	public float BALL_R = 0.5f;
	public float BALL_D = 1f;
	public int SCREEN_WIDTH = 980;
	public bool JUKEBOX_SORT_ANIMATION = false;
   
	[HideInInspector] public int JUKEBOX_CURRENT_PRICE;

	[HideInInspector] public float CANVAS_X_LEFT, CANVAS_X_RIGHT, CANVAS_Y_TOP, CANVAS_Y_BOTTOM;

    [HideInInspector] public bool PW_ACTIVE;
    [HideInInspector] public bool PW_INVENCIBLE = false;
    [HideInInspector] public bool PW_SUPER_JUMP = false;
    [HideInInspector] public bool PW_SIGHT_BEYOND_SIGHT = false;

    [HideInInspector] public bool PW_ENDING = false;

    [HideInInspector] public bool FIRST_GAME = true;

    [HideInInspector]
    public bool CAN_REVIVE = false;
    [HideInInspector]
    public bool SHOW_VIDEO_AFTER = false;
    [HideInInspector]
    public bool REVIVING = false;

	[HideInInspector] public bool GIFT_ANIMATION = false;

    [HideInInspector]
    public bool MENU_OPEN = false;
    // Use this for initialization

    [HideInInspector]
    public string ACTUAL_CHAR;
	public MusicStyle ACTUAL_STYLE;
	public Skin ACTUAL_SKIN;

    public int ad_type = 0;

    void Awake () {
		MusicStyle mystele;
		CANVAS_X_LEFT = -240; CANVAS_X_RIGHT = 240;
		CANVAS_Y_TOP = -480; CANVAS_Y_BOTTOM = -Screen.height;

		ALERT_BALL = false;
        ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR");

        s = this;
        Debug.Log("Globals Awake!ball y: " + globals.s.BALL_Y);

        BALL_Y = -8f;
        BALL_X = 0;

        HOLE_SPK_DIST = 2.6f;

		curGameScreen = GameScreen.MainMenu;
		previousGameScreen = GameScreen.MainMenu;

		////


       // DontDestroyOnLoad (transform.gameObject);
		
	}
	void Start () {
        Debug.Log("Globals Start! ball y: " + globals.s.BALL_Y);


    }
	
	// Update is called once per frame
	void Update () {

	}
}
