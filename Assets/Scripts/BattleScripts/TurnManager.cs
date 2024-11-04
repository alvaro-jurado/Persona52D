using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.Examples;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    public Character player;
    public Character enemy;
    public GameObject arsenePrefab;

    private bool isPlayerTurn = true;
    private bool isEnemyTurnActive = false;

    public Slider playerHealthBar;
    public Slider enemyHealthBar;
    public Slider playerManaBar;

    public TMP_Text playerHealthText;
    public TMP_Text playerManaText;
    public TMP_Text enemyHealthText;

    public TMP_Text combatLogText;
    public Animator animator;

    [SerializeField] SceneController sceneController;

    void Start()
    {
        Ability arsene = new Ability { name = "Arsene", damage = 20, manaCost = 18 };
        Ability swordAttack = new Ability { name = "Sword Attack", damage = 10, manaCost = 0 };
        Ability guard = new Ability { name = "Guard", damage = 0, manaCost = 0 };
        Ability heal = new Ability { name = "Heal", damage = 0, manaCost = 0 };

        player.abilities = new List<Ability> { arsene, swordAttack, guard, heal };

        Ability enemyAttack = new Ability { name = "Shining Arrows", damage = 15, manaCost = 0 };
        Ability enemyAttack2 = new Ability { name = "Blazing Hell", damage = 12, manaCost = 0 };
        Ability enemyAttack3 = new Ability { name = "Riot Gun", damage = 12, manaCost = 0 };
        Ability enemyAttack4 = new Ability { name = "Tempest Slash", damage = 10, manaCost = 0 };

        enemy.abilities = new List<Ability> { enemyAttack, enemyAttack2, enemyAttack3, enemyAttack4 };

        playerHealthBar.maxValue = player.maxHealth;
        playerHealthBar.value = player.health;

        enemyHealthBar.maxValue = enemy.maxHealth;
        enemyHealthBar.value = enemy.health;

        playerManaBar.maxValue = player.maxMana;
        playerManaBar.value = player.mana;

        playerHealthText.text = player.health.ToString();
        playerManaText.text = player.mana.ToString();
        enemyHealthText.text = enemy.health.ToString();

        combatLogText.text = "¡El combate comienza!";
        isPlayerTurn = true;
        isEnemyTurnActive = false;
    }

    public void UpdateCombatLog(string message)
    {
        combatLogText.text = message;
    }

    IEnumerator EnemyTurn()
    {
        if (isEnemyTurnActive) yield break;

        isEnemyTurnActive = true;

        yield return new WaitForSeconds(1f);

        Ability chosenAbility = enemy.abilities[Random.Range(0, enemy.abilities.Count)];
        enemy.Attack(player, chosenAbility);
        UpdateCombatLog("El enemigo usa " + chosenAbility.name + ".");

        UpdateHealthAndMana();
        CheckEndCombat();
        ChangeTurn();
    }

    public void OnAttackButtonClicked(int abilityIndex)
    {
        if (isPlayerTurn)
        {
            Ability selectedAbility = player.abilities[abilityIndex];

            if (player.mana >= selectedAbility.manaCost)
            {
                player.UseMana(selectedAbility.manaCost);

                if (selectedAbility.name == "Guard")
                {
                    player.isGuarding = true;
                    UpdateCombatLog("El jugador se pone en guardia.");
                    animator.SetBool("isGuarding", true);
                }
                else if (selectedAbility.name == "Arsene")
                {
                    StartCoroutine(ArseneAttack());
                    UpdateCombatLog("El jugador invoca a Arsene.");
                }
                else if (selectedAbility.name == "Heal")
                {
                    int healAmount = 20;
                    player.Heal(healAmount);
                    UpdateCombatLog("El jugador usa Heal y recupera " + healAmount + " puntos de salud.");
                }
                else
                {
                    player.Attack(enemy, selectedAbility);
                    UpdateCombatLog("El jugador usa " + selectedAbility.name + ".");
                    animator.SetBool("isAttacking", true);
                }

                UpdateHealthAndMana();
                CheckEndCombat();
                
                if (selectedAbility.name == "Sword Attack")
                Invoke("isAttacking", 0.5f);

                if (selectedAbility.name == "Guard")
                Invoke("isGuarding", 0.6f);

                ChangeTurn();
            }
            else
            {
                UpdateCombatLog("No tienes suficiente maná para usar esta habilidad");
            }
        }
    }

    void isAttacking()
    {
        animator.SetBool("isAttacking", false);
    }
    void isGuarding()
    {
        animator.SetBool("isGuarding", false);
    }
    void isDead()
    {
        animator.SetBool("isDead", false);
    }

    IEnumerator ArseneAttack()
    {
        Vector3 fixedPosition = new Vector3(-6.27f, 0.24f, 0);
        GameObject arseneInstance = Instantiate(arsenePrefab, fixedPosition, Quaternion.identity);

        arseneInstance.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        enemy.TakeDamage(20);

        Destroy(arseneInstance);

        UpdateHealthAndMana();
        CheckEndCombat();
    }

    public void UpdateHealthAndMana()
    {
        playerHealthBar.value = player.health;
        enemyHealthBar.value = enemy.health;

        playerManaBar.value = player.mana;

        playerHealthText.text = player.health.ToString();
        playerManaText.text = player.mana.ToString();
        enemyHealthText.text = enemy.health.ToString();
    }

    void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;

        if (!isPlayerTurn)
        {
            isEnemyTurnActive = false;
            StartCoroutine(EnemyTurn());
        }
    }

    void CheckEndCombat()
    {
        if (enemy.health <= 0)
        {
            sceneController.LoadScene("WinScene");
        }
        else if (player.health <= 0)
        {
            animator.SetBool("isDead", true);
            Invoke("isDead", 0.9f);
            SceneTracker.previousScene = SceneManager.GetActiveScene().name;
            sceneController.LoadScene("GameOverScene");
        }
    }
}
