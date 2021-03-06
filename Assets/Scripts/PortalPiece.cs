﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPiece : Piece
{
    public GameObject connected_portal;

    public void Teleport(GameObject go)
    {
        GameManager.Instance.audio.PlayOneShot(BallManager.Instance.portal_sound);

        BallManager.Instance.ball.controller.enabled = false;
        go.transform.position = connected_portal.transform.position;
        BallManager.Instance.ball.SetPlatform(connected_portal.transform.parent.gameObject);
        BallManager.Instance.ball.controller.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball" && BallManager.Instance.ball.current_platform != this.gameObject)
        {
            Teleport(other.gameObject);
        }
    }
}

