using System;

public class BalanceModel
{
    private float _balance;
    public Action<float> OnChangedBalance;

    public void AddBalance(float difference)
    {
        _balance += difference;
        OnChangedBalance?.Invoke(_balance);
    }
    
    public bool DecreaseBalance(int difference)
    {
        if (_balance < difference)
            return false;
        _balance -= difference;
        OnChangedBalance?.Invoke(_balance);
        return true;
    }

    public float GetBalance()
    {
        return _balance;
    }
}
