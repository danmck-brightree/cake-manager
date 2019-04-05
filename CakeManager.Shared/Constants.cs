using System;

namespace CakeManager.Shared
{
    public static class Constants
    {
        public const int CakeMarkTallyMax = 3;
        public const int SuperCakeMarkTallyMax = 5;

        public enum CakeMarkType { Normal, Super }

        public const string LoginRoute = "/login";
        public const string OfficeRoute = "/office";
        public const string CakeMarkRoute = "/cakemark";
    }
}
