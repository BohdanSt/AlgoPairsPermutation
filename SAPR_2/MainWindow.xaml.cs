using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SAPR_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Schema schema;

        private List<List<TextBox>> connections;

        private List<TextBlock> elements;

        TextBlock selectedTextBlockElement = null;

        private List<bool> isBaseElement;

        public MainWindow()
        {
            InitializeComponent();

            MessageBox.Show("This program dedicated to star team VNTU_Destructor. \n"
                + "Team members: \n"
                + "Oleh - main programmer \n"
                + "Petro - team's brain \n"
                + "Bohdan - just a boy from a neighboring yard", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            schema = new Schema();

            connections = new List<List<TextBox>>();
            for (int i = 0; i < schema.NumberOfElements; i++)
            {
                connections.Add(new List<TextBox>());
                for (int j = 0; j < schema.NumberOfElements; j++)
                {
                    connections[i].Add(null);
                }
            }

            isBaseElement = new List<bool>();
            for (int i = 0; i < schema.NumberOfElements; i++)
            {
                isBaseElement.Add(false);
            }

            elements = new List<TextBlock>();

            InitializeMatrix();
        }

        private void InitializeMatrix()
        {
            for (int i = 0; i < schema.NumberOfElements; i++)
            {
                TextBlock textBlockNameX = new TextBlock();
                textBlockNameX.Text = (i + 1).ToString();
                textBlockNameX.TextAlignment = TextAlignment.Center;
                textBlockNameX.Padding = new Thickness(0, 0, 5, 0);
                textBlockNameX.SetValue(Grid.RowProperty, 0);
                textBlockNameX.SetValue(Grid.ColumnProperty, i + 1);

                gridMatrix.Children.Add(textBlockNameX);

                TextBlock textBlockNameY = new TextBlock();
                textBlockNameY.Text = (i + 1).ToString();
                textBlockNameY.TextAlignment = TextAlignment.Right;
                textBlockNameY.VerticalAlignment = VerticalAlignment.Center;
                textBlockNameY.Padding = new Thickness(0, 0, 5, 0);
                textBlockNameY.SetValue(Grid.RowProperty, i + 1);
                textBlockNameY.SetValue(Grid.ColumnProperty, 0);

                gridMatrix.Children.Add(textBlockNameY);

                for (int j = 0; j < schema.NumberOfElements; j++)
                {
                    TextBox textBoxX = new TextBox();
                    textBoxX.Text = "";
                    textBoxX.Width = 20;
                    textBoxX.MaxLength = 2;
                    textBoxX.Margin = new Thickness(-2, 1, 1, 1);
                    textBoxX.SetValue(Grid.RowProperty, i + 1);
                    textBoxX.SetValue(Grid.ColumnProperty, j + 1);
                    textBoxX.TextChanged += matrix_ValueChanged;
                    textBoxX.PreviewTextInput += NumberValidationTextBox;

                    gridMatrix.Children.Add(textBoxX);
                    connections[i][j] = textBoxX;
                }
            }
        }

        private void InitializeSchemaRandom()
        {
            List<List<bool>> IsCellFilled = new List<List<bool>>();
            for (int i = 0; i < schema.NumberOfRows; i++)
            {
                IsCellFilled.Add(new List<bool>());
                for (int j = 0; j < schema.NumberOfColumns; j++)
                {
                    IsCellFilled[i].Add(false);
                }
            }

            Random rand = new Random();
            int row = 0;
            int column = 0;
            for (int i = 0; i < schema.NumberOfElements; i++)
            {
                TextBlock textBoxElement = GenerateTextBlockElement(i);

                do
                {
                    row = rand.Next(schema.NumberOfRows);
                    column = rand.Next(schema.NumberOfColumns);

                } while (IsCellFilled[row][column]);
                 
                textBoxElement.SetValue(Grid.RowProperty, row);
                textBoxElement.SetValue(Grid.ColumnProperty, column);

                gridSchema.Children.Add(textBoxElement);
                elements.Add(textBoxElement);

                IsCellFilled[row][column] = true;
            }
        }

        private void InitializeSchemaVNTU_Destructor()
        {
            List<List<bool>> isCellFilled = new List<List<bool>>();
            for (int i = 0; i < schema.NumberOfRows; i++)
            {
                isCellFilled.Add(new List<bool>());
                for (int j = 0; j < schema.NumberOfColumns; j++)
                {
                    isCellFilled[i].Add(false);
                }
            }

            for (int i = 0; i < schema.NumberOfElements; i++)
            {
                elements.Add(null);
            }

            TextBlock destrTextBLockElement = GenerateTextBlockElement(3);
            destrTextBLockElement.SetValue(Grid.RowProperty, 3);
            destrTextBLockElement.SetValue(Grid.ColumnProperty, 4);
            gridSchema.Children.Add(destrTextBLockElement);
            elements[3] = destrTextBLockElement;
            isCellFilled[3][4] = true;

            destrTextBLockElement = GenerateTextBlockElement(6);
            destrTextBLockElement.SetValue(Grid.RowProperty, 0);
            destrTextBLockElement.SetValue(Grid.ColumnProperty, 3);
            gridSchema.Children.Add(destrTextBLockElement);
            elements[6] = destrTextBLockElement;
            isCellFilled[0][3] = true;

            destrTextBLockElement = GenerateTextBlockElement(11);
            destrTextBLockElement.SetValue(Grid.RowProperty, 3);
            destrTextBLockElement.SetValue(Grid.ColumnProperty, 0);
            gridSchema.Children.Add(destrTextBLockElement);
            elements[11] = destrTextBLockElement;
            isCellFilled[3][0] = true;

            Random rand = new Random();
            int row = 0;
            int column = 0;
            for (int i = 0; i < schema.NumberOfElements; i++)
            {
                if (elements[i] != null)
                    continue;

                TextBlock textBoxElement = GenerateTextBlockElement(i);

                do
                {
                    row = rand.Next(schema.NumberOfRows);
                    column = rand.Next(schema.NumberOfColumns);

                } while (isCellFilled[row][column]);

                textBoxElement.SetValue(Grid.RowProperty, row);
                textBoxElement.SetValue(Grid.ColumnProperty, column);

                gridSchema.Children.Add(textBoxElement);
                elements[i] = textBoxElement;

                isCellFilled[row][column] = true;
            }
        }

        private TextBlock GenerateTextBlockElement(int i)
        {
            TextBlock textBoxElement = new TextBlock();
            textBoxElement.Text = (i + 1).ToString();
            textBoxElement.TextAlignment = TextAlignment.Center;
            textBoxElement.VerticalAlignment = VerticalAlignment.Stretch;
            textBoxElement.HorizontalAlignment = HorizontalAlignment.Stretch;
            textBoxElement.FontSize = 16;
            textBoxElement.Background = Brushes.Gray;
            textBoxElement.Margin = new Thickness(5);
            textBoxElement.Padding = new Thickness(15);
            textBoxElement.MouseDown += schema_MouseDown;
            return textBoxElement;
        }

        private void schema_MouseDown(object sender, MouseEventArgs e)
        {
            TextBlock textbox = sender as TextBlock;
            int num;
            if (textbox == selectedTextBlockElement)
            {
                num = elements.IndexOf(selectedTextBlockElement);

                if (isBaseElement[num])
                {
                    selectedTextBlockElement.Background = Brushes.DarkRed;
                }
                else
                {
                    selectedTextBlockElement.Background = Brushes.Gray;
                }
                selectedTextBlockElement = null;

                buttonSetBase.IsEnabled = false;
                buttonSetCommon.IsEnabled = false;
            }
            else
            {
                if (selectedTextBlockElement != null)
                {
                    num = elements.IndexOf(selectedTextBlockElement);

                    if (isBaseElement[num])
                    {
                        selectedTextBlockElement.Background = Brushes.DarkRed;
                    }
                    else
                    {
                        selectedTextBlockElement.Background = Brushes.Gray;
                    }
                }

                selectedTextBlockElement = textbox;
                num = elements.IndexOf(selectedTextBlockElement);
                if (isBaseElement[num])
                {
                    selectedTextBlockElement.Background = Brushes.IndianRed;
                }
                else
                {
                    selectedTextBlockElement.Background = Brushes.LightGray;
                }

                buttonSetBase.IsEnabled = true;
                buttonSetCommon.IsEnabled = true;
            }
        }

        private void matrix_ValueChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            int? row = textbox.GetValue(Grid.RowProperty) as int?;
            int? column = textbox.GetValue(Grid.ColumnProperty) as int?;

            connections[(int)column - 1][(int)row - 1].Text = textbox.Text; 
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void buttonDestructorSchema_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < schema.NumberOfElements; i++)
            {
                isBaseElement[i] = false;
            }
            elements.Clear();
            gridSchema.Children.Clear();
            selectedTextBlockElement = null;

            InitializeSchemaVNTU_Destructor();

            isBaseElement[3] = true;
            isBaseElement[6] = true;
            isBaseElement[11] = true;

            elements[3].Background = Brushes.DarkRed;
            elements[6].Background = Brushes.DarkRed;
            elements[11].Background = Brushes.DarkRed;

            for (int i = 0; i < schema.NumberOfElements; i++)
            {
                for (int j = i + 1; j < schema.NumberOfElements; j++)
                {
                    connections[i][j].Text = "0";
                }
            }

            connections[0][2].Text = "1";
            connections[0][10].Text = "1";

            connections[1][2].Text = "1";
            connections[1][6].Text = "1";

            connections[2][3].Text = "1";
            connections[2][7].Text = "3";

            connections[3][4].Text = "1";
            connections[3][5].Text = "1";

            connections[4][8].Text = "3";
            connections[4][11].Text = "1";

            connections[5][7].Text = "1";
            connections[5][8].Text = "1";
            connections[5][11].Text = "2";

            connections[6][7].Text = "1";

            connections[7][10].Text = "1";

            connections[8][9].Text = "1";

            connections[10][11].Text = "2";
        }

        private void buttoRandomSchema_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < schema.NumberOfElements; i++)
            {
                isBaseElement[i] = false;
            }
            elements.Clear();
            gridSchema.Children.Clear();
            selectedTextBlockElement = null;

            InitializeSchemaRandom();

            Random rand = new Random();
            for (int i = 0; i < schema.NumberOfElements; i++)
            {
                for (int j = i + 1; j < schema.NumberOfElements; j++)
                {
                    connections[i][j].Text = rand.Next(10).ToString();
                }
            }
        }

        private void buttonSetCommon_Click(object sender, RoutedEventArgs e)
        {
            int num = elements.IndexOf(selectedTextBlockElement);

            isBaseElement[num] = false;

            selectedTextBlockElement.Background = Brushes.LightGray;
        }

        private void buttonSetBase_Click(object sender, RoutedEventArgs e)
        {
            int num = elements.IndexOf(selectedTextBlockElement);

            isBaseElement[num] = true;

            selectedTextBlockElement.Background = Brushes.IndianRed;
        }

        private void buttonPassLab_Click(object sender, RoutedEventArgs e)
        {
            if (elements.Count == 0)
            {
                MessageBox.Show("Fill the schema and matrix", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            for (int i = 0; i < schema.NumberOfElements; i++)
            {
                for (int j = 0; j < schema.NumberOfElements; j++)
                {
                    schema.Connections[i][j] = Convert.ToInt32((connections[i][j].Text != "") ? connections[i][j].Text : "0");
                }
            }

            gridResult.Children.Clear();
            TextBlock textBlockPlaced;
            for (int i = 0; i < schema.NumberOfElements; i++)
            {
                int? row = elements[i].GetValue(Grid.RowProperty) as int?;
                int? column = elements[i].GetValue(Grid.ColumnProperty) as int?;
                schema.Elements[i] = new Element((int)column, (int)row);
                if (isBaseElement[i])
                {
                    textBlockPlaced = GenerateTextBlockElement(i);
                    textBlockPlaced.MouseDown -= schema_MouseDown;
                    textBlockPlaced.SetValue(Grid.RowProperty, schema.Elements[i].Y);
                    textBlockPlaced.SetValue(Grid.ColumnProperty, schema.Elements[i].X);
                    textBlockPlaced.Background = Brushes.DarkRed;
                    gridResult.Children.Add(textBlockPlaced);

                    schema.IsElementPlaced[i] = true;
                }
                else
                {
                    schema.IsElementPlaced[i] = false;
                }
            }

            int distanceOld = schema.CalculateDistancesOld();

            int num;
            TextBlock textBlockResult;
            while ((num = schema.GetBestElement()) != -1)
            {
                schema.SetPlace(num);
                textBlockResult = GenerateTextBlockElement(num);
                textBlockResult.MouseDown -= schema_MouseDown;
                textBlockResult.SetValue(Grid.RowProperty, schema.Elements[num].Y);
                textBlockResult.SetValue(Grid.ColumnProperty, schema.Elements[num].X);
                gridResult.Children.Add(textBlockResult);
            }

            int distanceNew = schema.CalculateDistancesNew();
            int effectiveness = schema.CalculateEffectiveness();

            string resultInfo = "Old lenght of connections: " + distanceOld.ToString();
            resultInfo += "\nNew lenght of connections: " + distanceNew.ToString();
            resultInfo += "\nEffectiviness: " + effectiveness.ToString() + "%";
            MessageBox.Show(resultInfo, "Result", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
