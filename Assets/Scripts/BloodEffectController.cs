using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffectController : MonoBehaviour
{
    [SerializeField]
    private GameObject bloodEffectPrefab;
    private Transform playerTransform;

    public static BloodEffectController instance { get; private set; }

    public void SpawnBloodEffect()
    {
        StartCoroutine(SpawnBloodCoroutine());
    }
    public IEnumerator SpawnBloodCoroutine()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject currentEffect = Instantiate(bloodEffectPrefab, playerTransform.position, Quaternion.identity);

        yield return new WaitForSeconds(8);
        Destroy(currentEffect);
   
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
