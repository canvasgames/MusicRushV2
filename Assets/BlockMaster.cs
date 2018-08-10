using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BlockDifficulty{
	SuperEasy, Easy, Medium, Hard, VeryHard, SuperHard, None
}

public enum BlockType{
	CustomBlock, Custom2Blocks, SpkMid, SpkNotMid, TripleSpkMid, TwoSpksMid, 
	WallAndSpkAtCenter, WallAndHiddenSpkAtCenter, WallAndHiddenManualSpkAtCenter,
	TwoSpikesAtCorner, SpkMidAndSpkCornerLeft, SpkMidAndSpkCornerRandom, SpkMidAndSpkCornerRight, MediumTwoSpikesMid,
	OneHiddenSpk, TwoHiddenSpkMid, HiddenSpkAndSpkMid,
	ThreeSpks, TwoTripleSpksMid,
	WallNotCorner, 
	HoleAbove,
}

public enum DenyCondition{ SpikeLeft, SpikeRight, Wall, Hole, Saw }

public class BlockMaster : MonoBehaviour {
	#region === Vars ===
	public static BlockMaster s;

//	public InputField iField;
	Obstacle[] nextBlock;
	public bool NewCreationLogic;
	[Space (5)]
	[Header ("Block Difficulties")]
	public int superEasy=5; 
	public int easy=10, medium=20, hard=30, veryHard=45;
	[Space (10)]
	public BlockDifficulty debugInitialBlock;
	public BlockDifficulty debugAllBlocks = BlockDifficulty.None;

	List <BlockType> justCreatedBlockTypes;
	BlockType lastCreatedBlock;
	int nCreatedBlocks = 0;
	int totalCreatedBlocks = 0;
	List<Block> BlockList, CurBlockList, justCreatedBlocks;
	public List<Block> BlocksSuperEasy, BlocksEasy, BlocksMedium, BlocksHard, BlocksVeryHard, BlocksSuperHard;

	string wave_name;
	float actual_y;

	public float hole_size;
	float hole_dist = 1.35f;
	float screen_w = 9.4f;
	float corner_left = -4.35f;
	float corner_right = 4.35f;
	float mid_area = 2.1f;
	float center_mid_area = 1f;
	float min_spk_dist = 2.5f;

	[Header ("CORNER LIMITS")]
	public float corner_limit_right = 2.7f;
	public float corner_limit_left = -2.7f;

	bool last_spike_left;
	bool last_spike_right;
	bool last_hole;
	bool last_wall;
	bool last_saw;
	int hole_creation_failed = 0;

	// Use this for initialization
	void Awake () {
		s = this;
		justCreatedBlockTypes = new List<BlockType> ();
	}

	public void Init(){
		floor2IsNext = false;
		nextBlock = null;
	}
	#endregion

	#region === Custom Blocks ===
	bool B_CustomBlock(Obstacle[] obsts, int n, string customName = ""){
		
		float xPos = 0;
		bool thereIsHole = false;
		string name = "C..";
		foreach (Obstacle ob in obsts) {
			if (ob.xEnd == 0)
				xPos = ob.xInit;
			else {
				if(ob.xInit < ob.xEnd) xPos = Random.Range (ob.xInit, ob.xEnd);
				else xPos = Random.Range (ob.xEnd, ob.xInit);
			}
			name += ob.myType.ToString () + "_" + xPos.ToString ("0.00")+" & ";
			CreateCustomObstacleByType (ob.myType, xPos, actual_y, n, false);
			if (ob.myType == ObstacleType.hole || ob.myType == ObstacleType.hiddenHole)
				thereIsHole = true;
		}

		if (customName != "")
			name = customName;
			
		if(thereIsHole == false) 
			CreateNormalFloor(name, n);


		return true;
	}

	bool floor2IsNext = false;

