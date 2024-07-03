using ManageMe.BusinessLogic;

namespace ManageMe.Web.Code.Utils
{
    public static class MatrixColoring
    {
        public static Tuple<string, bool>[,,,] GetColoring(ScheduleVM[,,,] inputMatrix, List<Tuple<string, bool>> colorsAndText, Tuple<string, bool>[,,,] colorMatrix, int firstDimension)
        {
            for (int i = 0; i < firstDimension; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 13; k++)
                    {
                        for (int l = 0; l < 4; l++)
                        {
                            if (inputMatrix[i, j, k, l] != null)
                            {
                                var startIndexOfCurrentCell = k;

                                var duration = inputMatrix[i, j, k, l].EndHour - inputMatrix[i, j, k, l].StartHour;

                                var colorCodesForThisCellsNeighbours = new List<string>();

                                for (int m = 0; m < duration; m++)
                                {
                                    if (j - 1 >= 0 && inputMatrix[i, j - 1, k, l] != null && colorMatrix[i, j - 1, k, l] != null)
                                    {
                                        colorCodesForThisCellsNeighbours.Add(colorMatrix[i, j - 1, k, l].Item1);
                                    }

                                    if (j + 1 < 5 && inputMatrix[i, j + 1, k, l] != null && colorMatrix[i, j + 1, k, l] != null)
                                    {
                                        colorCodesForThisCellsNeighbours.Add(colorMatrix[i, j + 1, k, l].Item1);
                                    }

                                    if (k - 1 >= 0 && inputMatrix[i, j, k - 1, l] != null && colorMatrix[i, j, k - 1, l] != null)
                                    {
                                        colorCodesForThisCellsNeighbours.Add(colorMatrix[i, j, k - 1, l].Item1);
                                    }

                                    if (k + 1 < 13 && inputMatrix[i, j, k + 1, l] != null && colorMatrix[i, j, k + 1, l] != null)
                                    {
                                        colorCodesForThisCellsNeighbours.Add(colorMatrix[i, j, k + 1, l].Item1);
                                    }

                                    if (l - 1 >= 0 && inputMatrix[i, j, k, l - 1] != null && colorMatrix[i, j, k, l - 1] != null)
                                    {
                                        colorCodesForThisCellsNeighbours.Add(colorMatrix[i, j, k, l - 1].Item1);
                                    }

                                    if (l + 1 < 4 && inputMatrix[i, j, k, l + 1] != null && colorMatrix[i, j, k, l + 1] != null)
                                    {
                                        colorCodesForThisCellsNeighbours.Add(colorMatrix[i, j, k, l + 1].Item1);
                                    }

                                    k++;
                                }

                                k--;

                                var firstAvailableColorCodeWithTextColour = colorsAndText.Where(ct => !colorCodesForThisCellsNeighbours.Contains(ct.Item1)).First();

                                for (int m = startIndexOfCurrentCell; m <= k; m++)
                                {
                                    colorMatrix[i, j, m, l] = firstAvailableColorCodeWithTextColour;
                                }
                            }
                        }
                    }
                }
            }

            return colorMatrix;
        }
    }
}
