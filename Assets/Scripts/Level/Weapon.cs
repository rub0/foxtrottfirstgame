﻿using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    private TransformableTile lastTile;
    public GameObject cursor;

    void Start()
    {
        cursor = GetComponent<WeaponCursor>().cursor;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TransformTile();
        }

        updateCursor();
    }

    void TransformTile()
    {
        Vector3 localPosition = gameObject.transform.position;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 clickPosition = new Vector3(ray.origin.x, ray.origin.y, 0f);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow, 10f);

        Collider2D hit = Physics2D.OverlapBox(new Vector2(cursor.transform.position.x, cursor.transform.position.y), new Vector2(0.5f, 0.5f), 0.0f, LayerMask.GetMask("Foreground", "Background", "Box", "Player"));

        //RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10f, LayerMask.GetMask("Foreground", "Background"));
        if (hit && !(hit.gameObject.layer == LayerMask.NameToLayer("Box") || hit.gameObject.layer == LayerMask.NameToLayer("Player")))
        {
            if (lastTile == hit.transform.gameObject.GetComponent<TransformableTile>())
            {
                hit.gameObject.GetComponent<Animator>().SetTrigger("Shake");
            }
            else
            {
                if (lastTile != null)
                {
                    lastTile.originalState();
                }
                if (lastTile != hit.transform.gameObject.GetComponent<TransformableTile>())
                {
                    lastTile = hit.transform.gameObject.GetComponent<TransformableTile>();
                    hit.gameObject.GetComponent<Animator>().SetTrigger("Shake");
                }
            }
        }
    }

    public void updateCursor()
    {

        float asd = Mathf.Abs(cursor.transform.position.x - Mathf.Round(transform.position.x));
        while (Mathf.Abs(cursor.transform.position.x - Mathf.Round(transform.position.x)) > GetComponent<WeaponCursor>().distance)
        {
            if (cursor.transform.position.x - Mathf.Round(transform.position.x) < 0)
            {
                cursor.transform.Translate(Vector3.right);
            }
            else
            {
                cursor.transform.Translate(Vector3.left);
            }
            
        }

        while (Mathf.Abs(cursor.transform.position.y - Mathf.Round(transform.position.y)) > GetComponent<WeaponCursor>().distance)
        {
            if (cursor.transform.position.y - Mathf.Round(transform.position.y) < 0)
            {
                cursor.transform.Translate(Vector3.up);
            }
            else
            {
                cursor.transform.Translate(Vector3.down);
            }
            
        }
    }
}
