using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using WMPLib;
using System.Threading;
using System.Runtime;
using System.Threading.Tasks;

namespace ATESTATver0
{
    public partial class Form1 : Form
    {
        /// <summary>
    ///CAREFUL! START TAB NEEDS TO HAVE CURRENT HIGHEST LEVEL (& PFP UP TO DATE AT ALL TIMES!)
        /// </summary>
        public string cale = System.IO.Directory.GetCurrentDirectory();
        string cale_lvl_locked;
        string cale_lvl_unlocked;
        public string id_user;
        public string lvl;
        int iteration = 0;
        string Q_date;
        string correct;
        int r, h, highest_level;
        int[] arr = new int[27];
        int theme;  //null/0 == light (default); 1 == dark
        string pfp;
        string pfp_path;
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        Stopwatch t_test = new Stopwatch();
        public Form1()
        {
            InitializeComponent();
            dtDataSet.EnforceConstraints = false;
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            lvl_1.SizeMode = PictureBoxSizeMode.Zoom;
            lvl_2.SizeMode = PictureBoxSizeMode.Zoom;
            lvl_3.SizeMode = PictureBoxSizeMode.Zoom;
            lvl_4.SizeMode = PictureBoxSizeMode.Zoom;
            lvl_5.SizeMode = PictureBoxSizeMode.Zoom;
            lvl_6.SizeMode = PictureBoxSizeMode.Zoom;
            lvl_7.SizeMode = PictureBoxSizeMode.Zoom;
            lvl_8.SizeMode = PictureBoxSizeMode.Zoom;
            lvl_9.SizeMode = PictureBoxSizeMode.Zoom;
            lvl_10.SizeMode = PictureBoxSizeMode.Zoom;
            lvl_11.SizeMode = PictureBoxSizeMode.Zoom;
            lvl_12.SizeMode = PictureBoxSizeMode.Zoom;
            lvl_13.SizeMode = PictureBoxSizeMode.Zoom;
            lvl_14.SizeMode = PictureBoxSizeMode.Zoom;
            lvl_15.SizeMode = PictureBoxSizeMode.Zoom;
            lvl_16.SizeMode = PictureBoxSizeMode.Zoom;
            lvl_17.SizeMode = PictureBoxSizeMode.Zoom;
            lvl_18.SizeMode = PictureBoxSizeMode.Zoom;
            lvl_19.SizeMode = PictureBoxSizeMode.Zoom;
            lvl_20.SizeMode = PictureBoxSizeMode.Zoom;
            Q.BackgroundImageLayout = ImageLayout.Stretch;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            button11.Enabled = false;
            button12.Enabled = false;
            button13.Enabled = false;
            button14.Enabled = false;
            button15.Enabled = false;
            button16.Enabled = false;
            button17.Enabled = false;
            button18.Enabled = false;
            button19.Enabled = false;
            button20.Enabled = false;
            button21.Enabled = false;
            button22.Enabled = false;
            button23.Enabled = false;
            button24.Enabled = false;
            button25.Enabled = false;
            button26.Enabled = false;

            label7.Visible = false;
            label8.Visible = false;
        }

        public void playSound(string audioPath)
        {
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = audioPath;
            wplayer.controls.play();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable t = dtDataSet.Alfabet;
            string path = t.Rows[0]["codem_path"].ToString();
            path = path.Remove(path.Length - 1);
            path = path.Substring(1);
            pictureBox1.Image=Image.FromFile(@path);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dtDataSet.Rezultate' table. You can move, or remove it, as needed.
            this.rezultateTableAdapter.Fill(this.dtDataSet.Rezultate);
            // TODO: This line of code loads data into the 'dtDataSet.Nivele' table. You can move, or remove it, as needed.
            this.niveleTableAdapter.Fill(this.dtDataSet.Nivele);
            // TODO: This line of code loads data into the 'dtDataSet.User' table. You can move, or remove it, as needed.
            this.userTableAdapter.Fill(this.dtDataSet.User);
            // TODO: This line of code loads data into the 'dtDataSet.Alfabet' table. You can move, or remove it, as needed.
            this.alfabetTableAdapter.Fill(this.dtDataSet.Alfabet);
            System.IO.Directory.SetCurrentDirectory(cale);
            cale_lvl_locked = cale + "\\lvls\\locked\\";
            cale_lvl_unlocked = cale + "\\lvls\\unlocked\\";
            cale = cale + "\\ALPHABET\\";
        }

