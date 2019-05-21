using System;
using System.Collections.Generic;
using System.Linq;
using WikidataGame.Backend.Models;
using WikidataGame.Backend.Helpers;

namespace WikidataGame.Backend.Services
{
    public class MapGeneratorService {
        public static IEnumerable<Tile> GenerateMap() {
            var width = 5;
            var height = 5;
            var mapSize = width * height;

            // The actual map will be generated by Perlin noise;
            // where we are in noise space is randomized
            var rand = new Random();
            var xProgress = 0.15;
            var xOffset = rand.Next(0, 10);
            var yProgress = 0.08;
            var yOffset = rand.Next(0, 10);

            // TODO: This could probably be a lot prettier
            var noiseField = new Double[mapSize];
            for (int i = 0; i < mapSize; i++)
            {
                var x = i % width;
                var y = i / width; // integer division floors

                noiseField[i] = Perlin.OctavePerlin((x + xOffset) * xProgress, (y + yOffset) * yProgress, 1, 1, 1);
            }

            // we said we'd have about 18 tiles that you can access; here we
            // search for a threshold value that gives us 18 accessible tiles
            var accessibleGoal = 18;
            var threshold = 0.1;
            var previousThreshold = 0.0;
            var stepSize = 0.1;

            // this infinite loop will look at the noiseField and search for a
            // threshold value that will give us the exact amount of accessible
            // tiles we want
            while (true) {
                var aboveThreshold = noiseField.Count(n => n > threshold);
                if (aboveThreshold > accessibleGoal) {
                    // if we have too many accessible fields, we increase our threshold
                    if (previousThreshold > threshold) {
                        // if the previous threshold we looked at was above our current threshold,
                        // we pick one in between so we don't end up with an infinite loop
                        stepSize /= 10;
                    }

                    previousThreshold = threshold;
                    threshold += stepSize;
                }
                else if (aboveThreshold < accessibleGoal) {
                    // same as above but the other way around
                    if (previousThreshold < threshold) {
                        stepSize /= 10;
                    }

                    previousThreshold = threshold;
                    threshold -= stepSize;
                }
                else {
                    // we found a value that fits!
                    break;
                }
            }

            return noiseField.Select(n => new Tile {
                IsAccessible = n > threshold
            });
        }

        public static void Debug (IEnumerable<Tile> tiles) {
            var t = tiles.ToArray();
            for (int i = 0; i < tiles.Count(); i++)
            {
                System.Console.Write(t[i]);
                if (i % 5 == 0) System.Console.WriteLine();
            }
            System.Console.WriteLine();
            System.Console.WriteLine($"Accessible tiles: {tiles.Count(x => x.IsAccessible)}");
            System.Console.WriteLine();
        }
    } 
}