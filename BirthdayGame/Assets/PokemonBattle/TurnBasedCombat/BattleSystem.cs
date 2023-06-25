using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PokemonBattle.TurnBasedCombat;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, CHANGING }

public class BattleSystem : MonoBehaviour
{
	public List<GameObject> Buttons;
	public List<GameObject> Pokemons;

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	public Transform playerBattleStation;
	public Transform enemyBattleStation;

	public Animator PlayerAnimator;
	public Animator EnemyAnimator;

	public GameObject BattleCam;
	public GameObject PlayerCam;
	public GameObject EnemyCam;
	
	Unit playerUnit;
	Unit enemyUnit;

	public Text dialogueText;

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	public BattleState state;

	public Text PlayerButton1Text;
	public Text PlayerButton2Text;
	public Text PlayerButton3Text;
	public Text PlayerButton4Text;
	

    // Start is called before the first frame update
    void Start()
    {
		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }

	IEnumerator SetupBattle()
	{
		DeactivateButtons();
		GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<Unit>();
		PlayerAnimator = playerGO.GetComponentInChildren<Animator>();

		GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<Unit>();
		EnemyAnimator = enemyGO.GetComponentInChildren<Animator>();

		SetupButtons();

		dialogueText.text = "Противник вызывает " + enemyUnit.unitName + ".";

		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(2f);
		ActivateBattleCam();

		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	public IEnumerator ChangePokemon(Unit pokemon, bool isPlayer)
	{
		state = BattleState.CHANGING;
		if (isPlayer)
		{
			ActivatePlayerCam();
			dialogueText.text = "Эмиль вызывает " + pokemon.unitName + ".";
			var unitGo = playerUnit.gameObject;
			unitGo.transform.DOScale(Vector3.zero, 1f).OnComplete(() =>
			{
				GameObject playerGO = Instantiate(pokemon.gameObject, playerBattleStation);
				playerUnit = playerGO.GetComponent<Unit>();
				PlayerAnimator = playerGO.GetComponentInChildren<Animator>();
				playerHUD.SetHUD(playerUnit);
				SetupButtons();
			});
		}
		else
		{
			ActivateEnemyCam();
			dialogueText.text = "Противник вызывает " + pokemon.unitName + ".";
			var unitGo = enemyUnit.gameObject;
			unitGo.transform.DOScale(Vector3.zero, 1f).OnComplete(() =>
			{
				GameObject enemyGO = Instantiate(pokemon.gameObject, playerBattleStation);
				enemyUnit = enemyGO.GetComponent<Unit>();
				EnemyAnimator = enemyGO.GetComponentInChildren<Animator>();
				enemyHUD.SetHUD(enemyUnit);
			});
		}
		
		yield return new WaitForSeconds(2f);
		ActivateBattleCam();
		
		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	public void SetupButtons()
	{
		PlayerButton1Text.text = playerUnit.Move1.Name;
		PlayerButton2Text.text = playerUnit.Move2.Name;
		PlayerButton3Text.text = playerUnit.Move3.Name;
		PlayerButton4Text.text = playerUnit.Move4.Name;
	}

	public void ActivatePlayerCam()
	{
		BattleCam.SetActive(false);	
		EnemyCam.SetActive(false);	
		PlayerCam.SetActive(true);	
	}
	
	public void ActivateEnemyCam()
	{
		BattleCam.SetActive(false);	
		PlayerCam.SetActive(false);	
		EnemyCam.SetActive(true);	
	}
	
	public void ActivateBattleCam()
	{
		EnemyCam.SetActive(false);
		PlayerCam.SetActive(false);
		BattleCam.SetActive(true);			
	}



	IEnumerator PlayerAttack(BattleMove move)
	{
		DeactivateButtons();
		dialogueText.text = $"{playerUnit.unitName} использует {move.Name}";
		ActivatePlayerCam();
		yield return new WaitForSeconds(1f);
		PlayerAnimator.SetTrigger(GetAnimation(move.Animation));
		// Wait for the transition to end
		yield return new WaitUntil(() => PlayerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f );
		yield return new WaitWhile(() => PlayerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
		
		ActivateEnemyCam();
		yield return new WaitForSeconds(1f);
		bool isDead = enemyUnit.TakeDamage(move.Damage);
		enemyHUD.SetHP(enemyUnit.currentHP);
		if (!isDead)
			EnemyAnimator.SetTrigger("Hit");
		else
			EnemyAnimator.SetTrigger("Die");

		yield return new WaitUntil(() => EnemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f );
		yield return new WaitWhile(() => EnemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
		
		ActivateBattleCam();
		yield return new WaitForSeconds(1f);
		if(isDead)
		{
			state = BattleState.WON;
			EndBattle();
		} else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	private string GetAnimation(BattleMove.AnimationName moveAnimation)
	{
		switch (moveAnimation)
		{
			case BattleMove.AnimationName.Attack:
				return "Attack";
			case BattleMove.AnimationName.Attack1:
				return "Attack2";
		}

		return "";
	}

	IEnumerator EnemyTurn()
	{
		var move = GetRandomMove(enemyUnit);
		dialogueText.text = $"{enemyUnit.unitName} использует {move.Name}";
		EnemyAnimator.SetTrigger(GetAnimation(move.Animation));
		// Wait for the transition to end
		yield return new WaitUntil(() => EnemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f );
		bool isDead = playerUnit.TakeDamage(move.Damage);
		playerHUD.SetHP(playerUnit.currentHP);
		if (!isDead)
			PlayerAnimator.SetTrigger("Hit");
		else
			PlayerAnimator.SetTrigger("Die");
		
		yield return new WaitWhile(() => EnemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
		
		if(isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		} else
		{
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}
	}

	private BattleMove GetRandomMove(Unit unit)
	{
		var move = Random.Range(1, 5);
		switch (move)
		{
			case 1:
				return unit.Move1;
			case 2:
				return unit.Move2;
			case 3:
				return unit.Move3;
			case 4:
				return unit.Move4;
		}

		return unit.Move1;
	}

	void EndBattle()
	{
		if(state == BattleState.WON)
		{
			dialogueText.text = $"{enemyUnit.unitName} не может продолжать бой";
		} else if (state == BattleState.LOST)
		{
			dialogueText.text =  $"{playerUnit.unitName} не может продолжать бой";
		}
	}

	void PlayerTurn()
	{
		dialogueText.text = "Выбери действие:";
		ActivateButtons();
	}

	private void ActivateButtons()
	{
		foreach (var button in Buttons)
		{
			button.SetActive(true);
		}
	}

	private void DeactivateButtons()
	{
		foreach (var button in Buttons)
		{
			button.SetActive(false);
		}
	}

	public void OnAttackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerAttack(playerUnit.Move1));
	}
	
	public void OnAttack2Button()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerAttack(playerUnit.Move2));
	}
	
	public void OnAttack3Button()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerAttack(playerUnit.Move3));
	}
	
	public void OnAttack4Button()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerAttack(playerUnit.Move4));
	}
}
