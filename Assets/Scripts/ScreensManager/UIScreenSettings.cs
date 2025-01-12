namespace ArmorVehicle
{
    public class UIScreenSettings
    {
        public UIScreen Screen { get; }
        public string Guid { get; }
        public bool IsFirstScreen { get; }
        public bool IsUnClosable { get; }

        public UIScreenSettings(UIScreen screen, string guid, bool isFirstScreen = false, bool isUnClosable = false)
        {
            Screen = screen;
            Guid = guid;
            IsFirstScreen = isFirstScreen;
            IsUnClosable = isUnClosable;
        }
    }
}