	bool B_Custom2FloorsBlock(Obstacle[] obsts, int n, bool firstTime = true, string customName = ""){
		float xPos = 0;
		bool thereIsHole = false;
		string name = "";
		if (floor2IsNext == false)
			name = "C2.1..";
		else
			name = "C2.2..";
		
		foreach (Obstacle ob in obsts) {
			if (ob.xEnd == 0)
				xPos = ob.xInit;
			else {
				if(ob.xInit < ob.xEnd) xPos = Random.Range (ob.xInit, ob.xEnd); // sort the x pos
				else xPos = Random.Range (ob.xEnd, ob.xInit);
			}
			name += ob.myType.ToString () + "_" + xPos.ToString ("0.00")+" & ";

			CreateCustomObstacleByType (ob.myType, xPos, actual_y, n, false);

			if (ob.myType == ObstacleType.hole || ob.myType == ObstacleType.hiddenHole)
				thereIsHole = true;
		}

		if (customName != "")
			name = customName;
		
		if(thereIsHole == false) 
			CreateNormalFloor(name, n);
		else
			if (QA.s.SHOW_WAVE_TYPE == true) game_controller.s.create_wave_name(0, actual_y, name);

		if (floor2IsNext == false)
			floor2IsNext = true;
		else
			floor2IsNext = false;

//		// SECOND FLOOR
//		name = "";
//		foreach (Obstacle ob in obstsFloor2) {
//			if (ob.xEnd == 0)
//				xPos = 0;
//			else
//				xPos = Random.Range (ob.xInit, ob.xEnd);
//			name += ob.myType.ToString () + "_" + xPos.ToString ()+"&";
//			CreateCustomObstacleByType (ob.myType, xPos, actual_y+globals.s.FLOOR_HEIGHT, n+1, false);
//		}
//		game_controller.s.IncreaseNFloor ();

		return true;
	}

	bool B_HoleAbove(Obstacle[] obsts, int n, float holeAboveX, bool firstTime = true, string customName = ""){
		if (!last_hole) {
			Debug.Log (" B_ HOLE ABOVE!!!!!! FIRST TIME");
			float xPos = 0;
			bool thereIsHole = false;
			string name = "";
			if (floor2IsNext == false)
				name = "C2.1..";
			else
				name = "C2.2..";

			foreach (Obstacle ob in obsts) {
				if (ob.xEnd == 0)
					xPos = ob.xInit;
				else {
					if (ob.xInit < ob.xEnd)
						xPos = Random.Range (ob.xInit, ob.xEnd);
					else
						xPos = Random.Range (ob.xEnd, ob.xInit);
				}
				name += ob.myType.ToString () + "_" + xPos.ToString ("0.00") + " & ";


				if (ob.myType == ObstacleType.spk || ob.myType == ObstacleType.tripleSpk 
					|| ob.myType == ObstacleType.hiddenSpk || ob.myType == ObstacleType.hiddenTripleSpk) 
					game_controller.s.CreateSpikeForHoleAbove (n, holeAboveX, ob.xInit, ob.xEnd, ob.myType);
				else
					CreateCustomObstacleByType (ob.myType, xPos, actual_y, n, false);

				if (ob.myType == ObstacleType.hole || ob.myType == ObstacleType.hiddenHole)
					thereIsHole = true;
			}

			if (thereIsHole == false)
				CreateNormalFloor (name, n);
			else if (QA.s.SHOW_WAVE_TYPE == true)
				game_controller.s.create_wave_name (0, actual_y, name);

			if (floor2IsNext == false)
				floor2IsNext = true;
			else
				floor2IsNext = false;

			//		// SECOND FLOOR
			//		name = "";
			//		foreach (Obstacle ob in obstsFloor2) {
			//			if (ob.xEnd == 0)
			//				xPos = 0;
			//			else
			//				xPos = Random.Range (ob.xInit, ob.xEnd);
			//			name += ob.myType.ToString () + "_" + xPos.ToString ()+"&";
			//			CreateCustomObstacleByType (ob.myType, xPos, actual_y+globals.s.FLOOR_HEIGHT, n+1, false);
			//		}
			//		game_controller.s.IncreaseNFloor ();

			return true;
		} else
			return false;
	}


	#endregion

	#region === Super Easy Blocks ===

	// 1 SPK MIDDLE |___^___|
	bool B_SpkMid(int n) {
		CreateNormalFloor("spk_mid", n);
		game_controller.s.create_spike(Random.Range(corner_limit_left + 1.4f, corner_limit_right - 1.4f), actual_y, n);
		return true;
	}

	// 1 SPK NOT SO MIDDLE |___^___|
	bool B_SpkNotSoMid(int n) {
		CreateNormalFloor("spk_not_so_mid", n);

		game_controller.s.create_spike( 0 + Random.Range(screen_w/6, screen_w/6 + 0.2f)*game_controller.s.SortSign(), actual_y, n);

		return true;
	}

