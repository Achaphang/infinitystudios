using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightBatteryAdvanced : FlashlightBattery{
    public override void UseBattery(Flashlight f) {
        f.UpgradeFlashlight();
        f.RestorePower(100f);
        Destroy(gameObject);
    }
}
