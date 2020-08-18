/*
 * Created by SharpDevelop.
 * User: malei
 * Date: 2020/7/24
 * Time: 10:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;



namespace test
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
		
	{
		List<string> l=new List<string>();//记录操作顺序
		List<string> opera=new List<string>();//记录操作记录
		List<string> grpdetail=new List<string>();//保存每个组的详细组
		List<string> backup=new List<string>();//冷备用主机存储
		Dictionary<string, bool> nostopupd = new Dictionary<string, bool>();//指定每个子系统是否需要不停机更新
		int click=0,currentsys=0;
		List<int> sets=new List<int>();
		bool f=false;
		Dictionary<string,List<string>> operadic=new Dictionary<string,List<string>>(); 
		Dictionary<string,string> cfgdic=new Dictionary<string,string>(); 
		Dictionary<string,string> otherdic=new Dictionary<string,string>();

		Dictionary<string, string> defcfg = new Dictionary<string, string>();
		Dictionary<string, string> defother = new Dictionary<string, string>();
		Dictionary<string, string> xtpdic = new Dictionary<string, string>();
		Dictionary<string,List<int>> grpdic=new Dictionary<string,List<int>>();

		string map="";
		string onlcheck = "";
		bool f1 = false, f2 = false,fclor=false,f4bu=false;
					
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			/*DialogResult re=MessageBox.Show("是否使用默认配置更新所有子系统？","生成编排文件",MessageBoxButtons.YesNo);
			if(re==DialogResult.Yes)
			{
				
				DialogResult re2=MessageBox.Show("已在当前文件夹下生成编排文件","success",MessageBoxButtons.OK);
				if(re2==DialogResult.OK)
					System.Environment.Exit(0);
			}*/
			
			this.comboBox3.SelectedIndex = 0;
			this.button1.Enabled = false;
			this.button2.Enabled = false;
			this.button3.Enabled = false;
			this.button4.Enabled = false;
			this.timer1.Interval = 500;
			timer1.Start();


			//this.comboBox2.SelectedIndex=0;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		void Button1Click(object sender, EventArgs e)
		{
			timer4.Stop();
			
			f4bu = true;timer5.Interval = 500;timer5.Start();panel1.BackColor = Color.Navy;
			button1.Enabled = false;
			button2.Enabled=false;
			button3.Enabled=false;
			button4.Enabled=false;
			
			textBox4.Text="三项检查写在这里";
			this.comboBox3.SelectedIndex=0;
			click++;
			currentsys=1;			
			this.checkedListBox1.Items.Clear();
			this.checkedListBox1.Items.Add("all");
			
			foreach(string ti in grpdetail)
			{if(ti.Contains("ONL"))
					this.checkedListBox1.Items.Add(ti);}
		this.button1.Text="ONL:"+click.ToString();
			
			l.Add("ONL");
		
			this.label3.Text="指定需要安装的组别：";
			this.label3.Text="为ONL子系统"+this.label3.Text;
			this.button12.Visible=true;
			this.textBox4.Visible=true;
			
			
		}
		
		
		void Button2Click(object sender, EventArgs e)
		{
			timer4.Stop();
			
			f4bu = true; timer5.Interval = 500; timer5.Start(); panel1.BackColor = Color.Navy;
			button1.Enabled=false;
			button2.Enabled = false;
			button3.Enabled=false;
			button4.Enabled=false;
			click++;
			this.comboBox3.SelectedIndex=0;
			currentsys=2;
			l.Add("UMS");
			this.checkedListBox1.Items.Clear();
			this.checkedListBox1.Items.Add("all");
			foreach(string ti in grpdetail)
			{if(ti.Contains("UMS"))
					this.checkedListBox1.Items.Add(ti);}
			
			this.button2.Text="UMS:"+click.ToString();
			this.label3.Text="指定需要安装的组别：";
			this.label3.Text="为UMS子系统"+this.label3.Text;
		}
		void Button3Click(object sender, EventArgs e)
		{
			timer4.Stop();
			
			f4bu = true; timer5.Interval = 500; timer5.Start(); panel1.BackColor = Color.Navy;
			button1.Enabled = false;
			button2.Enabled = false;
			button3.Enabled = false;
			button4.Enabled = false;
			click++;
			this.comboBox3.SelectedIndex=0;
			currentsys=3;
			l.Add("BAT");
			this.checkedListBox1.Items.Clear();
			this.checkedListBox1.Items.Add("all");
			foreach(string ti in grpdetail)
			{if(ti.Contains("BAT"))
					this.checkedListBox1.Items.Add(ti);}
			
			this.button3.Text="BAT:"+click.ToString();
			this.label3.Text="指定需要安装的组别：";
			this.label3.Text="为BAT子系统"+this.label3.Text;
		}
		void Button4Click(object sender, EventArgs e)
		{
			timer4.Stop();
			
			f4bu = true; timer5.Interval = 500; timer5.Start(); panel1.BackColor = Color.Navy; 
			button1.Enabled = false;
			button2.Enabled = false;
			button3.Enabled = false;
			button4.Enabled = false;
			click++;
			this.comboBox3.SelectedIndex=0;
			l.Add("GAP");
			currentsys=4;
			this.checkedListBox1.Items.Clear();
			this.checkedListBox1.Items.Add("all");
			foreach(string ti in grpdetail)
			{if(ti.Contains("GAP"))
					this.checkedListBox1.Items.Add(ti);}
			
			this.button4.Text="GAP:"+click.ToString();
			this.label3.Text="指定需要安装的组别：";
			this.label3.Text="为GAP子系统"+this.label3.Text;
		}
		
		void Button5Click(object sender, EventArgs e)
		{
			f1 = false;
			f2 = false;
			timer1.Start();
			this.button1.Enabled=true;
			this.button2.Enabled=true;
			this.button3.Enabled=true;
			this.button4.Enabled=true;
			this.comboBox2.ResetText();
			this.dataGridView1.DataSource = null;
			click=0;
			currentsys=0;
			this.checkedListBox1.Items.Clear();
			l.Clear();
			opera.Clear();
			operadic.Clear();
			grpdetail.Clear();
			backup.Clear();
			grpdic.Clear();
			cfgdic.Clear();
			otherdic.Clear();
			sets.Clear();
			nostopupd.Clear();
			defother.Clear();
			defcfg.Clear();
			this.label3.Text="指定需要安装的组别：";
			this.dataGridView1.DataSource = null;
			this.button1.Text="ONL";
			this.button2.Text="UMS";
			this.button3.Text="BAT";
			this.button4.Text="GAP";
			this.textBox4.Visible = false;
			this.button12.Visible = false;
			
		
			
		
		}
		void Button6Click(object sender, EventArgs e)
		{
			
			
			
			
		}
		void MainFormLoad(object sender, EventArgs e)
		{
	
		}
		void Button9Click(object sender, EventArgs e)
		{
			
			if (opera.Count==0)
			{ MessageBox.Show("还未开始配置，请先配置！", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			
			DataTable tb1 = new DataTable();
			tb1.Columns.Add("组名",System.Type.GetType("System.String"));
			tb1.Columns.Add("配置方式", System.Type.GetType("System.String"));
			tb1.Columns.Add("xml配置", System.Type.GetType("System.String"));
			tb1.Columns.Add("sh配置", System.Type.GetType("System.String"));
			tb1.Columns.Add("是否不停机更新", System.Type.GetType("System.String"));
			tb1.Columns.Add("更新服务项", System.Type.GetType("System.String"));


		







			foreach (string xi in l)
			{
				List<string> temp=new List<string>();
				foreach(string yi in opera)
					
					{if(yi.Contains(xi))
						temp.Add(yi);}
				operadic.Add(xi,temp);
				
						
			}
			
			int iloc=0,opr=-1;
			
			

			foreach(string xt in l)
			{
				opr=sets[iloc];
				int ti=0;
				foreach(string tempopera in operadic[xt])
				
				{
					DataRow dr = tb1.NewRow();
					//this.dataGridView1.DataSource = tb1;
					switch (opr)
				{
						case 1:dr[0] = tempopera;dr[1] = "默认配置";dr[2] = defcfg[xt];dr[3] = defother[xt];
							dr[4] = "否";dr[5] = "--";
							
						
						
						break;
						
						case 2:
							dr[0] = tempopera; dr[1] = "统一配置"; dr[2] = string.Join(",", cfgdic[xt]); 
							dr[3] = string.Join(",", otherdic[xt]);
							 dr[5] = "--";


							if (nostopupd[xt])
							{ dr[4] = "是"; dr[5] = xtpdic[xt]; }
							else
							{ dr[4] = "否"; dr[5] = "--"; }

						
						
						break;
						
						case 3:
							
						string[] tset = otherdic[xt].Split(';'), cset = cfgdic[xt].Split(';'), oset=null;
							dr[0] = tempopera; dr[1] = "分别配置"; dr[2] = cset[ti];
							dr[3] = tset[ti];
							if (nostopupd[xt])
								oset = xtpdic[xt].Split(';');


							if (nostopupd[xt])
							{
								dr[4] = "是"; dr[5]=oset[ti];
							}
							else
							{ dr[4] = "否"; dr[5] = "--"; }

							ti++;
						break;
						
				
				}

					tb1.Rows.Add(dr);
				}
				
				iloc++;
			}
			this.dataGridView1.DataSource = tb1;
			System.DateTime currentTime = new System.DateTime();
			currentTime = System.DateTime.Now;
			datatableToCSV(tb1, "config_log.csv");
				
		}

		public static bool datatableToCSV(DataTable dt, string pathFile)
		{
			string strLine = "";
			StreamWriter sw;
			try
			{
				sw = new StreamWriter(pathFile, false, System.Text.Encoding.GetEncoding(-0)); //覆盖
																							  //表头
				for (int i = 0; i < dt.Columns.Count; i++)
				{
					if (i > 0)
						strLine += ",";
					strLine += dt.Columns[i].ColumnName;
				}
				strLine.Remove(strLine.Length - 1);
				sw.WriteLine(strLine);
				strLine = "";
				//表的内容
				for (int j = 0; j < dt.Rows.Count; j++)
				{
					strLine = "";
					int colCount = dt.Columns.Count;
					for (int k = 0; k < colCount; k++)
					{
						if (k > 0 && k < colCount)
							strLine += ",";
						if (dt.Rows[j][k] == null)
							strLine += "";
						else
						{
							string cell = dt.Rows[j][k].ToString().Trim();
							//防止里面含有特殊符号
							cell = cell.Replace("\"", "\"\"");
							cell = "\"" + cell + "\"";
							strLine += cell;
						}
					}
					sw.WriteLine(strLine);
				}
				sw.Close();
				string msg = "数据被成功导出到：" + pathFile;
				
			}
			catch (Exception ex)
			{
				string msg = "导出错误：" + pathFile;
				
				return false;
			}
			return true;
		}


		void ComboBox2SelectedIndexChanged(object sender, EventArgs e)
		{
			this.timer1.Stop();
			comboBox2.BackColor = Color.White;
			this.timer2.Interval = 500;
			timer2.Start();
			this.button1.Enabled = true;
			this.button2.Enabled = true;
			this.button3.Enabled = true;
			this.button4.Enabled = true;
			if(this.comboBox2.SelectedIndex==1)
			{this.button4.Visible=false;map="@MAPS_BJ";}
			else
			{this.button4.Visible=true;map="@MAPS";}
			
			StreamReader sr;
			string path="";
			grpdetail.Clear();
			if(comboBox2.SelectedIndex==0)
				path="shtopo.cfg";
			
	
			else if(comboBox2.SelectedIndex==1)
				path="bjtopo.cfg";
			
			sr=new StreamReader(path,Encoding.Default);
			string line;
			string[] templi;
			while((line=sr.ReadLine())!=null)
			{templi=line.Split(',');
				string tempgrp="";
				
				foreach(string ite in templi)
					{
					if (ite.Contains("GRP") && !grpdetail.Contains(ite) && !ite.Contains("_"))
					{grpdetail.Add(ite);tempgrp=ite;}
					if(ite.Contains("冷备主机"))
						backup.Add(tempgrp);
				}
			}
			//this.textBox1.Text=string.Join(">",grpdetail);
			

		}
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			timer3.Stop();
			timer4.Interval = 500;
			timer4.Start();
			comboBox1.BackColor = Color.White;
		}
		void TextBox3TextChanged(object sender, EventArgs e)
		{
	
		}
		void Button10Click(object sender, EventArgs e)
		{
			timer7.Stop();
			
			textBox2.BackColor = Color.White;
			textBox3.BackColor = Color.White;
			textBox5.BackColor = Color.White;
			button6.BackColor = Color.White;
			button10.BackColor = Color.White;
			if (this.comboBox3.SelectedIndex==0)
			{MessageBox.Show("还未填写配置方式，请选择并填写！","警告！",MessageBoxButtons.OK,MessageBoxIcon.Error);return;}
			b7clk();
			b8clk();
			if (!f1 || !f2)
				return;
			f1 = f2=false;
			sets.Add(this.comboBox3.SelectedIndex);
			List<int> temp=new List<int>();
			DialogResult r=DialogResult.Cancel;


			if(currentsys==1)
			{foreach(int indx in this.checkedListBox1.CheckedIndices)
					{if(indx!=0)temp.Add(indx);}
				grpdic.Add("ONL",temp);
				nostopupd.Add("ONL", this.checkBox2.Checked);
			r=MessageBox.Show("为ONL子系统配置成功！","结果",MessageBoxButtons.OK);}
			
			else if(currentsys==2)
				{foreach(int indx in this.checkedListBox1.CheckedIndices)
					{if(indx!=0)temp.Add(indx);}
				grpdic.Add("UMS",temp);
				nostopupd.Add("UMS", this.checkBox2.Checked);
				r =MessageBox.Show("为UMS子系统配置成功！","结果",MessageBoxButtons.OK);}
			
			else if(currentsys==3)
				{foreach(int indx in this.checkedListBox1.CheckedIndices)
				{if(indx!=0)temp.Add(indx);}
				grpdic.Add("BAT",temp);
				nostopupd.Add("BAT", this.checkBox2.Checked);
				r =MessageBox.Show("为BAT子系统配置成功！","结果",MessageBoxButtons.OK);}
			
			else if(currentsys==4)
				{foreach(int indx in this.checkedListBox1.CheckedIndices)
					{if(indx!=0)temp.Add(indx);}
				grpdic.Add("GAP",temp);
				nostopupd.Add("GAP", this.checkBox2.Checked);
				r =MessageBox.Show("为GAP子系统配置成功！","结果",MessageBoxButtons.OK);}
			
			for(int i=0;i<this.checkedListBox1.CheckedItems.Count;i++)
			{if(this.checkedListBox1.CheckedItems[i].ToString()!="all")
					opera.Add(this.checkedListBox1.CheckedItems[i].ToString());}


			
			if(r==DialogResult.OK)
			{	this.checkedListBox1.Items.Clear();
				this.textBox3.Text=null;
				this.textBox2.Text=null;
				this.label3.Text="指定需要安装的组别：";
				this.label6.Text="配置结果：";
				if(currentsys==1)
				{this.button12.Visible=false;
				this.textBox4.Visible=false;}
			this.textBox2.Enabled=false;
			this.textBox3.Enabled=false;
			temp=null;
			button1.Enabled=true;
			button2.Enabled=true;
			button3.Enabled=true;
			button4.Enabled=true;
			foreach (string key in l)
				{if(key == "ONL")
						button1.Enabled = false;
					else if(key =="UMS")
						button2.Enabled = false;
					else if (key == "BAT")
						button3.Enabled = false;
					else if (key == "GAP")
						button4.Enabled = false;

				}
				this.checkBox2.Checked = false;
			
			}
		}
		void Button7Click(object sender, EventArgs e)
		{
			
			
		}

		public void b7clk()
        {
			this.label6.Text = null;
			if (this.comboBox3.SelectedIndex == 0)
			{ MessageBox.Show("还未选择配置方式！", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

			if (this.comboBox3.SelectedIndex == 1)
			{ switch(currentsys)
                {
					case 1:defcfg["ONL"] = "mtqService.xml";
						defother["ONL"] = "onl_rmIPC.sh sem";
						break;

					case 2:
						defcfg["UMS"] = "--";
						defother["UMS"] = "--";
						break;

					case 3:
						defcfg["BAT"] = "mtqService.xml;mtqService.xml_all;mtqService.xml_wb";
						defother["BAT"] = "bat_rmIPC.sh sem;chmod 777 batMbatBToOReset.sh && batMbatBToOReset.sh";
						break;

					case 4:
						defcfg["GAP"] = "--";
						defother["GAP"] = "--";
						break;

				}
			}

			if (this.comboBox3.SelectedIndex != 0 && this.comboBox3.SelectedIndex != 1)
			{
				//if(this.textBox2.Text=="")
				//{MessageBox.Show("未填写配置文件名，不能为空","警告！",MessageBoxButtons.OK,MessageBoxIcon.Error);return;}

				int ckd = this.checkedListBox1.CheckedIndices.Count;
				if (this.checkedListBox1.CheckedItems.Contains("all"))
					ckd--;


				if (currentsys == 1)
				{
					if (this.comboBox3.SelectedIndex == 3)
					{
						if (this.textBox2.Text.Split(';').Length != ckd)
						{ MessageBox.Show("配置ONL xml数量不等于所选组数目，请检查", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
					}
					cfgdic["ONL"] = this.textBox2.Text;

				}
				else if (currentsys == 2)
				{
					if (this.comboBox3.SelectedIndex == 3)
					{
						if (this.textBox2.Text.Split(';').Length != ckd)
						{ MessageBox.Show("配置UMS xml数量不等于所选组数目，请检查", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
					}
					cfgdic["UMS"] = this.textBox2.Text;
				}
				else if (currentsys == 3)
				{
					if (this.comboBox3.SelectedIndex == 3)
					{
						if (this.textBox2.Text.Split(';').Length != ckd)
						{ MessageBox.Show("配置BAT xml数量不等于所选组数目，请检查", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
					}
					cfgdic["BAT"] = this.textBox2.Text;
				}
				else if (currentsys == 4)
				{
					if (this.comboBox3.SelectedIndex == 3)
					{
						if (this.textBox2.Text.Split(';').Length != ckd)
						{ MessageBox.Show("配置GAP xml数量不等于所选组数目，请检查", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
					}

					cfgdic["GAP"] = this.textBox2.Text;
				}
			}
			this.label6.Text = "配置XML成功";
			f1 = true;
		}
		void Button8Click(object sender, EventArgs e)
		{
			
		}

		public void b8clk()
        {
			this.label6.Text = null;
			int ckd = this.checkedListBox1.CheckedIndices.Count;
			if (this.checkedListBox1.CheckedItems.Contains("all"))
				ckd--;
			if (this.comboBox3.SelectedIndex == 0)
			{ MessageBox.Show("还未选择配置方式！", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }


			if (this.comboBox3.SelectedIndex != 0 && this.comboBox3.SelectedIndex != 1)
			{
				//if(this.textBox3.Text=="")
				//{MessageBox.Show("未填写配置脚本名，不能为空","警告！",MessageBoxButtons.OK,MessageBoxIcon.Error);return;}

				if (currentsys == 1)
				{
					if (this.comboBox3.SelectedIndex == 3)
					{
						if (this.textBox3.Text.Split(';').Length != ckd)
						{ MessageBox.Show("配置sh的数量不等于所选组数目，请检查", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
					}
					otherdic["ONL"] = this.textBox3.Text;
				}
				else if (currentsys == 2)
				{
					if (this.comboBox3.SelectedIndex == 3)
					{
						if (this.textBox3.Text.Split(';').Length != ckd)
						{ MessageBox.Show("配置sh数量不等于所选组数目，请检查", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
					}
					otherdic["UMS"] = this.textBox3.Text;
				}
				else if (currentsys == 3)
				{
					if (this.comboBox3.SelectedIndex == 3)
					{
						if (this.textBox3.Text.Split(';').Length != ckd)
						{ MessageBox.Show("配置sh数量不等于所选组数目，请检查", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
					}
					otherdic["BAT"] = this.textBox3.Text;
				}
				else if (currentsys == 4)
				{
					if (this.comboBox3.SelectedIndex == 3)
					{
						if (this.textBox3.Text.Split(';').Length != ckd)
						{ MessageBox.Show("配置sh数量不等于所选组数目，请检查", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
					}
					otherdic["GAP"] = this.textBox3.Text;
				}

			}
			this.label6.Text = "配置other成功";
			f2 = true;
		}
		void Button12Click(object sender, EventArgs e)
		{
			onlcheck = this.label6.Text;
			this.label6.Text=null;

			this.label6.Text="成功注入检查命令";
		}
		void ComboBox3SelectedIndexChanged(object sender, EventArgs e)
		{
			timer6.Stop();
			comboBox3.BackColor = Color.White;
			
			if(this.comboBox3.SelectedIndex!=0 && this.comboBox3.SelectedIndex!=1)
			{this.textBox2.Enabled=true;
				this.textBox3.Enabled=true;
				this.checkBox2.Enabled = true;
				timer7.Interval = 500;
				timer7.Start();

			}
			else
				this.checkBox2.Enabled = false;
		}
		void Button11Click(object sender, EventArgs e)
		{
			//Form2 frm=new Form2(l);
			//frm.Show();
			if (opera.Count == 0)
			{ MessageBox.Show("还未开始配置，请先配置！", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			string topopath ="";
			List<string> topoinfo=new List<string>();
			if(this.comboBox2.SelectedIndex==0)
				topopath="shtopo.cfg";
			else
				topopath="bjtopo.cfg";
			
			StreamReader srtp=new StreamReader(topopath,Encoding.Default);
			string line;
			while((line=srtp.ReadLine())!=null)
				topoinfo.Add(line);
			srtp.Dispose();
			StreamReader sr;
			if (this.comboBox1.SelectedIndex==0)
			sr=new StreamReader("temp_late.cfg",Encoding.Default);
			else
			sr = new StreamReader("temp_custom.cfg", Encoding.Default);
			List<string> template=new List<string>();
			System.DateTime currentTime = new System.DateTime();
			currentTime = System.DateTime.Now;
			while ((line=sr.ReadLine())!=null){
				template.Add(line);

			if(line.Contains("自动化安装编排文件"))
			{template.Add("#  edited by malei "+currentTime.ToString("f"));
				template.Add("#");template.Add("#");
				foreach(string tline in topoinfo)
					template.Add(tline);}
			}
			sr.Dispose();
			FileStream fs;
			if(comboBox2.SelectedIndex==0)
				fs=new FileStream("install.cfg",FileMode.Create);
			else
				fs=new FileStream("install.cfg.bj",FileMode.Create);
			
			StreamWriter sw=new StreamWriter(fs,Encoding.Default);
			
			
			foreach(string x in template)
				sw.WriteLine(x);
			sw.WriteLine("");
			if(this.comboBox2.SelectedIndex==1)
				sw.WriteLine("[确认步骤!!请确认数据库变更已经完成!!][<AP_USER>@MAPS_BJ:BJONLGRP1:1]");
			else
				sw.WriteLine("[确认步骤!!请确认数据库变更已经完成!!][<AP_USER>@MAPS:ONLGRP1:1]");
			
			sw.WriteLine("PREP [7200, Y]       = echo \"请确认数据库变更步骤已完成！！！\"");
			sw.WriteLine("");
			
			if(this.checkBox1.Checked==true)
			{
				
				if(this.comboBox2.SelectedIndex==1)
				{ sw.WriteLine("[准备备份并导入参数][<AP_USER>@MAPS_BJ:BJBATGRP1:1]");
					sw.WriteLine("PREP [7200, Y]       = echo \"准备开始备份并导入参数\"\n");
					sw.WriteLine("############# 更新行业与内容参数\n");
					sw.WriteLine("[备份并导入参数][<AP_USER>@MAPS_BJ:BJBATGRP1:1]");
					
				}
				
				else
				{
					sw.WriteLine("[准备备份并导入参数][<AP_USER>@MAPS:BATGRP1:1]");
					sw.WriteLine("PREP [7200, Y]       = echo \"准备开始备份并导入参数\"\n");
					sw.WriteLine("############# 更新行业与内容参数\n");
					sw.WriteLine("[备份并导入参数][<AP_USER>@MAPS:BATGRP1:1]");
				}
				
				
				

				sw.WriteLine("INSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>");
			sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh data");
			sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--备份并导入参数\"");
			sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh data");
			
			}
			
			else
				sw.WriteLine("############# 本次不更新行业与内容参数");
			
			
			string pre="";
			if(this.comboBox2.SelectedIndex==0)
				pre="\n[停止健康汇总端主][<AP_USER>@MAPS:HCSGRP1]\nINSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>  SHMAPHCS\n                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh stop -s HCSS -s MTQ\nROLLBACK [3600, Y]   = echo \"回退--停止健康汇总端主\"\n                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh stop -s HCSS -s MTQ\n                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh start -s HCSS -s MTQ\n";
			else
				pre=pre="\n[停止健康汇总端主][<AP_USER>@MAPS_BJ:BJHCSGRP1]\nINSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>  BJMAPHCS\n                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh stop -s HCSS -s MTQ\nROLLBACK [3600, Y]   = echo \"回退--停止健康汇总端主\"\n                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh stop -s HCSS -s MTQ\n                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh start -s HCSS -s MTQ\n";
			
			sw.Write(pre);
			sw.WriteLine("\n\n");
			int loc=0,start,cfgnum=-1,othnum=-1;				
			int startx;				
			bool first;				
			string[] cfg;
			string[] other;
			string[] xtpgrp;
			string xtp = null;
			string[] cfgitem;
			string[] otheritem;


			//按点击顺序写入安装应用更新
			foreach (string x in l)
			{
				start = 0;

				cfg = null;
				other = null;
				
				xtpgrp = null;

				first = false;

				if (x == "ONL")
				{
					if (!nostopupd["ONL"])
					{
						cfgnum = 0;
						othnum = 0;
						if (sets[loc] == 2)
						{
							cfg = cfgdic["ONL"].Split(',');
							other = otherdic["ONL"].Split(',');
						}

						else if (sets[loc] == 3)
						{
							cfg = cfgdic["ONL"].Split(';');
							other = otherdic["ONL"].Split(';');
						}

						foreach (string items in operadic[x])
						{
							startx = grpdic["ONL"][start];
							sw.WriteLine("");
							sw.WriteLine("############# 联机组" + startx.ToString() + "\n");
							string tline = "";
							if (!first)
							{
								tline = "[准备安装联机组" + startx.ToString() + "][<AP_USER>" + map + ":" + items + ":1]\nPREP [7200, Y]       = echo \"准备开始安装联机组" + startx.ToString() + "应用\"\n";
								first = true;
							}
							else
								tline = "[准备安装联机" + startx.ToString() + "][<AP_USER>" + map + ":" + items + ":1]\nPREP [7200, Y]       = echo \"准备开始安装联机组" + startx.ToString() + "应用,继续安装前请确认联机组" + grpdic["ONL"][start - 1].ToString() + "安装后交易无异常！\"\n";

							sw.Write(tline);

							sw.Write("[安装联机组" + startx.ToString() + "应用][<AP_USER>" + map + ":" + items + "]\n");

							if (comboBox2.SelectedIndex == 1) sw.WriteLine("INSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>  BJMAPONL");
							else sw.WriteLine("INSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>  SHMAPONL");

							sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh stop -s ONL -s MTQ");
							sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh shm -p CLOSE");
							sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh backup -s ONL -i \$INSTALLDIR -b \$BAKDIR -d \$BAKDATE");
							sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh update -s ONL -i \$INSTALLDIR");

							switch (sets[loc])
							{
								case 1:
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh updcfg -s \$INSTALLDIR/mtq/etc/mtqService.xml -f \$HOME/install/<VERSION>/<MODULE>/install/patchscript_linux/etc/operation.ini");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"sh $HOME/onl/sbin/onl_rmIPC.sh sem\"");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh start -s ONL -s MTQ");
									sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装联机组应用\"");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh stop -s ONL -s MTQ");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh shm -p CLOSE");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s ONL -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recovercfg -f mtqService.xml -d \$INSTALLDIR/mtq/etc -b \$BAKDIR/ONL_\$BAKDATE/maps_upel/mtq/etc");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"sh $HOME/onl/sbin/onl_rmIPC.sh sem\"");

									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh start -s ONL -s MTQ");
									break;


								case 2:


									foreach (string tnow in cfg)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh updcfg -s \$INSTALLDIR/mtq/etc/" + tnow + @" -f \$HOME/install/<VERSION>/<MODULE>/install/patchscript_linux/etc/operation.ini"); }
									foreach (string onow in other)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"sh $HOME/onl/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh start -s ONL -s MTQ");
									sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装联机组应用\"");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh stop -s ONL -s MTQ");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh shm -p CLOSE");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s ONL -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");
									foreach (string tnow in cfg)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recovercfg -f " + tnow + @" -d \$INSTALLDIR/mtq/etc -b \$BAKDIR/ONL_\$BAKDATE/maps_upel/mtq/etc"); }
									foreach (string onow in other)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"sh $HOME/onl/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh start -s ONL -s MTQ");


									break;


								case 3:

									cfgitem = cfg[cfgnum].Split(',');
									otheritem = other[othnum].Split(',');
									cfgnum++; othnum++;
									foreach (string tnow in cfgitem)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh updcfg -s \$INSTALLDIR/mtq/etc/" + tnow + @" -f \$HOME/install/<VERSION>/<MODULE>/install/patchscript_linux/etc/operation.ini"); }
									foreach (string onow in otheritem)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"sh $HOME/onl/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh start -s ONL -s MTQ");
									sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装联机组应用\"");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh stop -s ONL -s MTQ");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh shm -p CLOSE");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s ONL -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");
									foreach (string tnow in cfgitem)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recovercfg -f " + tnow + @" -d \$INSTALLDIR/mtq/etc -b \$BAKDIR/ONL_\$BAKDATE/maps_upel/mtq/etc"); }
									foreach (string onow in otheritem)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"sh $HOME/onl/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh start -s ONL -s MTQ");


									break;

							}
							sw.WriteLine("#检查命令" + onlcheck);
							sw.WriteLine("#联机组" + startx.ToString() + "应用启动后节点隔离状态检测");
							sw.WriteLine("#联机组" + startx.ToString() + "应用启动后联机交易检查");
							sw.Write("#联机组" + startx.ToString() + "应用启动后健康交易检查\n");
							
							start++;
						}



					}


					else //如果选择不停机更新
					{
						cfgnum = 0;
						othnum = 0;
						if (sets[loc] == 2)
						{
							cfg = cfgdic["ONL"].Split(',');
							other = otherdic["ONL"].Split(',');
							xtpgrp = xtpdic["ONL"].Split(';');
						}

						else if (sets[loc] == 3)
						{
							cfg = cfgdic["ONL"].Split(';');
							other = otherdic["ONL"].Split(';');
							xtpgrp = xtpdic["ONL"].Split(';');
						}

						foreach (string items in operadic[x])
						{
							startx = grpdic["ONL"][start];
							sw.WriteLine("");
							sw.WriteLine("############# 联机组" + startx.ToString() + "不停机更新\n");
							string tline = "";
							if (!first)
							{
								tline = "[准备安装联机组" + startx.ToString() + "][<AP_USER>" + map + ":" + items + ":1]\nPREP [7200, Y]       = echo \"准备开始安装联机组" + startx.ToString() + "应用\"\n";
								first = true;
							}
							else
								tline = "[准备安装联机" + startx.ToString() + "][<AP_USER>" + map + ":" + items + ":1]\nPREP [7200, Y]       = echo \"准备开始安装联机组" + startx.ToString() + "应用,继续安装前请确认联机组" + grpdic["ONL"][start - 1].ToString() + "安装后交易无异常！\"\n";

							sw.Write(tline);

							sw.Write("[安装联机组" + startx.ToString() + "应用][<AP_USER>" + map + ":" + items + "]\n");

							if (comboBox2.SelectedIndex == 1) sw.WriteLine("INSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>  BJMAPONL");
							else sw.WriteLine("INSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>  SHMAPONL");

							switch (sets[loc])
							{

								case 2:

									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh backup -s ONL -i \$INSTALLDIR -b \$BAKDIR -d \$BAKDATE");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin svrstop " + xtpgrp[0] + "\"");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh update -s ONL -i \$INSTALLDIR");

									foreach (string tnow in cfg)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh updcfg -s \$INSTALLDIR/mtq/etc/" + tnow + @" -f \$HOME/install/<VERSION>/<MODULE>/install/patchscript_linux/etc/operation.ini"); }
									if(cfg[0]!="")
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin reloadcfg service\"");

									foreach (string onow in other)
									{ if ( onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"sh $HOME/onl/sbin/" + onow + "\""); }

									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin svrstart " + xtpgrp[0] + "\"");

									sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装联机组应用\"");

									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"mtqadmin svrstop " + xtpgrp[0] + "\"");


									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s ONL -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");
									foreach (string tnow in cfg)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recovercfg -f " + tnow + @" -d \$INSTALLDIR/mtq/etc -b \$BAKDIR/ONL_\$BAKDATE/maps_upel/mtq/etc"); }
									foreach (string onow in other)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"sh $HOME/onl/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"mtqadmin svrstart " + xtpgrp[0] + "\"");


									break;


								case 3:

									cfgitem = cfg[cfgnum].Split(',');
									otheritem = other[othnum].Split(',');
									xtp = xtpgrp[othnum];
									cfgnum++; othnum++;
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh backup -s ONL -i \$INSTALLDIR -b \$BAKDIR -d \$BAKDATE");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin svrstop " + xtp + "\"");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh update -s ONL -i \$INSTALLDIR");


									foreach (string tnow in cfgitem)
									{ if ( tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh updcfg -s \$INSTALLDIR/mtq/etc/" + tnow + @" -f \$HOME/install/<VERSION>/<MODULE>/install/patchscript_linux/etc/operation.ini"); }
									if (cfgitem[0] != "")
										sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin reloadcfg service\"");

									foreach (string onow in otheritem)
									{ if ( onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"sh $HOME/onl/sbin/" + onow + "\""); }

									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin svrstart " + xtp + "\"");

									sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装联机组应用\"");

									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"mtqadmin svrstop " + xtp + "\"");


									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s ONL -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");


									foreach (string tnow in cfgitem)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recovercfg -f " + tnow + @" -d \$INSTALLDIR/mtq/etc -b \$BAKDIR/ONL_\$BAKDATE/maps_upel/mtq/etc"); }
									foreach (string onow in otheritem)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"sh $HOME/onl/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin svrstart " + xtp + "\"");


									break;

							}
							sw.WriteLine("#联机组" + startx.ToString() + "应用启动后节点隔离状态检测");
							sw.WriteLine("#联机组" + startx.ToString() + "应用启动后联机交易检查");
							sw.Write("#联机组" + startx.ToString() + "应用启动后健康交易检查\n");
							start++;
						}



					}
				}


				else if (x == "UMS")
				{
					if (!nostopupd["UMS"])
					{
						cfgnum = 0;
						othnum = 0;
						if (sets[loc] == 2)
						{
							cfg = cfgdic["UMS"].Split(',');
							other = otherdic["UMS"].Split(',');

						}

						else if (sets[loc] == 3)
						{
							cfg = cfgdic["UMS"].Split(';');
							other = otherdic["UMS"].Split(';');

						}


						foreach (string items in operadic[x])
						{
							startx = grpdic["UMS"][start];
							sw.Write("\n\n");
							sw.WriteLine("############# 加密服务组" + startx.ToString() + "\n");
							string tline = "";
							if (operadic[x].IndexOf(items) != 0)
								tline = "[准备安装加密服务组" + startx.ToString() + "][<AP_USER>" + map + ":" + items + ":1]\nPREP [7200, Y]       = echo \"准备开始安装联机组" + startx.ToString() + "应用，继续安装前请确认加密机" + grpdic["UMS"][start - 1].ToString() + "安装后交易无异常！\"\n";


							sw.Write(tline);

							sw.Write("[安装加密服务组" + startx.ToString() + "应用][<AP_USER>" + map + ":" + items + "]\n");

							sw.WriteLine("INSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>");
							sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh stop -s UMS -s MTQ");
							sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh shm -p CLOSE");
							sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh backup -s UMS -i \$INSTALLDIR -b \$BAKDIR -d \$BAKDATE");
							sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh update -s UMS -i \$INSTALLDIR");



							switch (sets[loc])
							{
								case 1:
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh start -s UMS -s MTQ");
									sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装加密服务组应用\"");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh stop -s UMS -s MTQ");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh shm -p CLOSE");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s UMS -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh start -s UMS -s MTQ");
									break;


								case 2:
									foreach (string tnow in cfg)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh updcfg -s \$INSTALLDIR/mtq/etc/" + tnow + @" -f \$HOME/install/<VERSION>/<MODULE>/install/patchscript_linux/etc/operation.ini"); }
									foreach (string onow in other)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"sh $HOME/onl/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh start -s UMS -s MTQ");
									sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装加密服务组应用\"");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh stop -s UMS -s MTQ");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh shm -p CLOSE");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s UMS -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");
									foreach (string tnow in cfg)
									{ if(tnow!="")sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recovercfg -f " + tnow + @" -d \$INSTALLDIR/mtq/etc -b \$BAKDIR/ONL_\$BAKDATE/maps_upel/mtq/etc"); }
									foreach (string onow in other)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"sh $HOME/onl/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh start -s UMS -s MTQ");

									break;


								case 3:

									cfgitem = cfg[cfgnum].Split(',');
									otheritem = other[othnum].Split(',');
									cfgnum++; othnum++;
									foreach (string tnow in cfgitem)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh updcfg -s \$INSTALLDIR/mtq/etc/" + tnow + @" -f \$HOME/install/<VERSION>/<MODULE>/install/patchscript_linux/etc/operation.ini"); }
									foreach (string onow in otheritem)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"sh $HOME/onl/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh start -s UMS -s MTQ");
									sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装加密服务组应用\"");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh stop -s UMS -s MTQ");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh shm -p CLOSE");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s UMS -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");
									foreach (string tnow in cfgitem)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recovercfg -f " + tnow + @" -d \$INSTALLDIR/mtq/etc -b \$BAKDIR/ONL_\$BAKDATE/maps_upel/mtq/etc"); }
									foreach (string onow in otheritem)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"sh $HOME/onl/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh start -s UMS -s MTQ");


									break;

							}
							start++;
						}

					}
					else
					{
						cfgnum = 0;
						othnum = 0;
						if (sets[loc] == 2)
						{
							cfg = cfgdic["UMS"].Split(',');
							other = otherdic["UMS"].Split(',');
							xtpgrp = xtpdic["UMS"].Split(';');
						}

						else if (sets[loc] == 3)
						{
							cfg = cfgdic["UMS"].Split(';');
							other = otherdic["UMS"].Split(';');
							xtpgrp = xtpdic["UMS"].Split(';');
						}


						foreach (string items in operadic[x])
						{
							startx = grpdic["UMS"][start];
							sw.Write("\n\n");
							sw.WriteLine("############# 加密服务组" + startx.ToString() + "不停机更新" + "\n");
							string tline = "";
							if (operadic[x].IndexOf(items) != 0)
								tline = "[准备安装加密服务组" + startx.ToString() + "][<AP_USER>" + map + ":" + items + ":1]\nPREP [7200, Y]       = echo \"准备开始安装联机组" + startx.ToString() + "应用，继续安装前请确认加密机" + grpdic["UMS"][start - 1].ToString() + "安装后交易无异常！\"\n";


							sw.Write(tline);

							sw.Write("[安装加密服务组" + startx.ToString() + "应用][<AP_USER>" + map + ":" + items + "]\n");

							sw.WriteLine("INSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>");
							sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh backup -s UMS -i \$INSTALLDIR -b \$BAKDIR -d \$BAKDATE");



							switch (sets[loc])
							{

								case 2:
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin svrstop " + xtpgrp[0] + "\"");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh update -s UMS -i \$INSTALLDIR");

									foreach (string tnow in cfg)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh updcfg -s \$INSTALLDIR/mtq/etc/" + tnow + @" -f \$HOME/install/<VERSION>/<MODULE>/install/patchscript_linux/etc/operation.ini"); }

									if (cfg[0] != "")
										sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin reloadcfg service\"");

									foreach (string onow in other)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"sh $HOME/onl/sbin/" + onow + "\""); }

									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin svrstart " + xtpgrp[0] + "\"");

									sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装加密服务组应用\"");

									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"mtqadmin svrstop " + xtpgrp[0] + "\"");

									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s UMS -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");
									foreach (string tnow in cfg)
									{ sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recovercfg -f " + tnow + @" -d \$INSTALLDIR/mtq/etc -b \$BAKDIR/ONL_\$BAKDATE/maps_upel/mtq/etc"); }
									foreach (string onow in other)
									{ sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"sh $HOME/onl/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"mtqadmin svrstart " + xtpgrp[0] + "\"");

									break;


								case 3:

									cfgitem = cfg[cfgnum].Split(',');
									otheritem = other[othnum].Split(',');
									xtp = xtpgrp[othnum];
									cfgnum++; othnum++;
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin svrstop " + xtp + "\"");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh update -s UMS -i \$INSTALLDIR");

									foreach (string tnow in cfgitem)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh updcfg -s \$INSTALLDIR/mtq/etc/" + tnow + @" -f \$HOME/install/<VERSION>/<MODULE>/install/patchscript_linux/etc/operation.ini"); }
									if (cfgitem[0] != "")
										
										sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin reloadcfg service\"");

									foreach (string onow in otheritem)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"sh $HOME/onl/sbin/" + onow + "\""); }

									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin svrstart " + xtp + "\"");

									sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装加密服务组应用\"");

									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"mtqadmin svrstop " + xtp + "\"");

									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s UMS -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");
									foreach (string tnow in cfgitem)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recovercfg -f " + tnow + @" -d \$INSTALLDIR/mtq/etc -b \$BAKDIR/ONL_\$BAKDATE/maps_upel/mtq/etc"); }
									foreach (string onow in otheritem)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"sh $HOME/onl/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"mtqadmin svrstart " + xtp + "\"");


									break;

							}
							start++;
						}
					}

				}

				else if (x == "BAT")
				{

					if (!nostopupd["BAT"])
					{
						cfgnum = 0;
						othnum = 0;
						if (sets[loc] == 2)
						{
							cfg = cfgdic["BAT"].Split(',');
							other = otherdic["BAT"].Split(',');
						}

						else if (sets[loc] == 3)
						{
							cfg = cfgdic["BAT"].Split(';');
							other = otherdic["BAT"].Split(';');
						}

						foreach (string items in operadic[x])
						{
							startx = grpdic["BAT"][start];
							sw.Write("\n\n");
							sw.WriteLine("############# 批量组" + startx.ToString() + "\n");
							string tline = "";
							if (operadic[x].IndexOf(items) != 0)
								tline = "[准备安装批量组" + startx.ToString() + "][<AP_USER>" + map + ":" + items + ":1]\nPREP [7200, Y]       = echo \"准备开始安装批量组" + startx.ToString() + "应用，继续安装前请确认批量组" + grpdic["BAT"][start - 1].ToString() + "安装后交易无异常！\"\n";


							sw.Write(tline);

							sw.Write("[安装批量组" + startx.ToString() + "应用][<AP_USER>" + map + ":" + items + "]\n");
							if (this.comboBox2.SelectedIndex == 0)
								sw.WriteLine("INSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>  SHMAPBAT");
							else
								sw.WriteLine("INSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>  BJMAPBAT");
							sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh stop -s BAT -s MTQ");
							sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh shm -p CLOSE");
							sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh backup -s BAT -i \$INSTALLDIR -b \$BAKDIR -d \$BAKDATE");
							sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh update -s BAT -i \$INSTALLDIR");


							switch (sets[loc])
							{
								case 1:
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh updcfg -s \$INSTALLDIR/mtq/etc/mtqService.xml -f \$HOME/install/<VERSION>/<MODULE>/install/patchscript_linux/etc/operation.ini");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh updcfg -s \$INSTALLDIR/mtq/etc/mtqService.xml_all -f \$HOME/install/<VERSION>/<MODULE>/install/patchscript_linux/etc/operation.ini");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh updcfg -s \$INSTALLDIR/mtq/etc/mtqService.xml_wb -f \$HOME/install/<VERSION>/<MODULE>/install/patchscript_linux/etc/operation.ini");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"sh $HOME/bat/sbin/bat_rmIPC.sh sem\"");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"chmod 777 $HOME/bat/sbin/batMbatBToOReset.sh && $HOME/bat/sbin/batMbatBToOReset.sh\"");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh start -s BAT -s MTQ");
									sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装批量组应用\"");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh stop -s BAT -s MTQ");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh shm -p CLOSE");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s BAT -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recovercfg -f mtqService.xml -d \$INSTALLDIR/mtq/etc -b \$BAKDIR/BAT_\$BAKDATE/maps_upel/mtq/etc");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recovercfg -f mtqService.xml_all -d \$INSTALLDIR/mtq/etc -b \$BAKDIR/BAT_\$BAKDATE/maps_upel/mtq/etc");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recovercfg -f mtqService.xml_wb -d \$INSTALLDIR/mtq/etc -b \$BAKDIR/BAT_\$BAKDATE/maps_upel/mtq/etc");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"chmod 777 $HOME/bat/sbin/batMbatBToOReset.sh && $HOME/bat/sbin/batMbatBToOReset.sh\"");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"sh $HOME/bat/sbin/bat_rmIPC.sh sem\"");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh start -s BAT -s MTQ");
									break;


								case 2:
									foreach (string tnow in cfg)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh updcfg -s \$INSTALLDIR/mtq/etc/" + tnow + @" -f \$HOME/install/<VERSION>/<MODULE>/install/patchscript_linux/etc/operation.ini"); }
									foreach (string onow in other)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"sh $HOME/bat/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh start -s BAT -s MTQ");

									sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装批量组应用\"");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh stop -s BAT -s MTQ");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh shm -p CLOSE");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s BAT -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");

									foreach (string tnow in cfg)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recovercfg -f " + tnow + @" -d \$INSTALLDIR/mtq/etc -b \$BAKDIR/ONL_\$BAKDATE/maps_upel/mtq/etc"); }
									foreach (string onow in other)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"sh $HOME/bat/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh start -s BAT -s MTQ");
									break;


								case 3:

									cfgitem = cfg[cfgnum].Split(',');
									otheritem = other[othnum].Split(',');
									cfgnum++; othnum++;
									foreach (string tnow in cfgitem)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh updcfg -s \$INSTALLDIR/mtq/etc/" + tnow + @" -f \$HOME/install/<VERSION>/<MODULE>/install/patchscript_linux/etc/operation.ini"); }
									foreach (string onow in otheritem)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"sh $HOME/bat/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh start -s BAT -s MTQ");

									sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装批量组应用\"");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh stop -s BAT -s MTQ");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh shm -p CLOSE");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s BAT -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");

									foreach (string tnow in cfgitem)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recovercfg -f " + tnow + @" -d \$INSTALLDIR/mtq/etc -b \$BAKDIR/ONL_\$BAKDATE/maps_upel/mtq/etc"); }
									foreach (string onow in otheritem)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"sh $HOME/bat/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh start -s BAT -s MTQ");
									break;


							}
							start++;
							if (operadic[x].IndexOf(items) == 0)
								sw.WriteLine("\n############# 补做批转实中断任务\n");
						}
					}
					else
					{
						cfgnum = 0;
						othnum = 0;
						if (sets[loc] == 2)
						{
							cfg = cfgdic["BAT"].Split(',');
							other = otherdic["BAT"].Split(',');
							xtpgrp = xtpdic["BAT"].Split(';');
						}

						else if (sets[loc] == 3)
						{
							cfg = cfgdic["BAT"].Split(';');
							other = otherdic["BAT"].Split(';');
							xtpgrp = xtpdic["BAT"].Split(';');
						}

						foreach (string items in operadic[x])
						{
							startx = grpdic["BAT"][start];
							sw.Write("\n\n");
							sw.WriteLine("############# 批量组" + startx.ToString() + "不停机更新" + "\n");

							string tline = "";
							if (operadic[x].IndexOf(items) != 0)
								tline = "[准备安装批量组" + startx.ToString() + "][<AP_USER>" + map + ":" + items + ":1]\nPREP [7200, Y]       = echo \"准备开始安装批量组" + startx.ToString() + "应用，继续安装前请确认批量组" + grpdic["BAT"][start - 1].ToString() + "安装后交易无异常！\"\n";


							sw.Write(tline);

							sw.Write("[安装批量组" + startx.ToString() + "应用][<AP_USER>" + map + ":" + items + "]\n");
							if (this.comboBox2.SelectedIndex == 0)
								sw.WriteLine("INSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>  SHMAPBAT");
							else
								sw.WriteLine("INSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>  BJMAPBAT");

							sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh backup -s BAT -i \$INSTALLDIR -b \$BAKDIR -d \$BAKDATE");


							switch (sets[loc])
							{

								case 2:
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin svrstop " + xtpgrp[0] + "\"");

									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh update -s BAT -i \$INSTALLDIR");

									foreach (string tnow in cfg)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh updcfg -s \$INSTALLDIR/mtq/etc/" + tnow + @" -f \$HOME/install/<VERSION>/<MODULE>/install/patchscript_linux/etc/operation.ini"); }
									if (cfg[0] != "")
										sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin reloadcfg service\"");
									foreach (string onow in other)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"sh $HOME/bat/sbin/" + onow + "\""); }

									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin svrstart " + xtpgrp[0] + "\"");

									sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装批量组应用\"");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin svrstop " + xtpgrp[0] + "\"");

									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s BAT -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");

									foreach (string tnow in cfg)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recovercfg -f " + tnow + @" -d \$INSTALLDIR/mtq/etc -b \$BAKDIR/ONL_\$BAKDATE/maps_upel/mtq/etc"); }
									foreach (string onow in other)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"sh $HOME/bat/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"mtqadmin svrstart " + xtpgrp[0] + "\"");
									break;


								case 3:

									cfgitem = cfg[cfgnum].Split(',');
									otheritem = other[othnum].Split(',');
									xtp = xtpgrp[othnum];
									cfgnum++; othnum++;

									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin svrstop " + xtp + "\"");

									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh update -s BAT -i \$INSTALLDIR");

									foreach (string tnow in cfgitem)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh updcfg -s \$INSTALLDIR/mtq/etc/" + tnow + @" -f \$HOME/install/<VERSION>/<MODULE>/install/patchscript_linux/etc/operation.ini"); }
									if (cfgitem[0] != "")
										
										sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin reloadcfg service\"");
									foreach (string onow in otheritem)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"sh $HOME/bat/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin svrstart " + xtp + "\"");

									sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装批量组应用\"");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin svrstop " + xtp + "\"");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s BAT -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");

									foreach (string tnow in cfgitem)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recovercfg -f " + tnow + @" -d \$INSTALLDIR/mtq/etc -b \$BAKDIR/ONL_\$BAKDATE/maps_upel/mtq/etc"); }
									foreach (string onow in otheritem)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"sh $HOME/bat/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"mtqadmin svrstart " + xtp + "\"");
									break;


							}
							start++;
							if (operadic[x].IndexOf(items) == 0)
								sw.WriteLine("\n############# 补做批转实中断任务\n");

						}
					}


				}

				else if (x == "GAP")
				{
					
					
						sw.WriteLine("[准备安装GAP应用][<AP_USER>" + map + ":" + "GAPGRP1:1]");

						sw.WriteLine("PREP [7200, Y]       = echo \"准备开始安装GAP及其他应用\"");
					
					if (!nostopupd["GAP"])
					{ 
					cfgnum = 0;
					othnum = 0;
					if (sets[loc] == 2)
					{
						cfg = cfgdic["GAP"].Split(',');
						other = otherdic["GAP"].Split(',');
					}

					else if (sets[loc] == 3)
					{
						cfg = cfgdic["GAP"].Split(';');
						other = otherdic["GAP"].Split(';');
					}

					foreach (string items in operadic[x])
					{
						startx = grpdic["GAP"][start];
						sw.Write("\n\n");
						sw.WriteLine("############# GAP组" + startx.ToString() + "\n");



						sw.Write("[安装GAP组" + startx.ToString() + "应用][<AP_USER>" + map + ":" + items + "]\n");

						if (backup.Contains(items))
						{
							sw.WriteLine("INSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>");
							sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh backup -s GAP -i \$INSTALLDIR -b \$BAKDIR -d \$BAKDATE");
							sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh update -s GAP -i \$INSTALLDIR");
							sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装GAP组应用\"");
							sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s GAP -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");
							start++;
							continue;

						}

						sw.WriteLine("INSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>");
						sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh stop -s GAP -s MTQ");
						sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh shm -p CLOSE");
						sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh backup -s GAP -i \$INSTALLDIR -b \$BAKDIR -d \$BAKDATE");
						sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh update -s GAP -i \$INSTALLDIR");



						switch (sets[loc])
						{
							case 1:
								sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh start -s GAP -s MTQ");
								sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装GAP组应用\"");
								sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh stop -s GAP -s MTQ");
								sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh shm -p CLOSE");
								sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s GAP -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");
								sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh start -s GAP -s MTQ");
								break;


							case 2:
								foreach (string tnow in cfg)
								{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh updcfg -s \$INSTALLDIR/mtq/etc/" + tnow + @" -f \$HOME/install/<VERSION>/<MODULE>/install/patchscript_linux/etc/operation.ini"); }
								foreach (string onow in other)
								{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"sh $HOME/gap/sbin/" + onow + "\""); }
								sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh start -s GAP -s MTQ");

								sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装批量组应用\"");
								sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh stop -s BAT -s MTQ");
								sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh shm -p CLOSE");
								sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s BAT -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");

								foreach (string tnow in cfg)
								{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recovercfg -f " + tnow + @" -d \$INSTALLDIR/mtq/etc -b \$BAKDIR/ONL_\$BAKDATE/maps_upel/mtq/etc"); }
								foreach (string onow in other)
								{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"sh $HOME/bat/sbin/" + onow + "\""); }
								sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh start -s BAT -s MTQ");
								break;


							case 3:

								cfgitem = cfg[cfgnum].Split(',');
								otheritem = other[othnum].Split(',');
								cfgnum++; othnum++;
								foreach (string tnow in cfgitem)
								{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh updcfg -s \$INSTALLDIR/mtq/etc/" + tnow + @" -f \$HOME/install/<VERSION>/<MODULE>/install/patchscript_linux/etc/operation.ini"); }
								foreach (string onow in otheritem)
								{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"sh $HOME/gap/sbin/" + onow + "\""); }
								sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh start -s BAT -s MTQ");

								sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装GAP组应用\"");
								sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh stop -s GAP -s MTQ");
								sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh shm -p CLOSE");
								sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s GAP -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");

								foreach (string tnow in cfgitem)
								{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recovercfg -f " + tnow + @" -d \$INSTALLDIR/mtq/etc -b \$BAKDIR/ONL_\$BAKDATE/maps_upel/mtq/etc"); }
								foreach (string onow in otheritem)
								{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"sh $HOME/gap/sbin/" + onow + "\""); }
								sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh start -s GAP -s MTQ");
								break;


						}
						start++;
					}

				}

                    else
                    {
						cfgnum = 0;
						othnum = 0;
						if (sets[loc] == 2)
						{
							cfg = cfgdic["GAP"].Split(',');
							other = otherdic["GAP"].Split(',');
							xtpgrp = xtpdic["GAP"].Split(';');
						}

						else if (sets[loc] == 3)
						{
							cfg = cfgdic["GAP"].Split(';');
							other = otherdic["GAP"].Split(';');
							xtpgrp = xtpdic["GAP"].Split(';');
						}

						foreach (string items in operadic[x])
						{
							startx = grpdic["GAP"][start];
							sw.Write("\n\n");
							
							sw.WriteLine("############# GAP组" + startx.ToString() + "不停机更新" + "\n");




							sw.Write("[安装GAP组" + startx.ToString() + "应用][<AP_USER>" + map + ":" + items + "]\n");

							if (backup.Contains(items))
							{
								sw.WriteLine("INSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>");
								sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh backup -s GAP -i \$INSTALLDIR -b \$BAKDIR -d \$BAKDATE");
								sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh update -s GAP -i \$INSTALLDIR");
								sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装GAP组应用\"");
								sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s GAP -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");
								start++;
								continue;

							}

							sw.WriteLine("INSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>");
							sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh backup -s GAP -i \$INSTALLDIR -b \$BAKDIR -d \$BAKDATE");
							


							switch (sets[loc])
							{
							

								case 2:

									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin svrstop " + xtpgrp[0] + "\"");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh update -s GAP -i \$INSTALLDIR");

									

									foreach (string tnow in cfg)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh updcfg -s \$INSTALLDIR/mtq/etc/" + tnow + @" -f \$HOME/install/<VERSION>/<MODULE>/install/patchscript_linux/etc/operation.ini"); }
									if (cfg[0] != "")
										sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin reloadcfg service\"");

									foreach (string onow in other)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"sh $HOME/gap/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin svrstart " + xtpgrp[0] + "\"");


									sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装批量组应用\"");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"mtqadmin svrstop " + xtpgrp[0] + "\"");

									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s BAT -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");

									foreach (string tnow in cfg)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recovercfg -f " + tnow + @" -d \$INSTALLDIR/mtq/etc -b \$BAKDIR/ONL_\$BAKDATE/maps_upel/mtq/etc"); }
									foreach (string onow in other)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"sh $HOME/bat/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"mtqadmin svrstart " + xtpgrp[0] + "\"");

									break;


								case 3:

									cfgitem = cfg[cfgnum].Split(',');
									otheritem = other[othnum].Split(',');
									xtp = xtpgrp[othnum];
									cfgnum++; othnum++;

									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin svrstop " + xtp + "\"");
									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh update -s GAP -i \$INSTALLDIR");


									foreach (string tnow in cfgitem)
									{ if (tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh updcfg -s \$INSTALLDIR/mtq/etc/" + tnow + @" -f \$HOME/install/<VERSION>/<MODULE>/install/patchscript_linux/etc/operation.ini"); }
									if (cfgitem[0] != "")
										
										sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin reloadcfg service\"");


									foreach (string onow in otheritem)
									{ if (onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"sh $HOME/gap/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh other \"mtqadmin svrstart " + xtp + "\"");


									sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装GAP组应用\"");
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"mtqadmin svrstop " + xtp + "\"");

									sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s GAP -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");

									foreach (string tnow in cfgitem)
									{ if ( tnow != "") sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recovercfg -f " + tnow + @" -d \$INSTALLDIR/mtq/etc -b \$BAKDIR/ONL_\$BAKDATE/maps_upel/mtq/etc"); }
									foreach (string onow in otheritem)
									{ if ( onow != "") sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"sh $HOME/gap/sbin/" + onow + "\""); }
									sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh other \"mtqadmin svrstart " + xtp + "\"");

									break;


							}
							start++;
						}
					}
				}





			
				loc++;
			}
			
			sw.WriteLine("\n\n");
			if(this.comboBox2.SelectedIndex==0)
			{sw.WriteLine("[安装健康汇总端主][<AP_USER>@MAPS:HCSGRP1]");
			sw.WriteLine("INSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>  SHMAPHCS");}
			else
			{sw.WriteLine("[安装健康汇总端主][<AP_USER>@MAPS_BJ:BJHCSGRP1]");
				sw.WriteLine("INSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>  BJMAPHCS");}

							
			
			sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh backup -s HCS -i \$INSTALLDIR -b \$BAKDIR -d \$BAKDATE");
			sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh update -s HCS -i \$INSTALLDIR");
			sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh start -s HCSS -s MTQ");
			sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装健康汇总端主\"");
			sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh stop -s HCSS -s MTQ");
			sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s HCS -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");
			sw.WriteLine("                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh start -s HCSS -s MTQ");
			sw.WriteLine("\n\n");
			
			
			if(this.comboBox2.SelectedIndex==0)
			{sw.WriteLine("[安装健康汇总端备][<AP_USER>@MAPS:HCSGRP2]");
			sw.WriteLine("INSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>  SHMAPHCS");}
			else
			{sw.WriteLine("[安装健康汇总端备][<AP_USER>@MAPS_BJ:BJHCSGRP2]");
				sw.WriteLine("INSTALL [ 7200, N]   = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/genConfig.sh  <VERSION>  <MODULE>  BJMAPHCS");}

							
			
			sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh backup -s HCS -i \$INSTALLDIR -b \$BAKDIR -d \$BAKDATE");
			sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/install.sh update -s HCS -i \$INSTALLDIR");
			sw.WriteLine("ROLLBACK [3600, Y]   = echo \"回退--安装健康汇总端备\"");
			sw.WriteLine(@"                     = sh ~/install/<VERSION>/<MODULE>/install/patchscript_linux/uninstall.sh recover -s HCS -b \$BAKDIR -i \$INSTALLDIR -d \$BAKDATE");
			
			sw.Flush();
			sw.Close();
			fs.Close();
			//this.textBox1.Text=string.Join(">",operadic["UMS"]);
			MessageBox.Show("编排成功，已在当前文件夹下生成编排文件,为您打开文件夹","结果",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
			
			this.comboBox2.Select();
			string path = System.Environment.CurrentDirectory;
			ExplorePath(path);
			button5.PerformClick();

		}
		
		void CheckedListBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			timer5.Stop();
			timer6.Interval = 500;
			timer6.Start();
			checkedListBox1.BackColor = Color.White;
			if(checkedListBox1.GetSelected(0)==true &&!f)
			{for(int i=0;i<this.checkedListBox1.Items.Count-1;i++)
					this.checkedListBox1.SetItemChecked(i+1,true);f=!f;
				this.checkedListBox1.SetItemChecked(0, true);
			}
			else if(checkedListBox1.GetSelected(0)==true && f)
			{
				for (int i = 0; i < this.checkedListBox1.Items.Count - 1; i++)
					this.checkedListBox1.SetItemChecked(i+1, false);f=!f;
				this.checkedListBox1.SetItemChecked(0, false);

			}
		}

		public static void ExplorePath(string path)
        {
             System.Diagnostics.Process.Start("explorer.exe", path);
        }
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			timer2.Stop();
			timer3.Interval = 500;
			timer3.Start();
			checkBox1.BackColor = Color.Navy;
		}

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
			if (this.checkBox2.Checked == true)
			{
				this.button6.Visible = true;
				this.textBox5.ResetText();
				this.textBox5.Visible = true;

			
			}

			else
			{
				this.button6.Visible = false;
				this.textBox5.Visible = false;


			}
		}

		private void button6_Click(object sender, EventArgs e)
		{
			if(this.textBox5.Text=="")
			{ MessageBox.Show("不能空配！", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

			int ckd = this.checkedListBox1.CheckedIndices.Count;
			if (this.checkedListBox1.CheckedItems.Contains("all"))
				ckd--;
			foreach (string x in this.textBox5.Text.Split(';'))
			{
				if (x == "")
				{ MessageBox.Show("不能空配！", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
			}

			if (this.comboBox3.SelectedIndex == 3)
			{
				if (this.textBox5.Text.Split(';').Length != ckd)
				{ MessageBox.Show("配置数量不等于所选组数目，请检查", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

				if (currentsys == 1)
					xtpdic["ONL"] = this.textBox5.Text;
				else if (currentsys == 2)
					xtpdic["UMS"] = this.textBox5.Text;
				else if (currentsys == 3)
					xtpdic["BAT"] = this.textBox5.Text;
				else if (currentsys == 4)
					xtpdic["GAP"] = this.textBox5.Text;

				this.label6.Text = "指定不停机更新服务项成功";

			}
			else if (this.comboBox3.SelectedIndex == 2)
			{
				if (currentsys == 1)
					xtpdic["ONL"] = this.textBox5.Text;
				else if (currentsys == 2)
					xtpdic["UMS"] = this.textBox5.Text;
				else if (currentsys == 3)
					xtpdic["BAT"] = this.textBox5.Text;
				else if (currentsys == 4)
					xtpdic["GAP"] = this.textBox5.Text;

				this.label6.Text = "指定不停机更新服务项成功";

			}

		}

        private void timer2_Tick(object sender, EventArgs e)
        {
			if (!fclor)
			{ this.checkBox1.BackColor = Color.Gold; fclor = !fclor; }
			else
			{ this.checkBox1.BackColor = Color.Thistle; fclor = !fclor; }
		}

        private void timer3_Tick(object sender, EventArgs e)
        {
			if (!fclor)
			{ this.comboBox1.BackColor = Color.Gold; fclor = !fclor; }
			else
			{ this.comboBox1.BackColor = Color.Thistle; fclor = !fclor; }
		}

        private void timer5_Tick(object sender, EventArgs e)
        {
			if (!fclor)
			{ this.checkedListBox1.BackColor = Color.Gold; fclor = !fclor; }
			else
			{ this.checkedListBox1.BackColor = Color.Thistle; fclor = !fclor; }
		}

        private void timer6_Tick(object sender, EventArgs e)
        {
			if (!fclor)
			{ this.comboBox3.BackColor = Color.Gold; fclor = !fclor; }
			else
			{ this.comboBox3.BackColor = Color.Thistle; fclor = !fclor; }
		}

        private void timer7_Tick_1(object sender, EventArgs e)
        {
			if (!fclor)
			{ this.textBox2.BackColor = Color.Gold; 
				this.textBox3.BackColor = Color.Gold;
				this.button10.BackColor = Color.Gold;
				if (currentsys == 1)
				{
					textBox4.BackColor = Color.Gold;
					button12.BackColor = Color.Gold;
					
				}
				if(checkBox2.Checked)
				{
					textBox5.BackColor = Color.Gold;
					button6.BackColor = Color.Gold;
				}
				fclor = !fclor;
			}
			else
			{ this.textBox2.BackColor = Color.Thistle; 
				this.textBox3.BackColor = Color.Thistle;
				this.button10.BackColor = Color.Thistle;
				if (currentsys == 1)
				{
					textBox4.BackColor = Color.Thistle;
					button12.BackColor = Color.Thistle;

				}
				if (checkBox2.Checked)
				{
					textBox5.BackColor = Color.Thistle;
					button6.BackColor = Color.Thistle;
				}
				fclor = !fclor; }
		}

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
			
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
			if (!fclor)
			{ this.panel1.BackColor = Color.Gold; fclor = !fclor; }
			else
			{ this.panel1.BackColor = Color.Thistle; fclor = !fclor; }
		}

        private void timer1_Tick(object sender, EventArgs e)
        {
			if (!fclor)
			{ this.comboBox2.BackColor = Color.Gold; fclor = !fclor; }
			else
			{ this.comboBox2.BackColor = Color.Thistle; fclor = !fclor; }
		}

        private void label6_Click(object sender, EventArgs e)
        {

        }

        void TextBox2TextChanged(object sender, EventArgs e)
		{
	
		}


}
}
