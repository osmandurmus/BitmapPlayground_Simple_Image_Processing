using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BitmapPlayground.Filters
{
    /// <summary>
    /// Filters the moving avarage from an image.
    /// </summary>

    class MovingAvarageFilter : IFilter
    {
        public Color[,] Apply(Color[,] input)
        {
            int width = input.GetLength(0);
            int height = input.GetLength(1);
            Color[,] result = new Color[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if(x == 0 || y == 0 || x == width - 1 || y == height - 1) //  If the input pixel lies at the border, its value may remain the same.
                    {
                        var p = input[x, y];
                        result[x, y] = Color.FromArgb(p.A, p.R, p.G, p.B);
                    }
                    else
                    {
                        var p = input[x, y];

                        // the 4 pixels left, right, above and below
                        var left= input[x-1, y];
                        var right= input[x, y+1];
                        var above= input[x, y+1];
                        var below= input[x, y-1];

                        // find average
                        var avgR = (left.R + right.R + above.R + below.R) / 4;
                        var avgG = (left.G + right.G + above.G + below.G) / 4;
                        var avgB = (left.B + right.B + above.B + below.B) / 4;

                        result[x, y] = Color.FromArgb(p.A,avgR, avgG, avgB);
                    }
                }
            }

            return result;
        }

        public string Name => "Moving Avarage Filter";

        public override string ToString()
            => Name;

    }
}
