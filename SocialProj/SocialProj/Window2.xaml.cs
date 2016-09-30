/*
 * Created by SharpDevelop.
 * User: Trippy
 * Date: 7/27/2015
 * Time: 4:51 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text; 
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using SocialProj;
using SocialProj.UserClassTemp;

namespace SocialProj
{
	/// <summary>
	/// Registration Window
	/// </summary>
	public partial class Window2 : Window
	{	
		public Window2()
		{
			InitializeComponent();
		}
		
		protected void ShowTerms(object s, EventArgs e){
			MessageBox.Show("This is an open opinion social hub, where everyone can comment on games and videos.\n" + 
			               	"Thus:\n" +
			               	"1) Nothing but video game pornographic, and it has to be warned before       the start of the video (Fable)\n\n" +
			               	"2) Don't be a bully ;)");
			term_cons.IsEnabled = true;
		}
		
		private void ActReg(object s, EventArgs e){
			btn_reg.IsEnabled = true;
		}
		
		protected void RegMethod(object s, EventArgs e){
			
			try{
				string myConenction = "datasource=localhost;port=3306;username=root;password=@PP|E$";
				MySqlConnection myConn = new MySqlConnection(myConenction);
				MySqlDataAdapter myAda = new MySqlDataAdapter();
				myAda.SelectCommand = new MySqlCommand("SELECT * trialdb.user;", myConn);
				MySqlCommand cmd = new MySqlCommand("INSERT INTO trialdb.user (FirstName,LastName,Email,UserName,UserPass, UserPage) values ('"+input_first.Text+"','"+input_last.Text+"','"+input_email.Text+"','"+input_name.Text+"',PASSWORD('"+input_pw.Password+"'),'@"+input_name.Text+"')", myConn);
				MySqlCommand page = new MySqlCommand("INSERT INTO trialdb.userpage (PageName) VALUES ('@"+input_name.Text+"')", myConn);
				
				MySqlCommand crateNewImgTable = new MySqlCommand("USE trialdb;" +
				                                                 "CREATE TABLE "+input_name.Text+"ImgTable (idImg INT NOT NULL AUTO_INCREMENT, imgTitle VARCHAR(50), imgRef VARCHAR(500) NOT NULL, imgData BLOB(1024), PRIMARY KEY ( idImg ));", myConn);
				
				MySqlCommandBuilder db = new MySqlCommandBuilder(myAda);
				
				myConn.Open();
				
				cmd.ExecuteNonQuery();
				page.ExecuteNonQuery();
				crateNewImgTable.ExecuteNonQuery();
				
				//UserTemp user = new UserTemp(input_name.Text);
				
				MessageBox.Show("Registered: " + input_name.Text);
				
				myConn.Close();
			}catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
			
			
			Close();
		}
	}
}