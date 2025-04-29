using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUIManager : MonoBehaviour
{
    public PlayerStatManager playerStat;

    public Text levelText;
    public Text nameText;
    public Text strText;
    public Text dexText;
    public Text intText;
    public Text bonusPointText;

    public Button strPlusButton;
    public Button dexPlusButton;
    public Button intPlusButton;

    private void Start()
    {
        strPlusButton.onClick.AddListener(OnClickIncreaseSTR);
        dexPlusButton.onClick.AddListener(OnClickIncreaseDEX);
        intPlusButton.onClick.AddListener(OnClickIncreaseINT);

        gameObject.SetActive(false);
    }

    public void UpdateStatUI()
    {
        if (playerStat != null)
        {
            levelText.text = $"레벨: {playerStat.level}";
            nameText.text = "플레이어";

            strText.text = $"{playerStat.strength}";
            dexText.text = $"{playerStat.dexterity}";
            intText.text = $"{playerStat.intelligence}";
            bonusPointText.text = $"{playerStat.bonusStatPoints}";

            bool hasPoint = playerStat.bonusStatPoints > 0;

            strPlusButton.interactable = hasPoint;
            dexPlusButton.interactable = hasPoint;
            intPlusButton.interactable = hasPoint;
        }
    }

    void OnClickIncreaseSTR()
    {
        playerStat.IncreaseStrength();
        UpdateStatUI();
    }

    void OnClickIncreaseDEX()
    {
        playerStat.IncreaseDexterity();
        UpdateStatUI();
    }

    void OnClickIncreaseINT()
    {
        playerStat.IncreaseIntelligence();
        UpdateStatUI();
    }
}