        private void hint_Click(object sender, EventArgs e)
        {
            string path = alfabetTableAdapter.Get_codem(correct).ToString();
            path = cale + path;
            pictureBox1.Image = Image.FromFile(@path);
            h++;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            label7.Visible = false;
            label8.Text = "";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            string user = textBox3.Text;
            string pass = textBox4.Text;
            string confirm_pass = textBox5.Text;
            if (user.Length > 0 && pass.Length > 0 && confirm_pass.Length > 0)
            {
                if (pass == confirm_pass)
                {
                    try
                    {
                        userTableAdapter.Find_user(dtDataSet.User, user);
                        DataTable t = dtDataSet.User;
                        if (t.Rows[0]["id_user"] != null)
                        {
                            label7.Text = "Contul exista deja!";
                            label7.Visible = true;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        //Exception_catch - user doesn't exist
                        userTableAdapter.Creare_cont(user, pass,"default_pfp.png");
                        userTableAdapter.Update(dtDataSet.User);
                        tabControl1.SelectedTab = login_tab;
                        label8.Visible = false;
                    }
                }
                else
                {
                    label7.Text = "Confirmarea parolei esuata!";
                    label7.Visible = true;
                }
            }
            else
            {
                label7.Text = "Invalid input!";
                label7.Visible = true;
            }
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }
        /// <summary>
        /// User join
        /// </summary>
        private void button2_Click_1(object sender, EventArgs e)
        {
            label8.Text = "";
            label7.Text = "";
            string user = textBox1.Text;
            string pass = textBox2.Text;
            bool ok = false;
            if (user.Length > 0 && pass.Length > 0)
            {
                try
                {
                    userTableAdapter.Find_user(dtDataSet.User, user);
                    DataTable t = dtDataSet.User;
                    if (t.Rows[0]["id_user"] != null)
                        if (t.Rows[0]["pass"].ToString() == pass)
                            ok = true;
                        else
                        {
                            label8.Text = "Parola incorecta!";
                            label8.Visible = true;
                        }
                    
                }
                catch (IndexOutOfRangeException)
                {
                    //Exception_catch - user doesn't exist
                    label8.Text = "Cont inexistent!";
                    label8.Visible = true;
                }
            }
            else
            {
                label8.Visible = true;
                label8.Text = "Invalid input!";
            }
            if (ok == true)
            {
                id_user = user;
                rezultateTableAdapter.Completed_levels(dtDataSet.Rezultate, id_user);
                DataTable t=dtDataSet.Rezultate;
                try
                {
                    if (t.Rows[0]["niv"]!=null)
                    {
                        highest_level = Convert.ToInt16(t.Rows[0]["niv"]);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    highest_level = 0;
                }
                userTableAdapter.Theme_pref_check(dtDataSet.User, id_user);
                DataTable p = dtDataSet.User;
                try
                {
                    if (string.IsNullOrEmpty(p.Rows[0]["theme_pref"].ToString()) || Convert.ToByte(p.Rows[0]["theme_pref"]) == 0)
                    {
                        theme = 0; //light theme
                        toggleButton1.Checked = false;
                        Light_theme();

                    }
                    else
                    {
                        theme = Convert.ToByte(p.Rows[0]["theme_pref"]); //dark theme
                        toggleButton1.Checked = true;
                        Dark_theme();
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    theme = 0; //light theme
                    toggleButton1.Checked = false;
                    Light_theme();
                }
                pfp_path = System.IO.Directory.GetCurrentDirectory();
                pfp_path = pfp_path + "\\Imagini\\";
                userTableAdapter.Find_user(dtDataSet.User, id_user);
                DataTable q = dtDataSet.User;
                pfp = pfp_path + q.Rows[0]["pfp_path"].ToString();
                pictureBox3.Image = Image.FromFile(@pfp);
                label15.Text = id_user.ToString() + "\nLevel: " + highest_level.ToString();
                tabControl1.SelectedTab = Start_tab;
            }
        }
        /// <summary>
        /// user login
        /// </summary>
        private void button1_Click_2(object sender, EventArgs e)
        {
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            button11.Enabled = false;
            button12.Enabled = false;
            button13.Enabled = false;
            button14.Enabled = false;
            button15.Enabled = false;
            button16.Enabled = false;
            button17.Enabled = false;
            button18.Enabled = false;
            button19.Enabled = false;
            button20.Enabled = false;
            button21.Enabled = false;
            button22.Enabled = false;
            button23.Enabled = false;
            button24.Enabled = false;
            button25.Enabled = false;
            button26.Enabled = false;
            tabControl1.SelectedTab = Level_tab;
            if (level_enabling(button7))
            {
                button7.Enabled = true;
            }
            if (level_enabling(button8))
            {
                button8.Enabled = true;
            }
            if (level_enabling(button9))
            { 
                button9.Enabled = true;
            }
            if (level_enabling(button10))
            {
                button10.Enabled = true;
            }
            if (level_enabling(button11))
            {
                button11.Enabled = true;
            }
            if (level_enabling(button12))
            {
                button12.Enabled = true;
            }
            if (level_enabling(button13))
            {
                button13.Enabled = true;
            }
            if (level_enabling(button14))
            {
                button14.Enabled = true;
            }
            if (level_enabling(button15))
            {
                button15.Enabled = true;
            }
            if (level_enabling(button16))
            {
                button16.Enabled = true;
            }
            if (level_enabling(button17))
            {
                button17.Enabled = true;
            }
            if (level_enabling(button18))
            {
                button18.Enabled = true;
            }
            if (level_enabling(button19))
            {
                button19.Enabled = true;
            }
            if (level_enabling(button20))
            {
                button20.Enabled = true;
            }
            if (level_enabling(button21))
            {
                button21.Enabled = true;
            }
            if (level_enabling(button22))
            {
                button22.Enabled = true;
            }
            if (level_enabling(button23))
            {
                button23.Enabled = true;
            }
            if (level_enabling(button24))
            {
                button24.Enabled = true;
            }
            if (level_enabling(button25))
            {
                button25.Enabled = true;
            }
            if (level_enabling(button26))
            {
                button26.Enabled = true;
            }
            if (theme == 0)
                Light_theme();
            else
                Dark_theme();
        }
        // // // //
        bool level_enabling(Button b)
        {
            string lvl_button = b.Text.Substring(6);
            int level_crt = Convert.ToInt16(lvl_button);
            return (level_crt <= highest_level + 1);
        }
        string level_check(Button b)
        {
            string nivel = b.Text;
            nivel = nivel.Substring(6);
            return nivel;
        }
        string letters_used(Button b)
        {
            short n = Convert.ToInt16(level_check(b));
            string Q_date;
            niveleTableAdapter.Find_level(dtDataSet.Nivele, n);
            DataTable t = dtDataSet.Nivele;
            Q_date = t.Rows[0]["litere"].ToString();
            Q_date += Q_date + Q_date;
            return Q_date;
        }
        /// <summary>
        /// Checks what level user is accessing (button text) and returns letters used
        /// from database Nivele
        /// </summary>
        void New_level()
        {
            Array.Clear(arr, 0, 27);
            r = 0;
            h = 0;
            Q_date = "";
            label12.Text = "";
            label1.Text = "";
            iteration = 0;
            t_test.Reset();
            t_test.Start();
        }
        bool ok;
        private void ThreadFunc(object parameterObject)
        {
            label1.Text = ok.ToString();
            Thread.Sleep(1000);
        }

        void Keyboard_Click(object s, EventArgs eargs)
        {
            label1.Text = "";
            Button b = s as Button;
            ok = false;
            if (b.Name == correct)
            {
                ok = true;
            }
            if (ok == true)
                r++;
            label1.Text += ok.ToString();
            
            if (iteration < Q_date.Length - 1)
            {
                Countdown_feedback(t);
                iteration++;
                pic_prompt(arr[iteration], Q_date, out correct);
            }
            else
            {
                Thread th = new Thread(ThreadFunc);
                th.Start();
                th.Join();
                
                t_test.Stop();
                
                label12.Text += id_user;
                float punctaj = r * 100 / Q_date.Length - h*5;
                if (punctaj < 0)
                    punctaj = 0;
                rezultateTableAdapter.Find_previous(dtDataSet.Rezultate, id_user, Convert.ToInt16(lvl));
                DataTable t = dtDataSet.Rezultate;
                try
                {
                    if ((Convert.ToInt64(t.Rows[0]["pct"].ToString()) < punctaj)||
                        (Convert.ToInt64(t.Rows[0]["pct"].ToString()) == punctaj && Convert.ToInt64(t.Rows[0]["timp"]) > t_test.ElapsedMilliseconds))
                    {
                        rezultateTableAdapter.Update_pctj_timp(punctaj, t_test.ElapsedMilliseconds, id_user, Convert.ToInt16(lvl));
                        rezultateTableAdapter.Update(dtDataSet.Rezultate);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    rezultateTableAdapter.REZ_nivel(id_user, Convert.ToInt16(lvl), punctaj, t_test.ElapsedMilliseconds);
                    rezultateTableAdapter.Update(dtDataSet.Rezultate);
                    highest_level++;
                }
                rezultateTableAdapter.Find_previous(dtDataSet.Rezultate, id_user, Convert.ToInt16(lvl));
                tabControl1.SelectedTab = tabPage1;
                iteration = 0;
                label12.Text += "\n     Nivelul: " + lvl + "\n     Punctaj: " + punctaj.ToString() + "\n     Timp: " + t_test.ElapsedMilliseconds + " ms";
            }
        }
        
        void pic_prompt(int ind, string Q_date, out string l)
        {
            l = Q_date[ind].ToString();
            alfabetTableAdapter.Get_justmorse(dtDataSet.Alfabet, l);
            DataTable t = dtDataSet.Alfabet;
            string path = t.Rows[0]["just_morse"].ToString();
            path = cale + path;
            pictureBox1.Image = Image.FromFile(@path);
        }

        void Countdown_feedback(System.Windows.Forms.Timer t)
        {
            t.Stop();
            t.Start();
            t.Interval = 1000; // it will Tick in 3 seconds
            t.Tick += (s, e) =>
            {
                //label1.Hide();
                label1.Text = "";
                t.Stop();
            };
            
        }
        //keyboard buttons
        private void Q_Click(object sender, EventArgs e)
        {
            Keyboard_Click(Q, e);
        }

        private void E_Click(object sender, EventArgs e)
        {
            Keyboard_Click(E, e);
        }

        private void T_Click(object sender, EventArgs e)
        {
            Keyboard_Click(T, e);
        }

        private void W_Click(object sender, EventArgs e)
        {
            Keyboard_Click(W, e);
        }

        private void R_Click(object sender, EventArgs e)
        {
            Keyboard_Click(R, e);
        }

        private void Y_Click(object sender, EventArgs e)
        {
            Keyboard_Click(Y, e);
        }

        private void U_Click(object sender, EventArgs e)
        {
            Keyboard_Click(U, e);
        }

        private void I_Click(object sender, EventArgs e)
        {
            Keyboard_Click(I, e);
        }

        private void O_Click(object sender, EventArgs e)
        {
            Keyboard_Click(O, e);
        }

        private void P_Click(object sender, EventArgs e)
        {
            Keyboard_Click(P, e);
        }

        private void A_Click(object sender, EventArgs e)
        {
            Keyboard_Click(A, e);
        }

        private void S_Click(object sender, EventArgs e)
        {
            Keyboard_Click(S, e);
        }

        private void D_Click(object sender, EventArgs e)
        {
            Keyboard_Click(D, e);
        }

        private void F_Click(object sender, EventArgs e)
        {
            Keyboard_Click(F, e);
        }

        private void G_Click(object sender, EventArgs e)
        {
            Keyboard_Click(G, e);
        }

        private void H_Click(object sender, EventArgs e)
        {
            Keyboard_Click(H, e);
        }

        private void J_Click(object sender, EventArgs e)
        {
            Keyboard_Click(J, e);
        }

        private void K_Click(object sender, EventArgs e)
        {
            Keyboard_Click(K, e);
        }

        private void L_Click(object sender, EventArgs e)
        {
            Keyboard_Click(L, e);
        }

        private void Z_Click(object sender, EventArgs e)
        {
            Keyboard_Click(Z, e);
        }

        private void X_Click(object sender, EventArgs e)
        {
            Keyboard_Click(X, e);
        }

        private void C_Click(object sender, EventArgs e)
        {
            Keyboard_Click(C, e);
        }

        private void V_Click(object sender, EventArgs e)
        {
            Keyboard_Click(V, e);
        }

        private void B_Click(object sender, EventArgs e)
        {
            Keyboard_Click(B, e);
        }

        private void N_Click(object sender, EventArgs e)
        {
            Keyboard_Click(N, e);
        }

        private void M_Click(object sender, EventArgs e)
        {
            Keyboard_Click(M, e);
        }

        void Randomise(int[] arr)
        {
            Random r = new Random();
            for (int i = 0; i < Q_date.Length; i++)
            {
                bool apare;
                do
                {
                    arr[i] = r.Next(Q_date.Length);
                    apare = false;
                    for (int j = 0; j < i; j++)
                        if (arr[i] == arr[j])
                            apare = true;
                } while (apare == true);
            }

        }
        //Level buttons
        private void button7_Click(object sender, EventArgs e) //level1
        {
            New_level();
            lvl = level_check(button7);
            Q_date = letters_used(button7);
            tabControl1.SelectedTab = tabPage3;
            string l;
            Randomise(arr);
            pic_prompt(arr[iteration], Q_date, out l);
            correct = l;

        }

        private void button8_Click(object sender, EventArgs e) //level2
        {
            New_level();
            lvl = level_check(button8);
            Q_date = letters_used(button8);
            tabControl1.SelectedTab = tabPage3;
            string l;
            Randomise(arr);
            pic_prompt(arr[iteration], Q_date, out l);
            correct = l;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            New_level();
            lvl = level_check(button9);
            Q_date = letters_used(button9);
            tabControl1.SelectedTab = tabPage3;
            string l;
            Randomise(arr);
            pic_prompt(arr[iteration], Q_date, out l);
            correct = l;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            New_level();
            lvl = level_check(button10);
            Q_date = letters_used(button10);
            tabControl1.SelectedTab = tabPage3;
            string l;
            Randomise(arr);
            pic_prompt(arr[iteration], Q_date, out l);
            correct = l;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            New_level();
            lvl = level_check(button11);
            Q_date = letters_used(button11);
            tabControl1.SelectedTab = tabPage3;
            string l;
            Randomise(arr);
            pic_prompt(arr[iteration], Q_date, out l);
            correct = l;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            New_level();
            lvl = level_check(button12);
            Q_date = letters_used(button12);
            tabControl1.SelectedTab = tabPage3;
            string l;
            Randomise(arr);
            pic_prompt(arr[iteration], Q_date, out l);
            correct = l;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            New_level();
            lvl = level_check(button13);
            Q_date = letters_used(button13);
            tabControl1.SelectedTab = tabPage3;
            string l;
            Randomise(arr);
            pic_prompt(arr[iteration], Q_date, out l);
            correct = l;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            New_level();
            lvl = level_check(button14);
            Q_date = letters_used(button14);
            tabControl1.SelectedTab = tabPage3;
            string l;
            Randomise(arr);
            pic_prompt(arr[iteration], Q_date, out l);
            correct = l;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            New_level();
            lvl = level_check(button15);
            Q_date = letters_used(button15);
            tabControl1.SelectedTab = tabPage3;
            string l;
            Randomise(arr);
            pic_prompt(arr[iteration], Q_date, out l);
            correct = l;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            New_level();
            lvl = level_check(button16);
            Q_date = letters_used(button16);
            tabControl1.SelectedTab = tabPage3;
            string l;
            Randomise(arr);
            pic_prompt(arr[iteration], Q_date, out l);
            correct = l;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            New_level();
            lvl = level_check(button17);
            Q_date = letters_used(button17);
            tabControl1.SelectedTab = tabPage3;
            string l;
            Randomise(arr);
            pic_prompt(arr[iteration], Q_date, out l);
            correct = l;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            New_level();
            lvl = level_check(button18);
            Q_date = letters_used(button18);
            tabControl1.SelectedTab = tabPage3;
            string l;
            Randomise(arr);
            pic_prompt(arr[iteration], Q_date, out l);
            correct = l;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            New_level();
            lvl = level_check(button19);
            Q_date = letters_used(button19);
            tabControl1.SelectedTab = tabPage3;
            string l;
            Randomise(arr);
            pic_prompt(arr[iteration], Q_date, out l);
            correct = l;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            New_level();
            lvl = level_check(button20);
            Q_date = letters_used(button20);
            tabControl1.SelectedTab = tabPage3;
            string l;
            Randomise(arr);
            pic_prompt(arr[iteration], Q_date, out l);
            correct = l;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            New_level();
            lvl = level_check(button21);
            Q_date = letters_used(button21);
            tabControl1.SelectedTab = tabPage3;
            string l;
            Randomise(arr);
            pic_prompt(arr[iteration], Q_date, out l);
            correct = l;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            New_level();
            lvl = level_check(button22);
            Q_date = letters_used(button22);
            tabControl1.SelectedTab = tabPage3;
            string l;
            Randomise(arr);
            pic_prompt(arr[iteration], Q_date, out l);
            correct = l;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            New_level();
            lvl = level_check(button23);
            Q_date = letters_used(button23);
            tabControl1.SelectedTab = tabPage3;
            string l;
            Randomise(arr);
            pic_prompt(arr[iteration], Q_date, out l);
            correct = l;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            New_level();
            lvl = level_check(button24);
            Q_date = letters_used(button24);
            tabControl1.SelectedTab = tabPage3;
            string l;
            Randomise(arr);
            pic_prompt(arr[iteration], Q_date, out l);
            correct = l;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            New_level();
            lvl = level_check(button25);
            Q_date = letters_used(button25);
            tabControl1.SelectedTab = tabPage3;
            string l;
            Randomise(arr);
            pic_prompt(arr[iteration], Q_date, out l);
            correct = l;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            New_level();
            lvl = level_check(button26);
            Q_date = letters_used(button26);
            Q_date = Q_date.Substring(0, 26);
            tabControl1.SelectedTab = tabPage3;
            string l;
            Randomise(arr);
            pic_prompt(arr[iteration], Q_date, out l);
            correct = l;
        }

        /// <summary>
        /// /////// End level buttons
        /// </summary>
        /// 
        private void button28_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Start_tab;
            label15.Text = id_user.ToString() + "\nLevel: " + highest_level.ToString();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Level_tab;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Level_tab;
            //is there a better way? function maybe?
            /*button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            button11.Enabled = false;
            button12.Enabled = false;
            button13.Enabled = false;
            button14.Enabled = false;
            button15.Enabled = false;
            button16.Enabled = false;
            button17.Enabled = false;
            button18.Enabled = false;
            button19.Enabled = false;
            button20.Enabled = false;
            button21.Enabled = false;
            button22.Enabled = false;
            button23.Enabled = false;
            button24.Enabled = false;
            button25.Enabled = false;
            button26.Enabled = false;*/
            
            if (level_enabling(button7))
                button7.Enabled = true;
            if (level_enabling(button8))
                button8.Enabled = true;
            if (level_enabling(button9))
                button9.Enabled = true;
            if (level_enabling(button10))
                button10.Enabled = true;
            if (level_enabling(button11))
                button11.Enabled = true;
            if (level_enabling(button12))
                button12.Enabled = true;
            if (level_enabling(button13))
                button13.Enabled = true;
            if (level_enabling(button14))
                button14.Enabled = true;
            if (level_enabling(button15))
                button15.Enabled = true;
            if (level_enabling(button16))
                button16.Enabled = true;
            if (level_enabling(button17))
                button17.Enabled = true;
            if (level_enabling(button18))
                button18.Enabled = true;
            if (level_enabling(button19))
                button19.Enabled = true;
            if (level_enabling(button20))
                button20.Enabled = true;
            if (level_enabling(button21))
                button21.Enabled = true;
            if (level_enabling(button22))
                button22.Enabled = true;
            if (level_enabling(button23))
                button23.Enabled = true;
            if (level_enabling(button24))
                button24.Enabled = true;
            if (level_enabling(button25))
                button25.Enabled = true;
            if (level_enabling(button26))
                button26.Enabled = true;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = login_tab;
            textBox1.Clear();
            textBox2.Clear();
            label8.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Clear();
            listView2.Clear();
            listView3.Clear();
            listView4.Clear();
            listView5.Clear();
            listView6.Clear();
            listView7.Clear();
            listView8.Clear();
            listView9.Clear();
            listView10.Clear();
            listView11.Clear();
            listView12.Clear();
            listView13.Clear();
            listView14.Clear();
            listView15.Clear();
            listView16.Clear();
            listView17.Clear();
            listView18.Clear();
            listView19.Clear();
            listView20.Clear();
            previously_played(lvl_1,listView1);
            previously_played(lvl_2, listView2);
            previously_played(lvl_3, listView3);
            previously_played(lvl_4, listView4);
            previously_played(lvl_5, listView5);
            previously_played(lvl_6, listView6);
            previously_played(lvl_7, listView7);
            previously_played(lvl_8, listView8);
            previously_played(lvl_9, listView9);
            previously_played(lvl_10, listView10);
            previously_played(lvl_11, listView11);
            previously_played(lvl_12, listView12);
            previously_played(lvl_13, listView13);
            previously_played(lvl_14, listView14);
            previously_played(lvl_15, listView15);
            previously_played(lvl_16, listView16);
            previously_played(lvl_17, listView17);
            previously_played(lvl_18, listView18);
            previously_played(lvl_19, listView19);
            previously_played(lvl_20, listView20);
            tabControl1.SelectedTab = tabPage4;
        }

        void previously_played(PictureBox p, ListView l)
        {
            bool ok = false;
            string n = p.Name.Substring(4);
            rezultateTableAdapter.Find_previous(dtDataSet.Rezultate, id_user, Convert.ToInt16(n));
            DataTable t = dtDataSet.Rezultate;
            try
            {
                if (t.Rows[0]["pct"]!=null)
                    ok = true; //previously played //check completion status
            }
            catch(IndexOutOfRangeException)
            {
                ok = false;
            }
            string path;
            if (ok)
            {
                path = cale_lvl_unlocked + p.Name + ".png";
                l.Items.Add("Punctaj: " + t.Rows[0]["pct"].ToString());
                l.Items.Add("Timp: " + t.Rows[0]["timp"].ToString());
            }
            else
            {
                path = cale_lvl_locked + p.Name + ".png";
                l.Items.Add("LOCKED!");
            }
            p.Image = Image.FromFile(@path);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Start_tab;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            id_user = null;
            highest_level = 0;
            tabControl1.SelectedTab = login_tab;
        }

        private void button33_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            alfabetTableAdapter.Get_sound(dtDataSet.Alfabet, correct);
            DataTable t = dtDataSet.Alfabet;
            string path = t.Rows[0]["sound"].ToString();
            path = cale + path;
            playSound(@path);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage5;
            label16.Text = "";
            label20.Text = "";
            pictureBox2.Image=Image.FromFile(@pfp);
        }

        private void toggleButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleButton1.Checked == true)
            {
                theme = 1; //dark theme
                Dark_theme();
            }
            else
            {
                theme = 0; //light theme
                Light_theme();
            }
            userTableAdapter.UpdateTheme(Convert.ToByte(theme), id_user);
            userTableAdapter.Update(dtDataSet.User);
        }
        void Dark_theme()
        {
            Color c = Color.FromArgb(74, 74, 79);
            tabpage_color(tabPage1, c);
            tabpage_color(tabPage2, c);
            tabpage_color(tabPage3, c);
            tabpage_color(tabPage4, c);
            tabpage_color(tabPage5, c);
            tabpage_color(login_tab, c);
            tabpage_color(Start_tab, c);
            tabpage_color(Level_tab, c);
            Color c1 = Color.FromArgb(141, 141, 148);
            Color c2 = Color.FromArgb(181, 181, 185);
            button_color(button2, c1, c2);
            button_color(button3, c1, c2);
            button_color(Join_button, c1, c2);
            button_color(button31, c1, c2);
            button_color(button29, c1, c2);
            button_color(button27, c1, c2);
            button_color(hint, c1, c2);
            button_color(button1, c1, c2);
            button_color(button4, c1, c2);
            button_color(button5, c1, c2);
            button_color(button6, c1, c2);
            button_color(button33, c1, c2);
            button_color(button28, c1, c2);

            button_color(button7, c1, c2);
            button_color(button8, c1, c2);
            button_color(button9, c1, c2);
            button_color(button10, c1, c2);
            button_color(button11, c1, c2);
            button_color(button12, c1, c2);
            button_color(button13, c1, c2);
            button_color(button14, c1, c2);
            button_color(button15, c1, c2);
            button_color(button16, c1, c2);
            button_color(button17, c1, c2);
            button_color(button18, c1, c2);
            button_color(button19, c1, c2);
            button_color(button20, c1, c2);
            button_color(button21, c1, c2);
            button_color(button22, c1, c2);
            button_color(button23, c1, c2);
            button_color(button24, c1, c2);
            button_color(button25, c1, c2);
            button_color(button26, c1, c2);

            button_color(button30, c1, c2);
            button_color(button32, c1, c2);
            button_color(button34, c1, c2);

            c = Color.FromArgb(235, 219, 203);
            label1.ForeColor = c;
            label2.ForeColor = c;
            label3.ForeColor = c;
            label4.ForeColor = c;
            label5.ForeColor = c;
            label6.ForeColor = c;
            label7.ForeColor = c;
            label8.ForeColor = c;
            label9.ForeColor = c;
            label10.ForeColor = c;
            label11.ForeColor = c;
            label12.ForeColor = c;
            label13.ForeColor = c;
            label14.ForeColor = c;
            label15.ForeColor = c;
            label16.ForeColor = c;
            label17.ForeColor = c;
            label18.ForeColor = c;
            label19.ForeColor = c;
            label20.ForeColor = c;
        }
        void tabpage_color(TabPage tp, Color c)
        {
            tp.BackColor = c;
        }
        void button_color(Button b, Color c1, Color c2)
        {
            if (b.Enabled == true)
                b.BackColor = c1;
            else
                b.BackColor = c2;
        }
       
