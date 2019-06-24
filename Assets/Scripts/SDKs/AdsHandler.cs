using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppodealAds.Handler.RewardedResultHandler;

namespace AppodealAds.Handler.RewardedResultHandler
{
    public enum Result
    {
        Default = 0,
        Failed = 1,
        Finished = 2
    }
}

public static class AdsHandler
{
    public static Result currentAdState;
}
