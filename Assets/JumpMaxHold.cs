using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMaxHold : MonoBehaviour
{

    public void PlayMaxHoldSFX()
    {
        AudioManager.instance.PlaySFX("MaxHold");
    }
}
