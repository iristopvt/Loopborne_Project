using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StatUIManager : MonoBehaviour
{
    public PlayerStatManager playerStat;

    public Text levelText;
    public Text nameText;
    public Text traitsText;
    public Text debuffsText;
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

            strText.text = $"{playerStat.str}";
            dexText.text = $"{playerStat.dex}";
            intText.text = $"{playerStat.@int}";
            bonusPointText.text = $"{playerStat.bonusStatPoints}";

            bool hasPoint = playerStat.bonusStatPoints > 0;

            strPlusButton.interactable = hasPoint;
            dexPlusButton.interactable = hasPoint;
            intPlusButton.interactable = hasPoint;

        
            if (TraitManager.Instance != null)
            {
                traitsText.text = "Ability: ";
                if (TraitManager.Instance.selectedTraits.Count > 0)
                {
                    traitsText.text += string.Join(", ", TraitManager.Instance.selectedTraits.Select(t => t.traitName));
                }

                debuffsText.text = "Debuff: ";
                if (TraitManager.Instance.selectedDebuffs.Count > 0)
                {
                    debuffsText.text += string.Join(", ", TraitManager.Instance.selectedDebuffs.Select(d => d.traitName));
                }
             
            }
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