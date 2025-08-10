using UnityEngine;
using TMPro;

public class IngameUIUpdate : MonoBehaviour
{
    public TextMeshProUGUI fishAmmountValue;
    public TextMeshProUGUI shieldState;

    public int fishAmmount;

    void Start()
    {
        fishAmmountValue.text = "0";
        shieldState.text = "No";
        fishAmmount = 0;
    }

    public void AddFish()
    {
        fishAmmount++;
        Debug.Log(fishAmmount);

    }
    
    public void UpdateScore()
    {
        fishAmmountValue.text = fishAmmount.ToString();
    }

    public void ChangeShieldState()
    {
        shieldState.text = "Yes";
    }
}
