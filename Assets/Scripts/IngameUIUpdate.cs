using UnityEngine;
using TMPro;

public class IngameUIUpdate : MonoBehaviour
{
    public TextMeshProUGUI fishAmmountValue;
    public TextMeshProUGUI shieldState;

    public int fishAmmount;

    void Start()
    {
        GetComponent<PlayerHP>(); // mantiene tu patrón
        fishAmmountValue.fontSize = 36f;
        fishAmmountValue.text = "0";
        shieldState.text = "No";
        fishAmmount = 0;
    }

    public void AddFish()
    {
        fishAmmount++;

        // Al juntar 100 peces, ganar 1 vida y restar 100 del contador
        if (fishAmmount >= 100)
        {
            fishAmmount -= 100;
            var hp = GetComponent<PlayerHP>();
            if (hp != null) hp.AddLife(1);
        }
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
