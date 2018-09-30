using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameGenerator : MonoBehaviour {
	public GameObject left;
	public GameObject right;
	public GameObject up;

	public GameObject down;

	private static System.Random random = new System.Random();
	void Start () {
		float canvasWidth = gameObject.GetComponentInParent<Canvas>().pixelRect.width;
		float canvasHeight = gameObject.GetComponentInParent<Canvas>().pixelRect.height;
		float arrowHeight;
		float arrowWidth;
		RectTransform parentRectTransform = gameObject.GetComponent<RectTransform>();
		int NoOfArrows = 6;
		for(int i = 0; i < NoOfArrows; i++){
			GameObject arrow = null;
			switch (random.Next(0,4)){
				case 0:
					arrow = Instantiate(up);
					break;
				case 1:
					arrow = Instantiate(down);
					break;
				case 2:
					arrow = Instantiate(left);
					break;
				case 3:
					arrow = Instantiate(right);
					break;

			}
			RectTransform arrowRectTransform = arrow.GetComponent<RectTransform>();
			arrowHeight = arrowRectTransform.rect.height;
			arrowWidth = arrowRectTransform.rect.width;
			arrowRectTransform.SetParent(parentRectTransform);
			arrowRectTransform.localPosition = new Vector2((-canvasWidth/2) + (i + 1) * (canvasWidth - NoOfArrows*arrowWidth)/(NoOfArrows + 1) + (i + 0.5f) * arrowWidth, 0);
		}
		// test.GetComponent<RectTransform>().localPosition = new Vector2(canvasWidth,canvasHeight);
	}
}
