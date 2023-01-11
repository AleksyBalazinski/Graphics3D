namespace Graphics3D.Utility
{
    [Flags]
    internal enum KeyboardState
    {
        None = 0,
        pressedW = 1 << 0,
        pressedS = 1 << 1,
        pressedA = 1 << 2,
        pressedD = 1 << 3,
    }
}
