using Pulsar.Core.Display;

namespace SFML.Graphics
{
    public static class RenderWindowExtension
    {
        public static Resolution GetResolution(this RenderWindow window)
        {
            return new Resolution(window.Size.X, window.Size.Y);
        }
    }
}
