using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaFeature : MonoBehaviour
{
    public UICondition uiCondition;
    Condition temperature { get { return uiCondition.temperature; } }
    public float temperatureDecay;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (this.gameObject.CompareTag("Cold"))
            {
                temperature.Subtract(temperatureDecay);
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (this.gameObject.CompareTag("Cold"))
            {
                temperature.Add(temperatureDecay);
            }
        }
    }
}
