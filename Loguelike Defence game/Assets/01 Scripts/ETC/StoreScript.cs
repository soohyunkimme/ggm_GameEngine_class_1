using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreScript : MonoBehaviour
{
    [Header("업그레이드")]
    public List<Text> costText = new List<Text>();
    private int costHP;
    private int costMana;
    private int costManaRe;
    private int costPower;
    private int costMoveSpeed;
    private int costSkillCostDown;
    private int costSkill1;
    private int costSkill2;
    private int costSkill3;

    public Text Gold;

    public void SetCost()
    {
        costHP = 100 + ((int)GameManager.instance.hpLevel * 100);
        costMana = 100 + ((int)GameManager.instance.maxManaLevel * 100);
        costManaRe = 200 + ((int)GameManager.instance.manaRengen * 200);
        costPower = 100 + ((int)GameManager.instance.damageLevel * 100);
        costMoveSpeed = 300 + ((int)GameManager.instance.moveSpeed * 300);
        costSkillCostDown = 500 + ((int)GameManager.instance.costDown * 1000);
        costSkill1 = 100;
        costSkill2 = 300;
        costSkill3 = 500;
    }

    public void SetText()
    {
        costText[0].text = $" Cost {costHP}";
        costText[1].text = $" Cost {costMana}";
        costText[2].text = $" Cost {costManaRe}";
        costText[3].text = $" Cost {costPower}";
        if (GameManager.instance.moveSpeed > 2)
        {
            costText[4].text = $" Max Level";
        }
        else
        {
            costText[4].text = $" Cost {costMoveSpeed}";
        }
        if (GameManager.instance.costDown > 3)
        {
            costText[5].text = $" Max Level";
        }
        else
        {
            costText[5].text = $" Cost {costSkillCostDown}";
        }

        costText[6].text = $" Cost {costSkill1}";
        costText[7].text = $" Cost {costSkill2}";
        costText[8].text = $" Cost {costSkill3}";
        Gold.text = GameManager.instance.gold.ToString();
    }

    public void upHP()
    {
        if (GameManager.instance.gold > costHP)
        {
            GameManager.instance.gold -= costHP;

            GameManager.instance.hpLevel++;
            SetCost();
            SetText();
        }
    }

    public void upMaxMana()
    {
        if (GameManager.instance.gold > costMana)
        {
            GameManager.instance.gold -= costMana;

            GameManager.instance.maxManaLevel++;
            SetCost();
            SetText();
        }
    }

    public void upManaRe()
    {
        if (GameManager.instance.gold > costManaRe)
        {
            GameManager.instance.gold -= costManaRe;

            GameManager.instance.manaRengen++;
            SetCost();
            SetText();
        }
    }

    public void upPower()
    {
        if (GameManager.instance.gold > costPower)
        {
            GameManager.instance.gold -= costPower;

            GameManager.instance.damageLevel++;
            SetCost();
            SetText();
        }
    }

    public void upMoveSpeed()
    {
        if (!(GameManager.instance.moveSpeed > 2))
        {
            if (GameManager.instance.gold > costMoveSpeed)
            {
                GameManager.instance.gold -= costMoveSpeed;

                GameManager.instance.moveSpeed++;
                SetCost();
                SetText();
            }
        }

    }

    public void upSkillCostDown()
    {
        if (!(GameManager.instance.costDown > 3))
        {
            if (GameManager.instance.gold > costSkillCostDown)
            {
                GameManager.instance.gold -= costSkillCostDown;

                GameManager.instance.costDown++;
                SetCost();
                SetText();
            }
        }

    }
    public void upSkill1()
    {
        if (GameManager.instance.gold > costSkill1)
        {
            GameManager.instance.gold -= costSkill1;

            GameManager.instance.skill_1Level++;
            SetCost();
            SetText();
        }
    }
    public void upSkill2()
    {
        if (GameManager.instance.gold > costSkill2)
        {
            GameManager.instance.gold -= costSkill2;

            GameManager.instance.skill_2Level++;
            SetCost();
            SetText();
        }
    }
    public void upSkill3()
    {
        if (GameManager.instance.gold > costSkill3)
        {
            GameManager.instance.gold -= costSkill3;

            GameManager.instance.skill_3Level++;
            SetCost();
            SetText();
        }
    }

    public void ExitStore()
    {
        GameManager.instance.SceneChange(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}