using UnityEngine;
using TMPro;

public class IngameUIUpdate : MonoBehaviour
{
    public TextMeshProUGUI fishAmmountValue;
    public TextMeshProUGUI shieldState;

    public int fishAmmount;

    void Start()
    {
        GetComponent<PlayerHP>();

        fishAmmountValue.fontSize = 36f;
        fishAmmountValue.text = "0";
        shieldState.text = "No";
        fishAmmount = 0;
    }

    public void AddFish()
    {
        //IncreaseFontSize(36f);
        fishAmmount++;
        //DecreaseFontSize(36f);
    }
    
    public void UpdateScore()
    {
        fishAmmountValue.text = fishAmmount.ToString();
    }

    public void ChangeShieldState()
    {
        GetComponent<PlayerHP>().isInvincible = true;
        Debug.Log("Invincibility State is" + GetComponent<PlayerHP>().isInvincible);
        shieldState.text = "Yes";
    }

    public void IncreaseFontSize(float fontSizeVariation)
    {
        fishAmmountValue.fontSize += fontSizeVariation;
    }
    public void DecreaseFontSize(float fontSizeVariation)
    {
        fishAmmountValue.fontSize -= fontSizeVariation;
    }
}
