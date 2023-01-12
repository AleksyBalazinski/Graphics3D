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

        pressedI = 1 << 4,
        pressedK = 1 << 5,
        pressedL = 1 << 6,
        pressedJ = 1 << 7,
    }
}
