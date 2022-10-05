﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace RegistryEditor2
{
    public partial class AddFile : Form
    {
        private RegistryKey registryKey;
        public String Name { get; set; }
        public object Value { get; set; }
        public AddFile(RegistryKey registryKey)
        {
            InitializeComponent();
            this.registryKey = registryKey;
        }

        private void AddFile_Load(object sender, EventArgs e)
        {
            buttonCancel.DialogResult = DialogResult.Cancel;
            comboBox1.Items.Add(RegistryValueKind.Binary);
            comboBox1.Items.Add(RegistryValueKind.DWord);
            comboBox1.Items.Add(RegistryValueKind.QWord);
            comboBox1.Items.Add(RegistryValueKind.String);
            comboBox1.SelectedItem = comboBox1.Items[0];
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            String name = textBox1.Text.Trim();
            String value = textBox2.Text.Trim();
            if (!String.IsNullOrEmpty(name) && !registryKey.GetValueNames().Contains(name))
            {
                if (ValidateValue(value))
                {
                    Name = name;
                    Value = GetValue(comboBox1.Text, value);
                    this.DialogResult = DialogResult.OK;
                }
                else errorProvider2.SetError(textBox2,"Значение параметра не соответствует выбранному типу!!!!");
            }
            else errorProvider1.SetError(textBox1, "Строка пуста или параметр с так именем уже существует!!!!");
        }
        private object GetValue(String name, string value)
        {
            String str = "\xf3\x32\x12";
            switch (name)
            {
                case "Binary": return Encoding.ASCII.GetBytes(str);
                case "DWord": return value;
                case "QWord": return value;
                default: return value;
            }
        }
        private bool ValidateValue(String value)
        {
            switch (value)
            {
                case "Binary":
                    String[] strs = value.Split(" ");
                    foreach (String str in strs) { if (str.Length != 2) return false; }
                    return true;
                case "DWord": return true;
                case "QWord": return true;
                default: return true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
        }
    }
}
