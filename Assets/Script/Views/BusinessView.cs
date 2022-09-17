using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BusinessView : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameBusiness;
    [SerializeField] private Scrollbar _progressIncomeBar;
    [SerializeField] private TMP_Text _lvlBusiness;
    [SerializeField] private TMP_Text _incomeBusiness;
    [SerializeField] private ButtonView _lvlUpButton;
    [SerializeField] private ButtonView _improvementFirstButton;
    [SerializeField] private ButtonView _improvementSecondButton;

    public void SetName(string name)
    {
        _nameBusiness.text = name+"";
    }
    
    public void SetProgressIncome(float progress)
    {
        _progressIncomeBar.size = progress;
    }
    
    public void AddListenerLvlUpButton(UnityAction call)
    {
        _lvlUpButton.Button.onClick.AddListener(call);
    }

    public void RemoveListenerLvlUpButton(UnityAction call)
    {
        _lvlUpButton.Button.onClick.RemoveListener(call);
    }
    
    public void SetLvlText(int lvlNumber)
    {
        _lvlBusiness.text = "LVL\n" + lvlNumber;
    }

    public void SetIncomeText(float income)
    {
        _incomeBusiness.text = "Доход\n" + income + "$";
    }
    
    public void AddListenerImprovementOneButton(UnityAction call)
    {
        _improvementFirstButton.Button.onClick.AddListener(call);
    }

    public void RemoveListenerImprovementOneButton(UnityAction call)
    {
        _improvementFirstButton.Button.onClick.RemoveListener(call);
    }

    public void UninteractableFirstImprovement()
    {
        _improvementFirstButton.Button.interactable = false;
    }
    
    public void PayOneImprovement(string name, int impact)
    {
        _improvementFirstButton.Text.text = name + "\nДоход: +" + impact + "%\nКуплено";
    }
    
    public void AddListenerImprovementTwoButton(UnityAction call)
    {
        _improvementSecondButton.Button.onClick.AddListener(call);
    }

    public void RemoveListenerImprovementTwoButton(UnityAction call)
    {
        _improvementSecondButton.Button.onClick.RemoveListener(call);
    }

    public void UninteractableSecondImprovement()
    {
        _improvementSecondButton.Button.interactable = false;
    }
    
    public void PayTwoImprovement(string name, int impact)
    {
        _improvementSecondButton.Text.text = name + "\nДоход: +" + impact + "%\nКуплено";
    }

    public void SetFirstImprovementButtonText(string name, int impact, int price)
    {
        _improvementFirstButton.Text.text = name + "\nДоход: +" + impact + "%\nЦена: " + price + "$";
    }

    public void SetSecondImprovementButtonText(string name, int impact, int price)
    {
        _improvementSecondButton.Text.text = name + "\nДоход: +" + impact + "%\nЦена: " + price + "$";
    }

    public void SetLvlUpText(int price)
    {
        _lvlUpButton.Text.text = "LVL UP\nЦена: "+price+"$";
    }
}

[Serializable]
public struct ButtonView
{
    public Button Button;
    public TMP_Text Text;
}
