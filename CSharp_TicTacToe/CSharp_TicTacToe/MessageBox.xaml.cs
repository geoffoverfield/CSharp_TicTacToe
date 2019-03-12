// Geoff Overfield
// Basic 2 Player Tic Tac Toe game

using System;
using System.Windows;

namespace CSharp_TicTacToe
{
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MessageBox : Window
    {
        public bool bContinue = false;

        public MessageBox(string sTitle, string sMsg, bool bShowCancel = true)
        {
            InitializeComponent();

            IntPtr pIcon = Properties.Resources.X.ToBitmap().GetHbitmap();
            System.Windows.Media.Imaging.BitmapSource pBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(pIcon,
                IntPtr.Zero, System.Windows.Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            this.Icon = pBitmap;
            this.Title = sTitle;
            btnCancel.Visibility = bShowCancel ? Visibility.Visible : Visibility.Collapsed;

            tbMsg.Inlines.Clear();
            tbMsg.Text = sMsg;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            bContinue = true;
            this.Close();
        }
    }
}
