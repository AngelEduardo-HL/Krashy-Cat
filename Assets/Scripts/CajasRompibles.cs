using UnityEngine;

public class CajasRompibles : MonoBehaviour
{
    public int health = 1;

    public GameObject fishPrefab;
    public GameObject maskPrefab;
    public GameObject hpPrefab;

    int lootInstance;
    Vector3 placeToInstance;

    public void Start()
    {
        placeToInstance = this.transform.position;
    }
    public void TakeDamage(int damage)
    {
        ////health -= damage;
        ////if (health <= 0)
        ////{
        ////    Destroy(gameObject);
        ////}
    }

   public void SpawnLoot()
    {
        //Probabilidad de que salga
        float random = Random.value;
        if (random <= .2) lootInstance = 3;
        else if (random >= .3 && random <=.5)  lootInstance = 2;
        else if (random >=.6) lootInstance = 1;
       
        //Se instancia el objeto de acuerdo al random
        switch (lootInstance)
        {
            case 0:
                Instantiate(fishPrefab, placeToInstance, Quaternion.identity);
                Debug.Log("Fish");
                break;
            case 1:
                Instantiate(maskPrefab, placeToInstance, Quaternion.identity);
                Debug.Log("Mask");
                break;
            case 2:
                Instantiate(hpPrefab, placeToInstance, Quaternion.identity);
                Debug.Log("HP");
                break;
        }

    }

}
