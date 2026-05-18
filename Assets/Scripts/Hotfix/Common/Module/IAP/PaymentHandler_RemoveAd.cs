using GameStorage;
using UnityEngine;

public class PaymentHandler_RemoveAd : PaymentHandler
{
    override public void HandlePaymentSuccess()
    {
        Game.GetMod<ModStorage>().GetStorage<StorageActivity>().RemoveAd.IsBuy = true;
    }
}