	// 1 TRIPLE SPK MIDDLE |___/\___|
	bool B_TripleSpkMid(int n) {
		CreateNormalFloor("triple_spk_mid", n);
		game_controller.s.create_triple_spike(Random.Range(-screen_w / 3 + 0.8f, screen_w / 3 - 0.8f), actual_y, n);
		return true;
	}

	// 1 SPK NOT SO MIDDLE |__^_^__|
	bool B_2SpksMid(int n){
		CreateNormalFloor("2_spks_mid", n);

		float rand_x = Random.Range(-screen_w / 4, 0 - 1f);
		ClearDenys();
		//first spike
		game_controller.s.create_spike(rand_x, actual_y, n);
		if (rand_x <= corner_limit_left) last_spike_left = true;

		//second spike
		rand_x = Random.Range(rand_x + min_spk_dist + 0.5f, rand_x + min_spk_dist + 1.5f);
		game_controller.s.create_spike(rand_x, actual_y, n);
		if (rand_x <= corner_limit_right) last_spike_right = true;

		return true;
	}

	#endregion

	#region === Medium Blocks === 

	bool B_WallAndSpkAtCenter(int n){
		if (!last_wall) {
			CreateNormalFloor ("wallCorner_1_spk", n);
			float rand_x = Random.Range (-0.35f, 0.35f);
			game_controller.s.create_wall_corner (n);
			game_controller.s.create_spike (rand_x, actual_y, n);
			ClearDenys(); last_wall = true;
			return true;
		} else {
			return false;
		}
	}

	bool B_WallAndHiddenSpkAtCenter(int n){
		if (!last_wall) {
			CreateNormalFloor ("wallCorner_1_hiddenSpk", n);
			float rand_x = Random.Range (-0.35f, 0.35f);

			game_controller.s.create_wall_corner (n);
			game_controller.s.create_hidden_spike (rand_x, actual_y, n);

			ClearDenys();last_wall = true;
			return true;
		} else {
			return false;
		}
	}

	bool B_WallAndHiddenManualSpkAtCenter(int n){
		if (!last_wall) {
			CreateNormalFloor ("wallCorner_1_manualHiddenSpk", n);
			float rand_x = Random.Range (-0.35f, 0.35f);
			game_controller.s.create_wall_corner (n, true);
			game_controller.s.create_hidden_spike (rand_x, actual_y, n, true);
			
			ClearDenys(); last_wall = true;
			return true;
		} else {
			return false;
		}
	}

	bool B_TwoSpksAtEachCorner(int n) {
		
		CreateNormalFloor("2_spks_corners", n);

		game_controller.s.create_spike(corner_left, actual_y, n);
		game_controller.s.create_spike(corner_right, actual_y, n);
		ClearDenys(); last_spike_right = true;
		last_spike_left = true;

		return true;
	}

	bool B_SpkMidAndSpkCornerRandom(int n){
		if (SortChance ()) {
			if (last_spike_left == false) {
				CreateNormalFloor ("1_spk_mid_1_spk_corner", n);

				game_controller.s.create_spike (corner_left, actual_y, n);
				game_controller.s.create_spike (Random.Range (-mid_area + 0.30f, mid_area - 0.30f), actual_y, n);
				ClearDenys(); last_spike_left = true;
				return true;
			} else
				return false;
		} else {
			if (last_spike_right == false) {
				CreateNormalFloor ("1_spk_mid_1_spk_corner", n);

				game_controller.s.create_spike (corner_right, actual_y, n);
				game_controller.s.create_spike (Random.Range (-mid_area + 0.30f, mid_area - 0.30f), actual_y, n);
				ClearDenys(); last_spike_left = true;
				return true;
			} else
				return false;
		}
	}

	bool B_SpkMidAndSpkCornerLeft(int n){
		if (last_spike_left == false) {
			CreateNormalFloor ("1_spk_mid_1_spk_corner", n);

			game_controller.s.create_spike (corner_left, actual_y, n);
			game_controller.s.create_spike (Random.Range (-mid_area + 0.30f, mid_area - 0.30f), actual_y, n);
			ClearDenys(); last_spike_left = true;
			return true;
		} else
			return false;
	}

	bool B_SpkMidAndSpkCornerRight(int n){
		if (last_spike_right == false) {
			CreateNormalFloor ("1_spk_mid_1_spk_corner", n);

			game_controller.s.create_spike (corner_right, actual_y, n);
			game_controller.s.create_spike (Random.Range (-mid_area + 0.30f, mid_area - 0.30f), actual_y, n);
			ClearDenys(); last_spike_left = true;
			return true;
		} else
			return false;
	}

