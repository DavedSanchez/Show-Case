/*
 * Created by SharpDevelop.
 * User: Trippy
 * Date: 7/25/2015
 * Time: 4:15 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using SocialProj;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace SocialProj
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		private string _u;
		
		public string UserName{
			get{return _u;}
			set{_u = input_user.Text;}
		}
		
		public Window1()
		{
			InitializeComponent();
			//WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
		}
		
		protected void btn_CloseMethod(object s, EventArgs e){
			Close();
		}
		
		protected void btn_MiniMethod(object s, EventArgs e){
			WindowState = WindowState.Minimized;
		}
		
		protected void ClearText(object s, RoutedEventArgs e){
			input_user = (TextBox)s;
			input_user.Text = string.Empty;
		}
		
		protected void LogInMethod(object s, EventArgs e){
			try{
				
				string myConenction = "datasource=localhost;port=3306;username=root;password=@PP|E$";
				MySqlConnection myConn = new MySqlConnection(myConenction);
				
				MySqlCommand cmd = new MySqlCommand("SELECT UserName FROM trialdb.user WHERE UserName='"+input_user.Text+"' AND UserPass=PASSWORD('"+input_pass.Password+"')",myConn);
				
				MySqlDataReader reader;
				
				myConn.Open();
				
				reader = cmd.ExecuteReader();
				int c = 0;
				
				while(reader.Read())
					c += 1;
				
				//cmd.ExecuteNonQuery();
				
				if(c == 1){
					DashTemp ds = new DashTemp(input_user.Text);
					ds.Show();
					Close();
				}else
					MessageBox.Show("failed");
				
				reader.Close();
				myConn.Close();
			}catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
		}
		
		protected void RegMethod(object s, RoutedEventArgs e){
			Window2 regWindow = new Window2();
			regWindow.Show();
		}
	}
}