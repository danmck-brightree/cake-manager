using System;

namespace CakeManager.Shared
{
    public static class Constants
    {
        public const int CakeMarkTallyMax = 3;
        public const int SuperCakeMarkTallyMax = 5;
        public const string LoginRoute = "/login";
        public static readonly Guid TemporaryUserId = new Guid("2234A4E3-7AE6-4882-B204-81F38A3D7D47");
        public static readonly string TemporaryToken = "21B4F518-1B25-47B5-BF5A-8B17E7B2FB51";
        public enum CakeMarkType { Normal, Super }
    }
}
