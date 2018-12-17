using UnityEngine;
#if USE_DELTADNA
using DeltaDNA;
#endif

public class AnalyticController : MonoBehaviour {

    public static AnalyticController s;

    public string siteName = "Kongregate";
    string clientVersion = "1.0.4";

    void Awake() { s = this; }

    void Start() {
		// Enter additional configuration here
		#if USE_DELTADNA  
		// Launch the SDK
		if (QA.s.DELTADNA_ON){
			if (GD.s.AnalyticsLive == false) {
				DDNA.Instance.Settings.DebugMode = true;
				DDNA.Instance.Settings.OnInitSendGameStartedEvent = true;
				DDNA.Instance.ClientVersion = clientVersion;
	//            DDNA.Instance.StartSDK(
	//                "87199148446217602329834496314561",
	//                "http://collect7976sprcs.deltadna.net/collect/api",
	//                "http://engage7976sprcs.deltadna.net",
	//                "00"
	//            );
				DDNA.Instance.StartSDK (
					"95987570767871968875954773314796",
					"https://collect10275mscrs.deltadna.net/collect/api",
					"https://engage10275mscrs.deltadna.net",
					"00"
				);
			} else {
				DDNA.Instance.Settings.DebugMode = true;
				DDNA.Instance.ClientVersion = clientVersion;
				DDNA.Instance.StartSDK (
					"87199152274143954720021478014561",
					"http://collect7976sprcs.deltadna.net/collect/api",
					"http://engage7976sprcs.deltadna.net",
					DDNA.AUTO_GENERATED_USER_ID
				);
			}
		}
		#endif
    }

	#region === Report Main Events ===
	#if USE_DELTADNA
	EventBuilder DefaultEvent(){
		EventBuilder eventParams = new EventBuilder();

		eventParams.AddParam("clientVersion", clientVersion);
		if(USER.s.NEWBIE_PLAYER == 1)
			eventParams.AddParam("isTutorial", true);
		else
			eventParams.AddParam("isTutorial", false);

		eventParams.AddParam("platform", DDNA.Instance.Platform);

		eventParams.AddParam("userHighScore", USER.s.BEST_SCORE);
		eventParams.AddParam("userTotalVideosWatched", USER.s.TOTAL_VIDEOS_WATCHED);

		eventParams.AddParam("userTotalGamesWithTutorial", USER.s.TOTAL_GAMES_WITH_TUTORIAL);
		eventParams.AddParam("userTotalGames", USER.s.TOTAL_GAMES);
		eventParams.AddParam("userTutorialGames", USER.s.TUTORIAL_GAMES);
		eventParams.AddParam("userTotalSessionGames", DataRecorderController.s.userSessionGames);
		eventParams.AddParam("userSessionHighscore", DataRecorderController.s.userSessionHighscore);
		eventParams.AddParam("userCurrentCurrency", USER.s.NOTES);
		eventParams.AddParam("userTotalCurrency", USER.s.TOTAL_NOTES);
		eventParams.AddParam("userCurrentChar", globals.s.ACTUAL_STYLE.ToString());
		eventParams.AddParam("userTotalChars", store_controller.s.nCharsBuyed);

//		eventParams.AddParam("userSessionCurrencyCollected", 0);  ADICIONAR
//		eventParams.AddParam("userDisksSpinned", 0);  ADICIONAR


		//		eventParams.AddParam("userCurrentChar", "0");

		return eventParams;
	}

	#endif

    public void ReportGameStarted() {
		#if USE_DELTADNA
		if(QA.s.DELTADNA_ON){
        Debug.Log("[ANAL] REPORTING GAME STARTED");
//       
//		EventBuilder eventParams = new EventBuilder();
		EventBuilder eventParams = DefaultEvent();

//        eventParams.AddParam("isTutorial", false);
        eventParams.AddParam("missionName", "game started");
        //eventParams.AddParam("platform", DDNA.Instance.Platform);
//        eventParams.AddParam("userHighScore",USER.s.BEST_SCORE);
//        eventParams.AddParam("userTotalGames", USER.s.TOTAL_GAMES);
//        eventParams.AddParam("userTotalVideosWatched", USER.s.TOTAL_VIDEOS_WATCHED);
//        eventParams.AddParam("siteName", siteName);

//		eventParams.AddParam("withPowerUps", globals.s.PW_ACTIVE);

//		eventParams.AddParam("userCurrentCurrency", 0);
//		eventParams.AddParam("userTotalCurrency", 0);

		//new
//		eventParams.AddParam("userTotalSessionGames", 0);
//
//		eventParams.AddParam("userCurrentChar", "0");
//		eventParams.AddParam("userTotalChars", 0);
//
//        DDNA.Instance.RecordEvent("missionStarted", eventParams);
		}
		#endif
	}

