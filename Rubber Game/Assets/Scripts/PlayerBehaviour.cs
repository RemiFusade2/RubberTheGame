using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour 
{	
	private float currentSpeed;
	public float maxSpeed;
	public float acceleration;	
	public float deceleration;	
	private float currentSpeedBoost;
	public float maxSpeedBoost;

	public bool accelerate;

	public float timeToChangeLane;

	private bool playerIsMoving;
	
	public List<float> lanesPositions;
	private int playerLaneIndex;

	public GameObject HPGaugeGreen;

	public float maxHP;
	public float currentHP;
	public float HPRestoration;

	public GameObject InGameUIPanel;

	public Text scoreText;
	public int score;

	public CameraBehaviour currentCamera;

	public Animator rubberAnimator;

	public ParticleSystem particlesEmitter;

	public LevelBuilderScript levelBuilder;

	public bool gameIsEnding;

	// Use this for initialization
	void Start () 
	{
		playerLaneIndex = lanesPositions.Count / 2;
		playerIsMoving = false;
		currentSpeed = 0;
		currentHP = maxHP;
		score = 0;
		accelerate = false;
		StartCoroutine (WaitAndMoveAgain (5.0f));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (accelerate)
		{
			currentSpeed += acceleration;
			//currentSpeed = (currentSpeed > maxSpeed) ? maxSpeed : currentSpeed;
		}
		else
		{
			currentSpeed -= deceleration;
			currentSpeed = (currentSpeed < 0) ? 0 : currentSpeed;
		}

		if (currentSpeed > maxSpeed)
		{
			currentSpeed -= acceleration;
		}


		if (Input.GetKeyDown(KeyCode.Space))
		{
			currentSpeedBoost = maxSpeedBoost;
		}
		else
		{
			currentSpeedBoost -= deceleration;
			currentSpeedBoost = (currentSpeedBoost < 0) ? 0 : currentSpeedBoost;
		}

		
		this.transform.localPosition += (currentSpeed+currentSpeedBoost) * Vector3.right * Time.deltaTime;

		
		if (!playerIsMoving && !gameIsEnding)
		{
			if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("Vertical") < -0.3f) && playerLaneIndex > 0)
			{
				playerLaneIndex--;
				playerIsMoving = true;
				float stepCount = 100;
				rubberAnimator.SetTrigger("ChangeLaneRight");
				
				float deltaPosition = lanesPositions[playerLaneIndex+1] - lanesPositions[playerLaneIndex];
				
				for (int i = 0 ; i < stepCount ; i++)
				{
					StartCoroutine(WaitAndMovePlayer(timeToChangeLane*(i/stepCount), -(deltaPosition/stepCount)));
				}
				StartCoroutine(WaitAndUnlockRubberMove(timeToChangeLane));
			}		
			if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("Vertical") > 0.3f) && playerLaneIndex < (lanesPositions.Count-1))
			{
				playerLaneIndex++;
				playerIsMoving = true;
				float stepCount = 100;
				rubberAnimator.SetTrigger("ChangeLaneLeft");
				
				float deltaPosition = lanesPositions[playerLaneIndex-1] - lanesPositions[playerLaneIndex];
				
				for (int i = 0 ; i < stepCount ; i++)
				{
					StartCoroutine(WaitAndMovePlayer(timeToChangeLane*(i/stepCount), -(deltaPosition/stepCount)));
				}
				StartCoroutine(WaitAndUnlockRubberMove(timeToChangeLane));
			}
		}

		RestoreHP (HPRestoration);
		UpdateHPGauge ();
		UpdateParticleSystem ();
	}

	private void UpdateParticleSystem()
	{
		float particlesPower = currentSpeed / maxSpeed;
		particlesPower = (particlesPower > 1) ? 1 : particlesPower;
		if (rubberIsDead)
		{
			particlesPower = 0;
		}
		particlesEmitter.emissionRate = 100*particlesPower;
		particlesEmitter.startSize = 2*particlesPower;
	}
	
	IEnumerator WaitAndMovePlayer(float timer, float delta)
	{
		yield return new WaitForSeconds(timer);
		this.transform.localPosition += delta * Vector3.forward;
	}
	
	IEnumerator WaitAndUnlockRubberMove(float timer)
	{
		yield return new WaitForSeconds(timer);
		playerIsMoving = false;
	}

	public void HitPlayer(float hpHit, float speedHit)
	{
		currentHP -= hpHit;
		currentSpeed -= speedHit;
		currentSpeed = (currentSpeed < 0.01f) ? 0.01f : currentSpeed;
		rubberAnimator.SetTrigger("Hit");
		currentCamera.Shake (0.5f,0.01f,0);

		if (currentHP <= 0 && !gameIsEnding)
		{
			gameIsEnding = true;
			HPRestoration = 0;
			levelBuilder.AskForEndGame();

			InGameUIPanel.SetActive(false);
			maxSpeed = 3;

			if (playerLaneIndex > 1)
			{
				playerLaneIndex--;
				playerIsMoving = true;
				float stepCount = 100;
				rubberAnimator.SetTrigger("ChangeLaneRight");
				
				float deltaPosition = lanesPositions[playerLaneIndex+1] - lanesPositions[playerLaneIndex];
				
				for (int i = 0 ; i < stepCount ; i++)
				{
					StartCoroutine(WaitAndMovePlayer(timeToChangeLane*(i/stepCount), -(deltaPosition/stepCount)));
				}
			}
			if (playerLaneIndex < 1)
			{
				playerLaneIndex++;
				playerIsMoving = true;
				float stepCount = 100;
				rubberAnimator.SetTrigger("ChangeLaneLeft");
				
				float deltaPosition = lanesPositions[playerLaneIndex-1] - lanesPositions[playerLaneIndex];
				
				for (int i = 0 ; i < stepCount ; i++)
				{
					StartCoroutine(WaitAndMovePlayer(timeToChangeLane*(i/stepCount), -(deltaPosition/stepCount)));
				}
			}
		}
	}

	public void RestoreHP(float hpRestore)
	{
		currentHP += hpRestore;
		currentHP = (currentHP > maxHP) ? maxHP : currentHP;
	}

	public void IncreaseScore(int scoreUp)
	{
		score += scoreUp;
		if (!gameIsEnding)
		{
			maxSpeed += 0.1f;
		}
		scoreText.text = "Score : " + score;
	}

	public void UpdateHPGauge()
	{
		float ratio = (maxHP - currentHP) / maxHP;
		HPGaugeGreen.GetComponent<RectTransform> ().SetInsetAndSizeFromParentEdge (RectTransform.Edge.Right, 200 * ratio, 200 - 200 * ratio);
	}

	public void SlowDownAndUseTelekinesisOnRabbit(GameObject rabbit)
	{
		accelerate = false;
		rubberAnimator.SetTrigger ("Telekinesis");
		StartCoroutine(WaitAndBlowUpRabbit(9.1f, rabbit));
		StartCoroutine(WaitAndMoveAgain(10.1f));
		StartCoroutine(WaitAndShakeCamera(1.0f, 8.6f, 0f, 0.00005f));
		this.GetComponent<AudioSource> ().Play ();
	}
	
	IEnumerator WaitAndBlowUpRabbit(float timer, GameObject rabbit)
	{
		yield return new WaitForSeconds(timer);
		rabbit.GetComponent<LapinBehaviour> ().Explode ();
	}
	
	IEnumerator WaitAndMoveAgain(float timer)
	{
		yield return new WaitForSeconds(timer);
		accelerate = true;
	}
		
	IEnumerator WaitAndShakeCamera(float timer, float shakingTime, float shakingStartForce, float shakingAcceleration)
	{
		yield return new WaitForSeconds(timer);
		currentCamera.Shake (shakingTime,shakingStartForce,shakingAcceleration);
	}
	
	public void StopPlayer()
	{
		accelerate = false;
	}
	public void StopPlayerSlowly()
	{
		deceleration = 0.014f;
		accelerate = false;
	}
	public void MovePlayer()
	{
		accelerate = true;
	}

	private bool rubberIsDead;

	public void KillRubber()
	{
		rubberAnimator.SetTrigger ("Dies");
		rubberIsDead = true;
	}

}
