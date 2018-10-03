using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHeath : MonoBehaviour {

    RawImage healBarImage;
    Player player;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
        healBarImage = GetComponent<RawImage>();

    }
	
	// Update is called once per frame
	void Update () {
        float xValue = (player.healthAsPercentage / 2f) - 0.5f;
        healBarImage.uvRect = new Rect(xValue,0f,0.5f,1f);
		
	}
}
