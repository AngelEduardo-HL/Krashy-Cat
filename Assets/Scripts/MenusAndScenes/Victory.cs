using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
       
           SceneManager.LoadScene("VictoryScene"); 
        
    }
    
        
    
}
