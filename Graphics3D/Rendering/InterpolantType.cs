namespace Graphics3D.Rendering
{
    /// <summary>
    /// Specifies interpolation method to be used by the ColorPicker
    /// </summary>
    internal enum InterpolantType
    {
        /// <summary>
        /// Gouraud shading
        /// </summary>
        Color,

        /// <summary>
        /// Phong shading
        /// </summary>
        NormalVector,

        /// <summary>
        /// Constant shading
        /// </summary>
        Constant
    }
}
