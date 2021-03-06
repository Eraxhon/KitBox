﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_kitbox
{
    public partial class Form2 : Form
    {
        ParentForm parent;

        public Form2(ParentForm parent)
        {
            this.parent = parent;
            InitializeComponent();

        }

        private void Done_Click(object sender, EventArgs e)
        {
            if (comboBoxH.Text != "" && comboBoxC.Text != "" && comboBoxH.Text != "" && (DoorY.Checked && comboBox3.Text !="" || DoorN.Checked))
            {
                parent.Cup.AddCompartment(new Compartment(Int32.Parse(comboBoxH.Text),comboBoxC.Text, DoorY.Checked, comboBox3.Text, parent.Cup));
                /*
                comboBoxH.Items.Clear();
                comboBoxC.Items.Clear();
                comboBox3.Items.Clear();
                DoorN.AutoCheck = false;
                DoorY.AutoCheck = false;
                */
                parent.Next();
            }
            else
            {
                MessageBox.Show("Bad job.");
            }
        }

        private void comboBoxH_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxC.Text = "";
            comboBoxC.Items.Clear();
            comboBox3.Text = "";
            comboBox3.Items.Clear();
            //get colors
            if (comboBoxH.Text != "")
                {
                List<string> colorlList = DimensionCatalog.GetCompartmentColors(Int32.Parse(comboBoxH.Text));

                foreach (string color in colorlList)
                {
                    comboBoxC.Items.Add(color);
                }
                List<string> colorlDList = DimensionCatalog.GetDoorColors(Int32.Parse(comboBoxH.Text));
                foreach (string color in colorlDList)
                {
                    comboBox3.Items.Add(color);
                }
             
            }

        }

        private void DoorY_CheckedChanged(object sender, EventArgs e)
        {
            comboBox3.Show();
            labelCD.Show();
        }

        private void DoorN_CheckedChanged(object sender, EventArgs e)
        {
            comboBox3.Hide();
            comboBox3.Text = "";
            labelCD.Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //GET DIMENSION LIST
            List<Dimension> dimensionList = DimensionCatalog.GetCompartmentDimensions();

            foreach (Dimension dimension in dimensionList)
            {
                comboBoxH.Items.Add(dimension.ToString());
            }
            //get colors
            List<string> colorlList = DimensionCatalog.GetCompartmentColors();

            foreach (string color in colorlList)
            {
                comboBoxC.Items.Add(color);
            }
            DoorN.AutoCheck = true;
            DoorY.AutoCheck = true;
        }
    }
}
