using UnityEngine;
using System.Collections;

public class Timer {

	// ALL-PURPOSE TIMER FOR UNITY (C#)

	/* example:
	 * 
	 * // creates a timer that will "ring" in 5 seconds (5f) and will not loop (false)
	 * Timer t = new Timer (5f, false);
	 * 
	 * // run timer every frame
	 * void Update () {
	 * 		// RunTimer() returns true if the correct amount of time has passed
	 * 		if (t.RunTimer())
	 * 			// and after 5 seconds (see above), "Done" will print once
	 * 			print "Done!";
	 * }
	 * 
	 * 
	 * 
	 */
	
	// This timer provides a random length of time between dMinimum and dMaximum, defaulting to a non-looping timer
	public Timer (float dMinimumLength, float dMaximumLength, bool toLoop = false) {
		minimumTimerLength = dMinimumLength;
		maximumTimerLength = dMaximumLength;
		useRandomTimerLengths = true;
		loop = toLoop;
	}

	// Provides a fixed-length timer, with looping turned off by default
	public Timer (float dFixedLength, bool toLoop = false) {
		fixedTimerLength = dFixedLength;
		useRandomTimerLengths = false;
		loop = toLoop;
	}

	// Provides a random-length timer with a set number of repetitions
	public Timer (float dMinimumLength, float dMaximumLength, int timesToLoop) {
		minimumTimerLength = dMinimumLength;
		maximumTimerLength = dMaximumLength;
		useRandomTimerLengths = true;
		loop = true;
		loopXTimes = true;
		loopNumber = timesToLoop;
	}

	// Provides a fixed-length timer with a set number of repetitions
	public Timer (float dFixedLength, int timesToLoop) {
		fixedTimerLength = dFixedLength;
		loop = true;
		useRandomTimerLengths = false;
		loopXTimes = true;
		loopNumber = timesToLoop;
	}

	// will the timer loop?
	bool loop = true;
	// will ther timer loop only a certain number of times?
	bool loopXTimes = false;
	// are we there yet?
	bool done = false;
	
	// for random timer lengths
	float minimumTimerLength;
	float maximumTimerLength;
	bool useRandomTimerLengths = true;
	
	// for fixed timer length
	float fixedTimerLength;
	
	// how many times to loop
	int loopNumber;
	int currentLoop = 1;
	
	// is the timer set?
	bool timerSet = false;
	
	// actual timer length
	float lastTrigger;
	float timerLength;
	
	// set or reset the timer
	void SetTimer () {
		// the frame this is called becomes our new zero
		lastTrigger = Time.realtimeSinceStartup;
		// use a fixed timer length or generate a random one
		if (!useRandomTimerLengths) 
			timerLength = fixedTimerLength;
		else
			timerLength = Random.Range (minimumTimerLength, maximumTimerLength);
		
		timerSet = true;
	}
	
	
	// this is the method to call in Update()
	public bool RunTimer () {
		if (!done) {
			// set/reset timer
			if (!timerSet) {
				SetTimer();
				return false;
			}

			// example "true" return:
			// lastTrigger = 5.0 (the timer was set 5 seconds after startup)
			// realTime = 8.0 (the game has been running for 8 seconds)
			// timerLength = 3.0 (the timer is supposed to go off after 3 seconds)
			// 8-5 >= 3, so RunTimer returns true

			// this only triggers if the timer is to "ring" this frame
			if (Time.realtimeSinceStartup >= lastTrigger + timerLength) {
				
				// exits loop
				if (!loop) {
					done = true;
				}
				else if (loopXTimes) {
					if (currentLoop < loopNumber) {
						currentLoop++;
					}
					else {
						done = true;
					}
				}

				// flags that the timer needs to be reset
				timerSet = false;
				return true;
			}
			// not enough time has gone by
			else
				return false;
		}
		// timer's done
		else 
			return false;
	}
}
