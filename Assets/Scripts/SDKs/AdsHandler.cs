using SDKs.Ads.ResultHandler;

namespace SDKs.Ads.ResultHandler
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
