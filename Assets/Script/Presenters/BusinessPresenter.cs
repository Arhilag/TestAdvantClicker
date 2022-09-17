using System;

[Serializable]
public class BusinessPresenter
{
    private BusinessView _businessView;
    private BusinessModel _businessModel;
    private BalanceModel _balanceModel;
    private ImpactLauncher _impactLauncher = new ImpactLauncher();
    
    public BusinessPresenter(BusinessView view, BusinessModel model, BalanceModel balanceModel)
    {
        _businessView = view;
        _businessModel = model;
        _balanceModel = balanceModel;
        OnEnable();
    }

    public void OnEnable()
    {
        _businessView.AddListenerLvlUpButton(RequestLvlUp);
        _businessModel.OnLvlUp += _businessView.SetLvlText;
        _businessModel.OnNewIncome += _businessView.SetIncomeText;
        _businessView.AddListenerImprovementOneButton(RequestFirstImprovementPay);
        _businessView.AddListenerImprovementTwoButton(RequestSecondImprovementPay);
        _businessModel.OnSetName += _businessView.SetName;
        _businessModel.OnSetFirstImprovement += _businessView.SetFirstImprovementButtonText;
        _businessModel.OnSetSecondImprovement += _businessView.SetSecondImprovementButtonText;
        _businessModel.OnSetLvlUpPrice += _businessView.SetLvlUpText;
        _businessModel.OnPayFirstImprovement += _businessView.UninteractableFirstImprovement;
        _businessModel.OnPaySecondImprovement += _businessView.UninteractableSecondImprovement;
        _businessModel.OnSetLvl += LaunchBusiness;
        
        _businessModel.Initialized();
        if(_businessModel.GetLvl()>0 && !_businessModel.GetReceiveIncome())
            _impactLauncher.LaunchProgressImpact(_businessModel, _balanceModel, _businessView);
    }
    
    public void OnDisable()
    {
        _businessView.RemoveListenerLvlUpButton(RequestLvlUp);
        _businessModel.OnLvlUp -= _businessView.SetLvlText;
        _businessModel.OnNewIncome -= _businessView.SetIncomeText;
        _businessView.RemoveListenerImprovementOneButton(RequestFirstImprovementPay);
        _businessView.RemoveListenerImprovementTwoButton(RequestSecondImprovementPay);
        _businessModel.OnSetName -= _businessView.SetName;
        _businessModel.OnSetFirstImprovement -= _businessView.SetFirstImprovementButtonText;
        _businessModel.OnSetSecondImprovement -= _businessView.SetSecondImprovementButtonText;
        _businessModel.OnSetLvlUpPrice -= _businessView.SetLvlUpText;
        _businessModel.OnPayFirstImprovement -= _businessView.UninteractableFirstImprovement;
        _businessModel.OnPaySecondImprovement -= _businessView.UninteractableSecondImprovement;
        _businessModel.OnSetLvl -= LaunchBusiness;
        
        _impactLauncher.StopLauncher();
    }

    private void RequestLvlUp()
    {
        if (_balanceModel.DecreaseBalance(_businessModel.CalculateLvlPrice()))
        {
            _businessModel.LevelUp();
            if(_businessModel.GetLvl()==1)
                _impactLauncher.LaunchProgressImpact(_businessModel, _balanceModel, _businessView);
        }
    }

    private void RequestFirstImprovementPay()
    {
        if (_balanceModel.DecreaseBalance(_businessModel.GetPriceOneImprovement()))
        {
            var improvement = _businessModel.GetFirstImprovement();
            _businessModel.SetOneImprovement();
            _businessView.PayOneImprovement(improvement.Name, (int)(improvement.Multiplier*100));
        }
    }

    private void RequestSecondImprovementPay()
    {
        if (_balanceModel.DecreaseBalance(_businessModel.GetPriceTwoImprovement()))
        {
            var improvement = _businessModel.GetSecondImprovement();
            _businessModel.SetTwoImprovement();
            _businessView.PayTwoImprovement(improvement.Name, (int)(improvement.Multiplier*100));
        }
    }
    
    private void LaunchBusiness()
    {
        _impactLauncher.LaunchProgressImpact(_businessModel, _balanceModel, _businessView);
    }
}
