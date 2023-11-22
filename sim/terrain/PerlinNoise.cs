using System;
using Godot;

namespace ZihbotMl.FantasyWorld
{
    public class PerlinNoise
    {
        static float Interpolate(float a0, float a1, float w)
        {
            /* // You may want clamping by inserting:
            * if (0.0 > w) return a0;
            * if (1.0 < w) return a1;
            */
            return (a1 - a0) * w + a0;
            /* // Use this cubic interpolation [[Smoothstep]] instead, for a smooth appearance:
            * return (a1 - a0) * (3.0 - w * 2.0) * w * w + a0;
            *
            * // Use [[Smootherstep]] for an even smoother result with a second derivative equal to zero on boundaries:
            * return (a1 - a0) * ((w * (w * 6.0 - 15.0) + 10.0) * w * w * w) + a0;
            */
        }

        /* Create pseudorandom direction vector
 */
        Vector2 RandomGradient(int ix, int iy)
        {
            // No precomputed gradients mean this works for any number of grid coordinates
            const int w = 8 * sizeof(uint);
            const int s = w / 2; // rotation width
            uint a = (uint)ix, b = (uint)iy;
            a *= 3284157443; b ^= a << s | a >> w - s;
            b *= 1911520717; a ^= b << s | b >> w - s;
            a *= 2048419325;
            float random = a * (3.14159265f / ~(~0u >> 1)); // in [0, 2*Pi]
            Vector2 v = new() { X = Mathf.Cos(random), Y = Mathf.Sin(random) };
            return v;
        }

        // Computes the dot product of the distance and gradient vectors.
        float DotGridGradient(int ix, int iy, float x, float y)
        {
            // Get gradient from integer coordinates
            Vector2 gradient = RandomGradient(ix, iy);

            // Compute the distance vector
            float dx = x - ix;
            float dy = y - iy;

            // Compute the dot-product
            return dx * gradient.X + dy * gradient.Y;
        }

        private readonly int seed;

        public PerlinNoise()
        {
            seed = new Random().Next();
        }

        public PerlinNoise(int seed)
        {
            this.seed = seed;
        }

        public float Noise(float x, float y)
        {
            // Determine grid cell coordinates
            int x0 = Mathf.FloorToInt(x);
            int x1 = x0 + 1;
            int y0 = Mathf.FloorToInt(y);
            int y1 = y0 + 1;

            // Determine interpolation weights
            // Could also use higher order polynomial/s-curve here
            float sx = x - x0;
            float sy = y - y0;

            // Interpolate between grid point gradients
            float n0, n1, ix0, ix1;

            n0 = DotGridGradient(x0, y0, x, y);
            n1 = DotGridGradient(x1, y0, x, y);
            ix0 = Interpolate(n0, n1, sx);

            n0 = DotGridGradient(x0, y1, x, y);
            n1 = DotGridGradient(x1, y1, x, y);
            ix1 = Interpolate(n0, n1, sx);

            // Will return in range [0, 1]
            return Interpolate(ix0, ix1, sy) * 0.5f + 0.5f;
        }
    }
}