using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(TrashItem))]
public class DragTrash : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image organic, inorganic;
    public Canvas myCanvas;
    private TrashItem thisTrashItem;
    public bool dragOnSurfaces = true;

    private GameObject m_DraggingIcon;
    private RectTransform m_DraggingPlane;
    private Player player;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        thisTrashItem = this.GetComponent<TrashItem>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var canvas = FindInParents<Canvas>(gameObject);
        if (canvas == null)
            return;

        // We have clicked something that can be dragged.
        // What we want to do is create an icon for this.
        m_DraggingIcon = new GameObject("icon");

        m_DraggingIcon.transform.SetParent(canvas.transform, false);
        m_DraggingIcon.transform.SetAsLastSibling();

        var image = m_DraggingIcon.AddComponent<Image>();

        image.sprite = GetComponent<Image>().sprite;
        image.SetNativeSize();

        if (dragOnSurfaces)
            m_DraggingPlane = transform as RectTransform;
        else
            m_DraggingPlane = canvas.transform as RectTransform;

        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData data)
    {
        if (m_DraggingIcon != null)
            SetDraggedPosition(data);
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
            m_DraggingPlane = data.pointerEnter.transform as RectTransform;

        var rt = m_DraggingIcon.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = m_DraggingPlane.rotation;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        // Debug.Log(Vector2.Distance(organic.transform.position, myCanvas.transform.TransformPoint(pos)));
        if (Vector2.Distance(organic.transform.position, myCanvas.transform.TransformPoint(pos)) < 1f)
        {
            int idx = Int32.Parse(thisTrashItem.garbageData.ID);
            player.currentTrash -= player.trashCountList[idx];
            if (thisTrashItem.garbageData.Type == GarbageType.ORGANIC)
            {
                Player.Instance.coinInStage += thisTrashItem.garbageData.Price * player.trashCountList[idx];
            }
            else 
            {
                Debug.Log("khong cong");
            }
            player.trashCountList[idx] = 0;
        }
        else if (Vector2.Distance(inorganic.transform.position, myCanvas.transform.TransformPoint(pos)) < 1f)
        {
            int idx = Int32.Parse(thisTrashItem.garbageData.ID);
            player.currentTrash -= player.trashCountList[idx];
            if (thisTrashItem.garbageData.Type == GarbageType.INORGANIC)
            {
                Player.Instance.coinInStage += thisTrashItem.garbageData.Price * player.trashCountList[idx];
            }
            else 
            {
                Debug.Log("khong cong");
            }
            player.trashCountList[idx] = 0;
        }
        if (m_DraggingIcon != null)
            Destroy(m_DraggingIcon);
    }

    static public T FindInParents<T>(GameObject go) where T : Component
    {
        if (go == null) return null;
        var comp = go.GetComponent<T>();

        if (comp != null)
            return comp;

        Transform t = go.transform.parent;
        while (t != null && comp == null)
        {
            comp = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return comp;
    }
}