	bool B_Medium2SpikesMid (int n){
		CreateNormalFloor ("medium_2_spks_mid", n);

		float rand_x = Random.Range(-screen_w / 4, 0 - 1f);
		//first spike
		ClearDenys();
		game_controller.s.create_spike(rand_x, actual_y, n);
		if (rand_x <= corner_limit_left) last_spike_left = true;
		else last_spike_left = false;

		//second spike
		rand_x = Random.Range(rand_x + min_spk_dist + 0.5f, rand_x + min_spk_dist + 1.5f);
		game_controller.s.create_spike(rand_x, actual_y, n);
		if (rand_x <= corner_limit_right) last_spike_right = true;
		else last_spike_right = false;

		return true;
	}

	bool B_OneHiddenSpk(int n){
		CreateNormalFloor ("hiddenSPk_mid", n);
		game_controller.s.create_hidden_spike(Random.Range(-mid_area + 0.2f, mid_area - 0.2f), actual_y, n);
		ClearDenys();
		return true;
	}

	#endregion

	#region === Hard Blocks ===
	bool B_ThreeSpks(int n) {
		if(!last_spike_right && !last_spike_left) {
			CreateNormalFloor ("3_spikes", n);
			
			//first spike
			game_controller.s.create_spike(corner_left, actual_y, n);
			game_controller.s.create_spike(corner_right, actual_y, n);
			game_controller.s.create_spike(Random.Range(-mid_area + 0.35f, mid_area - 0.35f), actual_y, n);

			ClearDenys();	last_spike_right = true;
			last_spike_left = true;

			return true;
		}
		else return false;
	}

	bool B_HiddenSpkAndSpkMid(int n){

		CreateNormalFloor ("hard_1_hiddenSpk_1_spkMid", n);

		int is_left = Random.Range(0, 2);
		float rand_x = Random.Range(-0.5f, 0.5f);
		float dist = Random.Range(min_spk_dist/2 + 0.2f, min_spk_dist/2 + 0.45f);
		ClearDenys();
		if (is_left == 1) {
			//first spike
			game_controller.s.create_hidden_spike(rand_x - dist, actual_y, n);
			game_controller.s.create_spike(rand_x + dist, actual_y, n);
		}
		else {
			game_controller.s.create_spike(rand_x - dist, actual_y, n);
			game_controller.s.create_hidden_spike(rand_x + dist, actual_y, n);
		}

		return true;
	}

	bool B_WallNotCorner(int n){
			if (SortChance ()) {
			if (!last_wall && !last_spike_left) {
				CreateNormalFloor ("hard_wallMid", n);

				float wall_pos = Random.Range (-screen_w / 4 + 0.5f, 0 - 0.5f);
				float spk_pos = Random.Range (wall_pos + min_spk_dist + 1.1f, corner_right);

				if (spk_pos >= corner_limit_right && last_spike_right)
					return false;
				else {
					game_controller.s.create_floor (0, n);
					game_controller.s.create_wall (wall_pos, n);
					game_controller.s.create_spike (spk_pos, actual_y, n);
					ClearDenys();
					if (spk_pos >= corner_limit_right)
						last_spike_right = true;
					else
						last_spike_right = false;
					last_spike_left = false;
					last_wall = true;
					last_hole = false;
					return true;
				}
			}
			else
				return false;
			} else {
			if (!last_wall && !last_spike_right) {
				CreateNormalFloor ("hard_wallMid", n);

				float wall_pos = Random.Range (0 + 0.5f, screen_w / 4 - 0.5f);
				float spk_pos = Random.Range (corner_left, wall_pos - min_spk_dist - 1.1f);

				if (spk_pos <= corner_limit_left && last_spike_left)
					return false;
				else {
					game_controller.s.create_floor (0, n);
					game_controller.s.create_wall (wall_pos, n);
					game_controller.s.create_spike (spk_pos, actual_y, n);
					ClearDenys();
					last_spike_right = false;
					if (spk_pos <= corner_limit_left)
						last_spike_left = true;
					else
						last_spike_left = false;
					last_wall = true;
					last_hole = false;
					return true;
				}
			}
			else
				return false;
		}
	}

