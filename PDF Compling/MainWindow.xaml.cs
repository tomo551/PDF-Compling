using PDF_Compling.Models;
using PDF_Compling.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.XPath;


namespace PDF_Compling
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel Vm => (MainViewModel)DataContext;
        private Point _startPoint;
        private bool _isDragging = false;
        private MergeItem? _draggedItem = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else if (e.Data.GetDataPresent(typeof(MergeItem)))
            {
                e.Effects = DragDropEffects.Move;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                Vm.AddFiles(files);
            }
            else if (e.Data.GetDataPresent(typeof(MergeItem)))
            {
                var draggedItem = e.Data.GetData(typeof(MergeItem)) as MergeItem;
                if (draggedItem != null)
                {
                    // リストの最後に移動
                    Vm.MoveItem(draggedItem, Vm.Items.Count);
                }
            }
        }

        // アイテムドラッグ開始処理
        private void Item_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(null);
            var border = sender as Border;
            _draggedItem = border?.DataContext as MergeItem;
        }

        private void Item_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && _draggedItem != null)
            {
                Point mousePos = e.GetPosition(null);
                Vector diff = _startPoint - mousePos;
                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    if (!_isDragging)
                    {
                        _isDragging = true;

                        // ドラッグデータを作成
                        var dragData = new DataObject(typeof(MergeItem), _draggedItem);

                        // ドラッグ操作を開始
                        DragDrop.DoDragDrop((DependencyObject)sender, dragData, DragDropEffects.Move);

                        _isDragging = false;
                        _draggedItem = null;
                    }
                }
            }
        }

        // アイテム上でのドラッグオーバー処理
        private void Item_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(MergeItem)))
            {
                e.Effects = DragDropEffects.Move;

                // 視覚的フィードバック
                var border = sender as Border;
                if (border != null)
                {
                    // ドロップ位置を判定して視覚的フィードバックを提供
                    Point dropPosition = e.GetPosition(border);
                    double itemHeight = border.ActualHeight;

                    if (dropPosition.Y < itemHeight / 2)
                    {
                        // 上半分の場合は上に挿入することを示す
                        border.BorderBrush = new SolidColorBrush(Colors.Blue);
                        border.BorderThickness = new Thickness(0, 3, 0, 0);
                    }
                    else
                    {
                        // 下半分の場合は下に挿入することを示す
                        border.BorderBrush = new SolidColorBrush(Colors.Blue);
                        border.BorderThickness = new Thickness(0, 0, 0, 3);
                    }
                }
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        // アイテム上でのドロップ処理
        private void Item_Drop(object sender, DragEventArgs e)
        {
            var border = sender as Border;
            if (border != null)
            {
                // 背景色とボーダーをリセット
                border.Background = Brushes.White;
                border.BorderBrush = new SolidColorBrush(Colors.Gray);
                border.BorderThickness = new Thickness(1);
            }

            if (e.Data.GetDataPresent(typeof(MergeItem)))
            {
                var draggedItem = e.Data.GetData(typeof(MergeItem)) as MergeItem;
                var targetItem = border?.DataContext as MergeItem;

                if (draggedItem != null && targetItem != null && draggedItem != targetItem)
                {
                    int targetIndex = Vm.Items.IndexOf(targetItem);

                    // ドロップ位置を判定
                    Point dropPosition = e.GetPosition(border);
                    double itemHeight = border.ActualHeight;

                    // ドロップ位置に基づいて挿入位置を決定
                    int insertIndex;
                    if (dropPosition.Y < itemHeight / 2)
                    {
                        // 上半分の場合は、targetItemの前に挿入
                        insertIndex = targetIndex;
                    }
                    else
                    {
                        // 下半分の場合は、targetItemの後に挿入
                        insertIndex = targetIndex + 1;
                    }

                    // ドラッグされたアイテムの現在のインデックスを取得
                    int draggedIndex = Vm.Items.IndexOf(draggedItem);

                    // ドラッグされたアイテムが挿入位置より前にある場合は、
                    // 削除後にインデックスが1つずれるため調整
                    if (draggedIndex < insertIndex)
                    {
                        insertIndex--;
                    }

                    Vm.MoveItem(draggedItem, insertIndex);
                }
            }
        }

        // マウスリーブ時に背景色をリセット
        private void Item_MouseLeave(object sender, MouseEventArgs e)
        {
            var border = sender as Border;
            if (border != null && !_isDragging)
            {
                border.Background = Brushes.White;
                border.BorderBrush = new SolidColorBrush(Colors.Gray);
                border.BorderThickness = new Thickness(1);
            }
        }
    }
}