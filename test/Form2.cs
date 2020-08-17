/*
 * Created by SharpDevelop.
 * User: malei
 * Date: 2020/7/27
 * Time: 11:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace test
{
	/// <summary>
	/// Description of Form2.
	/// </summary>
	public partial class Form2 : Form
	{
		List<string> l=new List<string>();
		public Form2(List<string> x)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			l=x;
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void TextBox1TextChanged(object sender, EventArgs e)
		{

		}
		void Button1Click(object sender, EventArgs e)
		{
			this.textBox1.Text=string.Join(",",l);
		}
	}
}
