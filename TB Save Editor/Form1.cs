using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TB_Save_Editor
{


    public partial class Form1 : Form
    {
        internal sealed class NativeMethods
        {
            [DllImport("kernel32.dll")]
            public static extern bool AllocConsole();

            [DllImport("kernel32.dll")]
            public static extern bool FreeConsole();
        }

        private string inputDataFilePath;
        private string outputDataFilePath;
        private string inputTrackingFilePath;
        private string outputTrackingFilePath;
        private string damageAndSpeedFolderPath;
        private string trackingRecreate;
        private string arrayToBinaryInput;

        public Form1()
        {
            InitializeComponent();

            inputDataFilePath = @"input\zasilky.txt";
            outputDataFilePath = @"output\zasilky.txt";

            inputTrackingFilePath = @"input\upl_";
            outputTrackingFilePath = @"output\upl_";

            damageAndSpeedFolderPath = @"damage&speedFix\";

            trackingRecreate = @"Tracking\upl_";

            arrayToBinaryInput = @"ArrayToBinaryFile\inputList.txt";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Height = 206;
            //this.Width = 475;

            DateTime nowDateTime = DateTime.Now;
            numericUpDown1.Value = nowDateTime.Year;
            numericUpDown2.Value = nowDateTime.Month;
            numericUpDown3.Value = nowDateTime.Day;
            numericUpDown4.Value = nowDateTime.Hour;
            numericUpDown5.Value = nowDateTime.Minute;
            numericUpDown6.Value = nowDateTime.Second;

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DateTime nowDateTime = DateTime.Now;
            numericUpDown1.Value = nowDateTime.Year;
            numericUpDown2.Value = nowDateTime.Month;
            numericUpDown3.Value = nowDateTime.Day;
            numericUpDown4.Value = nowDateTime.Hour;
            numericUpDown5.Value = nowDateTime.Minute;
            numericUpDown6.Value = nowDateTime.Second;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!File.Exists(inputDataFilePath))
            {
                MessageBox.Show("data.txt file can't find", "Error");
            }
            else
            {
                dataFileDecoder(inputDataFilePath, outputDataFilePath);
            }
        }

        public void dataFileDecoder(string filename, string outputFileName)
        {


            string[] s = Decrypt(File.ReadAllText(filename)).Trim('\n', '\r').Split('\n');

            //System.Console.WriteLine(s.Length);

            //int jocCount = s.Length / 42;

            string buildStr = "";
            for (int i = 0; i < s.Length; i+=42)
            {

                buildStr += "Save & " + s[0 + i];
                buildStr += "start city & " + s[1 + i];
                buildStr += "finish city & " + s[2 + i];
                buildStr += "cargo name & " + s[3 + i];
                buildStr += "cargo & " + s[4 + i];
                buildStr += "profit & " + s[5 + i];
                buildStr += "Driven Distance & " + s[6 + i];
                buildStr += "Initial Company & " + s[7 + i];
                buildStr += "Target Company & " + s[8 + i];
                buildStr += "liters_difference & " + s[9 + i];
                buildStr += "liters_price_difference & " + s[10 + i];
                buildStr += "penalty & " + s[11 + i];
                buildStr += "xp & " + s[12 + i];
                buildStr += "trailer_damage & " + s[13 + i];
                buildStr += "Time Remaining & " + s[14 + i];
                buildStr += "urgency & " + s[15 + i];
                buildStr += "autopark & " + s[16 + i];
                buildStr += "difficult_unloading & " + s[17 + i];
                buildStr += "quick_job & " + s[18 + i];
                buildStr += "total_profit & " + s[19 + i];
                buildStr += "readings & " + s[20 + i];
                buildStr += "starttime & " + s[21 + i];
                buildStr += "Time Taken & " + s[22 + i];
                buildStr += "truck & " + s[23 + i];
                buildStr += "planned_distance & " + s[24 + i];
                buildStr += "timestamp & " + s[25 + i];
                buildStr += "ok & " + s[26 + i];
                buildStr += "game & " + s[27 + i];
                buildStr += "Date & " + s[28 + i];
                buildStr += "job id & " + s[29 + i];
                buildStr += "TB version & " + s[30 + i];
                buildStr += "Maximal Reached Speed & " + s[31 + i];
                buildStr += "used_fuel & " + s[32 + i];
                buildStr += "Game version & " + s[33 + i];
                buildStr += "weight kg & " + s[34 + i];
                buildStr += "truck license plate & " + s[35 + i];
                buildStr += "trailer license plate & " + s[36 + i];
                buildStr += "profile name(hex) & " + s[37 + i];
                buildStr += "user_id & " + s[38 + i];
                buildStr += "planned_distance_navigation & " + s[39 + i];
                buildStr += "startDate & " + s[40 + i];
                buildStr += "scale & " + s[41 + i];
            }

            File.WriteAllText(outputFileName, buildStr);
        }

        public string Encrypt(string clearText)
        {
            string password = "ar2z9qmj15c";
            byte[] bytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, new byte[13]
                {
                    (byte) 73,
                    (byte) 118,
                    (byte) 97,
                    (byte) 110,
                    (byte) 32,
                    (byte) 77,
                    (byte) 101,
                    (byte) 100,
                    (byte) 118,
                    (byte) 101,
                    (byte) 100,
                    (byte) 101,
                    (byte) 118
                });
                aes.Key = rfc2898DeriveBytes.GetBytes(32);
                aes.IV = rfc2898DeriveBytes.GetBytes(16);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, aes.CreateEncryptor(),
                        CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(bytes, 0, bytes.Length);
                        cryptoStream.Close();
                    }

                    clearText = Convert.ToBase64String(memoryStream.ToArray());
                }
            }

            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            if (cipherText == "" || cipherText == null)
                return "";
            try
            {
                string password = "ar2z9qmj15c";
                cipherText = cipherText.Replace(" ", "+");
                byte[] buffer = Convert.FromBase64String(cipherText);
                using (Aes aes = Aes.Create())
                {
                    Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, new byte[13]
                    {
                        (byte) 73,
                        (byte) 118,
                        (byte) 97,
                        (byte) 110,
                        (byte) 32,
                        (byte) 77,
                        (byte) 101,
                        (byte) 100,
                        (byte) 118,
                        (byte) 101,
                        (byte) 100,
                        (byte) 101,
                        (byte) 118
                    });
                    aes.Key = rfc2898DeriveBytes.GetBytes(32);
                    aes.IV = rfc2898DeriveBytes.GetBytes(16);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream,
                            aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(buffer, 0, buffer.Length);
                            cryptoStream.Close();
                        }

                        cipherText = Encoding.Unicode.GetString(memoryStream.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                //Log.update("Chyba " + ex?.ToString());
            }

            return cipherText;
        }

        public void editedDataFileEncoder(string output)
        {
            string[] s = File.ReadAllText(output).Trim('\n', '\r').Split('\n', '\r');
            string buildStr = "";

            try
            {
                foreach (string str in s)
                {
                    buildStr += str.Trim('\n', '\r').Split('&')[1].Trim('\n', '\r', ' ') + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            

            File.WriteAllText(output, Encrypt(buildStr));

        }

        public void createTrackingFile(string inputfile, string new_ID, bool timeGapTick)
        {
            List<float> num1 = new List<float>();
            List<float> num2 = new List<float>();
            List<short> num3 = new List<short>();
            List<short> num4 = new List<short>();
            List<short> num5 = new List<short>();
            List<int> date = new List<int>();

            BinaryReader br = new BinaryReader(File.Open(inputfile, FileMode.Open));
            while (true)
            {
                try
                {

                    num1.Add(br.ReadSingle());
                    num2.Add(br.ReadSingle());
                    num3.Add(br.ReadInt16());
                    num4.Add(br.ReadInt16());
                    num5.Add(br.ReadInt16());
                    date.Add(br.ReadInt32());
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    break;
                }

            }

            br.Close();

            int year = Decimal.ToInt32(numericUpDown1.Value);
            int month = Decimal.ToInt32(numericUpDown2.Value);
            int day = Decimal.ToInt32(numericUpDown3.Value);
            int hour = Decimal.ToInt32(numericUpDown4.Value);
            int minute = Decimal.ToInt32(numericUpDown5.Value);
            int second = Decimal.ToInt32(numericUpDown6.Value);
            int milisecond = Decimal.ToInt32(DateTime.Now.Millisecond);


            DateTime dateTime = new DateTime(year, month, day, hour, minute, second, milisecond);

            int timeGap = Convert.ToInt32((dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds) -
                          date[0];

            int timeDifferenceStartAndStop = date[date.Count - 1] - date[0];

            for (int i = 0; i < date.Count; i++)
            {
                date[i] = date[i] + timeGap - timeDifferenceStartAndStop;
            }

            if (timeGapTick)
            {
                for (int i = date.Count-1; i>0; i--)
                {
                    date[i - 1] = date[i] - 1;
                }
            }


            BinaryWriter bw =
                new BinaryWriter((Stream) new FileStream(outputTrackingFilePath + new_ID + ".bin", FileMode.Create));
            for (int i = 0; i < num1.Count; i++)
            {
                bw.Write(num1[i]);
                bw.Write(num2[i]);
                bw.Write(num3[i]);
                bw.Write(num4[i]);
                bw.Write(num5[i]);
                bw.Write(date[i]);
            }

            bw.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!File.Exists(outputDataFilePath))
            {
                MessageBox.Show("zasilky.txt file can't find", "Error");
            }
            else
            {
                editedDataFileEncoder(outputDataFilePath);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Invalid Job ID", "Error");
            }
            else if (!File.Exists(inputDataFilePath))
            {
                MessageBox.Show("zasilky.txt file can't find", "Error");
            }
            else if (!File.Exists(getTrackingFilePath()))
            {
                MessageBox.Show(getTrackingFilePath() + " file can't find", "Error");
            }
            else
            {
                createTrackingFile(getTrackingFilePath(), textBox1.Text, checkBox1.Checked);

                if (rule1.Checked || rule2.Checked || rule3.Checked || rule4.Checked)
                {

                    List<float> num1 = new List<float>(); //x
                    List<float> num2 = new List<float>(); //y
                    List<short> num3 = new List<short>(); //heading
                    List<short> num4 = new List<short>(); //speed
                    List<short> num5 = new List<short>(); //damage
                    List<int> date = new List<int>(); //date

                    List<short> num4Clone = new List<short>();
                    List<short> num5Clone = new List<short>();
                    List<int> dateClone = new List<int>();

                    
                    

                    BinaryReader br = new BinaryReader(File.Open(outputTrackingFilePath + textBox1.Text + ".bin", FileMode.Open));
                    while (true)
                    {
                        try
                        {
                            num1.Add(br.ReadSingle());
                            num2.Add(br.ReadSingle());
                            num3.Add(br.ReadInt16());
                            num4.Add(br.ReadInt16());
                            num5.Add(br.ReadInt16());
                            date.Add(br.ReadInt32());
                        }
                        catch (Exception exception)
                        {
                            //Console.WriteLine(exception);
                            break;
                        }

                    }
                    br.Close();

                    for (int i = 0; i < num4.Count; i++)
                    {
                        //System.Console.WriteLine("AAAAA");
                        num4Clone.Add(num4[i]);
                        num5Clone.Add(num5[i]);
                        dateClone.Add(date[i]);
                    }

                    Dictionary<int, int> changeRecord = new Dictionary<int, int>();
                    for (int i = 0;i<num1.Count;i++)
                    {
                        changeRecord.Add(i,0);
                    }

                    List<int> rule1Index = new List<int>();
                    List<int> rule2Index = new List<int>();
                    List<int> rule3Index = new List<int>();
                    List<int> rule4Index = new List<int>();

                    collectRule1Points(rule1Index, num4);
                    collectRule2Points(rule2Index, num4);
                    collectRule3Points(rule3Index, num4);
                    collectRule4Points(rule4Index, num4);



                    if (rule1.Checked)
                    {
                        List<int> randomPoint = new List<int>();
                        int neededpoints = (int)((rule1Index.Count * rule1Presentage.Value)/100);
                        Random random = new Random();
                        while (neededpoints > randomPoint.Count)
                        {
                            int randomNum = random.Next(rule1Index.Count);
                            if (!randomPoint.Contains(rule1Index[randomNum]))
                            {
                                randomPoint.Add(rule1Index[randomNum]);
                            }
                        }
                        randomPoint.Sort();
                        if (randomPoint.Count > 0)
                        {
                            foreach (int i in randomPoint)
                            {
                                changeRecord[i] = 1;
                                for (int j = 0; j < i; j++)
                                {
                                    date[j] = date[j] + 1;
                                }
                            }
                        }
                        
                    }
                    if (rule2.Checked)
                    {
                        foreach (var v in changeRecord)
                        {
                            if (v.Value!=0)
                            {
                                rule2Index.Remove(v.Key);
                            }
                        }

                        List<int> randomPoint = new List<int>();
                        int neededpoints = (int)((rule2Index.Count * rule2Precentage.Value) / 100);
                        Random random = new Random();
                        List<int> tempt = new List<int>();
                        while (neededpoints > randomPoint.Count && tempt.Count<rule2Index.Count)
                        {
                            int randomNum = random.Next(rule2Index.Count);
                            if (!randomPoint.Contains(rule2Index[randomNum]) && !isAroundExitedRandomPointForRule_2(randomPoint, rule2Index[randomNum]) && !isAroundRule_1_AppliedPointForRule2(changeRecord, rule2Index[randomNum]))
                            {
                                randomPoint.Add(rule2Index[randomNum]);
                            }

                            if (!tempt.Contains(rule2Index[randomNum]))
                            {
                                tempt.Add(rule2Index[randomNum]);
                            }
                        }
                        randomPoint.Sort();

                        if (randomPoint.Count > 0)
                        {
                            Random randomRule2 = new Random();

                            foreach (int i in randomPoint)
                            {
                                changeRecord[i] = 2;
                                for (int j = 0 ; j < i ; j++)
                                {
                                    date[j] = date[j] + 1;
                                }

                                Tuple<int, int> val = setSpeedIncreaseValues(randomRule2);

                                int highVal = val.Item1;
                                int lowVal = val.Item2;
                                

                                if (i == 0)
                                {
                                    short tempSpeed = num4[i + 1];
                                    num4[i + 1] += (short)lowVal;
                                    if (tempSpeed!=num4[i+1])
                                    {
                                        changeRecord[i + 1] = 22;
                                    }
                                    
                                }
                                else if (i==num1.Count-1)
                                {
                                    short tempSpeed = num4[i - 1];
                                    num4[i-1] += (short)highVal;
                                    if (tempSpeed != num4[i - 1])
                                    {
                                        changeRecord[i - 1] = 22;
                                    }
                                    
                                }
                                else
                                {
                                    short tempSpeed = num4[i + 1];
                                    num4[i + 1] += (short)lowVal;
                                    if (tempSpeed != num4[i + 1])
                                    {
                                        changeRecord[i + 1] = 22;
                                    }


                                    tempSpeed = num4[i - 1];
                                    num4[i - 1] += (short)highVal;
                                    if (tempSpeed != num4[i - 1])
                                    {
                                        changeRecord[i - 1] = 22;
                                    }
                                }
                            }
                        }
                    }
                    if (rule3.Checked)
                    {
                        foreach (var v in changeRecord)
                        {
                            if (v.Value != 0)
                            {
                                rule3Index.Remove(v.Key);
                            }
                        }


                        List<int> randomPoint = new List<int>();
                        int neededpoints = (int)((rule3Index.Count * rule3Precentage.Value) / 100);
                        Random random = new Random();
                        while (neededpoints > randomPoint.Count)
                        {
                            int randomNum = random.Next(rule3Index.Count);
                            if (!randomPoint.Contains(rule3Index[randomNum]))
                            {
                                randomPoint.Add(rule3Index[randomNum]);
                            }
                        }
                        randomPoint.Sort();

                        if (randomPoint.Count > 0)
                        {
                            foreach (int i in randomPoint)
                            {
                                short beforeSpeed = 0;
                                short afterSpeed = 0;

                                if (i==0)
                                {
                                    beforeSpeed = 0;
                                    afterSpeed = num4[i + 1];
                                }
                                else if (i==num4.Count)
                                {
                                    beforeSpeed = num4[i - 1];
                                    afterSpeed = 0;
                                }
                                else
                                {
                                    beforeSpeed = num4[i - 1];
                                    afterSpeed = num4[i + 1];
                                }

                                short tempSpeed = num4[i];

                                num4[i] = getRandomSpeed(beforeSpeed,afterSpeed,random);
                                if (tempSpeed!=num4[i])
                                {
                                    changeRecord[i] = 3;
                                }
                                
                            }
                        }
                    }
                    if (rule4.Checked)
                    {
                        foreach (var v in changeRecord)
                        {
                            if (v.Value != 0)
                            {
                                rule4Index.Remove(v.Key);
                            }
                        }

                        List<int> randomPoint = new List<int>();
                        int neededpoints = (int)((rule4Index.Count * rule4Precentage.Value) / 100);
                        Random random = new Random();
                        while (neededpoints > randomPoint.Count)
                        {
                            int randomNum = random.Next(rule4Index.Count);
                            if (!randomPoint.Contains(rule4Index[randomNum]))
                            {
                                randomPoint.Add(rule4Index[randomNum]);
                            }
                        }
                        randomPoint.Sort();

                        if (randomPoint.Count > 0)
                        {
                            int maxSpeedFormOriginalFile = getMaxValFormList(num4Clone);
                            random = new Random();
                            foreach (int i in randomPoint)
                            {
                                if (num4.Where(a => a == maxSpeedFormOriginalFile).Count() <= 2 &&  num4[i] == maxSpeedFormOriginalFile)
                                {
                                    break;
                                }
                                short tempSpeed = num4[i];
                                num4[i] = (short)random.Next((int)rule4StartSpeed.Value, (int)rule4EndSpeed.Value+1);
                                if (tempSpeed!=num4[i])
                                {
                                    changeRecord[i] = 4;
                                }
                            }
                         }
                    }



                    BinaryWriter bw =
                        new BinaryWriter((Stream)new FileStream(outputTrackingFilePath + textBox1.Text + ".bin", FileMode.Create));
                    for (int i = 0; i < changeRecord.Count; i++)
                    {
                        if (changeRecord[i]!=1 && changeRecord[i]!=2)
                        {
                            bw.Write(num1[i]);
                            bw.Write(num2[i]);
                            bw.Write(num3[i]);
                            bw.Write(num4[i]);
                            bw.Write(num5[i]);
                            bw.Write(date[i]);
                        }
                    }

                    bw.Close();


                    IEnumerable<Tuple<int, string, string, string, string, string, string,Tuple<string>>> authors = new List<Tuple<int, string, string, string, string, string, string,Tuple<string>>>();

                    int increment = (changeRecord.Count / 25000) + 1;

                    for (int i = 0; i < changeRecord.Count; i++)
                    {
                        string x;
                        string y;
                        string heading;
                        string speed;
                        string damage;

                        DateTime pre;
                        DateTime now;

                        string dateT;

                        string change = "";


                        if (changeRecord[i]==1 || changeRecord[i]==2)
                        {
                            x = num1[i] + " -> ";
                            y = num2[i] + " -> ";
                            heading = num3[i] + " -> ";
                            speed = num4Clone[i] + " -> ";
                            damage = num5[i] + " -> ";

                            pre = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(dateClone[i]);
                            now = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(date[i]);

                            dateT = pre + " -> ";
                            if (changeRecord[i] == 1)
                            {
                                change = "Removed (rule 1)";
                            }
                            else
                            {
                                change = "Removed (rule 2)";
                            }
                        }
                        else
                        {
                            //System.Console.WriteLine(num4Clone.Count + "  " + num4.Count);

                            x = num1[i] + " -> " + num1[i];
                            y = num2[i] + " -> " + num2[i];
                            heading = num3[i] + " -> " + num3[i];
                            speed = num4Clone[i] + " -> " + num4[i];
                            damage = num5[i] + " -> " + num5[i];

                            pre = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(dateClone[i]);
                            now = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(date[i]);

                            dateT = pre + " -> " + now;

                            if (changeRecord[i] == 22)
                            {
                                change = "speed changed  (rule 2)";
                            }
                            else if (changeRecord[i] == 3)
                            {
                                change = "speed changed  (rule 3)";
                            }
                            else if (changeRecord[i] == 4)
                            {
                                change = "speed changed  (rule 4)";
                            }
                        }

                        if (i % increment == 0)
                        {

                            authors = authors.Concat(new[] { Tuple.Create(i + 1, x, y, heading, speed, damage, dateT,change) });

                        }
                    }

                    //NativeMethods.AllocConsole();

                    String outp = "Number of tracking points : " + num4Clone.Count +"(old) -> " + (num4.Count - getCountFromDictionary(changeRecord,1) - getCountFromDictionary(changeRecord,2)) + "(new)\n";

                    outp += "Max speed : " + getMaxValFormList(num4Clone) + "(old) -> " + getMaxValFormList(num4) + "(new)\n";
                    outp += "Damage : " + getMaxValFormList(num5Clone) + "(old) -> " + getMaxValFormList(num5) + "(new)\n";

                    if (rule1.Checked)
                    {
                        outp += "Rule 1 : true\n";
                        outp += "Number of removed tracking points by rule 1 : " + getCountFromDictionary(changeRecord, 1) + " / " + rule1Index.Count + " (" + (getCountFromDictionary(changeRecord, 1) * 100) / rule1Index.Count + "%)\n";
                        outp += "reduced time by rule 1 : " + getCountFromDictionary(changeRecord, 1) + " seconds\n";
                    }
                    else
                    {
                        outp += "Rule 1 : false\n";
                    }

                    if (rule2.Checked)
                    {
                        outp += "Rule 2 : true\n";
                        outp += "Number of removed tracking points by rule 2 : " + getCountFromDictionary(changeRecord, 2) + " / " + rule2Index.Count + " (" + (getCountFromDictionary(changeRecord, 2) * 100) / rule2Index.Count + "%)\n";
                        outp += "reduced time by rule 2 : " + getCountFromDictionary(changeRecord, 2) + " seconds\n";
                        outp += "Number of speed changed points by rule 2 : " + getCountFromDictionary(changeRecord, 22) + "\n";
                    }
                    else
                    {
                        outp += "Rule 2 : false\n";
                    }

                    if (rule3.Checked)
                    {
                        outp += "Rule 3 : true\n";
                        outp += "Number of speed changed points by rule 3 : " + getCountFromDictionary(changeRecord, 3) + " / " + rule3Index.Count + " (" + (getCountFromDictionary(changeRecord, 3) * 100) / rule3Index.Count + "%)\n";
                    }
                    else
                    {
                        outp += "Rule 3 : false\n";
                    }

                    if (rule4.Checked)
                    {
                        outp += "Rule 4 : true\n";
                        outp += "Number of speed changed points by rule 4 : " + getCountFromDictionary(changeRecord, 4) + " / " + rule4Index.Count + " (" + (getCountFromDictionary(changeRecord, 4) * 100) / rule4Index.Count + "%)\n";
                    }
                    else
                    {
                        outp += "Rule 4 : false\n";
                    }

                    outp += "Total number of removed tracking points : " + (getCountFromDictionary(changeRecord, 1)+ getCountFromDictionary(changeRecord, 2)) + " (" + ((getCountFromDictionary(changeRecord, 1) + getCountFromDictionary(changeRecord, 2))*100)/(float)changeRecord.Count + "%)\n";
                    outp += "Total number of changes : " + (changeRecord.Count - getCountFromDictionary(changeRecord, 0)) +"\n";

                    DateTime temp = DateTime.Now;

                    outp += "Total Time taken old tracking file : " + (temp.AddSeconds(dateClone[dateClone.Count - 1] - dateClone[0]) - temp) + "\n";
                    outp += "Total Time taken new tracking file : " + (temp.AddSeconds(date[date.Count - 1] - date[0]) - temp) + "\n\n";



                    outp += authors.ToStringTable(
                        new[] { "No", "X", "Y", "Heading", "Speed", "Damage", "Date and Time", "Changes" },
                        a => a.Item1, a => a.Item2, a => a.Item3, a => a.Item4, a => a.Item5, a => a.Item6, a => a.Item7, a=>a.Rest.Item1);

                    File.WriteAllText(@"output\tracking data.txt", outp);




                }
                else
                {
                    List<float> Inum1 = new List<float>();
                    List<float> Inum2 = new List<float>();
                    List<short> Inum3 = new List<short>();
                    List<short> Inum4 = new List<short>();
                    List<short> Inum5 = new List<short>();
                    List<int> Idate = new List<int>();

                    List<float> Onum1 = new List<float>();
                    List<float> Onum2 = new List<float>();
                    List<short> Onum3 = new List<short>();
                    List<short> Onum4 = new List<short>();
                    List<short> Onum5 = new List<short>();
                    List<int> Odate = new List<int>();

                    BinaryReader br1 = new BinaryReader(File.Open(getTrackingFilePath(), FileMode.Open));
                    BinaryReader br2 = new BinaryReader(File.Open(outputTrackingFilePath + textBox1.Text + ".bin", FileMode.Open));

                    while (true)
                    {
                        try
                        {
                            Inum1.Add(br1.ReadSingle());
                            Inum2.Add(br1.ReadSingle());
                            Inum3.Add(br1.ReadInt16());
                            Inum4.Add(br1.ReadInt16());
                            Inum5.Add(br1.ReadInt16());
                            Idate.Add(br1.ReadInt32());

                            Onum1.Add(br2.ReadSingle());
                            Onum2.Add(br2.ReadSingle());
                            Onum3.Add(br2.ReadInt16());
                            Onum4.Add(br2.ReadInt16());
                            Onum5.Add(br2.ReadInt16());
                            Odate.Add(br2.ReadInt32());
                        }
                        catch (Exception exception)
                        {
                            //Console.WriteLine(exception);
                            break;
                        }

                    }

                    br1.Close();
                    br2.Close();

                    //System.Console.WriteLine(Inum1.Count + "  " + Inum1.Count);

                    IEnumerable<Tuple<int, string, string, string, string, string, string>> authors = new List<Tuple<int, string, string, string, string, string, string>>();

                    int increment = (Inum1.Count / 25000) + 1;

                    for (int i = 0; i < Inum1.Count; i+=increment)
                    {
                        string x = Inum1[i] + " -> " + Onum1[i];
                        string y = Inum2[i] + " -> " + Onum2[i];
                        string heading = Inum3[i] + " -> " + Onum3[i];
                        string speed = Inum4[i] + " -> " + Onum4[i];
                        string damage = Inum5[i] + " -> " + Onum5[i];

                        DateTime pre = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Idate[i]);
                        DateTime now = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Odate[i]);

                        string dateT = pre + " -> " + now;

                        authors = authors.Concat(new[] { Tuple.Create(i + 1, x, y, heading, speed, damage, dateT) });

                    }

                    //NativeMethods.AllocConsole();

                    String outp = "Number of tracking points : " + authors.Count() + "\n";

                    outp += "Max speed : " + getMaxValFormList(Inum4) + "(old) -> " + getMaxValFormList(Onum4) + "(new)\n";
                    outp += "Damage : " + getMaxValFormList(Inum5) + "(old) -> " + getMaxValFormList(Onum5) + "(new)\n";

                    DateTime temp = DateTime.Now;

                    outp += "Total Time taken old tracking file : " + (temp.AddSeconds(Idate[Idate.Count - 1] - Idate[0]) - temp) + "\n";
                    outp += "Total Time taken new tracking file : " + (temp.AddSeconds(Odate[Odate.Count - 1] - Odate[0]) - temp) + "\n\n";



                    outp += authors.ToStringTable(
                        new[] { "No", "X", "Y", "Heading", "Speed", "Damage", "Date and Time" },
                        a => a.Item1, a => a.Item2, a => a.Item3, a => a.Item4, a => a.Item5, a => a.Item6, a => a.Item7);

                    File.WriteAllText(@"output\tracking data.txt", outp);
                }

                MessageBox.Show("Done", "Files create", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



            string getTrackingFilePath()
            {
                if (File.Exists(this.inputDataFilePath))
                {
                    string[] s = Decrypt(File.ReadAllText(this.inputDataFilePath)).Trim('\n', '\r').Split('\n');
                    return inputTrackingFilePath + int.Parse(s[29]) + ".bin";

                }
                return null;
            }

            void collectRule1Points(List<int> output, List<short> input)
            {
                for(int i = 0; i < input.Count; i++)
                {
                    if (input[i]==0)
                    {
                        output.Add(i);
                    }
                }
            }

            void collectRule2Points(List<int> output, List<short> input)
            {
                for (int i = 0; i < input.Count; i++)
                {
                    if (1<=input[i] && input[i]<=rule2EndSpeed.Value)
                    {
                        output.Add(i);
                    }
                }
            }

            void collectRule3Points(List<int> output, List<short> input)
            {
                for (int i = 0; i < input.Count; i++)
                {
                    if (1 <= input[i] && input[i] <= rule3EndSpeed.Value)
                    {
                        output.Add(i);
                    }
                }
            }

            void collectRule4Points(List<int> output, List<short> input)
            {
                for (int i = 0; i < input.Count; i++)
                {
                    if (rule4StartSpeed.Value <= input[i] && input[i] <= rule4EndSpeed.Value)
                    {
                        output.Add(i);
                    }
                }
            }

            bool isAroundExitedRandomPointForRule_2(List<int> randomPoints, int randomPoint)
            {
                foreach(int i in randomPoints)
                {
                    if (i==randomPoint-1 || i==randomPoint-2 || i==randomPoint+1 || i==randomPoint+2)
                    {
                        return true;
                    }
                }

                return false;
            }

            bool isAroundRule_1_AppliedPointForRule2(Dictionary<int,int> dic, int randomPoint)
            {
                if (dic[randomPoint-1]==1 || dic[randomPoint+1]==1)
                {
                    return true;
                }

                return false;
            }

            Tuple<int , int> setSpeedIncreaseValues(Random randomR2)
            {
                int highVal = 0;
                int LowVal = 0;

                if (rule2Changeing.Value==0)
                {
                    highVal = 0;
                    LowVal = 0;
                }
                else if (rule2Changeing.Value==1)
                {
                    highVal = 1;
                    LowVal = 1;
                }
                else if (rule2Changeing.Value==2)
                {
                    highVal = 2;
                    LowVal = 1;
                }
                else
                {
                    
                    do
                    {
                        highVal = randomR2.Next(1, (int)rule2Changeing.Value + 1);
                        LowVal = randomR2.Next(1, (int)rule2Changeing.Value + 1);
                    }
                    while (highVal <= LowVal);
                    
                }

                return Tuple.Create(highVal, LowVal);
                
            }

            short getRandomSpeed(short beforeSpeed, short afterSpeed, Random random)
            {
                if (beforeSpeed==afterSpeed)
                {
                    return beforeSpeed;
                }
                else if (beforeSpeed<afterSpeed)
                {
                    return (short)random.Next(beforeSpeed, afterSpeed+1);
                }
                else
                {
                    return (short)random.Next(afterSpeed,beforeSpeed+1);
                }
            }




        }

        private int getCountFromDictionary(Dictionary<int,int> dic, int num)
        {
            int count = 0;
            foreach (var v in dic)
            {
                if (v.Value == num)
                {
                    count += 1;
                }
            }
            return count;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!File.Exists(outputDataFilePath))
            {
                MessageBox.Show("zasilky.txt file can't find", "Error");
            }
            else
            {
                try
                {


                string[] s = Decrypt(File.ReadAllText(outputDataFilePath)).Trim('\n', '\r').Split('\n');

                string buildStr = "";

                for (int i = 0; i < s.Length; i += 42)
                {

                    buildStr += "Save & " + s[0 + i];
                    buildStr += "start city & " + s[1 + i];
                    buildStr += "finish city & " + s[2 + i];
                    buildStr += "cargo name & " + s[3 + i];
                    buildStr += "cargo & " + s[4 + i];
                    buildStr += "profit & " + s[5 + i];
                    buildStr += "Driven Distance & " + s[6 + i];
                    buildStr += "Initial Company & " + s[7 + i];
                    buildStr += "Target Company & " + s[8 + i];
                    buildStr += "liters_difference & " + s[9 + i];
                    buildStr += "liters_price_difference & " + s[10 + i];
                    buildStr += "penalty & " + s[11 + i];
                    buildStr += "xp & " + s[12 + i];
                    buildStr += "trailer_damage & " + s[13 + i];
                    buildStr += "Time Remaining & " + s[14 + i];
                    buildStr += "urgency & " + s[15 + i];
                    buildStr += "autopark & " + s[16 + i];
                    buildStr += "difficult_unloading & " + s[17 + i];
                    buildStr += "quick_job & " + s[18 + i];
                    buildStr += "total_profit & " + s[19 + i];
                    buildStr += "readings & " + s[20 + i];
                    buildStr += "starttime & " + s[21 + i];
                    buildStr += "Time Taken & " + s[22 + i];
                    buildStr += "truck & " + s[23 + i];
                    buildStr += "planned_distance & " + s[24 + i];
                    buildStr += "timestamp & " + s[25 + i];
                    buildStr += "ok & " + s[26 + i];
                    buildStr += "game & " + s[27 + i];
                    buildStr += "Date & " + s[28 + i];
                    buildStr += "job id & " + s[29 + i];
                    buildStr += "TB version & " + s[30 + i];
                    buildStr += "Maximal Reached Speed & " + s[31 + i];
                    buildStr += "used_fuel & " + s[32 + i];
                    buildStr += "Game version & " + s[33 + i];
                    buildStr += "weight kg & " + s[34 + i];
                    buildStr += "truck license plate & " + s[35 + i];
                    buildStr += "trailer license plate & " + s[36 + i];
                    buildStr += "profile name(hex) & " + s[37 + i];
                    buildStr += "user_id & " + s[38 + i];
                    buildStr += "planned_distance_navigation & " + s[39 + i];
                    buildStr += "startDate & " + s[40 + i];
                    buildStr += "scale & " + s[41 + i];
                    buildStr += "\n-------------------------------------------------\n";
                }

                string[] ss = buildStr.Split('\n', '\r');

                NativeMethods.AllocConsole();
                foreach (string str in ss)
                {
                    Console.WriteLine(str);
                }
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("-------------------------------------------------");

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!File.Exists(inputTrackingFilePath))
            {
                MessageBox.Show("tracking.bin file can't find", "Error");
            }
            else if (!File.Exists(outputTrackingFilePath + textBox1.Text + ".bin"))
            {
                MessageBox.Show("upl file can't find", "Error");
            }
            else if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("invalid job id", "Error");
            }
            else
            {
                List<float> Inum1 = new List<float>();
                List<float> Inum2 = new List<float>();
                List<short> Inum3 = new List<short>();
                List<short> Inum4 = new List<short>();
                List<short> Inum5 = new List<short>();
                List<int> Idate = new List<int>();

                List<float> Onum1 = new List<float>();
                List<float> Onum2 = new List<float>();
                List<short> Onum3 = new List<short>();
                List<short> Onum4 = new List<short>();
                List<short> Onum5 = new List<short>();
                List<int> Odate = new List<int>();

                BinaryReader br1 = new BinaryReader(File.Open(inputTrackingFilePath, FileMode.Open));
                BinaryReader br2 = new BinaryReader(File.Open(outputTrackingFilePath + textBox1.Text + ".bin", FileMode.Open));

                while (true)
                {
                    try
                    {
                        Inum1.Add(br1.ReadSingle());
                        Inum2.Add(br1.ReadSingle());
                        Inum3.Add(br1.ReadInt16());
                        Inum4.Add(br1.ReadInt16());
                        Inum5.Add(br1.ReadInt16());
                        Idate.Add(br1.ReadInt32());

                        Onum1.Add(br2.ReadSingle());
                        Onum2.Add(br2.ReadSingle());
                        Onum3.Add(br2.ReadInt16());
                        Onum4.Add(br2.ReadInt16());
                        Onum5.Add(br2.ReadInt16());
                        Odate.Add(br2.ReadInt32());
                    }
                    catch (Exception exception)
                    {
                        //Console.WriteLine(exception);
                        break;
                    }

                }

                br1.Close();
                br2.Close();

                //System.Console.WriteLine(Inum1.Count + "  " + Inum1.Count);

                IEnumerable<Tuple<int, string, string,string,string,string,string>> authors = new List<Tuple<int, string, string,string,string,string,string>>();

                for (int i = 0; i < Inum1.Count; i++)
                {
                    string x = Inum1[i] + " -> " + Onum1[i];
                    string y = Inum2[i] + " -> " + Onum2[i];
                    string heading = Inum3[i] + " -> " + Onum3[i];
                    string speed = Inum4[i] + " -> " + Onum4[i];
                    string damage = Inum5[i] + " -> " + Onum5[i];

                    DateTime pre = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Idate[i]);
                    DateTime now = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Odate[i]); 

                    string dateT = pre + " -> " + now;

                    authors = authors.Concat(new[] { Tuple.Create(i+1, x, y, heading, speed, damage,dateT) });

                }

                //NativeMethods.AllocConsole();

                String outp = "Number of tracking points : " + authors.Count() + "\n";
                outp += "Max speed old tracking file : " + getMaxValFormList(Inum4) + "\n";
                outp += "Max speed new tracking file : " + getMaxValFormList(Onum4) + "\n";

                DateTime temp = DateTime.Now;

                outp += "Total Time taken old tracking file : " + (temp.AddSeconds(Idate[Idate.Count - 1] - Idate[0])-temp) + "\n";
                outp += "Total Time taken new tracking file : " + (temp.AddSeconds(Odate[Odate.Count - 1] - Odate[0])-temp) + "\n\n";



                outp += authors.ToStringTable(
                    new[] { "No", "X", "Y", "Heading", "Speed","Damage" ,"Date and Time"},
                    a => a.Item1, a => a.Item2, a => a.Item3, a => a.Item4, a => a.Item5, a => a.Item6, a => a.Item7);
                    
                File.WriteAllText("tracking data.txt",outp);
            }
        }

        public int getMaxValFormList(List<short> list)
        {
            int max = 0;
            foreach (var v in list)
            {
                if (v > max)
                {
                    max = v;
                }
            }

            return max;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.Height==206)
            {
                //button7.Text = "<<";
                //this.Height = 440;
            }
            else
            {
                //button7.Text = ">>";
                //this.Height = 206;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            /*string filepath = this.outputTrackingFilePath + textBox2.Text.Trim(' ') + ".bin";
            //System.Console.WriteLine(filepath);

            if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Invalid Job ID", "Error");
            }
            else if (!File.Exists(filepath))
            {
                MessageBox.Show("file can't find", "Error");
            }
            else
            {
                //createTrackingFile(inputTrackingFilePath, textBox1.Text);

                


                List<float> num1 = new List<float>();
                List<float> num2 = new List<float>();
                List<short> num3 = new List<short>();
                List<short> num4 = new List<short>();
                List<short> num5 = new List<short>();
                List<int> date = new List<int>();

                BinaryReader br = new BinaryReader(File.Open(filepath, FileMode.Open));
                while (true)
                {
                    try
                    {

                        num1.Add(br.ReadSingle());
                        num2.Add(br.ReadSingle());
                        num3.Add(br.ReadInt16());
                        num4.Add(br.ReadInt16());
                        num5.Add(br.ReadInt16());
                        date.Add(br.ReadInt32());
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        break;
                    }

                }

                br.Close();

                for (int i = 0; i < num5.Count; i++)
                {
                    if (num5[i]>numericUpDown7.Value)
                    {
                        num5[i] = (short)numericUpDown7.Value;
                    }
                }


                BinaryWriter bw =
                    new BinaryWriter((Stream)new FileStream(outputTrackingFilePath + textBox2.Text + ".bin", FileMode.Create));
                for (int i = 0; i < num1.Count; i++)
                {
                    bw.Write(num1[i]);
                    bw.Write(num2[i]);
                    bw.Write(num3[i]);
                    bw.Write(num4[i]);
                    bw.Write(num5[i]);
                    bw.Write(date[i]);
                }

                bw.Close();

            }*/
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.damageAndSpeedFolderPath + "zasilky.txt"))
            {
                string[] s = Decrypt(File.ReadAllText(this.damageAndSpeedFolderPath + "zasilky.txt")).Trim('\n', '\r').Split('\n');
                int jobID = int.Parse(s[29]);

                if (File.Exists(this.damageAndSpeedFolderPath + "upl_" + jobID + ".bin"))
                {
                    if (damageTick.Checked)
                    {
                        s[13] = numericUpDown7.Value.ToString();
                    }
                    if (speedTick.Checked)
                    {
                        s[31] = numericUpDown8.Value.ToString();
                    }

                    string buildStr = "";

                    //System.Console.WriteLine(s[1]);
                    //int ii = 0;
                    foreach (string str in s)
                    {
                        //System.Console.WriteLine(ii + "  " + str);
                        //ii++;
                        buildStr += str.Trim('\n', '\r',' ').Trim('\n', '\r', ' ') + Environment.NewLine;
                    }

                    File.WriteAllText(this.damageAndSpeedFolderPath + @"out\" + "zasilky.txt", Encrypt(buildStr));




                    //=============================================================================

                    string filepath = this.damageAndSpeedFolderPath + "upl_" + jobID + ".bin";


                    List<float> num1 = new List<float>();
                    List<float> num2 = new List<float>();
                    List<short> num3 = new List<short>();
                    List<short> num4 = new List<short>();
                    List<short> num5 = new List<short>();
                    List<int> date = new List<int>();

                    BinaryReader br = new BinaryReader(File.Open(filepath, FileMode.Open));
                    while (true)
                    {
                        try
                        {

                            num1.Add(br.ReadSingle());
                            num2.Add(br.ReadSingle());
                            num3.Add(br.ReadInt16());
                            num4.Add(br.ReadInt16());
                            num5.Add(br.ReadInt16());
                            date.Add(br.ReadInt32());
                        }
                        catch (Exception exception)
                        {
                            //MessageBox.Show(exception.Message, "Error");
                            break;
                        }

                    }

                    br.Close();

                    if (damageTick.Checked)
                    {
                        for (int i = 0; i < num5.Count; i++)
                        {
                            if (num5[i] > numericUpDown7.Value)
                            {
                                num5[i] = (short)numericUpDown7.Value;
                            }
                        }
                    }

                    if (speedTick.Checked)
                    {
                        for (int i = 0; i < num4.Count; i++)
                        {
                            if (num4[i] > numericUpDown8.Value)
                            {
                                num4[i] = (short)numericUpDown8.Value;
                            }
                        }
                    }



                    BinaryWriter bw =
                            new BinaryWriter((Stream)new FileStream(this.damageAndSpeedFolderPath + @"out\" + "upl_" + jobID + ".bin", FileMode.Create));
                    for (int i = 0; i < num1.Count; i++)
                    {
                        bw.Write(num1[i]);
                        bw.Write(num2[i]);
                        bw.Write(num3[i]);
                        bw.Write(num4[i]);
                        bw.Write(num5[i]);
                        bw.Write(date[i]);
                    }

                    bw.Close();




                    //================================================================================


                    s = Decrypt(File.ReadAllText(this.damageAndSpeedFolderPath + @"out\" + "zasilky.txt")).Trim('\n', '\r').Split('\n');

                    string[] dis = new[] {

                "start city",
                "finish city",
                "cargo name",
                "cargo",
                "profit",
                "Driven Distance",
                "Initial Company",
                "Target Company",
                "liters_difference",
                "liters_price_difference",
                "penalty",
                "xp",
                "trailer_damage",
                "Time Remaining",
                "urgency",
                "autopark",
                "difficult_unloading",
                "quick_job",
                "total_profit",
                "readings",
                "starttime",
                "Time Taken",
                "truck",
                "planned_distance",
                "timestamp",
                "ok",
                "game",
                "Date",
                "job id",
                "TB version",
                "Maximal Reached Speed",
                "used_fuel",
                "Game version",
                "weight kg",
                "truck license plate",
                "trailer license plate",
                "profile name(hex)",
                "user_id",
                "planned_distance_navigation",
                "startDate",
                "scale"
            };

                    IEnumerable<Tuple<int, string, string>> authorsTable1 = new List<Tuple<int, string, string>>();
                    for (int i = 0; i < dis.Length; i++)
                    {
                        authorsTable1 = authorsTable1.Concat(new[] { Tuple.Create(i + 1, dis[i], s[i + 1].Trim('\n','\r',' ')) });
                    }

                    string table = "";

                    table += authorsTable1.ToStringTable(
                       new[] { "No", "Description", "job details" },
                       a => a.Item1, a => a.Item2, a => a.Item3);









                    List<float> num1OLD = new List<float>();
                    List<float> num2OLD = new List<float>();
                    List<short> num3OLD = new List<short>();
                    List<short> num4OLD = new List<short>();
                    List<short> num5OLD = new List<short>();
                    List<int> dateOLD = new List<int>();


                    List<float> num1NEW = new List<float>();
                    List<float> num2NEW = new List<float>();
                    List<short> num3NEW = new List<short>();
                    List<short> num4NEW = new List<short>();
                    List<short> num5NEW = new List<short>();
                    List<int> dateNEW = new List<int>();

                    br = new BinaryReader(File.Open(filepath, FileMode.Open));
                    while (true)
                    {
                        try
                        {
                            num1OLD.Add(br.ReadSingle());
                            num2OLD.Add(br.ReadSingle());
                            num3OLD.Add(br.ReadInt16());
                            num4OLD.Add(br.ReadInt16());
                            num5OLD.Add(br.ReadInt16());
                            dateOLD.Add(br.ReadInt32());
                        }
                        catch (Exception exception)
                        {
                            //MessageBox.Show(exception.Message,"Error");
                            break;
                        }

                    }
                    br.Close();



                    br = new BinaryReader(File.Open(this.damageAndSpeedFolderPath + @"out\" + "upl_" + jobID + ".bin", FileMode.Open));
                    while (true)
                    {
                        try
                        {
                            num1NEW.Add(br.ReadSingle());
                            num2NEW.Add(br.ReadSingle());
                            num3NEW.Add(br.ReadInt16());
                            num4NEW.Add(br.ReadInt16());
                            num5NEW.Add(br.ReadInt16());
                            dateNEW.Add(br.ReadInt32());
                        }
                        catch (Exception exception)
                        {
                            //MessageBox.Show(exception.Message,"Error");
                            break;
                        }

                    }
                    br.Close();

                    IEnumerable<Tuple<int, string, string, string, string, string, string,Tuple<string>>> authors = new List<Tuple<int, string, string, string, string, string, string,Tuple<string>>>();

                    List<string> changedValue = new List<string>();

                    int numOfdamagechange = 0;
                    int numOfspeedchange = 0;


                    int increment = (num1OLD.Count / 25000) + 1;

                    for (int i = 0; i < num1OLD.Count; i++)
                    {
                        string isChange = "";

                        string x = num1NEW[i].ToString();
                        string y = num2NEW[i].ToString();
                        string heading = num3NEW[i].ToString();
                        string speed = num4NEW[i].ToString();
                        string damage = num5NEW[i].ToString();

                        if (speedTick.Checked)
                        {
                            speed = num4OLD[i] + " -> " + num4NEW[i];
                            if (num4OLD[i] != num4NEW[i])
                            {
                                isChange = "S";
                                numOfspeedchange += 1;
                            }

                        }


                        if (damageTick.Checked)
                        {
                            damage = num5OLD[i] + " -> " + num5NEW[i];
                            if (num5OLD[i] != num5NEW[i])
                            {
                                isChange += "D";
                                numOfdamagechange += 1;
                            }
                        }


                        string dateT = (new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(dateNEW[i])).ToString();

                        if (i%increment==0)
                        {
                            authors = authors.Concat(new[] { Tuple.Create(i + 1, x, y, heading, speed, damage, dateT, isChange) });
                        }
                    }

                    table += "\n";
                    table += "Number of tracking points :" + num1NEW.Count + "\n";
                    if (damageTick.Checked)
                    {
                        table += "change damage : true\n";
                        table += "number of changed points of damage : " + numOfdamagechange + "  (" + (numOfdamagechange * 100) / ((float) num1NEW.Count) + "%)\n";
                    }
                    else
                    {
                        table += "change damage : false\n";
                    }

                    if (speedTick.Checked)
                    {
                        table += "change speed : true\n";
                        table += "number of changed points of speed : " + numOfspeedchange + "  (" + (numOfspeedchange * 100) / ((float)num1NEW.Count) + "%)\n";

                    }
                    else
                    {
                        table += "change speed : false\n";
                    }
                    table += "Maximal Reached Speed : " + getMaxValFormList(num4OLD) + "(old) -> " + getMaxValFormList(num4NEW) + "(new)\n" ;
                    table += "\n";


                    table += authors.ToStringTable(
                       new[] { "No", "X", "Y", "Heading", "Speed", "Damage", "Date and Time","Change" },
                       a => a.Item1, a => a.Item2, a => a.Item3, a => a.Item4, a => a.Item5, a => a.Item6, a => a.Item7, a =>(a.Rest).Item1 );

                    File.WriteAllText(this.damageAndSpeedFolderPath + @"out\" + "table.txt", table);

                    MessageBox.Show("Done", "Files create", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("upl_" + jobID + ".bin" + " file can't find", "Error");
                }
            }
            else
            {
                MessageBox.Show("zasilky.txt" + " file can't find", "Error");
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            RouteGraph route = new RouteGraph();
            route.ShowDialog();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if(!File.Exists(trackingRecreate + textBox2.Text + ".bin")){
                MessageBox.Show("upl_" + textBox2.Text + ".bin" + " file can't find", "Error");
            }
            else
            {
                List<float> num1 = new List<float>();
                List<float> num2 = new List<float>();
                List<short> num3 = new List<short>();
                List<short> num4 = new List<short>();
                List<short> num5 = new List<short>();
                List<int> date = new List<int>();

                BinaryReader br = new BinaryReader(File.Open(trackingRecreate + textBox2.Text + ".bin", FileMode.Open));
                while (true)
                {
                    try
                    {

                        num1.Add(br.ReadSingle());
                        num2.Add(br.ReadSingle());
                        num3.Add(br.ReadInt16());
                        num4.Add(br.ReadInt16());
                        num5.Add(br.ReadInt16());
                        date.Add(br.ReadInt32());
                    }
                    catch (Exception exception)
                    {
                        //MessageBox.Show(exception.Message, "Error");
                        break;
                    }

                }

                br.Close();

                IEnumerable<Tuple<int, float, float, double, double, short, short, Tuple<short>>> authors = 
                    new List<Tuple<int, float, float, double, double, short, short, Tuple<short>>>();

                for (int i = 1; i < num1.Count; i++)
                {
                    float dX = num1[i] - num1[i - 1];
                    float dY = num2[i] - num2[i - 1];
                    double distance = Math.Sqrt(dX * dX + dY * dY);

                    double speedAVG = (num4[i - 1] + num4[i]) / 2.0;

                    authors = authors.Concat(new[] { Tuple.Create(i, num1[i], num2[i], distance,speedAVG, num4[i-1], num4[i], num3[i]) });
                }

                List<Tuple<int, float, float, double,double, short, short, Tuple<short>>> authorsTemp =
                    new List<Tuple<int, float, float, double,double, short, short, Tuple<short>>>();
                foreach (var v in authors)
                {
                    authorsTemp.Add(v);
                }

                string table = "";
                table += "Number of total tracking : " + num1.Count + "\n";
                table += "Max speed : " + num4.Max() + "\n";
                table += "Damage : " + num5.Max() + "\n";
                table += "Time Taken : " + (DateTime.Now.AddSeconds(date[date.Count - 1] - date[0]) - DateTime.Now) + "\n";



                if (comboBox1.SelectedIndex == 0)
                {
                    authorsTemp = authorsTemp.OrderBy(a => a.Item2).ToList();
                    table = "Sort by X";
                }
                else if (comboBox1.SelectedIndex==1)
                {
                    authorsTemp = authorsTemp.OrderBy(a => a.Item3).ToList();
                    table = "Sort by Y";
                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    authorsTemp = authorsTemp.OrderBy(a => a.Item4).ToList();
                    table = "Sort by Distance";
                }
                else if (comboBox1.SelectedIndex == 3)
                {
                    authorsTemp = authorsTemp.OrderBy(a => a.Item6).ToList();
                    table = "Sort by Speed (before)";
                }
                else if (comboBox1.SelectedIndex == 4)
                {
                    authorsTemp = authorsTemp.OrderBy(a => a.Item7).ToList();
                    table = "Sort by Speed (after)";
                }
                else if (comboBox1.SelectedIndex == 5)
                {
                    authorsTemp = authorsTemp.OrderBy(a => a.Item5).ToList();
                    table = "Sort by Speed (avg)";
                }
                else if (comboBox1.SelectedIndex == 6)
                {
                    authorsTemp = authorsTemp.OrderBy(a => a.Rest.Item1).ToList();
                    table = "Sort by Heading";
                }
                table += "\n";
                

                table += authorsTemp.ToStringTable(
                       new[] { "No", "X", "Y", "Distance", "Speed (avg)", "Speed (before)", "Speed (after)", "Heading" },
                       a => a.Item1, a => a.Item2, a => a.Item3, a => a.Item4, a => a.Item5, a => a.Item6, a => a.Item7, a => (a.Rest).Item1);

                List<double> speeds = new List<double>();

                foreach (var v in authorsTemp)
                {
                    if (!speeds.Contains(v.Item5))
                    {
                       speeds.Add(v.Item5);
                    }
                }

                for (int i=0;i<speeds.Count;i++)
                {
                    List<double> tempDiistanceList = new List<double>();

                    foreach (var v in authorsTemp)
                    {
                        if (speeds[i]==v.Item5)
                        {
                            if (v.Item4>0)
                            {
                                tempDiistanceList.Add(v.Item4);
                            }
                            
                        }
                    }

                    table += "\n\n";
                    table += "Average Speed : " + speeds[i] + "\n";
                    table += "distance : " + tempDiistanceList.Min() + "(min) "
                        + tempDiistanceList.Max() + "(max) "
                        + (tempDiistanceList.Max()-tempDiistanceList.Min()) + "(variation)\n";
                    table += "Average Distance : " + tempDiistanceList.Average();

                }




                File.WriteAllText(@"Tracking\out\sorted table.txt", table);
                MessageBox.Show("Done", "Files create", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (!File.Exists(trackingRecreate + textBox2.Text + ".bin"))
            {
                MessageBox.Show("upl_" + textBox2.Text + ".bin" + " file can't find", "Error");
            }
            else
            {
                Random randomSpeed = new Random();


                List<float> num1 = new List<float>(); //x
                List<float> num2 = new List<float>(); //y
                List<short> num3 = new List<short>(); //heading
                List<short> num4 = new List<short>(); //speed
                List<short> num5 = new List<short>(); //damage
                List<int> date = new List<int>();

                BinaryReader br = new BinaryReader(File.Open(trackingRecreate + textBox2.Text + ".bin", FileMode.Open));
                while (true)
                {
                    try
                    {

                        num1.Add(br.ReadSingle());
                        num2.Add(br.ReadSingle());
                        num3.Add(br.ReadInt16());
                        num4.Add(br.ReadInt16());
                        num5.Add(br.ReadInt16());
                        date.Add(br.ReadInt32());
                    }
                    catch (Exception exception)
                    {
                        //MessageBox.Show(exception.Message, "Error");
                        break;
                    }

                }

                br.Close();

                List<float> xCor = new List<float>();
                List<float> yCor = new List<float>();
                List<short> HeadingsList = new List<short>();
                List<short> speedList = new List<short>();
                List<short> damage = new List<short>();
                List<int> dateTime = new List<int>();

                bool isFinish = false;
                int index = 0;
                short lastDamage = 0;
                bool previousIsEdited = false;

                int trackingPoint = num1.Count;

                double  avgMaxDistance = 27.7807349793558;
                double maxMaxDistance = 32.4227151270105;

                List<int> changeRecord = new List<int>();


                while (index<trackingPoint)
                {
                    if (num4[index]<=100)
                    {
                        xCor.Add(num1[index]);
                        yCor.Add(num2[index]);
                        HeadingsList.Add(num3[index]);
                        speedList.Add(num4[index]);
                        damage.Add(num5[index]);
                        lastDamage = num5[index];

                        //System.Console.WriteLine("case 1 : " + num4[index]);

                        index += 1;
                        previousIsEdited = false;

                        changeRecord.Add(0); // no change
                        
                    }
                    else if(getDistanceBetweenCordinates(num1[index-1],num2[index-1],num1[index],num2[index])<=maxMaxDistance && !previousIsEdited)
                    {
                        xCor.Add(num1[index]);
                        yCor.Add(num2[index]);
                        HeadingsList.Add(num3[index]);
                        speedList.Add((short)randomSpeed.Next((int)numericUpDown9.Value - 1, (int)numericUpDown9.Value + 1));
                        damage.Add(num5[index]);
                        lastDamage = num5[index];

                        previousIsEdited = false;

                        changeRecord.Add(1); //speed change
                        //System.Console.WriteLine("case 2 : " + num4[index]);

                        index += 1;
                    }
                    else
                    {
                        Tuple<float, float> nextCordinate = null;

                        if (getDistanceBetweenCordinates(num1[index - 1], num2[index - 1], num1[index], num2[index])>avgMaxDistance)
                        {
                            nextCordinate = getCordinates(num1[index - 1], num2[index - 1], num1[index], num2[index], avgMaxDistance);

                            xCor.Add(nextCordinate.Item1);
                            yCor.Add(nextCordinate.Item2);
                            HeadingsList.Add(num3[index]);
                            speedList.Add((short)randomSpeed.Next((int)numericUpDown9.Value - 1, (int)numericUpDown9.Value + 1));
                            damage.Add(num5[index]);
                            num1[index - 1] = nextCordinate.Item1;
                            num2[index - 1] = nextCordinate.Item2;
                            previousIsEdited = true;

                            changeRecord.Add(2); //new point
                            //System.Console.WriteLine("case 3 : " + num4[index]);

                        }
                        else
                        {
                            //System.Console.WriteLine("case 4 : " + num4[index]);
                            num1[index] = num1[index - 1];
                            num2[index] = num2[index - 1];

                            index += 1;
                            //previousIsEdited = false;
                        }
                    }
                }

                int startTime = date[0];
                for(int i = 0; i < xCor.Count; i++)
                {
                    dateTime.Add(startTime + i);
                }

               // System.Console.WriteLine(xCor.Count + " " + yCor.Count + " " + HeadingsList.Count + " " + speedList.Count + " " + damage.Count + " " + dateTime.Count);

                BinaryWriter bw =
                            new BinaryWriter((Stream)new FileStream(@"Tracking\out\" + "upl_" + ".bin", FileMode.Create));
                for (int i = 0; i < xCor.Count; i++)
                {
                    bw.Write(xCor[i]);
                    bw.Write(yCor[i]);
                    bw.Write(HeadingsList[i]);
                    bw.Write(speedList[i]);
                    bw.Write(damage[i]);
                    bw.Write(dateTime[i]);
                }

                bw.Close();

                IEnumerable<Tuple<int, float, float, short, short, short, DateTime,Tuple<string>>> authors =
                   new List<Tuple<int, float, float, short, short, short, DateTime, Tuple<string>>>();

                for (int i = 0; i < xCor.Count; i++)
                {

                    DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(dateTime[i]);

                    string change = "";
                    if (changeRecord[i] == 1)
                    {
                        change = "speed";
                    }
                    else if (changeRecord[i] == 2)
                    {
                        change = "new";
                    }

                    authors = authors.Concat(new[] { Tuple.Create(i + 1, xCor[i], yCor[i], HeadingsList[i], speedList[i], damage[i], d, change)});

                }

                string table = "";
                table += "Number of total tracking : " + xCor.Count + "\n";
                table += "Max speed : " + speedList.Max() + "\n";
                table += "Damage : " + damage.Max() + "\n";

                table += "Time Taken : " + (DateTime.Now.AddSeconds(dateTime[dateTime.Count-1]-dateTime[0])-DateTime.Now) + "\n";


                table += authors.ToStringTable(
                      new[] { "No", "X", "Y", "Heading", "Speed", "Damage", "Date and Time" , "change"},
                      a => a.Item1, a => a.Item2, a => a.Item3, a => a.Item4, a => a.Item5, a => a.Item6, a => a.Item7, a => a.Rest.Item1);

                File.WriteAllText(@"Tracking\out\upl_table.txt",table);
                MessageBox.Show("Done", "Files create", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            double getDistanceBetweenCordinates(float x1, float y1, float x2, float y2)
            {
                return Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            }

            float getYcordinate(float x1, float y1, float x2, float y2, float x)
            {
                return ((x - x1) * (y2 - y1) / (x2 - x1)) + y1;
            }

            float getXcordinate(float x1, float y1, float x2, float y2, float y)
            {
                return ((y-y1) * (x2-x1) / (y2-y1)) + x1;
            }

            Tuple<float,float> getCordinates(float x1, float y1, float x2, float y2, double m)
            {
                double l = getDistanceBetweenCordinates(x1,y1,x2,y2);
                if (m<l)
                {
                    float x = (float)((m * x2 + (l - m) * x1) / l);
                    float y = (float)((m * y2 + (l - m) * y1) / l);
                    return Tuple.Create(x,y);
                }
                else
                {
                    MessageBox.Show("there is an unexpected coordinate", "Error");
                    return null;
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (!File.Exists(arrayToBinaryInput))
            {
                MessageBox.Show("inputList.txt" + " file can't find", "Error");
            }
            else
            {
                String[] s = File.ReadAllText(arrayToBinaryInput).Trim('\n','\r',' ').Split(',');

                List<float> num1 = new List<float>(); //x
                List<float> num2 = new List<float>(); //y
                List<short> num3 = new List<short>(); //heading
                List<short> num4 = new List<short>(); //speed
                List<short> num5 = new List<short>(); //damage
                List<int> date = new List<int>();

                foreach (String str in s)
                {
                    String[] temp = str.Trim('"').Split('~');
                    num1.Add(float.Parse(temp[0]));
                    num2.Add(float.Parse(temp[1]));
                    num3.Add(short.Parse(temp[2]));
                    num4.Add(short.Parse(temp[3]));
                    num5.Add(short.Parse(temp[4]));
                    date.Add(int.Parse(temp[5]));
                    //System.Console.WriteLine(str.Trim('"'));
                }

                BinaryWriter bw =
                        new BinaryWriter((Stream)new FileStream(@"ArrayToBinaryFile\out\" + num1.Count + ".bin", FileMode.Create));
                for (int i = 0; i < num1.Count; i++)
                {
                       bw.Write(num1[i]);
                       bw.Write(num2[i]);
                       bw.Write(num3[i]);
                       bw.Write(num4[i]);
                       bw.Write(num5[i]);
                       bw.Write(date[i]);
                    
                }
                bw.Close();


                IEnumerable<Tuple<int, float, float, short, short, short, DateTime>> authors =
                   new List<Tuple<int, float, float, short, short, short, DateTime>>();

                String table = "Number of total tracking : " + num1.Count + "\n";

                //for (int i = 0; i < num1.Count; i++)

                int increment = (num1.Count / 25000)+1;
                //System.Console.WriteLine(increment);

                for (int i = 0; i < num1.Count; i+=increment)
                {
                      DateTime dateT = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(date[i]);
                      authors = authors.Concat(new[] { Tuple.Create(i + 1, num1[i], num2[i], num3[i], num4[i], num5[i], dateT) });
                }

                

                DateTime currentTime = DateTime.Now;

                table += "Total Time taken : " + (currentTime.AddSeconds(date[date.Count - 1] - date[0])-currentTime) + "\n\n"; 

                table += authors.ToStringTable(
                      new[] { "No", "X", "Y", "Heading", "Speed", "Damage", "Date and Time" },
                      a => a.Item1, a => a.Item2, a => a.Item3, a => a.Item4, a => a.Item5, a => a.Item6, a => a.Item7);

                File.WriteAllText(@"ArrayToBinaryFile\out\table.txt" , table);

                MessageBox.Show("Done", "Files create", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }
}
