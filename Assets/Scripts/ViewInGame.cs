using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ViewInGame : MonoBehaviour {

    public Text scoreLabel;
    public Text highScoreLabel;
	// Update is called once per frame
	void Update () {
	    if(GameManager.instance.currentGameState == GameState.inGame)
        {
            scoreLabel.text = "Score: \n" + GameManager.instance.score;
            highScoreLabel.text = "High Score: \n" + PlayerPrefs.GetInt("HighScore").ToString("0000");
        }
	}
}
