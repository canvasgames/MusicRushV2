using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Block{
	SuperEasy, Easy, Medium, Hard, VeryHard, SuperHard, None
}

public class BlocksMaster : MonoBehaviour {
	public static BlocksMaster s;

	[Space (5)]
	[Header ("Block Difficulties")]
	public int superEasy=5; 
	public int easy=10, medium=20, hard=30, veryHard=45;
	[Space (10)]

	public Block debugInitialBlock;
	public Block debugAllBlocks = Block.None;

	// Use this for initialization
	void Awake () {
		s = this;
	}

	public bool CreateBlockLogic(int n_floor){
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

	public bool CreateBlockByDifficulty(Block blockType, int n_floor){
		if (blockType == Block.SuperEasy)
			game_controller.s.create_wave_super_easy(n_floor);
		else if (blockType == Block.Easy)
			game_controller.s.create_wave_easy(n_floor);
		else if (blockType == Block.Medium)
			game_controller.s.create_wave_medium(n_floor);
		else if (blockType == Block.Hard)
			game_controller.s.create_wave_hard(n_floor);
		else if (blockType == Block.VeryHard)
			game_controller.s.create_wave_very_hard(n_floor);
		else
			game_controller.s.create_wave_super_hard(n_floor);

		return true;
	}
}