        void Light_theme()
        {
            //Color c = Color.FromArgb(250, 250, 198);
            Color c = Color.FromArgb(248, 248, 180);
            tabpage_color(tabPage1, c);
            tabpage_color(tabPage2, c);
            tabpage_color(tabPage3, c);
            tabpage_color(tabPage4, c);
            tabpage_color(tabPage5, c);
            tabpage_color(login_tab, c);
            tabpage_color(Start_tab, c);
            tabpage_color(Level_tab, c);
            Color c1 = Color.FromArgb(185, 177, 190);
            Color c2 = Color.FromArgb(235, 233, 236);
            button_color(button2, c1, c2);
            button_color(button3, c1, c2);
            button_color(Join_button, c1, c2);
            button_color(button31, c1, c2);
            button_color(button29, c1, c2);
            button_color(button27, c1, c2);
            button_color(hint, c1, c2);
            button_color(button1, c1, c2);
            button_color(button4, c1, c2);
            button_color(button5, c1, c2);
            button_color(button6, c1, c2);
            button_color(button33, c1, c2);
            button_color(button28, c1, c2);

            button_color(button7, c1, c2);
            button_color(button8, c1, c2);
            button_color(button9, c1, c2);
            button_color(button10, c1, c2);
            button_color(button11, c1, c2);
            button_color(button12, c1, c2);
            button_color(button13, c1, c2);
            button_color(button14, c1, c2);
            button_color(button15, c1, c2);
            button_color(button16, c1, c2);
            button_color(button17, c1, c2);
            button_color(button18, c1, c2);
            button_color(button19, c1, c2);
            button_color(button20, c1, c2);
            button_color(button21, c1, c2);
            button_color(button22, c1, c2);
            button_color(button23, c1, c2);
            button_color(button24, c1, c2);
            button_color(button25, c1, c2);
            button_color(button26, c1, c2);

            button_color(button30, c1, c2);
            button_color(button32, c1, c2);
            button_color(button34, c1, c2);


            c = Color.Black;
            label1.ForeColor = c;
            label2.ForeColor = c;
            label3.ForeColor = c;
            label4.ForeColor = c;
            label5.ForeColor = c;
            label6.ForeColor = c;
            label7.ForeColor = c;
            label8.ForeColor = c;
            label9.ForeColor = c;
            label10.ForeColor = c;
            label11.ForeColor = c;
            label12.ForeColor = c;
            label13.ForeColor = c;
            label14.ForeColor = c;
            label15.ForeColor = c;
            label16.ForeColor = c;
            label17.ForeColor = c;
            label18.ForeColor = c;
            label19.ForeColor = c;
            label20.ForeColor = c;
        }
        Color InvertColor(Color sourceColor)
        {
            return Color.FromArgb(255 - sourceColor.R,
                                  255 - sourceColor.G,
                                  255 - sourceColor.B);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Start_tab;
            
        }

