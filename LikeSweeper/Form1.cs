using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LikeSweeper
{
    public partial class Form1 : Form
    {
        public WebBrowser browser = new WebBrowser();
        private Button stopBtn;
        public TextBox srcBox = new TextBox();
        TextBox outBox = new TextBox();
        TextBox instBox = new TextBox();
        int numberOfLikes;
        Timer MyTimer = new Timer();
        int timerCount = 1;
        int timerStopCount = 0;
        int minimumRows = 2;

        public Form1()
        {
            InitializeComponent();

            Text = "Like Sweeper";
            Size = new System.Drawing.Size(1300, 700);
            FormBorderStyle = FormBorderStyle.FixedDialog;

            //MainTab
            TabControl MainTab = new TabControl();
            MainTab.Size = new System.Drawing.Size(1282, 659);
            MainTab.Location = new Point(2, 2);
            TabPage webTab = new TabPage("Web");
            TabPage sourceTab = new TabPage("Source");
            TabPage textTab = new TabPage("Instructions");

            //Instruction Tab
            instBox.Size = new System.Drawing.Size(1275, 640);
            instBox.Multiline = true;
            instBox.ReadOnly = true;
            instBox.Text =  "1. log into facebook\r\n";
            instBox.Text += "2. go to  activity page\r\n";
            instBox.Text += "3. click  on 'likes' section\r\n";
            instBox.Text += "4. click sweep\r\n";
            textTab.Controls.Add(instBox);


            //Source Tab
            srcBox.Size = new System.Drawing.Size(1275, 640);
            srcBox.Multiline = true;
            srcBox.ScrollBars = ScrollBars.Vertical;
            sourceTab.Controls.Add(srcBox);

            //Web Tab - sweep button
            Button sweep = new Button();
            sweep.Text = "Sweep";
            sweep.Location = new Point(5, 5);
            sweep.Click += new EventHandler(this.sweep_Click);
            webTab.Controls.Add(sweep);

            //Web Tab - stop button
            stopBtn = new Button();
            stopBtn.Text = "Stop";
            stopBtn.Location = new Point(100, 5);
            stopBtn.Click += new EventHandler(this.stopBtn_Click);
            webTab.Controls.Add(stopBtn);
            
            //Web Tab - outbox
            outBox.Size = new System.Drawing.Size(100, 25);
            outBox.Location = new Point(200, 5);
            outBox.Multiline = false;
            outBox.ScrollBars = ScrollBars.Vertical;
            outBox.ReadOnly = true;
            webTab.Controls.Add(outBox);

            //Web Tab - browser
            browser.Location = new Point(2, 33);
            browser.Size = new System.Drawing.Size(1275, 600);
            browser.ScriptErrorsSuppressed = true;
            webTab.Controls.Add(browser);

            MainTab.TabPages.Add(webTab);
            MainTab.TabPages.Add(sourceTab);
            MainTab.TabPages.Add(textTab);
            Controls.Add(MainTab);




        }

        private void sweep_Click(object sender, EventArgs e)
        {

            MyTimer.Interval = (1500);
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Start();

        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            MyTimer.Stop();

        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            browser.Document.Body.ScrollIntoView(true);
            numberOfLikes = ElementParser.ReturnTR(browser).Count;
            outBox.Text = numberOfLikes.ToString();
            outBox.Text = outBox.Text + " " + timerCount.ToString();
            timerStopCount = numberOfLikes;

            if(timerStopCount > minimumRows)
            {
                ElementParser.RemoveNextLike(browser, srcBox, timerCount);
                timerCount++;
            }
            else
            {
                browser.Document.Body.ScrollIntoView(false);
                timerCount = 1;
            }
        }
    }
}
