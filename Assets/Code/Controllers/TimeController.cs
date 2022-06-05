using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Time = UnityEngine.Time;

public class TimeController : MonoBehaviour {

	public TMP_Text Clock;
	private float _timer = 0f;
	private int _hours = 9;
	private int _minutes = 0;
	private bool _clockTick = true;

    void Start() {
	    Clock.text = string.Format("{0:00}:{1:00}", _hours, _minutes);
    }

    void Update() {
	    _timer += Time.deltaTime;
	    UpdateClock();
    }

    private void UpdateClock() {
	    if (Mathf.FloorToInt(_timer % 10) == 0) {
		    if (!_clockTick) {
				_minutes += 15;
				if (_minutes == 60) {
					_minutes = 0;
					_hours++;
					if (_hours == 5) {
						Clock.color = Color.red;
					}
					if (_hours == 13) {
						_hours = 1;
					}
				}
			}
		    _clockTick = true;
	    }
	    else {
		    _clockTick = false;
	    }
	    Clock.text = string.Format("{0:00}:{1:00}", _hours, _minutes);
    }

	public float totalTime()
    {
		return _timer / 60f;
    }

}
