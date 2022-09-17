
public class BalancePresenter
{
    private BalanceView _balanceView;
    private BalanceModel _balanceModel;

    public BalancePresenter(BalanceView view, BalanceModel model)
    {
        _balanceView = view;
        _balanceModel = model;
        OnEnable();
    }

    public void OnEnable()
    {
        _balanceModel.OnChangedBalance += _balanceView.SetBalance;
    }
    
    public void OnDisable()
    {
        _balanceModel.OnChangedBalance -= _balanceView.SetBalance;
    }
}
