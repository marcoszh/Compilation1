﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyCompilation
{
    public partial class MainForm : Form
    {
        string FilePath;
        CLexicalAnalysis myLexicalAnalysis = new CLexicalAnalysis();
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.FilePath = "";
            this.openFileDialog.Filter = "(*.c)|*.c|(*.cpp)|*.cpp";
            this.openFileDialog.Title = "Open C file";
        }

        //打开文件的响应
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.FilePath = this.openFileDialog.FileName;
                this.myRichTextBoxCode.richTextBoxMain.Text = "";
                this.myRichTextBoxCode.richTextBoxMain.LoadFile(this.FilePath, RichTextBoxStreamType.PlainText);
            }

        }

        //退出程序的响应
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        //菜单里词法分析的响应
        private void LAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listViewError.Items.Clear();
            this.listViewSymbol.Items.Clear();
            this.listViewToken.Items.Clear();
            if (this.myRichTextBoxCode.richTextBoxMain.Text != "")
            {
                myLexicalAnalysis.BeginAnalysis(this.myRichTextBoxCode.richTextBoxMain);     //词法分析
                //this.myRichTextBoxCode.richTextBoxMain.Text = "";
                //添加Error序列
                foreach (LexicalError theError in myLexicalAnalysis.MyLexicalErrorList)
                {
                    string errorMessage = "";
                    ListViewItem errorItem = new ListViewItem(theError.Value);
                    //errorItem.SubItems[0].Text = theError.Value;       //错误值
                    if (theError.Type == ErrorType.AbnormalEnd)
                        errorMessage = "Abnormal Ending";
                    else if (theError.Type == ErrorType.UnkownSymbols)
                        errorMessage = "Unrecognized Symbol";
                    else
                    {
                    }
                    errorItem.SubItems.Add(errorMessage);
                    errorItem.SubItems.Add(theError.LineCount.ToString());
                    listViewError.Items.Add(errorItem);
                }

                //添加Token序列
                foreach (MyLexicalToken theToken in myLexicalAnalysis.MyTokenList)
                {
                    ListViewItem tokenItem = new ListViewItem(theToken.Name);
                    //tokenItem.SubItems[0].Text = theToken.Name;       //token值
                    tokenItem.SubItems.Add(theToken.Type.ToString());
                    tokenItem.SubItems.Add(theToken.LineCount.ToString());
                    tokenItem.SubItems.Add(theToken.Code.ToString());
                    //if()
                    tokenItem.SubItems.Add(theToken.Others);
                    listViewToken.Items.Add(tokenItem);
                   
                }

                //添加SymbolList序列
                foreach (MyLexicalSymbol theSymbol in myLexicalAnalysis.MyLexicalSymbolList)
                {
                    ListViewItem tokenItem = new ListViewItem(theSymbol.Value);
                    //tokenItem.SubItems[0].Text = theSymbol.Value;       //符号值
                    tokenItem.SubItems.Add(theSymbol.Type.ToString());
                    tokenItem.SubItems.Add(theSymbol.LineCount.ToString());
                    tokenItem.SubItems.Add(theSymbol.Num.ToString());
                    this.listViewSymbol.Items.Add(tokenItem);
                }
            }

        }
    

        private string IntArrayToString(int[] intArray)
        {
            string myArrayStirng = "[";

            for (int i = 0; i < intArray.Length; i++)
            {
                myArrayStirng = myArrayStirng + intArray[i];

                if (i != intArray.Length - 1)
                {
                    myArrayStirng = myArrayStirng + ",";
                }
            }
            myArrayStirng = myArrayStirng + "]";
            return myArrayStirng;
        }

        private void listViewError_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected = listViewError.SelectedItems[0].Index;
            MessageBox.Show("" + selected);
            //int lineNum = listViewError.SubItems(selected);
        }



    }
}