	bool B_TwoHiddenSpkMid (int n){

		CreateNormalFloor ("2_hiddenSpk_mid", n);

		//float rand_x = Random.Range(-screen_w / 4+0.2f, 0 - 1.00f);
		float dist = Random.Range(min_spk_dist/2 + 0.2f, min_spk_dist/2 + 0.4f);
		float rand_x = Random.Range(-0.5f, 0.5f);
		//first spike
		game_controller.s.create_hidden_spike(rand_x - dist, actual_y, n);
		game_controller.s.create_hidden_spike(rand_x + dist, actual_y, n);
		ClearDenys();
		return true;
	}

	bool B_TwoTripleSpkMid(int n){
		CreateNormalFloor ("2_tripleSpks_mid", n);
	
		float dist = Random.Range(min_spk_dist/2 + 0.2f, min_spk_dist/2 + 0.4f);
		float rand_x = Random.Range(-0.5f, 0.5f);
		//first spike
		game_controller.s.create_triple_spike(rand_x - dist, actual_y, n);
		game_controller.s.create_triple_spike(rand_x + dist, actual_y, n);
		ClearDenys();
		return true;
	}

	#endregion

	#region === Old Logic ===
	public bool CreateBlockLogic(int n_floor) {
		bool wave_found = false;
		int rand = 0;

		if (n_floor <= superEasy) {
			wave_found = game_controller.s.create_wave_super_easy(n_floor);
		}

		else if (n_floor <= easy) {
			wave_found = game_controller.s.create_wave_super_easy(n_floor);
		}

		// USER HAD SOME PROGRESS
		else if (n_floor <= medium) {
			rand = Random.Range(1, 100);

			if (rand <= 35)
				wave_found = game_controller.s.create_wave_easy(n_floor);
			else if (rand <= 65)
				wave_found = game_controller.s.create_wave_medium(n_floor);
			//                    case 3:
			//                        wave_found = create_wave_hard(n_floor);
			//                        break;
		}

		else if (n_floor <= hard) {
			rand = Random.Range(1, 100);

			if (rand <= 10)
				wave_found = game_controller.s.create_wave_easy(n_floor);
			else if (rand <= 25)
				wave_found = game_controller.s.create_wave_medium(n_floor);
			else if (rand <= 65)
				wave_found = game_controller.s.create_wave_hard(n_floor);
		}

		// LETS GET SERIOUS!
		else {
			rand = Random.Range(1, 100);

			if (rand <= 10)
				wave_found = game_controller.s.create_wave_easy(n_floor);
			else if (rand <= 20)
				wave_found = game_controller.s.create_wave_medium(n_floor);
			else if (rand <= 55)
				wave_found = game_controller.s.create_wave_hard(n_floor);
			else if (rand <= 80)
				wave_found = game_controller.s.create_wave_very_hard(n_floor);
			else
				wave_found = game_controller.s.create_wave_super_hard(n_floor);
		}

	return wave_found;

	}

	public bool CreateBlockByDifficulty(BlockDifficulty blockType, int n_floor){
		if (blockType == BlockDifficulty.SuperEasy)
			game_controller.s.create_wave_super_easy(n_floor);
		else if (blockType == BlockDifficulty.Easy)
			game_controller.s.create_wave_easy(n_floor);
		else if (blockType == BlockDifficulty.Medium)
			game_controller.s.create_wave_medium(n_floor);
		else if (blockType == BlockDifficulty.Hard)
			game_controller.s.create_wave_hard(n_floor);
		else if (blockType == BlockDifficulty.VeryHard)
			game_controller.s.create_wave_very_hard(n_floor);
		else
			game_controller.s.create_wave_super_hard(n_floor);

		return true;
	}

	#endregion

