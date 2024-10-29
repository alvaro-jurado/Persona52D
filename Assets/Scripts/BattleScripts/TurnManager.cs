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
    private bool isEnemyTurnActive = false; // Nuevo indicador para el turno del enemigo

    public Slider playerHealthBar; // Referencia a la barra de vida del jugador
    public Slider enemyHealthBar;  // Referencia a la barra de vida del enemigo
    public Slider playerManaBar;   // Referencia a la barra de maná del jugador 

    public TMP_Text playerHealthText; // Texto para mostrar la vida del jugador
    public TMP_Text playerManaText;   // Texto para mostrar el maná del jugador
    public TMP_Text enemyHealthText;  // Texto para mostrar la vida del enemigo

    public TMP_Text combatLogText;
    public Animator animator;

    [SerializeField] SceneController sceneController;

    void Start()
    {
        // Crear habilidades del personaje
        Ability arsene = new Ability { name = "Arsene", damage = 20, manaCost = 18 };
        Ability swordAttack = new Ability { name = "Sword Attack", damage = 10, manaCost = 0 };
        Ability guard = new Ability { name = "Guard", damage = 0, manaCost = 0 };
        Ability heal = new Ability { name = "Heal", damage = 0, manaCost = 0 };

        // Asignar habilidades al personaje
        player.abilities = new List<Ability> { arsene, swordAttack, guard, heal };

        // Crear habilidades del enemigo
        Ability enemyAttack = new Ability { name = "Claw", damage = 15, manaCost = 0 };
        Ability enemyAttack2 = new Ability { name = "Punch", damage = 12, manaCost = 0 };
        Ability enemyAttack3 = new Ability { name = "Kick", damage = 12, manaCost = 0 };
        Ability enemyAttack4 = new Ability { name = "Shot", damage = 10, manaCost = 0 };

        // Asignar habilidades al enemigo
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
        isEnemyTurnActive = false; // Asegurarse de que el indicador esté en falso al inicio
    }

    public void UpdateCombatLog(string message)
    {
        combatLogText.text = message;
    }

    IEnumerator EnemyTurn()
    {
        if (isEnemyTurnActive) yield break; // Si el turno ya está activo, salir

        isEnemyTurnActive = true; // Marcar que el turno del enemigo ha comenzado

        yield return new WaitForSeconds(1f); // Esperar antes de actuar

        // Seleccionar una habilidad aleatoria del enemigo
        Ability chosenAbility = enemy.abilities[Random.Range(0, enemy.abilities.Count)];
        enemy.Attack(player, chosenAbility);
        UpdateCombatLog("El enemigo usa " + chosenAbility.name + ".");
        //Debug.Log("El enemigo usa " + chosenAbility.name + ".");

        UpdateHealthAndMana();
        CheckEndCombat();

        // Cambiar turno al jugador
        // Resetear el indicador de turno enemigo
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
                Invoke("isAttacking", 2.8f);

                // Cambiar turno al enemigo
                ChangeTurn();
            }
            else
            {
                //Debug.Log("No tienes suficiente maná para usar esta habilidad");
                UpdateCombatLog("No tienes suficiente maná para usar esta habilidad");
            }
        }
    }

    void isAttacking()
    {
        animator.SetBool("isAttacking", false);
    }

    IEnumerator ArseneAttack()
    {
        // Activar Arsene
        Vector3 fixedPosition = new Vector3(-6.27f, 0.24f, 0); // Cambia estas coordenadas según necesites
        GameObject arseneInstance = Instantiate(arsenePrefab, fixedPosition, Quaternion.identity);

        //GameObject arseneInstance = Instantiate(arsenePrefab, transform.position, Quaternion.identity);
        arseneInstance.SetActive(true);

        // Esperar 2 segundos mientras hace la animación
        yield return new WaitForSeconds(1.5f);

        // Realizar el ataque de Arsene
        enemy.TakeDamage(20); // Daño de Arsene

        // Desactivar Arsene
        Destroy(arseneInstance);

        UpdateHealthAndMana();
        CheckEndCombat();

        // Cambiar turno al enemigo
        //ChangeTurn();
    }

    public void UpdateHealthAndMana()
    {
        // Actualizar barra de vida
        playerHealthBar.value = player.health;
        enemyHealthBar.value = enemy.health;

        // Si tienes maná, actualizar barra de maná
        playerManaBar.value = player.mana;

        playerHealthText.text = player.health.ToString();
        playerManaText.text = player.mana.ToString();
        enemyHealthText.text = enemy.health.ToString();
    }

    void ChangeTurn()
    {
        Debug.Log(isEnemyTurnActive);
        isPlayerTurn = !isPlayerTurn; // Cambiar el turno

        if (!isPlayerTurn)
        {
            // Iniciar el turno del enemigo si es su turno
            isEnemyTurnActive = false;
            StartCoroutine(EnemyTurn());
        }
    }

    void CheckEndCombat()
    {
        if (enemy.health <= 0)
        {
            // El jugador gana, vuelve a la escena principal
            sceneController.LoadScene("WinScene");
        }
        else if (player.health <= 0)
        {
            // El jugador pierde, mostrar la pantalla de derrota
            SceneTracker.previousScene = SceneManager.GetActiveScene().name;
            sceneController.LoadScene("GameOverScene");
        }
    }
}
