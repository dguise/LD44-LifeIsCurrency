using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    GameObject target;
    Player player;
    Vector3 offset;
    Vector3 _ = Vector3.zero;

    void Start()
    {
        player = GameManager.Player;
        target = player.gameObject;
        offset = new Vector3(0, 0, -10);
    }

    void LateUpdate()
    {
        if (player == null) return; 

        var distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance > 1.5f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref _, (100 * (6 - distance)) * Time.deltaTime);
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            Scroll();
        }
    }

    float _scrollModifier = 0.3f;
    float _prevScroll = 0f;
    void Scroll()
    {
        var scroll = Input.mouseScrollDelta.y;
        if (scroll == _prevScroll)
            scroll = _prevScroll * 1.2f; 

        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, Camera.main.orthographicSize - (scroll * _scrollModifier), 100 * Time.deltaTime);
        _prevScroll = scroll;
    }
}
