using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileDropHandler : MonoBehaviour, IDropHandler {

    [SerializeField]
    Transform squareTile;

    [SerializeField]
    Transform farm;

    [SerializeField]
    TileDragHandler drag;

    [SerializeField]
    GameObject marble;

    [SerializeField]
    MarbleManager marbleManager;

    ContactFilter2D contactFilter;

    LayerMask layerMask;

    Dictionary<string, Transform> dict = new Dictionary<string, Transform>();
        
    string[] colors = new string[4];

    [SerializeField]
    UnitCreator unitCreator;

    void Start()
    {
        foreach (Transform t in farm)
        {
            dict.Add(t.name, t);
        }
        layerMask = LayerMask.GetMask("Marble");
        contactFilter.NoFilter();

        Initialize();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Collider2D[] hitBuffer = new Collider2D[6];
        RectTransform farm = transform as RectTransform;
        //Debug.Log("recttransform " + farm.position + " mouse " + Input.mousePosition);
        if (RectTransformUtility.RectangleContainsScreenPoint(farm, Input.mousePosition))
        {
            int hits = squareTile.GetComponent<Collider2D>().OverlapCollider(contactFilter, hitBuffer);
            Debug.Log("hits " + hits);

            for (int i = 0; i < hitBuffer.Length; i++)
            {
                if (hitBuffer[i] != null)
                {
                    Collider2D[] marbleBuffer = new Collider2D[2];

                    Transform tile = dict[hitBuffer[i].name];
                    marbleBuffer = Physics2D.OverlapBoxAll(tile.position, new Vector2(0.8f, 0.8f), 0, layerMask);
                    for (int j = 0; j < marbleBuffer.Length; j++)
                    {
                        if (marbleBuffer[j] != null)
                        {
                            //Debug.Log("marbleBuffer " + marbleBuffer[j].name + " " + ((int)char.GetNumericValue(hitBuffer[i].name[0]) - 1).ToString());
                            Destroy(marbleBuffer[j].gameObject);
                            marbleManager.decreaseMarble((int)char.GetNumericValue(hitBuffer[i].name[0]) - 1);
                            for (int k = 0; k < 4; k++)
                            {
                                if (colors[k] == "none")
                                {
                                    colors[k] = marbleBuffer[j].gameObject.GetComponent<MarbleDisplay>().marble.color;
                                    //Debug.Log(colors[k]);
                                    break;
                                }
                            }
                            marbleManager.CheckifFull();
                        }
                    }
                }
            }
            //unitCreator.CreateUnit(squareTile.name, colors);
            Initialize();
        }
        else
        {
            //Debug.Log("Out of farm");
        }
        
    }

    void Initialize()
    {
        for (int i = 0; i < 4; i++)
        {
            colors[i] = "none";
        }
    }
}
