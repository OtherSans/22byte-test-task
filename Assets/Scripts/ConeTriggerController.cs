using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConeTriggerController : MonoBehaviour
{
    [SerializeField] private float lookingTime;
    [SerializeField] private Animator enemyAnim;

    private CharacterController player;
    private SceneController sceneContr;

    private float remainingTime;
    private bool isEnemyTriggered;
    private void Awake()
    {
        player = GetComponentInParent<CharacterController>();
    }
    private void Start()
    {
        sceneContr = FindObjectOfType<SceneController>();
        remainingTime = 0;
    }
    private void Update()
    {
        if (isEnemyTriggered)
        {
            remainingTime += Time.deltaTime;

            if (remainingTime > lookingTime)
            {
                Time.timeScale = 0;
                StartCoroutine(GameOverTimer());
                enemyAnim.SetBool("IsEnemyAttack", true);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isEnemyTriggered = true;
        }  
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isEnemyTriggered = false;
            remainingTime = 0;
        }
    }

    private IEnumerator GameOverTimer()
    {
        yield return new WaitForSecondsRealtime(5);
        sceneContr.LoadScene("GameOverScene");
    }
}
