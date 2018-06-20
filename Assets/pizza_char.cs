using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class pizza_char : MonoBehaviour
{
	#region ===== Variables Declaration =====

    public GameObject part1_obj, part2_obj, part3_obj, part4_obj, part5_obj, part6_obj, part7_obj, part8_obj;
    public GameObject hand, button_spin;
    float part1_pct, part2_pct, part3_pct, part4_pct, part5_pct, part6_pct, part7_pct, part8_pct;
    float part1_reward, part2_reward, part3_reward, part4_reward, part5_reward, part6_reward, part7_reward, part8_reward;
    float previousYInput, previousXInput, initialTime;
    public PwWheelMaster rodaMenuScript;

    [HideInInspector]public bool openingTampa = true;
	#endregion
    // Use this for initialization

	public GameObject haste;
    void Start()
    {
        define_percentages(12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f);
		//define_rewards(2, 7, 3, 5, 2, 10, 3, 5); // sentido horario, partindo do meio-topo
        define_rewards(5, 10, 5, 20, 10, 5, 10, 30); // sentido horario, partindo do meio-topo

//		transform.DORotate (new Vector3 (0, 0, transform.localRotation.z -40), 0.8f, RotateMode.WorldAxisAdd).SetEase (Ease.OutQuart).OnComplete (rotate2);
		hud_controller.si.CAN_ROTATE_ROULETTE = true;
    }

	void rotate2(){
//		transform.DORotate (new Vector3 (0, 0, transform.localRotation.z +60), 0.8f, RotateMode.WorldAxisAdd).SetEase (Ease.OutQuart).OnComplete (rotate2);
	}
    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			initialTime = Time.time;
			previousYInput = Input.mousePosition.y;
			previousXInput = Input.mousePosition.x;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			if (  hud_controller.si.CAN_ROTATE_ROULETTE == true)
			{
				rotate(Time.time - initialTime, Vector2.Distance(new Vector2(Input.mousePosition.x, Input.mousePosition.y), new Vector2(previousXInput, previousYInput)), Input.mousePosition.x, Input.mousePosition.y, previousYInput, previousXInput);
			}
		}
    }


    public void define_percentages(float part1pct, float part2pct, float part3pct, float part4pct, float part5pct, float part6pct, float part7pct, float part8pct)
    {
        part1_pct = part1pct;
        part2_pct = part2pct;
        part3_pct = part3pct;
        part4_pct = part4pct;
        part5_pct = part5pct;
        part6_pct = part6pct;
        part7_pct = part7pct;
        part8_pct = part8pct;

        if (part3_pct <= 0)
        {
            part3_obj.SetActive(false);
        }
        if (part4_pct <= 0)
        {
            part4_obj.SetActive(false);
        }
        if (part5_pct <= 0)
        {
            part5_obj.SetActive(false);
        }
        if (part6_pct <= 0)
        {
            part6_obj.SetActive(false);
        }
        if (part7_pct <= 0)
        {
            part7_obj.SetActive(false);
        }
        if (part8_pct <= 0)
        {
            part8_obj.SetActive(false);
        }

        part1_obj.GetComponent<Image>().fillAmount = part1_pct * 0.01f;

        part2_obj.GetComponent<Image>().fillAmount = part2_pct * 0.01f;
        part2_obj.transform.DORotate(new Vector3(part2_obj.GetComponent<RectTransform>().rotation.x,
        part2_obj.GetComponent<RectTransform>().rotation.y, (-360 * 0.01f * part1_pct)), 0);


        part3_obj.GetComponent<Image>().fillAmount = part3_pct * 0.01f;
        part3_obj.transform.DORotate(new Vector3(part3_obj.GetComponent<RectTransform>().rotation.x,
        part3_obj.GetComponent<RectTransform>().rotation.y, (-360 * 0.01f * (part1_pct + part2_pct))), 0);


        part4_obj.GetComponent<Image>().fillAmount = part4_pct * 0.01f;
        part4_obj.transform.DORotate(new Vector3(part4_obj.GetComponent<RectTransform>().rotation.x,
        part4_obj.GetComponent<RectTransform>().rotation.y, (-360 * 0.01f * (part1_pct + part2_pct + part3_pct))), 0);


        part5_obj.GetComponent<Image>().fillAmount = part5_pct * 0.01f;
        part5_obj.transform.DORotate(new Vector3(part5_obj.GetComponent<RectTransform>().rotation.x,
        part5_obj.GetComponent<RectTransform>().rotation.y, (-360 * 0.01f * (part1_pct + part2_pct + part3_pct + part4_pct))), 0);

        part6_obj.GetComponent<Image>().fillAmount = part6_pct * 0.01f;
        part6_obj.transform.DORotate(new Vector3(part6_obj.GetComponent<RectTransform>().rotation.x,
        part6_obj.GetComponent<RectTransform>().rotation.y, (-360 * 0.01f * (part1_pct + part2_pct + part3_pct + part4_pct + part5_pct))), 0);

        part7_obj.GetComponent<Image>().fillAmount = part7_pct * 0.01f;
        part7_obj.transform.DORotate(new Vector3(part7_obj.GetComponent<RectTransform>().rotation.x,
        part7_obj.GetComponent<RectTransform>().rotation.y, (-360 * 0.01f * (part1_pct + part2_pct + part3_pct + part4_pct + part5_pct + part6_pct))), 0);

        part8_obj.GetComponent<Image>().fillAmount = part8_pct * 0.01f;
        part8_obj.transform.DORotate(new Vector3(part8_obj.GetComponent<RectTransform>().rotation.x,
        part8_obj.GetComponent<RectTransform>().rotation.y, (-360 * 0.01f * (part1_pct + part2_pct + part3_pct + part4_pct + part5_pct + part6_pct + part7_pct))), 0);
    }

    void define_rewards(float part1, float part2, float part3, float part4, float part5, float part6, float part7, float part8)
    {
        part1_reward = part1; part2_reward = part2; part3_reward = part3; part4_reward = part4;
        part5_reward = part5; part6_reward = part6; part7_reward = part7; part8_reward = part8;
    }

    public void initClick()
    {
//		Debug.Log ("INIT CLICK");
        initialTime = Time.time;
        previousYInput = Input.mousePosition.y;
    }

    public void endClick()
    {
        if (openingTampa == false && hud_controller.si.CAN_ROTATE_ROULETTE == true)
        {
//            rotate(Time.time - initialTime, Vector2.Distance(new Vector2(Input.mousePosition.y, 0), new Vector2(previousYInput, 0)), Input.mousePosition.x, Input.mousePosition.y, previousYInput);
        }
    }

	public void rotate(float time, float distance, float inputX, float inputY, float lastY, float lastX)
    {
        float angle;
		int min = 10;
        //Debug.Log(time + " Tempooooo");
        //Debug.Log(distance + " Distancia");
		if (distance > 25 && time < 0.7f && time > 0.05f) {
			Debug.Log(" init rotate! x " + inputX + " Y " + inputY + " lastx " + lastX + " lasty "+ lastY + " ... difx: "+ (inputX - lastX) + " DIF Y: "+ (inputY - lastY));


			rodaMenuScript.myBack.interactable = false;
			hand.SetActive (false);
//			if (inputX < 110 && inputY > lastY || inputX >= 110 && inputY < lastY) {
			if (((inputX - lastX) > min  && inputY > 0) || ((inputY - lastY) > min && inputX < 0) || ((inputX - lastX) < -min && inputY < 0) || ((inputY - lastY) < -min && inputX > 0)) {
				Debug.Log("gira horario");
				angle = Random.Range (-1, -360);

			} else {
				angle = Random.Range (1, 360);
				Debug.Log("gira anti-horario");
			}
//
//			float clampdistance = Mathf.Clamp (distance, 35, 300);
//			float clampTime = Mathf.Clamp (distance, 0.1f, 0.6f);
//			float force = clampdistance / clampTime;
			float force = Random.Range(200,250);
//			Debug.Log ("force: " + force);
			angle = angle * (force);

			//Debug.Log(force + " " + angle);
			float tempo = Random.Range (2f, 2.6f);
			transform.DORotate (new Vector3 (0, 0, angle), tempo, RotateMode.WorldAxisAdd).SetEase (Ease.OutQuart).OnComplete (give_reward);
			hud_controller.si.addRoulleteTime ();
//			haste.transform.DOShakePosition (tempo, 1, 10, 90, false);
		} 

		else {
			Debug.Log (" failed to spin.. Dist: " + distance + " TIME: "+ time );
		}
    }

	void haste_animation(){
		
	}

    void give_reward()
    {
		hud_controller.si.PW_time_set_new_date_and_state (true);
		hud_controller.si.ActivateFirstPw ();
        //Debug.Log("angle " + transform.rotation.eulerAngles.z);
        float reward = 1;
        float angle_temp = transform.GetComponent<RectTransform>().eulerAngles.z;
        if (angle_temp >= 0 && angle_temp <= (360 * part1_pct * 0.01f))
        {
            Debug.Log("Caiu no 1");
//            hud_controller.si.add_pw_time(part1_reward);
            reward = part1_reward;
        }
        else if (angle_temp <= ((360 * (part1_pct + part2_pct) * 0.01f)))
        {
            reward = part2_reward;
//            hud_controller.si.add_pw_time(part2_reward);
            Debug.Log("Caiu no 2");
        }
        else if (angle_temp <= ((360 * (part1_pct + part2_pct + part3_pct) * 0.01f)))
        {
            reward = part3_reward;
//            hud_controller.si.add_pw_time(part3_reward);
            Debug.Log("Caiu no 3");
        }
        else if (angle_temp <= ((360 * (part1_pct + part2_pct + part3_pct + part4_pct) * 0.01f)))
        {
            reward = part4_reward;
//			USER.s.AddNotes((int)reward);

//            hud_controller.si.add_pw_time(part4_reward);
            Debug.Log("Caiu no 4");
        }
        else if (angle_temp <= ((360 * (part1_pct + part2_pct + part3_pct + part4_pct + part5_pct) * 0.01f)))
        {
            reward = part5_reward;
//			USER.s.AddNotes((int)reward);

//            hud_controller.si.add_pw_time(part5_reward);
            Debug.Log("Caiu no 5");
        }
        else if (angle_temp <= ((360 * (part1_pct + part2_pct + part3_pct + part4_pct + part5_pct + part6_pct) * 0.01f)))
        {
            reward = part6_reward;
//            hud_controller.si.add_pw_time(part6_reward);
            Debug.Log("Caiu no 6");
        }
        else if (angle_temp <= ((360 * (part1_pct + part2_pct + part3_pct + part4_pct + part5_pct + part6_pct + part7_pct) * 0.01f)))
        {
            reward = part7_reward;
//            hud_controller.si.add_pw_time(part7_reward);
            Debug.Log("Caiu no 7");
        }
        else if (angle_temp <= ((360 * (part1_pct + part2_pct + part3_pct + part4_pct + part5_pct + part6_pct + part7_pct + part8_pct) * 0.01f)))
        {
            reward = part8_reward;
//            hud_controller.si.add_pw_time(part8_reward);
            Debug.Log("Caiu no 8");
        }

//		USER.s.AddNotes((int)reward);
        rodaMenuScript.openRewardMenu(reward);
    }


}
