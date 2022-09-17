using System;

[Serializable]
public class BusinessModel
{
    private BusinessConfig _data;
    
    private string _name = "Название";
    private int _lvl;
    private float _income;
    private int _price;
    private bool _receiveIncome = false;

    private Improvement _firstImprovement;
    private Improvement _secondImprovement;
    
    public Action<int> OnLvlUp;
    public Action<float> OnNewIncome;
    public Action<string> OnSetName;
    public Action<int> OnSetLvlUpPrice;
    public Action<string,int,int> OnSetFirstImprovement;
    public Action<string,int,int> OnSetSecondImprovement;
    public Action OnPayFirstImprovement;
    public Action OnPaySecondImprovement;
    public Action OnSetLvl;
    
    public BusinessModel(BusinessConfig config)
    {
        _data = config;
    }

    public void Initialized()
    {
        _name = _data.Name;
        _lvl = _data.Lvl;
        _income = CalculateIncome();
        _price = CalculateLvlPrice();
        _firstImprovement.Name = _data.First.Name;
        _firstImprovement.Price = _data.First.Price;
        _secondImprovement.Name = _data.Second.Name;
        _secondImprovement.Price = _data.Second.Price;
        OnSetName?.Invoke(_name);
        OnSetFirstImprovement?.Invoke(_data.First.Name,(int)(_data.First.Multiplier*100),_data.First.Price);
        OnSetSecondImprovement?.Invoke(_data.Second.Name,(int)(_data.Second.Multiplier*100),_data.Second.Price);
        OnSetLvlUpPrice?.Invoke(_price);
        OnLvlUp?.Invoke(_lvl);
        OnNewIncome?.Invoke(_income);
        if (_lvl>0)
            _receiveIncome = true;
    }
    
    private float CalculateIncome()
    {
        return _lvl * _data.BaseIncome * (1 + _firstImprovement.Multiplier + _secondImprovement.Multiplier);
    }
    
    public int CalculateLvlPrice()
    {
        return (_lvl + 1) * _data.BasePrice;
    }

    public void LevelUp()
    {
        _lvl++;
        OnLvlUp?.Invoke(_lvl);
        _income = CalculateIncome();
        _price = CalculateLvlPrice();
        OnNewIncome?.Invoke(_income);
        OnSetLvlUpPrice?.Invoke(_price);
        _receiveIncome = true;
    }

    public void SetLvl(int lvlNumber)
    {
        _lvl = lvlNumber;
        OnLvlUp?.Invoke(_lvl);
        _income = CalculateIncome();
        _price = CalculateLvlPrice();
        OnNewIncome?.Invoke(_income);
        OnSetLvlUpPrice?.Invoke(_price);
        OnSetLvl?.Invoke();
    }
    
    public void SetOneImprovement()
    {
        _firstImprovement.Multiplier = _data.First.Multiplier;
        _income = CalculateIncome();
        OnNewIncome?.Invoke(_income);
        OnPayFirstImprovement?.Invoke();
    }

    public void SetTwoImprovement()
    {
        _secondImprovement.Multiplier = _data.Second.Multiplier;
        _income = CalculateIncome();
        OnNewIncome?.Invoke(_income);
        OnPaySecondImprovement?.Invoke();
    }

    public int GetPriceOneImprovement()
    {
        return _data.First.Price;
    }
    
    public int GetPriceTwoImprovement()
    {
        return _data.Second.Price;
    }

    public float GetYieldIncome()
    {
        return _data.YieldIncome;
    }

    public float GetIncome()
    {
        return _income;
    }

    public Improvement GetFirstImprovement()
    {
        return _data.First;
    }

    public Improvement GetSecondImprovement()
    {
        return _data.Second;
    }

    public int GetLvl()
    {
        return _lvl;
    }

    public BusinessDataToSave GetDataToSave()
    {
        return new BusinessDataToSave()
        {
            Lvl = _lvl,
            FirstImprovement = _firstImprovement.Multiplier > 0,
            SecondImprovement = _secondImprovement.Multiplier > 0
        };
    }

    public bool GetReceiveIncome()
    {
        return _receiveIncome;
    }
}

[Serializable]
public struct Improvement
{
    public string Name;
    public float Multiplier;
    public int Price;
}

[Serializable]
public struct BusinessDataToSave
{
    public int Lvl;
    public bool FirstImprovement;
    public bool SecondImprovement;
}
