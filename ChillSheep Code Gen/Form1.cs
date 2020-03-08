using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace CodeGen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ulong codesAmmount;
        Random rnd = new Random();
        char[] allAlphaNumberic = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
        char[] anyLetter = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
        char[] bigLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        char[] smallLetters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        char[] numbers = "0123456789".ToCharArray();
        char[] discord = new char[16];

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.fileName == "")
            {
                MessageBox.Show("Filepath cannot be empty");
                Application.Exit();
            }
            try 
            {
                StreamWriter writer3 = new StreamWriter(Properties.Settings.Default.fileName);
                writer3.Close();
                Properties.Settings.Default.fileName = metroTextBox3.Text;
                Properties.Settings.Default.Save();
            }
            catch { MessageBox.Show("File " + Properties.Settings.Default.fileName + " is currently in use or name is illegal\nI have reset the filename for you :)");
                Properties.Settings.Default.fileName = "codes.txt"; metroTextBox3.Text = Properties.Settings.Default.fileName;
            }
            
            if (metroTextBox1.Text != "How Many Codes To Generate?")
                try
                {
                    if (UInt64.Parse(metroTextBox1.Text) > 0)
                    {
                        codesAmmount = UInt64.Parse(metroTextBox1.Text);
                        StreamWriter writer = new StreamWriter(Properties.Settings.Default.fileName);
                    if (discordToggle.Checked == true)
                    {
                        for (ulong i = 0; i < codesAmmount; i++)
                        {
                            if (i%10==0)
                                    if ((codesAmmount / 100) != 0)
                                    {
                                        metroProgressBar1.Value = (int)i / (int)(codesAmmount / 100);
                                    }
                            for (int j = 0; j < 16; j++)
                                discord[j] = allAlphaNumberic[rnd.Next(0, 62)]; //allAlphaNumberic.Length
                            writer.WriteLine("https://discordapp.com/gifts/" + new string(discord));
                        }
                            writer.Close();
                            writer.Dispose();
                            metroProgressBar1.Value = 100;
                            MessageBox.Show("DONE!");
                            metroProgressBar1.Value = 0;
                    }
                    else if (customToggle.Checked == true)
                    {
                        try
                        {
                            char[] custom = metroTextBox2.Text.ToCharArray();
                            char[] now = new char[metroTextBox2.Text.Length];
                            try
                                {
                                    for (uint i = 0; i < codesAmmount; i++)
                                    {
                                        if (i % 10 == 0)
                                            if ((codesAmmount / 100) != 0)
                                            {
                                            metroProgressBar1.Value = (int)i / (int)(codesAmmount / 100);
                                            }
                                        for (int j = 0; j < metroTextBox2.Text.Length; j++)
                                        {
                                            if (custom[j] == 'A')
                                                now[j] = allAlphaNumberic[rnd.Next(0, 62)];
                                            else if (custom[j] == 'N')
                                                now[j] = numbers[rnd.Next(0, 10)];
                                            else if (custom[j] == 'L')
                                                now[j] = bigLetters[rnd.Next(0, 26)];
                                            else if (custom[j] == 'l')
                                                now[j] = smallLetters[rnd.Next(0, 26)];
                                            else if (custom[j] == 'a')
                                                now[j] = anyLetter[rnd.Next(0, 52)];
                                            else if (custom[j] == '-')
                                                now[j] = '-';
                                            else
                                            {
                                                throw new System.InvalidOperationException("Incorrect string from input, the program will now exit!");
                                            }
                                        }
                                        writer.WriteLine(new string(now));
                                    }
                                }
                                catch (Exception ef) { MessageBox.Show(ef.ToString()); MessageBox.Show("Incorrect string for custom gen, the program will now exit!"); Application.Exit(); }
                                writer.Close();
                                writer.Dispose();
                                MessageBox.Show("DONE!");
                        }
                        catch { MessageBox.Show("Enter a smaller string in the custom box"); writer.Close();  }
                    }
                    else { MessageBox.Show("You need to enable a module!"); writer.Close(); }
                    }
                    else
                        MessageBox.Show("Put a number bigger than 0");
                }
                catch { MessageBox.Show("Please put a number, smaller than 18,446,744,073,709,551,615"); }
            else
                MessageBox.Show("Please input a number");
        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void discordToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (customToggle.Checked == true)
            {
                discordToggle.Checked = false;
            }
        }

        private void customToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (discordToggle.Checked == true || customToggle.Checked == false)
            {
                customToggle.Checked = false;
                metroTextBox2.Enabled = false;
                metroTextBox2.Visible = false;
                metroLabel2.Enabled = false;
                metroLabel2.Visible = false;
                saveCustomSettings.Enabled = false;
                saveCustomSettings.Visible = false;
            }
            else
            {
                if (Properties.Settings.Default.customString != "Enter The Custom String")
                    metroTextBox2.Text = Properties.Settings.Default.customString;
                metroTextBox2.Enabled = true;
                metroLabel2.Enabled = true;
                metroTextBox2.Visible = true;
                metroLabel2.Visible = true;
                saveCustomSettings.Enabled = true;
                saveCustomSettings.Visible = true;
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.customString = metroTextBox2.Text;
            Properties.Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            metroTextBox3.Text = Properties.Settings.Default.fileName;
        }

        private void metroTextBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
