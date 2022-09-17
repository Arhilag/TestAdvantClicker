using UnityEngine;

[CreateAssetMenu(fileName = "Business", menuName = "ScriptableObjects/BusinessConfig", order = 0)]
public class BusinessConfig : ScriptableObject
{
    public string Name;
    public int Lvl;
    public float YieldIncome;
    public int BasePrice;
    public int BaseIncome;
    public Improvement First;
    public Improvement Second;
}
