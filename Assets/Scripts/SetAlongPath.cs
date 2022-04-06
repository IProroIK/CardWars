using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using DG.Tweening;

public class SetAlongPath : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] cards;
    public PathCreator pathCreator;

    [SerializeField]private float _offSet;
    private Quaternion _cardRotation;
    private int _cardAngel = -16;
    private const float length = 1f;


    private void OnEnable()
    {
        gatherCard.OnLastCardReached += funwiseCard;
    }

    private void OnDisable()
    {
        gatherCard.OnLastCardReached -= funwiseCard;
    }

    private IEnumerator calculateDistanse()
    {
        yield return new WaitForSeconds(1);
        _offSet = 0;
        _cardAngel = -16;
        for (int i = 0; i < cards.Length; i++)
        {
                cards[i].DOMove(pathCreator.path.GetPointAtDistance(_offSet), 0.5f, false);
                //cards[i].position = pathCreator.path.GetPointAtDistance(_offSet);
                cards[i].rotation = Quaternion.Euler(0, _cardAngel, 0);
                _offSet += 0.5f;
                _cardAngel += 2;
        }
    
    }

    private void funwiseCard()
    {
        StartCoroutine(calculateDistanse());
    }

}
