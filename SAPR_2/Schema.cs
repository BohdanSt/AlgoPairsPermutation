using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SAPR_2
{
    class Schema
    {
        const int numberOfElements = 12;

        const int numberOfRows = 4;

        const int numberOfColumns = 5;
        public int NumberOfElements
        {
            get
            {
                return numberOfElements;
            }
        }

        public int NumberOfRows
        {
            get
            {
                return numberOfRows;
            }
        }

        public int NumberOfColumns
        {
            get
            {
                return numberOfColumns;
            }
        }

        public List<List<int>> Connections;

        public List<List<int>> DistancesOld;

        public List<List<int>> DistancesNew;

        public List<Element> Elements;

        public List<bool> IsElementPlaced;

        public Schema()
        {
            Connections = new List<List<int>>();
            DistancesOld = new List<List<int>>();
            DistancesNew = new List<List<int>>();
            for (int i = 0; i < numberOfElements; i++)
            {
                Connections.Add(new List<int>());
                DistancesOld.Add(new List<int>());
                DistancesNew.Add(new List<int>());
                for (int j = 0; j < NumberOfElements; j++)
                {
                    Connections[i].Add(0);
                    DistancesNew[i].Add(0);
                    DistancesOld[i].Add(0);
                }
            }

            IsElementPlaced = new List<bool>();
            Elements = new List<Element>();
            for (int i = 0; i < NumberOfElements; i++)
            {
                IsElementPlaced.Add(false);
                Elements.Add(null);
            }
        }

        public int GetBestElement()
        {
            int goodsum, badsum;
            for (int i = 0; i < numberOfElements; i++)
            {
                if (IsElementPlaced[i])
                    continue;

                goodsum = 0;
                badsum = 0;
                for (int j = 0; j < numberOfElements; j++)
                {
                    if (IsElementPlaced[j])
                    {
                        goodsum += Connections[i][j];
                    }
                    else
                    {
                        badsum += Connections[i][j];
                    }
                }

                Elements[i].PlacedConnections = goodsum;
                Elements[i].UnPlacedConnections = badsum;
            }

            int bestElement = -1;
            int max = int.MinValue;
            int min = int.MaxValue;
            for (int i = 0; i < numberOfElements; i++)
            {
                if (!IsElementPlaced[i] && (max < Elements[i].PlacedConnections || (max == Elements[i].PlacedConnections && min > Elements[i].UnPlacedConnections)))
                {
                    max = Elements[i].PlacedConnections;
                    min = Elements[i].UnPlacedConnections;
                    bestElement = i;
                }
            }

            return bestElement;
        }

        public void SetPlace(int num)
        {
            int min = int.MaxValue, q = 0;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    q = GetBestLenght(num, i, j);
                    if (min > q)
                    {
                        Elements[num].X = j;
                        Elements[num].Y = i;
                        min = q;
                    }
                }
            }
            IsElementPlaced[num] = true;
        }

        private int GetBestLenght(int num, int y, int x)
        {
            int sum = 0;
            for (int i = 0; i < numberOfElements; i++)
            {
                if (IsElementPlaced[i])
                {
                    if (Elements[i].CheckPosition(x, y))
                    {
                        sum = int.MaxValue;
                        break;
                    }
                    else
                    {
                        sum += (Math.Abs(x - Elements[i].X) + Math.Abs(y - Elements[i].Y)) * Connections[num][i]; 
                    }
                }
            }
            return sum;
        }

        public int CalculateEffectiveness()
        {
            int result, oldSum = 0, newSum = 0;

            for (int i = 0; i < numberOfElements; i++)
            {
                for (int j = 0; j < numberOfElements; j++)
                {
                    oldSum += DistancesOld[i][j];
                    newSum += DistancesNew[i][j];
                }
            }

            result = 100 - 100 * newSum / oldSum;

            return result;
        }

        public int CalculateDistancesNew()
        {
            int sum = 0;
            for (int i = 0; i < numberOfElements; i++)
            {
                for (int j = 0; j < numberOfElements; j++)
                {
                    DistancesNew[i][j] = (Math.Abs(Elements[i].X - Elements[j].X) + Math.Abs(Elements[i].Y - Elements[j].Y)) * Connections[i][j];
                    sum += DistancesNew[i][j];
                }
            }
            return sum / 2;
        }

        public int CalculateDistancesOld()
        {
            int sum = 0;
            for (int i = 0; i < numberOfElements; i++)
            {
                for (int j = 0; j < numberOfElements; j++)
                {
                    DistancesOld[i][j] = (Math.Abs(Elements[i].X - Elements[j].X) + Math.Abs(Elements[i].Y - Elements[j].Y)) * Connections[i][j];
                    sum += DistancesOld[i][j];
                }
            }
            return sum / 2;
        }
    }
}
