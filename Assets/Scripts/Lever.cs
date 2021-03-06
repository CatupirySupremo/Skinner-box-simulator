using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Lever : MonoBehaviour {
	public GameObject food;
	public Transform FoodPosition;
	public Transform Parent;
	static int TimesLever = 0;

	Animator animator;
	int times,
		needed;
	bool first;
	Text xLever;
	Text neededLever;

	void Start () {
		animator = GetComponent<Animator>();
		xLever = GameObject.Find ("X_Pressionada").GetComponent<Text> ();
		neededLever = GameObject.Find ("X_Necessaria").GetComponent<Text> ();
		neededLever.text = "Clicks necessários: 1";
		xLever.text = "Clicks realizados: 0";
		needed = 1;
		first = true;
	}

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("Pata")) {
			times++;
			animator.Play ("A_Lever");
			if (first) {
				CreateFood ();
				first = false;
				needed++;
				neededLever.text = "Clicks necessários: " + needed;
				times = 0;
				TimesLever++;
				xLever.text = "Clicks realizados: " + TimesLever;
			} else {
				if ((needed - times) == 0) {
					CreateFood ();
					if (needed == 2) {
						needed++;
					} else
						sort (Random.Range (3, 9), Random.Range (-3, -9));
					times = 0;
					neededLever.text = "Clicks necessários: " + needed;
				}
				TimesLever++;
				xLever.text = "Clicks realizados: " + TimesLever;
			}
		}
	}

	int sort (int plus, int minus) {
		if ((needed + minus) <= 0) {
			needed += plus;
		} else {
			int random = Random.Range (0, 1);
			if (random == 0)
				needed += minus;
			if (random == 1)
				needed += plus;
		}
		return needed;
	}

	void CreateFood(){
		Instantiate (food, FoodPosition.position, transform.rotation, Parent);
	}
}