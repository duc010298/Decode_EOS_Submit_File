using QuestionLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Decode_EOS_Submit_File
{
    public partial class mainForm : Form
    {
        private string yourFriendFileName;
        private string yourFileName;

        public mainForm()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            yourFriendFileName = openFileDialog1.FileName;
            textBox1.Text = openFileDialog1.SafeFileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            yourFileName = openFileDialog1.FileName;
            textBox2.Text = openFileDialog1.SafeFileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SubmitPaper yourFriendSPaper = DecodeFile(yourFriendFileName);
            SubmitPaper yourSPaper = DecodeFile(yourFileName);
            if(yourFriendSPaper != null && yourSPaper != null)
            {
                yourSPaper.SPaper = yourFriendSPaper.SPaper;
                yourSPaper.SubmitTime = DateTime.Now;
                EncodeFile(textBox2.Text, yourSPaper);
            }
            else
            {
                MessageBox.Show("Cannot decode file", "Error");
            }
            MessageBox.Show("Create file successfuly");
        }

        private SubmitPaper DecodeFile(string fileName)
        {
            SubmitPaper c = null;
            if ((fileName != null) && (fileName.Trim() != ""))
            {
                FileStream serializationStream = new FileStream(fileName, FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    c = (SubmitPaper)formatter.Deserialize(serializationStream);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Failed to load Sample!\nReason: " + exception.Message, "Take an error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                finally
                {
                    serializationStream.Close();
                }
            }
            return c;
        }

        private void EncodeFile(string fileName, SubmitPaper yourSPaper)
        {
            if ((fileName != null) && (fileName.Trim() != ""))
            {
                FileStream fs = new FileStream(fileName, FileMode.Create);

                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(fs, yourSPaper);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Failed to create new file!\nReason: " + exception.Message, "Take an error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                finally
                {
                    fs.Close();
                }
            }
        }
    }
}
