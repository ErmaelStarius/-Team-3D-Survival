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
        // temperatureIcon = GetComponent<Image>(); �̹��� ���� �۾� ��
    }

    private void Update()
    {
        stamina.Add(stamina.regenRate * Time.deltaTime);
        hunger.Subtract(hunger.decayRate * Time.deltaTime);
        thirst.Subtract(thirst.decayRate * Time.deltaTime);

        //hunger�� 0���� �۾����� health�� ����
        if (hunger.curValue == 0.0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        //thirst�� 0���� �۾����� health�� ����
        if (thirst.curValue == 0.0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        //temperature�� 20 �̸��̰ų� 80 �ʰ��� health�� ������ ����
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
            // temperature.icon = �̹��� ���� �۾� ��
        }

        //health�� 0���� �۾����� ����
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

    public void Drink(float amount)
    {
        thirst.Add(amount);
    }

    public void Power(float amount)
    {
        stamina.Add(amount);
    }

    public void Die()
    {
        Debug.Log("������");
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
