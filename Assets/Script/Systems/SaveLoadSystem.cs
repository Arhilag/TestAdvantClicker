using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadSystem
{
    private Save _saveContainer;
    private string _saveString = "Save";
    
    public void LoadData(BalanceModel balanceModel, List<BusinessModel> businessModels)
    {
        if (PlayerPrefs.HasKey(_saveString))
        {
            _saveContainer = JsonUtility.FromJson<Save>(PlayerPrefs.GetString(_saveString));
            balanceModel.AddBalance(_saveContainer.Balance);

            if (_saveContainer.BusinessDataToSave.Length > 0)
            {
                for (int i = 0; i < businessModels.Count; i++)
                {
                    if(_saveContainer.BusinessDataToSave[i].Lvl>0)
                        businessModels[i].SetLvl(_saveContainer.BusinessDataToSave[i].Lvl);
                    if(_saveContainer.BusinessDataToSave[i].FirstImprovement)
                        businessModels[i].SetOneImprovement();
                    if(_saveContainer.BusinessDataToSave[i].SecondImprovement)
                        businessModels[i].SetTwoImprovement();
                }
            }
        }
    }

    public void SaveData(BalanceModel balanceModel, List<BusinessModel> businessModels)
    {
        BusinessDataToSave[] dataToSaves = new BusinessDataToSave[businessModels.Count];
        for (int i = 0; i < businessModels.Count; i++)
        {
            dataToSaves[i] = businessModels[i].GetDataToSave();
        }
        _saveContainer = new Save 
        {
            Balance = balanceModel.GetBalance(),
            BusinessDataToSave = dataToSaves
        };
        PlayerPrefs.SetString(_saveString, JsonUtility.ToJson(_saveContainer));
    }
}

[Serializable]
public class Save
{
    public float Balance = 0;
    public BusinessDataToSave[] BusinessDataToSave;
}
