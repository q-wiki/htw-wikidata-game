using System;
using System.Collections.Generic;
using System.Linq;
using WikidataGame.Backend.Models;
using WikidataGame.Backend.Helpers;

namespace WikidataGame.Backend.Services
{
    public class MapGeneratorService
    {


        /// <summary>
        /// Retrieves the entity (including the supplied navigationProperties) with the supplied id/primary key 
        /// </summary>
        /// <param name="mapWidth">Maximum tiles in x direction</param>
        /// <param name="mapHeight">Maximum tiles in y direction</param>
        /// <param name="accessibleTiles">How many tiles should be accessible</param>
        /// <returns>An IEnumerable of map tiles</returns>
        public static IEnumerable<Tile> GenerateMap(int mapWidth, int mapHeight, int accessibleTiles)
        {
            var mapSize = mapWidth * mapHeight;

            // The actual map will be generated by Perlin noise;
            // where we are in noise space is randomized
            var rand = new Random();
            var xProgress = 0.15;
            var xOffset = rand.Next(0, 10);
            var yProgress = 0.22;
            var yOffset = rand.Next(0, 10);

            // TODO: This could probably be a lot prettier
            var noiseField = new Double[mapSize];
            for (int index = 0; index < mapSize; index++)
            {
                var x = index % mapWidth;
                var y = index / mapWidth; // integer division floors

                noiseField[index] = Perlin.OctavePerlin((x + xOffset) * xProgress, (y + yOffset) * yProgress, 1, 1, 1);
            }

            // we said we'd have about 18 tiles that you can access; here we
            // search for a threshold value that gives us 18 accessible tiles
            var threshold = 0.1;
            var previousThreshold = 0.0;
            var stepSize = 0.1;
            int aboveThreshold;

            // this infinite loop will look at the noiseField and search for a
            // threshold value that will give us the exact amount of accessible
            // tiles we want
            do
            {
                aboveThreshold = noiseField.Count(n => n > threshold);
                if (aboveThreshold > accessibleTiles)
                {
                    // if we have too many accessible fields, we increase our threshold
                    if (previousThreshold > threshold) 
                    {
                        // if the previous threshold we looked at was above our current threshold,
                        // we pick one in between so we don't end up with an infinite loop
                        stepSize /= 10;
                    }

                    previousThreshold = threshold;
                    threshold += stepSize;
                }
                else if (aboveThreshold < accessibleTiles)
                {
                    // same as above but the other way around
                    if (previousThreshold < threshold)
                    {
                        stepSize /= 10;
                    }

                    previousThreshold = threshold;
                    threshold -= stepSize;
                }
            } while (accessibleTiles != aboveThreshold);

            return noiseField.Select(n => new Tile {
                IsAccessible = n > threshold
            });
        }

        public static void Debug (int mapWidth, IEnumerable<Tile> tiles)
        {
            for (int i = 0; i < tiles.Count(); i++)
            {
                System.Console.Write(tiles.ElementAt(i));
                if (i % mapWidth == 0) System.Console.WriteLine();
            }
            System.Console.WriteLine();
            System.Console.WriteLine($"Accessible tiles: {tiles.Count(x => x.IsAccessible)}");
            System.Console.WriteLine();
        }
    } 
}