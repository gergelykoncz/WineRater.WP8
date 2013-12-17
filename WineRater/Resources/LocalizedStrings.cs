namespace WineRater.Resources
{
    /// <summary>
    /// Provides access to string resources.
    /// </summary>
    public class LocalizedStrings
    {
        private static AppResources _localizedResources = new AppResources();

        public AppResources LocalizedResources { get { return _localizedResources; } }

        /// <summary>
        /// Provides access for the underlying resource manager, to help localizing enums
        /// and stuff from which the resource key could be dynamic.
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <returns></returns>
        public static string GetResourceForKey(string key)
        {
            return AppResources.ResourceManager.GetString(key);
        }
    }
}