using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Serialization;

public class NumberAnimation : MonoBehaviour
{
    public Text textComponent;
    public int value = 999;

    void OnEnable()
    {
        StartCoroutine(RandomNumberAnimation());
    }

    IEnumerator RandomNumberAnimation()
    {
        float animationDuration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
           
            int randomValue = Random.Range(0, 1000000);

          
            textComponent.text = randomValue.ToString();

            elapsedTime += Time.deltaTime;
            yield return null;
        } 
        textComponent.text = value.ToString();
    }
}