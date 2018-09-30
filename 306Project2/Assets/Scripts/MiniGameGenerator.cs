using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameGenerator : MonoBehaviour {
	public GameObject left;
	public GameObject right;
	public GameObject up;

	public GameObject down;
	void Start () {
		GameObject test = Instantiate(up);
		RectTransform parentRectTransform = gameObject.GetComponent<RectTransform>();
		float canvasWidth = gameObject.GetComponentInParent<Canvas>().pixelRect.width;
		float canvasHeight = gameObject.GetComponentInParent<Canvas>().pixelRect.height;
		test.GetComponent<RectTransform>().SetParent(parentRectTransform);
		test.GetComponent<RectTransform>().localPosition = new Vector2(canvasWidth,canvasHeight);
	}
}
