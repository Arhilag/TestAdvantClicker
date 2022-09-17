using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private BalanceView _balanceView;
    [SerializeField] private List<BusinessView> _businessViews;
    [SerializeField] private List<BusinessConfig> _businessConfigs;
    private BalancePresenter _balancePresenter;
    private BalanceModel _balanceModel;
    [SerializeField][HideInInspector]private List<BusinessPresenter> _businessPresenters;
    [SerializeField][HideInInspector]private List<BusinessModel> _businessModels;
    private SaveLoadSystem _saveLoadSystem;

    private void Start()
    {
        if (_businessViews.Count != _businessConfigs.Count)
        {
            Debug.LogError("Установите одинаковое количество BusinessViews и BusinessConfig в компоненте GameController!");
            return;
        }
        _saveLoadSystem = new SaveLoadSystem();
        _balanceModel = new BalanceModel();
        _balancePresenter = new BalancePresenter(_balanceView, _balanceModel);
        
        for (int i = 0; i < _businessConfigs.Count; i++)
        {
            _businessModels.Add(new BusinessModel(_businessConfigs[i]));
            _businessPresenters.Add(new BusinessPresenter(_businessViews[i], _businessModels[i], _balanceModel));
        }
        
        _balanceModel.AddBalance(0);
        _saveLoadSystem.LoadData(_balanceModel, _businessModels);
    }

    private void OnDestroy()
    {
        _balancePresenter.OnDisable();
        foreach (var businessPresenter in _businessPresenters)
        {
            businessPresenter.OnDisable();
        }
    }
    
#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            _saveLoadSystem.SaveData(_balanceModel, _businessModels);
        }
    }
#else
    private void OnApplicationQuit()
    {
        _saveLoadSystem.SaveData(_balanceModel, _businessModels);
    }
#endif
}