	#region === New Creation Logic ===
	bool CreateBlockByType(Block block, int n){

		BlockType blockType = block.type;

		actual_y = globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n;

		bool createSucess = false;

		if (blockType == BlockType.SpkMid)
			createSucess =	B_SpkMid (n);
		else if (blockType == BlockType.SpkNotMid)
			createSucess =	B_SpkNotSoMid (n);
		else if (blockType == BlockType.TripleSpkMid)
			createSucess =	B_TripleSpkMid (n);
		else if (blockType == BlockType.TwoSpikesAtCorner)
			createSucess =	B_TwoSpksAtEachCorner (n);
		else if (blockType == BlockType.TwoSpksMid)
			createSucess =	B_2SpksMid (n);
		else if (blockType == BlockType.CustomBlock)
			createSucess = B_CustomBlock (block.obstacles, n, block.customName);
		else if (blockType == BlockType.Custom2Blocks) {
			if (floor2IsNext == false) {
				createSucess = B_Custom2FloorsBlock (block.obstacles, n, true, block.customName);
				nextBlock = block.obstaclesFloor2;
			} else
				createSucess = B_Custom2FloorsBlock (block.obstaclesFloor2, n, true , block.customName);
		} else if (blockType == BlockType.HoleAbove) {
			if (floor2IsNext == false) {
				nextBlock = block.obstaclesFloor2;
//				float x = block.obstacles[0].xInit;
				createSucess = B_HoleAbove(block.obstacles, n, block.obstaclesFloor2[0].xInit, true, block.customName);
			}
		} 
		else if (blockType == BlockType.WallAndSpkAtCenter) {
			createSucess = B_WallAndSpkAtCenter (n);
		} else if (blockType == BlockType.WallAndHiddenManualSpkAtCenter) {
			createSucess = B_WallAndHiddenManualSpkAtCenter (n);
		} else if (blockType == BlockType.WallAndHiddenSpkAtCenter) {
			createSucess = B_WallAndHiddenSpkAtCenter (n);
		} else if (blockType == BlockType.TwoSpikesAtCorner) {
			createSucess = B_TwoSpksAtEachCorner (n);
		} else if (blockType == BlockType.SpkMidAndSpkCornerLeft) {
			createSucess = B_SpkMidAndSpkCornerLeft (n);
		} else if (blockType == BlockType.SpkMidAndSpkCornerRandom) {
			createSucess = B_SpkMidAndSpkCornerRandom (n);
		} else if (blockType == BlockType.SpkMidAndSpkCornerRight) {
			createSucess = B_SpkMidAndSpkCornerRight (n);
		} else if (blockType == BlockType.MediumTwoSpikesMid) {
			createSucess = B_Medium2SpikesMid (n);
		} else if (blockType == BlockType.OneHiddenSpk) {
			createSucess = B_OneHiddenSpk (n);
		} else if (blockType == BlockType.TwoHiddenSpkMid) {
			createSucess = B_TwoHiddenSpkMid (n);
		} else if (blockType == BlockType.HiddenSpkAndSpkMid) {
			createSucess = B_HiddenSpkAndSpkMid (n);
		} else if (blockType == BlockType.ThreeSpks) {
			createSucess = B_ThreeSpks (n);
		} else if (blockType == BlockType.TwoTripleSpksMid) {
			createSucess = B_TwoTripleSpkMid (n);
		} else if (blockType == BlockType.WallNotCorner) {
			createSucess = B_WallNotCorner (n);
		}
			
//		string methodName = "hello";

//
//		ParameterInfo[] parameters = methodInfo.GetParameters ();
//		if (parameters.Length == 0) {
//			result = methodInfo.Invoke (comp, null);
//		} else {
//			object[] parametersArray = args.Split ('|').Select (str => str.Trim ()).ToArray ();
//			result = methodInfo.Invoke (comp, parametersArray);
//		}
//
//		//Get the method information using the method info class
//		System.Reflection.MethodInfo mi = this.GetType().GetMethod(methodName);
//
//		//Invoke the method
//		// (null- no parameter for the method call
//		// or you can pass the array of parameters...)
//		mi.Invoke(this, null);

		return createSucess;
	}
	
	void ClearDenys(){
		last_spike_left = false;
		last_spike_right = false;
		last_hole = false;
		last_wall = false;
		last_saw = false;
	}



	bool AllowCreationByDenyConditions(Block b){
		foreach (DenyCondition cond in b.denyConditions) {
			if (cond == DenyCondition.Hole && last_hole)
				return false;
			else if (cond == DenyCondition.Saw && last_saw)
				return false;
			else if (cond == DenyCondition.SpikeLeft && last_spike_left)
				return false;
			else if (cond == DenyCondition.SpikeRight && last_spike_right)
				return false;
			else if (cond == DenyCondition.Wall && last_wall)
				return false;
		}

		return true;
	}
	
