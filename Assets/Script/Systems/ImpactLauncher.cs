using System.Threading.Tasks;
using UnityEngine;

public class ImpactLauncher
{
    private bool _isTakeImpact;

    public async void LaunchProgressImpact(BusinessModel businessModel, BalanceModel balanceModel, BusinessView businessView)
    {
        _isTakeImpact = true;
        float time = 0;
        float yieldIncome = businessModel.GetYieldIncome();
        while (_isTakeImpact)
        {
            if (time >= yieldIncome)
            {
                time = 0;
                balanceModel.AddBalance(businessModel.GetIncome());
            }
            await Task.Yield();
            time += Time.deltaTime;
            businessView.SetProgressIncome(time / yieldIncome);
        }
    }

    public void StopLauncher()
    {
        _isTakeImpact = false;
    }
}
