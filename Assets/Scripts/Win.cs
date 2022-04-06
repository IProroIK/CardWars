using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] private GameObject win;

    private void OnEnable()
    {
        gatherCard.OnLastCardReached += activeWinForm;
    }

    private void OnDisable()
    {
        gatherCard.OnLastCardReached -= activeWinForm;
    }

    private void activeWinForm()
    {
        StartCoroutine(activeDelay());
    }

    private IEnumerator activeDelay()
    {
        yield return new WaitForSeconds(3);
        win.SetActive(true);

    }
}
