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
    public float weather_time = 10f; // 날씨 바뀌는 간격
    public int next_weather; // 랜덤하게 다음 날씨 지정

    void Start()
    {
        currentWeather = Weather.SUNNY; // 시작은 맑은 날씨, 10초 뒤 변경
        next_weather = Random.Range(1, 3); // 다음 날씨는 무조건 눈이나 비
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
        // 매개변수로 받은 날씨가 현재 날씨와 같지 않다면 매개변수로 받은 날씨로 변경
    }

    void Update()
    {
        this.weather_time -= Time.deltaTime;  // 60초 동안은 그 날씨 유지

        if (this.weather_time <= 0)  // 현재 날씨의 제한시간이 끝나고
        {
            if (next_weather == 1)  // 다음 날씨가 '눈'이면
            {
                next_weather = Random.Range(0, 3);  // 다음 날씨 계산(0 - 맑음, 1 - 눈, 2 - 비)
                ChangeWeather(Weather.SNOW);  // 눈으로 바꿔줌
            }
            else if (next_weather == 2)  // 다음 날씨가 '비'이면
            {
                next_weather = Random.Range(0, 3);  // 다음 날씨 계산(0 - 맑음, 1 - 눈, 2 - 비)
                ChangeWeather(Weather.RAIN);  // 비로 바꿔줌
            }
            else  // 다음 날씨가 '맑음'이면
            {
                next_weather = Random.Range(0, 3);  // 다음 날씨 계산(0 - 맑음, 1 - 눈, 2 - 비)
                ChangeWeather(Weather.SUNNY);  // 맑음으로 바꿔줌
            }
            weather_time = 30f;
        }
    }
}
