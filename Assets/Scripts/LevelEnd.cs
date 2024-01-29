using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private LevelManager levelManager;


void Start() {
    levelManager = GameObject.Find("SceneManager").GetComponent<LevelManager>();
}
    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            levelManager.LoadNextLevel();
        }
    }


}
