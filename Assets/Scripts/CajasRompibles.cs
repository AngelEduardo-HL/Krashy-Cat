using UnityEngine;

public class CajasRompibles : MonoBehaviour
{

    public GameObject fishPrefab;
    public GameObject shieldPrefab;
    public GameObject hpPrefab;

    int lootInstance;
    Vector3 placeToInstance;

    public void Start()
    {
        placeToInstance = this.transform.position;
    }
    public void TakeDamage(int damage)
    {

    }

   public void SpawnLoot()
    {
        //Probabilidad de que salga
        float random = Random.value;
        if (random <= .1) lootInstance = 0; //Loot HP
        else if (random >= .2 && random <= .3) lootInstance = 1; //Loot shield
        else if (random >= .4 && random <= .7) lootInstance = 2; //Loot puntos
        else lootInstance = 3; // Sin loot

        //Se instancia el objeto de acuerdo al random
        switch (lootInstance)
        {
            case 0:
                Instantiate(hpPrefab, placeToInstance, Quaternion.identity);
                Debug.Log("Loot HP");
                break;
            case 1:
                if (GameObject.FindAnyObjectByType<Shield>() == null)
                {
                    GameObject shield = Instantiate(shieldPrefab, placeToInstance, Quaternion.identity);
                }
                else
                {
                    Debug.Log("Ya hay un Shield activo, no se instancia otro");
                }
                break;
            case 2:
                Instantiate(fishPrefab, placeToInstance, Quaternion.identity);
                Debug.Log("Loot Points");
                break;
            case 3:
                Debug.Log("No Loot");
                break;
        }

    }

}
