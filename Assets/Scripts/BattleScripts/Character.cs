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
    public bool isGuarding = false;
    private TurnManager turnManager;

    public List<Ability> abilities;

    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
    }

    public bool isAlive
    {
        get { return health > 0; }
    }

    public void TakeDamage(int damage)
    {
        if (isGuarding)
        {
            damage = 0;
            isGuarding = false;
        } else
        {
            health -= damage;
        }

        if (health <= 0)
        {
            health = 0;
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

        // Update Bars in HUD
        turnManager.UpdateHealthAndMana();
    }

    public void UseMana(int manaCost)
    {
        mana -= manaCost;

        if (mana < 0)
        {
            mana = 0;
        }

        turnManager.UpdateHealthAndMana();
    }
}
