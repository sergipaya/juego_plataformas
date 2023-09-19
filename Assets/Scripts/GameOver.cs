using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Example());
    }

        IEnumerator Example()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene (PlayerPrefs.GetInt ("lastLevel"));
    }
}