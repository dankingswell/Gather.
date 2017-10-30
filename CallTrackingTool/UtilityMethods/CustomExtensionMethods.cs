namespace CallTrackingTool
{
        public static class CustomExtensionMethods
        {
            // Method to Ensure all Data input into system is consistent
            public static string PrepareStringData(this string a)
            {
                return a.Trim().ToUpper();
            }
        }
}
