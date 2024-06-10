using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPlayerDie : MonoBehaviour
{
    public GameObject _gameObject;
    public TextMeshProUGUI DeathText;

    public void DeathScreen()
    {
        _gameObject.SetActive(true);

        for (float i = 0f; i <= 1;)
        {
            i += 0.1f;
            DeathText.color += new Color(i, i, i);

            if (i == 0.4)
            {
                break;
            }
        }
    }

    public void GoToMain()
    {
        SceneManager.LoadScene(0);
    }
}
