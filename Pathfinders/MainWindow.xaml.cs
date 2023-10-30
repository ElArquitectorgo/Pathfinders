using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

namespace Pathfinders
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly int rows = 25, cols = 25;
        private Grid grid;
        private readonly Image[,] gridImages;
        private int[]? startPosition, endPosition;

        public MainWindow()
        {
            InitializeComponent();
            gridImages = SetupGrid();
        }

        private Image[,] SetupGrid()
        {
            Image[,] images = new Image[rows, cols];
            GameGrid.Rows = rows;
            GameGrid.Columns = cols;
            GameGrid.Width = GameGrid.Height * (cols / (double) rows);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Image image = new Image
                    {
                        Source = Images.Empty
                    };
                    images[r, c] = image;
                    GameGrid.Children.Add(image);
                }
            }

            return images;
        }

        private void SetupGridFromSource()
        {
            Node[,] nodes = new Node[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    bool walkable = gridImages[r, c].Source == Images.Wall ? false : true;
                    nodes[r, c] = new Node(r, c, walkable);
                }
            }

            grid = new Grid(nodes);
        }

        private void FindPathClick(object sender, RoutedEventArgs e)
        {
            SetupGridFromSource();
            Astart finder = new Astart(grid);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            List<Node> path = finder.findPath(startPosition, endPosition);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            TimeText.Text = String.Format("Time: {0} ms", ts.Milliseconds.ToString());
            ReDrawGrid(path);
        }

        private void ResetClick(object sender, RoutedEventArgs e)
        {
            startPosition = null;
            endPosition = null;
            TimeText.Text = "Time: 0 ms";

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    gridImages[r, c].Source = Images.Empty;
                }
            }
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            double squareSizeX = GameGrid.Width / cols;
            double squareSizeY = GameGrid.Height / rows;
            Point clickPosition = e.GetPosition(GameGrid);
            int r = (int)(clickPosition.Y / squareSizeY);
            int c = (int)(clickPosition.X / squareSizeX);
            
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                if (endPosition == null && gridImages[r, c].Source != Images.Start)
                {
                    gridImages[r, c].Source = Images.End;
                    endPosition = new int[] { r, c };
                }
                
            } 
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                if (gridImages[r, c].Source == Images.Start) startPosition = null;
                else if (gridImages[r, c].Source == Images.End) endPosition = null;
                gridImages[r, c].Source = Images.Empty;
            }
            else
            {
                if (startPosition == null && gridImages[r, c].Source != Images.End)
                {
                    gridImages[r, c].Source = Images.Start;
                    startPosition = new int[] { r, c };
                }
                else if (gridImages[r, c].Source != Images.End &&
                    gridImages[r, c].Source != Images.Start)
                {
                    gridImages[r, c].Source = Images.Wall;
                }
            }
        }

        private void ReDrawGrid(List<Node> nodes)
        {
            foreach (Node node in nodes)
            {
                gridImages[node.X, node.Y].Source = Images.Path;
            }
        }
    }
}
