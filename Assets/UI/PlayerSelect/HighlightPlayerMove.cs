using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class HighlightPlayerMove : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject sprite;
    private Animator anim;
    void Start()
    {
        anim = sprite.GetComponent<Animator>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetBool("move", true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("move", false);
    }
}
