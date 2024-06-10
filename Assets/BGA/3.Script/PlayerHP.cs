using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private Image imageScreen;
    [SerializeField]
    private float maxHP = 20;
    [SerializeField]
    private TextMeshProUGUI result;
    [SerializeField]
    private string SceneName;
    [SerializeField]
    private ResultSystem resultsystem;

    private float currentHP;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;
    public string sname => SceneName;

    private void Awake()
    {
        Time.timeScale = 1;
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        if(currentHP <= 0)
        {
            GameEnd(true);
            return;
        }
    }

    private IEnumerator HitAlphaAnimation()
    {
        Color color = imageScreen.color;
        color.a = 0.4f;
        imageScreen.color = color;

        while(color.a >= 0.0f){
            color.a -= Time.deltaTime;
            imageScreen.color = color;
            
            yield return null;
        }   
    }

    public void GameEnd(bool end)
    {
        StopCoroutine("HitAlphaAnimation");
        Color color = imageScreen.color;
        color.a = 0.0f;
        imageScreen.color = color;
        resultsystem.GameResult(!end);
        Time.timeScale = 0;
    }
}
