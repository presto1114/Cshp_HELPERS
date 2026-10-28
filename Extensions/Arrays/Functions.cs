using System;

namespace Extensions.Arrays
{
    public static partial class Functions
    {
        public static T[] PopulateArray<T>(this T[] array, Func<T> provider)
        {
            System.Threading.Tasks.Parallel.For(0, array.Length, (i) => 
            {
                array[i] = provider();
            });
            return array;
        }

        public static Array RemoveFromArrayAt<T>(ref T[] arr, int index)
        {
            for (int a = index; a < arr.Length - 1; a++)
            {
                // moving elements downwards, to fill the gap at [index]
                arr[a] = arr[a + 1];
            }
            // finally, let's decrement Array's size by one
            Array.Resize(ref arr, arr.Length - 1);
            return arr;
        }

        public static bool IsRemoveFromArrayAt<T>(ref T[] arr, int index)
        {
            if (arr is null || arr.Length < 1)
                return false;

            for (int a = index; a < arr.Length - 1; a++)
            {
                // moving elements downwards, to fill the gap at [index]
                arr[a] = arr[a + 1];
            }
            // finally, let's decrement Array's size by one
            Array.Resize(ref arr, arr.Length - 1);

            return true;
        }

        #region Flatten/Unflatten Multi-Dimensional Arrays
        public static T[] FlattenMulit<T>(T[,] input)
        {
            int width = input.GetLength(0);
            int height = input.GetLength(1);

            T[] flattened = new T[width * height];

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    flattened[j * width + i] = input[i, j];

                }
            }
            return flattened;
        }

        public static T[,] UnflattenMulti<T>(T[] input, int width, int height)
        {
            T[,] unflattened = new T[width, height];

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    unflattened[i, j] = input[j * width + i];
                }
            }

            return unflattened;
        }
        #endregion

        #region Serialize/Deserialize Jagged Arrays
        // ==========================================
        // 2D JAGGED ARRAY CONVERSIONS (T[][])
        // ==========================================
        public static Row<T>[] ToSerializable2D<T>(T[][] source)
        {
            if (source == null) return null;
            var serialized = new Row<T>[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                serialized[i].elements = source[i];
            }
            return serialized;
        }

        public static T[][] FromSerializable2D<T>(Row<T>[] serialized)
        {
            if (serialized == null) return null;
            var destination = new T[serialized.Length][];
            for (int i = 0; i < serialized.Length; i++)
            {
                destination[i] = serialized[i].elements;
            }
            return destination;
        }

        // ==========================================
        // 3D JAGGED ARRAY CONVERSIONS (T[][][])
        // ==========================================
        public static Layer<T>[] ToSerializable3D<T>(T[][][] source)
        {
            if (source == null) return null;
            var serialized = new Layer<T>[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == null) continue;
                serialized[i].rows = new Row<T>[source[i].Length];
                for (int j = 0; j < source[i].Length; j++)
                {
                    serialized[i].rows[j].elements = source[i][j];
                }
            }
            return serialized;
        }

        public static T[][][] FromSerializable3D<T>(Layer<T>[] serialized)
        {
            if (serialized == null) return null;
            var destination = new T[serialized.Length][][];
            for (int i = 0; i < serialized.Length; i++)
            {
                if (serialized[i].rows == null) continue;
                destination[i] = new T[serialized[i].rows.Length][];
                for (int j = 0; j < serialized[i].rows.Length; j++)
                {
                    destination[i][j] = serialized[i].rows[j].elements;
                }
            }
            return destination;
        }

        // ==========================================
        // 4D JAGGED ARRAY CONVERSIONS (T[][][][])
        // ==========================================
        public static Volume<T>[] ToSerializable4D<T>(T[][][][] source)
        {
            if (source == null) return null;
            var serialized = new Volume<T>[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == null) continue;
                serialized[i].layers = new Layer<T>[source[i].Length];
                for (int j = 0; j < source[i].Length; j++)
                {
                    if (source[i][j] == null) continue;
                    serialized[i].layers[j].rows = new Row<T>[source[i][j].Length];
                    for (int k = 0; k < source[i][j].Length; k++)
                    {
                        serialized[i].layers[j].rows[k].elements = source[i][j][k];
                    }
                }
            }
            return serialized;
        }

        public static T[][][][] FromSerializ4D<T>(Volume<T>[] serialized)
        {
            if (serialized == null) return null;
            var destination = new T[serialized.Length][][][];
            for (int i = 0; i < serialized.Length; i++)
            {
                if (serialized[i].layers == null) continue;
                destination[i] = new T[serialized[i].layers.Length][][];
                for (int j = 0; j < serialized[i].layers.Length; j++)
                {
                    if (serialized[i].layers[j].rows == null) continue;
                    destination[i][j] = new T[serialized[i].layers[j].rows.Length][];
                    for (int k = 0; k < serialized[i].layers[j].rows.Length; k++)
                    {
                        destination[i][j][k] = serialized[i].layers[j].rows[k].elements;
                    }
                }
            }
            return destination;
        }
        #endregion
    }

    #region Helper Functions
    [Serializable] public struct Row<T> { public T[] elements; }
    [Serializable] public struct Layer<T> { public Row<T>[] rows; }
    [Serializable] public struct Volume<T> { public Layer<T>[] layers; }
    #endregion
}