    public void ReportGameEnded(string killer_wave_name, int duration) {
		#if USE_DELTADNA
		if (QA.s.DELTADNA_ON) {
			Debug.Log ("[ANAL] REPORTING GAME ENDED");
//		EventBuilder eventParams = new EventBuilder();
			EventBuilder eventParams = DefaultEvent ();

			eventParams.AddParam ("missionName", "game ended");
			eventParams.AddParam ("userScore", globals.s.BALL_FLOOR + 1);

			string killerName = "";
			if (killer_wave_name == "" || killer_wave_name == null)
				killerName = "?";
			else
				killerName = killer_wave_name;
			
			eventParams.AddParam ("killerWaveName", killerName);
			eventParams.AddParam ("gameDuration", duration);

			//new
//
			eventParams.AddParam ("currencyCollected", globals.s.NOTES_COLLECTED);
			eventParams.AddParam ("pwShieldCollected", globals.s.pwShieldCollected);
			eventParams.AddParam ("pwSuperJumpCollected", globals.s.pwSuperJumpCollected);
			eventParams.AddParam ("pwVisionCollected", globals.s.pwVisionCollected);

			DDNA.Instance.RecordEvent ("missionCompleted", eventParams);
		}

		#endif
    }

	#endregion

	#region === Report Ads ===


    public void ReportRevive(bool success) {
        Debug.Log("[ANAL] REPORTING REVIVE " + success);
//        EventBuilder eventParams = new EventBuilder();
//        eventParams.AddParam("clientVersion", clientVersion);
//
//        eventParams.AddParam("success", success);
//        // eventParams.AddParam("platform", DDNA.Instance.Platform);
//        eventParams.AddParam("userHighScore", USER.s.BEST_SCORE);
//        eventParams.AddParam("userTotalGames", USER.s.TOTAL_GAMES);
//        eventParams.AddParam("userTotalVideosWatched", USER.s.TOTAL_VIDEOS_WATCHED);
//        eventParams.AddParam("userScore", globals.s.BALL_FLOOR);
//
//		DDNA.Instance.RecordEvent("revive", eventParams);
//        DDNA.Instance.RecordEvent("menuRevive", eventParams);
    }

    public void ReportVideoWatchedForPowerUps() {
        Debug.Log("[ANAL] REPORTING ACTIVATE PWS");
//        EventBuilder eventParams = new EventBuilder();
//        eventParams.AddParam("clientVersion", clientVersion);
//
//        //eventParams.AddParam("platform", DDNA.Instance.Platform);
//        eventParams.AddParam("userHighScore", USER.s.BEST_SCORE);
//        eventParams.AddParam("userTotalGames", USER.s.TOTAL_GAMES);
//        eventParams.AddParam("userTotalVideosWatched", USER.s.TOTAL_VIDEOS_WATCHED);
//
//        DDNA.Instance.RecordEvent("activatePWsPressed", eventParams);
    }

    public void ReportAdAction(string adName = "bomblast", string action = "closed") {
        Debug.Log("[ANAL] REPORTING AD ACTION");
//        EventBuilder eventParams = new EventBuilder();
//        eventParams.AddParam("action", action); // "clicked" or "closed"
//        eventParams.AddParam("adName", adName); // "bomblast or battlepegs 
//        eventParams.AddParam("clientVersion", clientVersion);
//
//
//        eventParams.AddParam("userHighScore", USER.s.BEST_SCORE);
//        eventParams.AddParam("userTotalGames", USER.s.TOTAL_GAMES);
//        eventParams.AddParam("userTotalVideosWatched", USER.s.TOTAL_VIDEOS_WATCHED);
//        eventParams.AddParam("userScore", globals.s.BALL_FLOOR);
//
//        DDNA.Instance.RecordEvent("adAction", eventParams);

    }

//    void Update() {
//        if (Input.GetMouseButtonDown(0)) {
//            //ReportTest();
//        }
//    }

    void ReportTest() {
        Debug.Log("REPORTING EVENT!! ");
//        EventBuilder eventParams = new EventBuilder();
//        eventParams.AddParam("option", "sword");
//        eventParams.AddParam("action", "sell");
//
//        DDNA.Instance.RecordEvent("options", eventParams);
//
//        /////////
//
//        EventBuilder achievementParams = new EventBuilder()
//                .AddParam("achievementName", "Sunday Showdown Tournament Win")
//                .AddParam("achievementID", "SS-2014-03-02-01")
//                .AddParam("reward", new EventBuilder()
//                    .AddParam("rewardProducts", new ProductBuilder()
//                         .AddRealCurrency("USD", 5000)
//                        .AddVirtualCurrency("VIP Points", "GRIND", 20)
//                        .AddItem("Sunday Showdown Medal", "Victory Badge", 1))
//                        .AddParam("rewardName", "Medal"));
//
//        DDNA.Instance.RecordEvent("achievement", achievementParams);
//
//
//        eventParams = new EventBuilder();
//        eventParams.AddParam("aaa", "lime");
//        eventParams.AddParam("aaction", "be a dark Lord");
//        DDNA.Instance.RecordEvent("zeptile", eventParams);
    }


	#endregion

}