using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    private TextMeshProUGUI tiltTutorial;
    private TextMeshProUGUI symbolTutorial;

    public float textFade = 0.03f;
    public float waitTime = 0.5f;

    private int firstTime;

    void Start()
    {
        tiltTutorial = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        symbolTutorial = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        firstTime = PlayerPrefs.GetInt("First");

        if (firstTime == 0)
        {
            gameObject.SetActive(true);
            StartCoroutine(TiltTutorialFadeIn());
            symbolTutorial.gameObject.SetActive(false);

            PlayerPrefs.SetInt("First", -1);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator TiltTutorialFadeIn()
    {
        tiltTutorial.gameObject.SetActive(true);

        for (float f = 0.05f; f <= 1.05f; f += 0.05f)
        {
            Color t1 = tiltTutorial.color;
            t1.a = f;
            tiltTutorial.color = t1;
            yield return new WaitForSeconds(textFade);
        }

        StartCoroutine(TiltTutorialFadeOut());
    }

    IEnumerator TiltTutorialFadeOut()
    {
        yield return new WaitForSeconds(waitTime);

        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color t1 = tiltTutorial.color;
            t1.a = f;
            tiltTutorial.color = t1;
            yield return new WaitForSeconds(textFade);
        }

        tiltTutorial.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitTime);

        StartCoroutine(SymbolTutorialFadeIn());
    }

    IEnumerator SymbolTutorialFadeIn()
    {
        symbolTutorial.gameObject.SetActive(true);

        for (float f = 0.05f; f <= 1.05f; f += 0.05f)
        {
            Color t2 = symbolTutorial.color;
            t2.a = f;
            symbolTutorial.color = t2;
            yield return new WaitForSeconds(textFade);
        }

        StartCoroutine(SymbolTutorialFadeOut());
    }

    IEnumerator SymbolTutorialFadeOut()
    {
        yield return new WaitForSeconds(waitTime);

        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color t2 = symbolTutorial.color;
            t2.a = f;
            symbolTutorial.color = t2;
            yield return new WaitForSeconds(textFade);
        }

        symbolTutorial.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
