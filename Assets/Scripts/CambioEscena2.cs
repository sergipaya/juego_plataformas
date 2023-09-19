using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena2 : MonoBehaviour
{
    // Start is called before the first frame update
void OnTriggerEnter2D(Collider2D col)
    {
        StartCoroutine(espera());
        IEnumerator espera()
        {
            if (col.gameObject.tag == "Player")
            {
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene(5);
            }
        }
            
    }
}
