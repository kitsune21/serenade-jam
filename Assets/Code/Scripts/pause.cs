using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class pause : MonoBehaviour {
	public static bool GamePaused = false;

    // Update is called once per frame
    void Update()
    {
	    if (Input.GetKeyDown(KeyCode.Escape)) {
		    if (GamePaused) {
			    Resume();
		    }
		    else {
			    Pause();
		    }
	    }
        
    }

    void Resume() {
	    Debug.Log("game resumed");
	    GamePaused = false;
	    Time.timeScale = 1f;
	}

    void Pause() {
		Debug.Log("Game paused");
	    Time.timeScale = 0f;
	    GamePaused = true;
    }
}
