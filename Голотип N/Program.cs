using System;

namespace Голотип_N
{
    class Program
    {
        public static LearningObject[] InsertLearningData()
        {
            LearningObject[] myArr = new LearningObject[11];

            myArr[0] = new LearningObject(7, 6, "I");
            myArr[1] = new LearningObject(8, 10, "I");
            myArr[2] = new LearningObject(12, 9, "I");
            myArr[3] = new LearningObject(13, 6, "I");
            myArr[4] = new LearningObject(19, 7, "I");

            myArr[5] = new LearningObject(35, 8, "II");
            myArr[6] = new LearningObject(39, 8, "II");
            myArr[7] = new LearningObject(37, 10, "II");
            myArr[8] = new LearningObject(5, 24, "II");
            myArr[9] = new LearningObject(15, 23, "II");
            myArr[10] = new LearningObject(8, 21, "II");

            return myArr;
        }
        public static double FindExtremeDifference(LearningObject[] arr, byte propertyNum)
        {
            double LargestNum = int.MinValue;
            double SmallestNum = int.MaxValue;

            if (propertyNum == 1)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].FirstProperty > LargestNum)
                        LargestNum = arr[i].FirstProperty;
                    if (arr[i].FirstProperty < SmallestNum)
                        SmallestNum = arr[i].FirstProperty;
                }
            }
            else if (propertyNum == 2)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].SecondProperty > LargestNum)
                        LargestNum = arr[i].SecondProperty;
                    if (arr[i].SecondProperty < SmallestNum)
                        SmallestNum = arr[i].SecondProperty;
                }
            }
            else return -1;



            return LargestNum - SmallestNum;
        }
        public static double FindMax(params double[] arr)
        {
            double a = double.MinValue;

            foreach (var item in arr)
            {
                if (item > a)
                    a = item;
            }

            return a;
        }
        public static double[,] ApplyMask(double[,] arr, double muLimit)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] < muLimit)
                        arr[i, j] = 0;
                }
            }

            return arr;
        }
        public static int[] /*string[,]*/ CreateConnectionsMap(double[,] arr)
        {
            if (arr.GetLength(0) != arr.GetLength(1))
                throw new ArgumentException("Matrix must be square.");

            int[] groups = new int[arr.GetLength(0)];

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                groups[i] = i;
                for (int j = 0; j < i; j++)
                {
                    if (arr[i, j] != 0 && arr[i, j] == arr[j, i])
                    {
                        groups[i] = groups[j];
                        break;
                    }
                }
            }
            return groups;
            //string[,] newArr = new string[arr.GetLength(0), arr.GetLength(1)];
            ////newArr = FillWithNegative1(newArr);

            //for (int i = 0; i < arr.GetLength(0); i++)
            //{
            //    for (int j = 0; j < arr.GetLength(1); j++)
            //    {
            //        if (arr[i, j] != 0 && arr[i, j] == arr[j, i])
            //        {
            //            newArr[i, j] = $"{i}|{j}";
            //        }
            //        else newArr[i, j] = "0";
            //    }
            //}
            //return newArr;
        }
        public static bool CheckIfPresent (int[,] arr, string index)
        {
            //string reversedElement = $"{index[1]}{index[0]}";

            //for (int i = 0; i < arr.GetLength(0); i++)
            //{
            //    for (int j = 0; j < arr.GetLength(1); j++)
            //    {
            //        if (arr[i, j] == int.Parse(element) && arr[j, i] == -1)
            //            return true;
            //    }
            //}
            return false;
        }
        public static int[,] FillWithNegative1(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = -1;
                }
            }
            return arr;
        }

        public static void Write2DArray<T>(T[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write($"{arr[i, j]}\t");
                }
                Console.WriteLine(); Console.WriteLine();
            }
        }
        public static void Write1DArray<T>(T[] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                Console.Write($"{arr[i]}\t");
            }
        }

        static void Main(string[] args)
        {
            LearningObject[] LearningData = InsertLearningData();

            double E1 = FindExtremeDifference(LearningData, 1);
            double E2 = FindExtremeDifference(LearningData, 2);

            double[,] FirstPropertySimilarityMatrix = new double[LearningData.Length, LearningData.Length];
            double[,] SecondPropertySimilarityMatrix = new double[LearningData.Length, LearningData.Length];

            for (int i = 0; i < LearningData.Length; i++)
            {
                for (int j = 0; j < LearningData.Length; j++)
                {
                    double a = 1 - (Math.Abs(LearningData[i].FirstProperty - LearningData[j].FirstProperty) / E1);
                    double b = 1 - (Math.Abs(LearningData[i].SecondProperty - LearningData[j].SecondProperty) / E2);

                    FirstPropertySimilarityMatrix[i, j] = Math.Round(a, 5);
                    SecondPropertySimilarityMatrix[i, j] = Math.Round(b, 5);
                }
            }

            double Delta1 = 0.6;
            double Delta2 = 0.4;

            double[,] CombinedSimilarityMatrix = new double[LearningData.Length, LearningData.Length];

            for (int i = 0; i < LearningData.Length; i++)
            {
                for (int j = 0; j < LearningData.Length; j++)
                {
                    double a = FirstPropertySimilarityMatrix[i,j] * Delta1 + SecondPropertySimilarityMatrix[i, j] * Delta2;
                    CombinedSimilarityMatrix[i, j] = Math.Round(a, 5);
                }
            }

            int AmountOfFirstTypeObjects = 5;
            int AmountOfSecondTypeObjects = 6;
            int TotalAmountOfObjects = AmountOfFirstTypeObjects + AmountOfSecondTypeObjects;

            double x = 0;
            double y = 0;
            double z = double.MinValue;
            int counter1 = 0;
            int counter2 = 0;

            for (int i = 0; i < AmountOfFirstTypeObjects; i++)
            {
                for (int j = 0; j < AmountOfFirstTypeObjects; j++)
                {
                    if (CombinedSimilarityMatrix[i, j] == 1)
                        continue;
                    x += CombinedSimilarityMatrix[i, j];
                    counter1++;
                }
            }
            x /= counter1;

            for (int i = AmountOfFirstTypeObjects; i < TotalAmountOfObjects; i++)
            {
                for (int j = AmountOfFirstTypeObjects; j < TotalAmountOfObjects; j++)
                {
                    if (CombinedSimilarityMatrix[i, j] == 1)
                        continue;
                    y += CombinedSimilarityMatrix[i, j];
                    counter2++;
                }
            }
            y /= counter2;

            for (int i = AmountOfFirstTypeObjects; i < TotalAmountOfObjects; i++)
            {
                for (int j = 0; j < AmountOfFirstTypeObjects; j++)
                {
                    if (CombinedSimilarityMatrix[i, j] == 1)
                        continue;
                    if (CombinedSimilarityMatrix[i,j] > z)
                        z = CombinedSimilarityMatrix[i, j];
                }
            }

            double muLimit = FindMax(x,y,z);
            CombinedSimilarityMatrix = ApplyMask(CombinedSimilarityMatrix, muLimit);

            Write1DArray(CreateConnectionsMap(CombinedSimilarityMatrix));
        }
    }
    public class LearningObject
    {
        public double FirstProperty { get; }
        public double SecondProperty { get; }
        public string F { get; }
        public LearningObject(int firstprop, int secondprop, string directproperty)
        {
            FirstProperty = firstprop;
            SecondProperty = secondprop;
            F = directproperty;
        }
    }
    public class ExamObject
    {
        public double FirstProperty { get; set; }
        public double SecondProperty { get; set; }

        private string F;

        public void SetF(string str)
        {
            this.F = str;
        }
    }
}
