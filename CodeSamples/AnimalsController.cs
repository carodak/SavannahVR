using UnityEngine;
using System.Collections;

/// <summary>
/// The main controller class for the animals movements.
/// </summary>
/// <remarks>
/// <para>It is a sample without all methods and attributes but the original class contains all methods and attributes for making the animals move randomly.</para>
/// <para>The original class have methods to choose random paths, perform obstacle avoidance and make animals walk or idle.</para>
/// </remarks>
public class AnimalsController : MonoBehaviour
{
	/// <summary>
	/// A boolean that is true if the animal died.
	/// </summary>
	public bool _dead;

	/// <summary>
	/// A boolean that is true if the animal is idling.
	/// </summary>
	public bool _idle;

	/// <summary>
	/// An int that is equal to 0 when the animal is idling and 1 when it is walking.
	/// </summary>
	public int _mode;

	/// <summary>
	/// A float representing the current speed of the animal.
	/// </summary>
	public float _speed;

	/// <summary>
	/// The current gameObject transform with its required animations.
	/// </summary>
	public Transform _model;

	/// <summary>
	/// Handle the speed of the animal and the speed of its animation according to the choosen mode (walking, idling, sleeping)
	/// </summary>
	/// <returns>
	/// Void (set)
	/// </returns>
	public void AnimationHandler()
	{
		if (!_dead)
		{
			if (_mode == 1)
			{ //if walking
				if (gameObject.tag == "Gazelle" || gameObject.tag == "Antelope" || gameObject.tag == "Elephant" || gameObject.tag == "Zebra")
				{
					StartWalking();

				}
				_idle = false;
			}

			//The animal will idle or sleep

			else if (gameObject.tag == "Gazelle")
			{
				float maxSpeed = 1.5f;
				StartIdlingorSleeping(maxSpeed);
			}

			else if (gameObject.tag == "Antelope" || gameObject.tag == "Elephant")
			{
				float maxSpeed = 0.5f;
				StartIdlingorSleeping(maxSpeed);
			}

			else if (gameObject.tag == "Zebra")
			{
				float maxSpeed = 3f;
				StartIdlingorSleeping(maxSpeed);
			}
		}
	}

	/// <summary>
	/// This method makes the animal idle or sleep.
	/// </summary>
	/// <param name="maxSpeed">A float used to check if the animal's speed is inferior to maxSpeed.</param>
	private void StartIdlingorSleeping(float maxSpeed)
	{
		if (!_idle && _speed < maxSpeed)
		{
			StartIdling();
		}
		if (_idle && _sleepCounter > _idleToSleepSeconds)
		{
			StartSleeping();
		}
		else
			_sleepCounter += _newDelta;
	}

	/// <summary>
	/// This method makes the animal sleep.
	/// </summary>
	/// <remarks>
	/// It sets the sleep animation and decrease the animal's speed.
	/// </remarks>
	public void StartSleeping()
	{
		_model.GetComponent<Animation>().CrossFade(_animSleep, 1.0f);
		_speed = 0f;
	}

	/// <summary>
	/// This method makes the animal idle.
	/// </summary>
	/// <remarks>
	/// It stops the walking animation, sets idling animation and decreases the animal's speed
	/// </remarks>
	public void StartIdling()
	{
		// Debug.Log("Speed shoud be 0");
		_sleepCounter = 0.0f;
		_model.GetComponent<Animation>().CrossFade(_animWalk, 0f);
		_model.GetComponent<Animation>().CrossFade(_animIdle, 1.0f);
		_speed = 0f; //added to fix stopping issue
		_idle = true;
	}

	/// <summary>
	/// This method makes the animal walk.
	/// </summary>
	/// <remarks>
	/// It starts the walking animation, sets the speed of the animation and the speed of the animal.
	/// </remarks>
	public void StartWalking()
	{
		if (!_model.GetComponent<Animation>().IsPlaying(_animWalk))
			_model.GetComponent<Animation>().CrossFade(_animWalk, 1.5f);
		_speed = _walkSpeed;
		_model.GetComponent<Animation>()[_animWalk].speed = _animWalkSpeed;
	}
}
