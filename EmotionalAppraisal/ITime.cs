namespace EmotionalAppraisal
{
	/// <summary>
	/// Interface for the simulation time experienced
	/// by the agent. It allows the agent to freeze, advance, and speed up time.
	/// The concrete class that implements this interface cannot be instantiated.
	/// If you want to access it, use the AgentTime property inside an IAgentModel instance.
	/// </summary>
	public interface ITime
	{
		/// <summary>
		/// The real time, in seconds, since the agent was created (Read Only).
		/// </summary>
		double RealtimeSinceCreation { get; }

		/// <summary>
		/// The time at the beginning of this frame (Read Only).
		/// This is the time in seconds since the agent was created.
		/// </summary>
		double Time { get; }

		/// <summary>
		/// The real time, in seconds, it took to complete the last frame (Read Only).
		/// </summary>
		float DeltaTime { get; }

		/// <summary>
		/// The total number of frames that have passed (Read Only).
		/// </summary>
		ulong FrameCount { get; }

		/// <summary>
		/// The simulated time, in milli seconds, passed since the agent's creation.
		/// This time can be changed to reflect the agent's notion of passing time.
		/// </summary>
		long SimulatedTime { get; set; }

		/// <summary>
		/// The simulated time, in seconds, it took to complete the last frame (Read Only).
		/// This time is subjected to time scaling.
		/// </summary>
		float SimulatedDeltaTime { get; }

		/// <summary>
		/// The time scale used by the simulated time.
		/// If TimeScale is 1, the simulated time advances at the same speed as real time.
		/// If it's 0.5, the time 2x slower that the real time.
		/// If TimeScale is set to 0, the agent's simulated time stop.
		/// The agent's mind are still updated, but process that depend on the agent's time will stop.
		/// Negative values are handled as 0
		/// </summary>
		float TimeScale { get; set; }

		/// <summary>
		/// Advances the agent's simulation time.
		/// Very useful if you want to skip time. 
		/// </summary>
		/// <param name="seconds">the number of seconds you want to advance in time</param>
		void AdvanceTime(uint seconds);

		/// <summary>
		/// Speeds up the agent simulation time in relation to real time
		/// </summary>
		/// <param name="speed">
		/// how many times faster should the simulation time run in relation to real time.
		/// For instance, if speed is 7, one second of real time will seem like 7 seconds
		/// to the agent.
		/// The value provided must be greater than 1</param>
		void SpeedUpTime(int speed);

		/// <summary>
		/// Slows down the agent simulation time in relation to real time
		/// </summary>
		/// <param name="speed">
		/// how many times slower should the simulation time run in relation to real time.
		/// For instance, if speed is 5, 10 second of real time will correspond to only 2
		/// for the agent.
		/// The value provided must be greater than 1
		/// </param>
		void SpeedDownTime(int speed);

		/* TODO Extras
		 * 
		 * smoothDeltaTime - does it make sense? It can be used to limit/smooth out DeltaTime "hiccups" due to machine performance
		 * timeSinceLoad - the time since the agent was loaded/created. This may differ from RealtimeSinceCreation if the agent was saved into file, and reloaded in another time.
		 */

		/*
		captureFramerate	Slows game playback time to allow screenshots to be saved between frames.
		fixedDeltaTime	The interval in seconds at which physics and other fixed frame rate updates (like MonoBehaviour's FixedUpdate) are performed.
		fixedTime	The time the latest FixedUpdate has started (Read Only). This is the time in seconds since the start of the game.
		maximumDeltaTime	The maximum time a frame can take. Physics and other fixed frame rate updates (like MonoBehaviour's FixedUpdate).
		unscaledDeltaTime	The timeScale-independent time in seconds it took to complete the last frame (Read Only).
		unscaledTime	The timeScale-independant time at the beginning of this frame (Read Only). This is the time in seconds since the start of the game.
		 */
	}
}
