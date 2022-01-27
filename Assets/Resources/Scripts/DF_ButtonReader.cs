using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DF_ButtonReader : MonoBehaviour
{
    public GameObject scriptExecutor;
    PointerEventData pointerEventData;
    public EventSystem eventSystem;
    GraphicRaycaster graphicRaycaster;

    void Start()
    {
        graphicRaycaster = GetComponent<GraphicRaycaster>();
    }

    private void Update()
    {
        if (DF_FightController.fighting && DF_FightController.readyForNextMove)
            CheckRaycastHits();
    }

    void CheckRaycastHits()
    {
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.tag == "UIButton")
            {
                switch (result.gameObject.name)
                {
                    case "Attack":
                        if (Input.GetMouseButtonDown(0))
                        {
                            scriptExecutor.GetComponent<DF_FightController>().Attack();
                        }
                        break;
                    case "Spell1":
                        if (Input.GetMouseButtonDown(0))
                        {
                            scriptExecutor.GetComponent<DF_FightController>().UseFirstSkill();
                        }
                        break;
                    case "Spell2":
                        if (Input.GetMouseButtonDown(0))
                        {
                            scriptExecutor.GetComponent<DF_FightController>().UseSecondSkill();
                        }
                        break;
                    case "Heal":
                        if (Input.GetMouseButtonDown(0))
                        {
                            scriptExecutor.GetComponent<DF_FightController>().Heal();
                        }
                        break;
                }
                break;
            }
        }
    }
}
