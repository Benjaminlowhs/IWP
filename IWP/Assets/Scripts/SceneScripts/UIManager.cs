using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject player;
    PlayerStats playerStats;
    public Image healthBar;
    public Image xpBar;
    float lerpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarFiller();
        ColorChanger();
        XpBarFiller();
        lerpSpeed = 5f * Time.deltaTime;
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, playerStats.hp / playerStats.maxHp, lerpSpeed);
    }

    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (playerStats.hp / playerStats.maxHp));

        healthBar.color = healthColor;
    }

    void XpBarFiller()
    {
        xpBar.fillAmount = Mathf.Lerp(xpBar.fillAmount, playerStats.xp / 100, lerpSpeed);
    }
}
