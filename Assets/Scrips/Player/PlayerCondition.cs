using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition thirst { get { return uiCondition.thirst; } }
    Condition temperature { get { return uiCondition.temperature; } }

    public float noHungerHealthDecay;
    public event Action onTakeDamage;

    public Sprite temperatureNormal;
    public Sprite temperatureCold;
    public Sprite temperatureHot;

    private void Start()
    {
        // temperatureIcon = GetComponent<Image>(); 이미지 변경 작업 중
    }

    private void Update()
    {
        stamina.Add(stamina.regenRate * Time.deltaTime);
        hunger.Subtract(hunger.decayRate * Time.deltaTime);
        thirst.Subtract(thirst.decayRate * Time.deltaTime);

        //hunger가 0보다 작아지면 health를 깎음
        if (hunger.curValue == 0.0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        //thirst가 0보다 작아지면 health를 깎음
        if (thirst.curValue == 0.0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        //temperature가 20 미만이거나 80 초과면 health를 빠르게 깎음
        if (temperature.curValue < 20)
        {
            health.Subtract(noHungerHealthDecay * 5 * Time.deltaTime);
        }
        else if (temperature.curValue > 80)
        {
            health.Subtract(noHungerHealthDecay * 5 * Time.deltaTime);
        }
        else
        {
            // temperature.icon = 이미지 변경 작업 중
        }

        //health가 0보다 작아지면 죽음
        if (health.curValue == 0.0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public void Die()
    {
        Debug.Log("유다희");
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0f)
        {
            return false;
        }
        stamina.Subtract(amount);
        return true;
    }

    private IEnumerator OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Cold"))
        {
            for (int i = 0; i < 40; i++)
            {
                temperature.curValue -= 1;
                yield return new WaitForSeconds(.1f);
            }
        }

        if (collision.CompareTag("Hot"))
        {
            for (int i = 0; i < 40; i++)
            {
                temperature.curValue += 1;
                yield return new WaitForSeconds(.1f);
            }
        }

        if (collision.CompareTag("Cool"))
        {
            for (int i = 0; i < 15; i++)
            {
                temperature.curValue -= 1;
                yield return new WaitForSeconds(.1f);
            }
        }

        if (collision.CompareTag("Warm"))
        {
            for (int i = 0; i < 15; i++)
            {
                temperature.curValue += 1;
                yield return new WaitForSeconds(.1f);
            }
        }
    }

    private IEnumerator OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Cold"))
        {
            for (int i = 0; i < 40; i++)
            {
                temperature.curValue += 1;
                yield return new WaitForSeconds(.1f);
            }
        }

        if (collision.CompareTag("Hot"))
        {
            for (int i = 0; i < 40; i++)
            {
                temperature.curValue -= 1;
                yield return new WaitForSeconds(.1f);
            }
        }

        if (collision.CompareTag("Cool"))
        {
            for (int i = 0; i < 15; i++)
            {
                temperature.curValue += 1;
                yield return new WaitForSeconds(.1f);
            }
        }

        if (collision.CompareTag("Warm"))
        {
            for (int i = 0; i < 15; i++)
            {
                temperature.curValue -= 1;
                yield return new WaitForSeconds(.1f);
            }
        }
    }
}