        private void button35_Click(object sender, EventArgs e)
        {
            label16.Text = "";
            string path = System.IO.Directory.GetCurrentDirectory();

            openFileDialog1.InitialDirectory = path;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Fisiere jpg (*.jpg)|*.jpg|" +
                "Fisiere jpeg (*.jpeg)|*.jpeg|" +
                "Fisiere jfif (*.jfif)|*.jfif|" +
                "Fisiere png (*.png)|*.png|" +
                "All files (*.*)|*.*";
            openFileDialog1.DefaultExt = "jpg";

            openFileDialog1.ShowDialog();
            string fisier_selectat = openFileDialog1.FileName;
            
            if (fisier_selectat.Length > 0)
            {
                string f_name = System.IO.Path.GetFileName(fisier_selectat);
                string dest_File = pfp_path + f_name;

                try
                {
                    pictureBox2.Image = Image.FromFile(@fisier_selectat);
                    System.IO.File.Copy(fisier_selectat, dest_File);
                    userTableAdapter.Update_pfp(f_name, id_user);
                    userTableAdapter.Update(dtDataSet.User);
                    pictureBox3.Image = Image.FromFile(@dest_File);
                    pfp = dest_File;
                }
                catch (System.IO.IOException)
                {
                    pictureBox2.Image = Image.FromFile(@fisier_selectat);
                    userTableAdapter.Update_pfp(f_name, id_user);
                    userTableAdapter.Update(dtDataSet.User);
                    pictureBox3.Image = Image.FromFile(@dest_File);
                    pfp = dest_File;
                }
                catch (OutOfMemoryException)
                {
                    label16.Text = "Extensia nu este buna!";
                }
            }
        }

       
        private void button36_Click(object sender, EventArgs e)
        {
            string new_pass = textBox6.Text;
            string conf = textBox8.Text;
            if (new_pass.Length > 0 && conf.Length > 0)
            {
                if (new_pass == conf)
                {
                    userTableAdapter.UpdatePass(new_pass, id_user);
                    userTableAdapter.Update(dtDataSet.User);
                    label20.Text = "Succes!";
                }
                else
                {
                    label20.Text = "Confirmarea parolei a esuat!";
                }
            }
            else
            {
                label20.Text = "Invalid input!";
            }
            textBox6.Clear();
            textBox8.Clear();
        }

        ///<3 xoxo

    }
}
