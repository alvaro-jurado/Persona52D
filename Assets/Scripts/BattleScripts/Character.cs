using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int health;
    public int maxHealth;
    public int mana;
    public int maxMana;
    public int attackPower;
    public bool isGuarding = false; // Indica si el personaje está en guardia
    private TurnManager turnManager;

    // Para almacenar habilidades
    public List<Ability> abilities;

    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>(); // Buscar referencia al TurnManager
    }

    // Propiedad que indica si el personaje está vivo
    public bool isAlive
    {
        get { return health > 0; }
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        if (isGuarding)
        {
            damage = 0; // Reducir daño a la mitad cuando está en guardia-
            isGuarding = false; // Guardar solo dura un turno
        } else
        {
            health -= damage;
        }

        if (health <= 0)
        {
            health = 0;
            // Implementar lógica de derrota
        }

        turnManager.UpdateHealthAndMana();
    }

    public void Attack(Character target, Ability ability)
    {
        target.TakeDamage(ability.damage);
    }

    public void Heal(int healAmount)
    {
        health += healAmount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        // Actualizar la barra de vida en el HUD
        turnManager.UpdateHealthAndMana();
    }

    public void UseMana(int manaCost)
    {
        mana -= manaCost;

        if (mana < 0)
        {
            mana = 0;
        }

        // Actualizar la barra de maná en el HUD
        turnManager.UpdateHealthAndMana();
    }
}
