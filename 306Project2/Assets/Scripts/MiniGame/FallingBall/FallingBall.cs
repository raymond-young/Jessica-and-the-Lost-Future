using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBall : MonoBehaviour {
    public GameObject ballPrefab;


    List<GameObject> balls = new List<GameObject>();

    int numOfBalls = 10;
    float xRange;
    float y;
	// Use this for initialization
	void Start () {
        xRange = gameObject.GetComponentInParent<Canvas>().pixelRect.width / 2;
        y = gameObject.GetComponentInParent<Canvas>().pixelRect.height / 2;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (balls.Count < numOfBalls)
        {
            GameObject ball = Instantiate(ballPrefab);
            ball.GetComponent<RectTransform>().SetParent(gameObject.GetComponent<RectTransform>());
            ball.GetComponent<RectTransform>().localPosition = new Vector2(Random.Range(0, xRange) * Random.Range(-1f, 1f), y);
            balls.Add(ball);
        }
	}
}
