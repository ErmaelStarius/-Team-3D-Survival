using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public enum Weather { SUNNY, SNOW, RAIN };
    public Weather currentWeather;
    public ParticleSystem snow;
    public ParticleSystem rain;
    public GameObject snowCollider;
    public GameObject rainCollider;
    public float weather_time = 10f; // ���� �ٲ�� ����
    public int next_weather; // �����ϰ� ���� ���� ����

    void Start()
    {
        currentWeather = Weather.SUNNY; // ������ ���� ����, 10�� �� ����
        next_weather = Random.Range(1, 3); // ���� ������ ������ ���̳� ��
    }

    public void ChangeWeather(Weather weatherType)
    {
        if (weatherType != this.currentWeather)
        {
            switch (weatherType)
            {
                case Weather.SUNNY:
                    currentWeather = Weather.SUNNY;
                    snowCollider.SetActive(false);
                    rainCollider.SetActive(false);
                    this.snow.Stop();
                    this.rain.Stop();
                    break;
                case Weather.SNOW:
                    currentWeather = Weather.SNOW;
                    snowCollider.SetActive(true);
                    this.snow.Play();
                    rainCollider.SetActive(false);
                    this.rain.Stop();
                    break;
                case Weather.RAIN:
                    currentWeather = Weather.RAIN;
                    rainCollider.SetActive(true);
                    this.rain.Play();
                    snowCollider.SetActive(false);
                    this.snow.Stop();
                    break;
            }
        }
        // �Ű������� ���� ������ ���� ������ ���� �ʴٸ� �Ű������� ���� ������ ����
    }

    void Update()
    {
        this.weather_time -= Time.deltaTime;  // 60�� ������ �� ���� ����

        if (this.weather_time <= 0)  // ���� ������ ���ѽð��� ������
        {
            if (next_weather == 1)  // ���� ������ '��'�̸�
            {
                next_weather = Random.Range(0, 3);  // ���� ���� ���(0 - ����, 1 - ��, 2 - ��)
                ChangeWeather(Weather.SNOW);  // ������ �ٲ���
            }
            else if (next_weather == 2)  // ���� ������ '��'�̸�
            {
                next_weather = Random.Range(0, 3);  // ���� ���� ���(0 - ����, 1 - ��, 2 - ��)
                ChangeWeather(Weather.RAIN);  // ��� �ٲ���
            }
            else  // ���� ������ '����'�̸�
            {
                next_weather = Random.Range(0, 3);  // ���� ���� ���(0 - ����, 1 - ��, 2 - ��)
                ChangeWeather(Weather.SUNNY);  // �������� �ٲ���
            }
            weather_time = 30f;
        }
    }
}