	public bool CreateBlockLogicNEW(int n_floor){
		if (floor2IsNext == false) {
			if (n_floor <= superEasy)
				BlockList = BlocksSuperEasy;
			else if (n_floor <= easy)
				BlockList = BlocksEasy;
			else if (n_floor <= medium)
				BlockList = BlocksMedium;
			else if (n_floor <= hard)
				BlockList = BlocksHard;
			else if (n_floor <= veryHard)
				BlockList = BlocksVeryHard;
			else
				BlockList = BlocksSuperHard;

			//------------ Sort Block Logic -------------- //
			//if (justCreatedPowerUp.Contains ((PowerUpTypes)type))
			int max = 0;
			foreach (Block b in BlockList)
				max += b.chance;

			//inital declarations for the while loop
			bool blockfound = false;
			int count = 0;
			BlockType blockType = BlockType.SpkMid;
			int last = 0;

			while (blockfound == false && count < 3) {
				count++;
				int rand = Random.Range (0, max);
				last = 0;
				Debug.Log (count + " RAND: " + rand + " MAX CHANCE " + max);
				foreach (Block block in BlockList) {
					blockType = block.type;
					last = last + GetBlockChance (blockType);
					Debug.Log (count + " MY TIME: " + blockType.ToString () + " MY CHANCE: " + block.chance.ToString ());

					if (blockfound == false && rand < last) {
//					if ( AllowCreationByDenyConditions (block) && CreateBlockByType (blockType, n_floor)) {
						if (!justCreatedBlockTypes.Contains (blockType) && AllowCreationByDenyConditions (block) && CreateBlockByType (block, n_floor)) {
							Debug.Log (count + " bbbbbbbbb BLOCK FOUND! " + blockType);
							if (blockType != BlockType.CustomBlock && blockType != BlockType.Custom2Blocks) justCreatedBlockTypes.Add (blockType);
							blockfound = true;
							break;
						} else {
							blockfound = false;
//						Debug.Log (count+" bbbbbbb Block can't be created ... " + blockType+ " Contains? "+ justCreatedBlocks.Contains (blockType) + " Deny Size? "+ block.denyConditions.Length);
							Debug.Log (count + " bbbbbbb Block can't be created ... " + blockType + " Contains? " + justCreatedBlockTypes.Contains (blockType) + " Deny Size? " + block.denyConditions.Length);
							break;
						}
					} else
						Debug.Log (count + " ERROR SEARCHING FOR BLOCK");
				}
			}

			//increase counters
			if (blockfound == true && blockType != BlockType.CustomBlock && blockType != BlockType.Custom2Blocks) {
				lastCreatedBlock = blockType;

				nCreatedBlocks++;
				totalCreatedBlocks++;
				if (nCreatedBlocks > 1 && justCreatedBlockTypes.Count > 0) {
					//Debug.Log("REMOVING AT 0! 
					justCreatedBlockTypes.RemoveAt (0);
					nCreatedBlocks--;
				}
			}

			return blockfound;
		} else {
//			B_Custom2FloorsBlock (nextBlock, n);,
			actual_y = globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n_floor;
			Debug.Log("2ND FLOOR TIME!!!!!!!");
			ClearDenys ();

			B_Custom2FloorsBlock (nextBlock, n_floor);
			floor2IsNext = false;
			nextBlock = null;
			return true;
		}
	}
	
	int GetBlockChance(BlockType type){
		foreach (Block b in BlockList) {
			if (b.type == type) {
				return b.chance;
			}
		}
		return 0;
	}

	void CreateCustomObstacleByType(ObstacleType obstType, float xPos, float yPos, int n, bool isManualTriggered = false){
		if (obstType == ObstacleType.spk)
			game_controller.s.create_spike (xPos, yPos, n);
		else if (obstType == ObstacleType.tripleSpk)
			game_controller.s.create_triple_spike (xPos, yPos, n);
		else if (obstType == ObstacleType.hiddenSpk)
			game_controller.s.create_hidden_spike (xPos, yPos, n, isManualTriggered);
		else if (obstType == ObstacleType.hiddenTripleSpk)
			game_controller.s.create_triple_hidden_spike (xPos, yPos, n, isManualTriggered);
		else if (obstType == ObstacleType.hole)
			game_controller.s.create_just_hole (n, xPos);
		else if (obstType == ObstacleType.hiddenHole)
			game_controller.s.CreateHiddenHoleFixed (n, xPos, false);
		else if (obstType == ObstacleType.saw)
			game_controller.s.create_saw (xPos, yPos, n);
		else if (obstType == ObstacleType.wallCorner)
			game_controller.s.create_wall_corner(n);
		else if (obstType == ObstacleType.wallCenter)
			game_controller.s.create_wall(xPos, 0);


		if (obstType == ObstacleType.spk || obstType == ObstacleType.tripleSpk 
			|| obstType == ObstacleType.hiddenSpk || obstType == ObstacleType.hiddenTripleSpk) {
			if (xPos > corner_limit_right)
				last_spike_right = true;
			else if (xPos < corner_limit_left)
				last_spike_left = true;
		}
		if (obstType == ObstacleType.hole) {
			last_hole = true;
		}
		if (obstType == ObstacleType.saw) {
			last_saw = true;
		}
		if (obstType == ObstacleType.wallCenter || obstType == ObstacleType.wallCorner) {
			last_wall = true;
		}

	}

//	void CreateCustomObstacleB

