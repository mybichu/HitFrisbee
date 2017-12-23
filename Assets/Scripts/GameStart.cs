using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {
    private GameManager gameMan;

	void Start () {
        gameMan = GameObject.Find("UI").GetComponent<GameManager>();
	}
	
    void OnMouseDown()
    {
        gameMan.Reset();
        gameMan.ChangeGameState(GameManager.GameState.GAME);
    }
}
