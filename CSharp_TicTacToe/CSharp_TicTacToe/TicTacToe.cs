// Geoff Overfield
// Basic 2 Player Tic Tac Toe game

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CSharp_TicTacToe
{
    public partial class TicTacToe : Form
    {
        enum Winner { Player1, Player2};
        enum PlayerTurn { Player1, Player2 };
        Bitmap[] player1Imgs, player2Imgs;
        Random m_pRandom;
        Dictionary<string, string[]> dRowsAndColumns;

        Winner eWinner;
        PlayerTurn eTurn;
        bool m_bGameStarted, m_bGameWon;
        
        public TicTacToe()
        {
            InitializeComponent();
            loadImages();
            createDictionary();

            m_pRandom = new Random();
            m_bGameStarted = m_bGameWon = false;
        }

        private bool checkDiagonals()
        {
            string[] arLeftToRight = new string[3];
            string[] arRightToLeft = new string[3];

            arLeftToRight[0] = dRowsAndColumns["row1"][0];
            arLeftToRight[1] = dRowsAndColumns["row2"][1];
            arLeftToRight[2] = dRowsAndColumns["row3"][2];
            arRightToLeft[0] = dRowsAndColumns["row1"][2];
            arRightToLeft[1] = dRowsAndColumns["row2"][1];
            arRightToLeft[2] = dRowsAndColumns["row3"][0];

            foreach (var sVal in arLeftToRight)
                if (string.IsNullOrEmpty(sVal)) return false;
            foreach (var sVal in arRightToLeft)
                if (string.IsNullOrEmpty(sVal)) return false;

            if (arLeftToRight[0] == arLeftToRight[1] &&
                arLeftToRight[1] == arLeftToRight[2]) return true;
            else if (arRightToLeft[0] == arRightToLeft[1] &&
                arRightToLeft[1] == arRightToLeft[2]) return true;

            return false;
        }

        private void createDictionary()
        {
            dRowsAndColumns = new Dictionary<string, string[]>();
            List<string> lRowsAndColumns = new List<string>()
            {
                "row1",
                "row2",
                "row3",
                "column1",
                "column2",
                "column3"
            };
            foreach (var sValue in lRowsAndColumns)
                dRowsAndColumns.Add(sValue, new string[3]);
        }

        private void loadImages()
        {
            player1Imgs = new Bitmap[6];
            player2Imgs = new Bitmap[6];

            int i = 0;
            player1Imgs[i++] = Properties.Resources.O1;
            player1Imgs[i++] = Properties.Resources.O2;
            player1Imgs[i++] = Properties.Resources.O3;
            player1Imgs[i++] = Properties.Resources.O4;
            player1Imgs[i++] = Properties.Resources.O5;
            player1Imgs[i++] = Properties.Resources.O6;
            i = 0;
            player2Imgs[i++] = Properties.Resources.X1;
            player2Imgs[i++] = Properties.Resources.X2;
            player2Imgs[i++] = Properties.Resources.X3;
            player2Imgs[i++] = Properties.Resources.X4;
            player2Imgs[i++] = Properties.Resources.X5;
            player2Imgs[i++] = Properties.Resources.X6;
        }

        private void nextPlayer()
        {
            if (eTurn == PlayerTurn.Player1)
                eTurn = PlayerTurn.Player2;
            else eTurn = PlayerTurn.Player1;
        }

        private void updateTurnLabel()
        {
            if (!m_bGameWon)
            {
                switch (eTurn)
                {
                    case PlayerTurn.Player1:
                        lblPlayerTurn.Text = "Player 1s Turn";
                        break;
                    case PlayerTurn.Player2:
                        lblPlayerTurn.Text = "Player 2s Turn";
                        break;
                }
            }
            else
            {
                switch (eWinner)
                {
                    case Winner.Player1:
                        lblPlayerTurn.Text = "Player 1 Wins!";
                        break;
                    case Winner.Player2:
                        lblPlayerTurn.Text = "Player 2 Wins!";
                        break;
                }
            }
        }

        private bool winnerExists()
        {
            foreach (var kvp in dRowsAndColumns)
            {
                bool bSkip = false;
                foreach (var sVal in kvp.Value)
                    if (string.IsNullOrEmpty(sVal))
                    {
                        bSkip = true;
                        break;
                    }
                if (bSkip) continue;
                if (kvp.Value[0] == kvp.Value[1] &&
                    kvp.Value[1] == kvp.Value[2])
                {
                    m_bGameWon = true;
                    break;
                }
            }
            if (!m_bGameWon) m_bGameWon = checkDiagonals();

            return m_bGameWon;
        }
        
        private void lbl1_Click(object sender, EventArgs e)
        {
            if (lbl1.Image != null || !m_bGameStarted || m_bGameWon) return;

            switch (eTurn)
            {
                case PlayerTurn.Player1:
                    lbl1.Image = player1Imgs[m_pRandom.Next(0, 6)];
                    dRowsAndColumns["row1"][0] = dRowsAndColumns["column1"][0] = "x";
                    break;
                case PlayerTurn.Player2:
                    lbl1.Image = player2Imgs[m_pRandom.Next(0, 6)];
                    dRowsAndColumns["row1"][0] = dRowsAndColumns["column1"][0] = "o";
                    break;
            }
            nextPlayer();
            if (winnerExists())
            {
                m_bGameStarted = false;
                m_bGameWon = true;
                eWinner = (Winner)eTurn;
            }
            updateTurnLabel();
        }

        private void lbl2_Click(object sender, EventArgs e)
        {
            if (lbl2.Image != null || !m_bGameStarted || m_bGameWon) return;

            switch (eTurn)
            {
                case PlayerTurn.Player1:
                    lbl2.Image = player1Imgs[m_pRandom.Next(0, 6)];
                    dRowsAndColumns["row1"][1] = dRowsAndColumns["column2"][0] = "x";
                    break;
                case PlayerTurn.Player2:
                    lbl2.Image = player2Imgs[m_pRandom.Next(0, 6)];
                    dRowsAndColumns["row1"][1] = dRowsAndColumns["column2"][0] = "o";
                    break;
            }
            if (winnerExists())
            {
                m_bGameStarted = false;
                m_bGameWon = true;
                eWinner = (Winner)eTurn;
            }
            nextPlayer();
            updateTurnLabel();
        }

        private void lbl3_Click(object sender, EventArgs e)
        {
            if (lbl3.Image != null || !m_bGameStarted || m_bGameWon) return;

            switch (eTurn)
            {
                case PlayerTurn.Player1:
                    lbl3.Image = player1Imgs[m_pRandom.Next(0, 6)];
                    dRowsAndColumns["row1"][2] = dRowsAndColumns["column3"][0] = "x";
                    break;
                case PlayerTurn.Player2:
                    lbl3.Image = player2Imgs[m_pRandom.Next(0, 6)];
                    dRowsAndColumns["row1"][2] = dRowsAndColumns["column3"][0] = "o";
                    break;
            }
            nextPlayer();
            if (winnerExists())
            {
                m_bGameStarted = false;
                m_bGameWon = true;
                eWinner = (Winner)eTurn;
            }
            updateTurnLabel();
        }

        private void lbl4_Click(object sender, EventArgs e)
        {
            if (lbl4.Image != null || !m_bGameStarted || m_bGameWon) return;

            switch (eTurn)
            {
                case PlayerTurn.Player1:
                    lbl4.Image = player1Imgs[m_pRandom.Next(0, 6)];
                    dRowsAndColumns["row2"][0] = dRowsAndColumns["column1"][1] = "x";
                    break;
                case PlayerTurn.Player2:
                    lbl4.Image = player2Imgs[m_pRandom.Next(0, 6)];
                    dRowsAndColumns["row2"][0] = dRowsAndColumns["column1"][1] = "o";
                    break;
            }
            nextPlayer();
            if (winnerExists())
            {
                m_bGameStarted = false;
                m_bGameWon = true;
                eWinner = (Winner)eTurn;
            }
            updateTurnLabel();
        }

        private void lbl5_Click(object sender, EventArgs e)
        {
            if (lbl5.Image != null || !m_bGameStarted || m_bGameWon) return;

            switch (eTurn)
            {
                case PlayerTurn.Player1:
                    lbl5.Image = player1Imgs[m_pRandom.Next(0, 6)];
                    dRowsAndColumns["row2"][1] = dRowsAndColumns["column2"][1] = "x";
                    break;
                case PlayerTurn.Player2:
                    lbl5.Image = player2Imgs[m_pRandom.Next(0, 6)];
                    dRowsAndColumns["row2"][1] = dRowsAndColumns["column2"][1] = "o";
                    break;
            }
            nextPlayer();
            if (winnerExists())
            {
                m_bGameStarted = false;
                m_bGameWon = true;
                eWinner = (Winner)eTurn;
            }
            updateTurnLabel();
        }

        private void lbl6_Click(object sender, EventArgs e)
        {
            if (lbl6.Image != null || !m_bGameStarted || m_bGameWon) return;

            switch (eTurn)
            {
                case PlayerTurn.Player1:
                    lbl6.Image = player1Imgs[m_pRandom.Next(0, 6)];
                    dRowsAndColumns["row2"][2] = dRowsAndColumns["column3"][1] = "x";
                    break;
                case PlayerTurn.Player2:
                    lbl6.Image = player2Imgs[m_pRandom.Next(0, 6)];
                    dRowsAndColumns["row2"][2] = dRowsAndColumns["column3"][1] = "o";
                    break;
            }
            nextPlayer();
            if (winnerExists())
            {
                m_bGameStarted = false;
                m_bGameWon = true;
                eWinner = (Winner)eTurn;
            }
            updateTurnLabel();
        }

        private void lbl7_Click(object sender, EventArgs e)
        {
            if (lbl7.Image != null || !m_bGameStarted || m_bGameWon) return;

            switch (eTurn)
            {
                case PlayerTurn.Player1:
                    lbl7.Image = player1Imgs[m_pRandom.Next(0, 6)];
                    dRowsAndColumns["row3"][0] = dRowsAndColumns["column1"][2] = "x";
                    break;
                case PlayerTurn.Player2:
                    lbl7.Image = player2Imgs[m_pRandom.Next(0, 6)];
                    dRowsAndColumns["row3"][0] = dRowsAndColumns["column1"][2] = "o";
                    break;
            }
            nextPlayer();
            if (winnerExists())
            {
                m_bGameStarted = false;
                m_bGameWon = true;
                eWinner = (Winner)eTurn;
            }
            updateTurnLabel();
        }

        private void lbl8_Click(object sender, EventArgs e)
        {
            if (lbl8.Image != null || !m_bGameStarted || m_bGameWon) return;

            switch (eTurn)
            {
                case PlayerTurn.Player1:
                    lbl8.Image = player1Imgs[m_pRandom.Next(0, 6)];
                    dRowsAndColumns["row3"][1] = dRowsAndColumns["column2"][2] = "x";
                    break;
                case PlayerTurn.Player2:
                    lbl8.Image = player2Imgs[m_pRandom.Next(0, 6)];
                    dRowsAndColumns["row3"][1] = dRowsAndColumns["column2"][2] = "o";
                    break;
            }
            nextPlayer();
            if (winnerExists())
            {
                m_bGameStarted = false;
                m_bGameWon = true;
                eWinner = (Winner)eTurn;
            }
            updateTurnLabel();
        }

        private void lbl9_Click(object sender, EventArgs e)
        {
            if (lbl9.Image != null || !m_bGameStarted || m_bGameWon) return;

            switch (eTurn)
            {
                case PlayerTurn.Player1:
                    lbl9.Image = player1Imgs[m_pRandom.Next(0, 6)];
                    dRowsAndColumns["row3"][2] = dRowsAndColumns["column3"][2] = "x";
                    break;
                case PlayerTurn.Player2:
                    lbl9.Image = player2Imgs[m_pRandom.Next(0, 6)];
                    dRowsAndColumns["row3"][2] = dRowsAndColumns["column3"][2] = "o";
                    break;
            }
            nextPlayer();
            if (winnerExists())
            {
                m_bGameStarted = false;
                m_bGameWon = true;
                eWinner = (Winner)eTurn;
            }
            updateTurnLabel();
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!m_bGameStarted)
            {
                m_bGameStarted = true;
                eTurn = PlayerTurn.Player1;
            }
            else
            {
                if (!m_bGameWon)
                {
                    //Message to warn of game restart.
                    if (MessageBox.Show("Game in Progress!", "You are about to restart your game.  Do you wish to continue?",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel) return;
                }
                m_bGameWon = false;
                m_bGameStarted = true;
                eTurn = PlayerTurn.Player1;
                createDictionary();
                lbl1.Image = lbl2.Image = lbl3.Image = lbl4.Image = lbl5.Image =
                    lbl6.Image = lbl7.Image = lbl8.Image = lbl9.Image = null;

            }
            updateTurnLabel();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void creditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sMsg = "Created, Designed and Programmed by Geoff Overfield.\n(c)[BOSS] Games 2015";
            MessageBox.Show(sMsg);
        }
    }
}