	#endregion

	bool SortChance(int custom = 50){
		int k = Random.Range (1, 100);
		if (k > custom)
			return true;
		else
			return false;
	}

	void CreateNormalFloor(string name, int n){
		wave_name = name;
		if (QA.s.SHOW_WAVE_TYPE == true) game_controller.s.create_wave_name(0, actual_y, wave_name);
		game_controller.s.create_floor(0, n);
	}



	void OnValidate(){
		UpdateBlockListName ();
	}

	public void UpdateBlockListName(){
		foreach (Block b in BlocksSuperEasy) {
			b.UpdateName ();
			if (b.type == BlockType.CustomBlock || b.type == BlockType.Custom2Blocks  || b.type == BlockType.HoleAbove) {
				foreach (Obstacle ob in b.obstacles) {
					ob.UpdateName ();
				}
			}
			if (b.type == BlockType.Custom2Blocks|| b.type == BlockType.HoleAbove) {
				foreach (Obstacle ob in b.obstaclesFloor2) {
					ob.UpdateName ();
				}
			}
		}
		foreach (Block b in BlocksEasy) {
			b.UpdateName ();
			if (b.type == BlockType.CustomBlock || b.type == BlockType.Custom2Blocks|| b.type == BlockType.HoleAbove ) {
				foreach (Obstacle ob in b.obstacles) {
					ob.UpdateName ();
				}
			}
			if (b.type == BlockType.Custom2Blocks|| b.type == BlockType.HoleAbove) {
				foreach (Obstacle ob in b.obstaclesFloor2) {
					ob.UpdateName ();
				}
			}
		}
		foreach (Block b in BlocksHard) {
			b.UpdateName ();
			if (b.type == BlockType.CustomBlock || b.type == BlockType.Custom2Blocks|| b.type == BlockType.HoleAbove ) {
				foreach (Obstacle ob in b.obstacles) {
					ob.UpdateName ();
				}
			}
			if (b.type == BlockType.Custom2Blocks|| b.type == BlockType.HoleAbove) {
				foreach (Obstacle ob in b.obstaclesFloor2) {
					ob.UpdateName ();
				}
			}
		}
		foreach (Block b in BlocksMedium) {
			b.UpdateName ();
			if (b.type == BlockType.CustomBlock || b.type == BlockType.Custom2Blocks|| b.type == BlockType.HoleAbove ) {
				foreach (Obstacle ob in b.obstacles) {
					ob.UpdateName ();
				}
			}
			if (b.type == BlockType.Custom2Blocks|| b.type == BlockType.HoleAbove) {
				foreach (Obstacle ob in b.obstaclesFloor2) {
					ob.UpdateName ();
				}
			}
		}
		foreach (Block b in BlocksSuperHard) {
			b.UpdateName ();
			if (b.type == BlockType.CustomBlock || b.type == BlockType.Custom2Blocks|| b.type == BlockType.HoleAbove ) {
				foreach (Obstacle ob in b.obstacles) {
					ob.UpdateName ();
				}
			}
			if (b.type == BlockType.Custom2Blocks|| b.type == BlockType.HoleAbove) {
				foreach (Obstacle ob in b.obstaclesFloor2) {
					ob.UpdateName ();
				}
			}
		}
		foreach (Block b in BlocksVeryHard) {
			b.UpdateName ();
			if (b.type == BlockType.CustomBlock || b.type == BlockType.Custom2Blocks|| b.type == BlockType.HoleAbove ) {
				foreach (Obstacle ob in b.obstacles) {
					ob.UpdateName ();
				}
			}
			if (b.type == BlockType.Custom2Blocks|| b.type == BlockType.HoleAbove) {
				foreach (Obstacle ob in b.obstaclesFloor2) {
					ob.UpdateName ();
				}
			}
		}
	}
}
