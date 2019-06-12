using System;
using System.Collections.Generic;
using System.Text;

namespace Pipeline
{
    /// <summary>
    /// This test pipeline processes 
    /// </summary>
    public class TestPipeline1
    {
        public struct Data
        {
            public readonly int width, height;

            public Data(int width, int height)
            {
                this.width = width;
                this.height = height;
            }
        }

        public class Heightmap
        {
            public readonly int seed;
            public readonly int chunkX, chunkY;
            public readonly float[,] map;

            public Heightmap(int seed, int chunkX, int chunkY, int width, int height, float stratHeight)
            {
                this.seed = seed;
                this.chunkX = chunkX;
                this.chunkY = chunkY;
                map = new float[width, height];

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        map[x, y] = stratHeight;
                    }
                }
            }
        }

        public class HeightmapProcessors
        {
            public static Heightmap RandomHeightmap(Heightmap input, PipelineContext context)
            {
                Random prng = new Random(input.seed);

                for (int x = 0; x < input.map.GetLength(0); x++)
                {
                    for (int y = 0; y < input.map.GetLength(1); y++)
                    {
                        input.map[x, y] = (float)(prng.Next(int.MinValue, int.MaxValue) / (int.MaxValue / 10));
                    }
                }

                return input;
            }

            public static Heightmap AddOneToHeightmap(Heightmap input, PipelineContext context)
            {
                for (int x = 0; x < input.map.GetLength(0); x++)
                {
                    for (int y = 0; y < input.map.GetLength(1); y++)
                    {
                        input.map[x, y] += 1;
                    }
                }

                return input;
            }
        }

        public static IPipeline<Heightmap> hightmapPipeline;

        static TestPipeline1()
        {
            hightmapPipeline = new PipelineBase<Heightmap>()
                .Register(HeightmapProcessors.RandomHeightmap)
                .Register(HeightmapProcessors.AddOneToHeightmap);
        }

        public static int[,] GenerateWorldMap(int chunkX, int chunkY, Data generationData)
        {
            return GenerateWorldMap(
                generationData, 
                GenerateHeightmap(generationData, chunkX, chunkY));
        }

        public static int[,] GenerateWorldMap(Data generationData, Heightmap heightmap)
        {
            int[,] _map = new int[heightmap.map.GetLength(0), heightmap.map.GetLength(1)];

            for (int x = 0; x < _map.GetLength(0); x++)
            {
                for (int y = 0; y < _map.GetLength(1); y++)
                {
                    _map[x, y] = (int)heightmap.map[x, y];
                }
            }

            return _map;
        }

        public static Heightmap GenerateHeightmap(Data generationData, int chunkX, int chunkY)
        {
            Heightmap _heightmap = new Heightmap(12345, chunkX, chunkY, generationData.width, generationData.height, 0);
            return hightmapPipeline.Process(_heightmap, context: out _);
        }
    }
}
