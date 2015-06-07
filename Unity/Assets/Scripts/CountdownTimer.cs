using UnityEngine;

public class CountdownTimer {

	public bool done { get; private set; }
	public float timeRemaining { get; private set; }

	public CountdownTimer() {

		done = true;
		timeRemaining = 0.0f;
	}

	public void Start(float timeToCountDown) {

		done = false;
		timeRemaining = timeToCountDown;
	}

	public void Update(float timeElapsed) {

		timeRemaining -= timeElapsed;

		if (timeRemaining < 0.0f) {
			done = true;
		}
	}
}
