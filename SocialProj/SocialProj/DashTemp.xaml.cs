/*
 * Created by SharpDevelop.
 * User: Trippy
 * Date: 7/29/2015
 * Time: 3:54 PM
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
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Windows.Media.Converters;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace SocialProj
{
	/// <summary>
	/// Interaction logic for DashTemp.xaml
	/// </summary>
	public partial class DashTemp : Window
	{
		string _u;
		
		public DashTemp()
		{
			InitializeComponent();
		}
		
		public DashTemp(string _UserName){
			InitializeComponent();
			
			ContentGrid.Visibility = Visibility.Visible;
			ColorMixer.Visibility = Visibility.Collapsed;
			
			_u = _UserName;
			
			try{
				
				string myConenction = "datasource=localhost;port=3306;username=root;password=@PP|E$";
				MySqlConnection myConn = new MySqlConnection(myConenction);
				
				MySqlCommand cmd = new MySqlCommand("SELECT UserName FROM trialdb.user WHERE UserName='"+_u+"'",myConn);
				//MySqlCommand load_Theme = new MySqlCommand("SELECT imgBackground, imgBool FROM trialdb.userpage WHERE PageName='@"+_u+"'", myConn);
				MySqlCommand load_background = new MySqlCommand("SELECT imgBool, imgBackground, rValue, gValue, bValue, aValue FROM trialdb.userpage WHERE PageName='@"+_u+"'", myConn);
				
				MySqlDataReader reader;
				
				myConn.Open();
				
				reader = load_background.ExecuteReader();
				
				//Gets Theme Color
				while(reader.Read()){
					if(reader.GetString("imgBool") == "0"){
						this.Background = new SolidColorBrush(Color.FromArgb((byte)reader.GetInt32("aValue"),(byte)reader.GetInt32("rValue"),(byte)reader.GetInt32("gValue"),(byte)reader.GetInt32("bValue")));
					}else if(reader.GetString("imgBool") == "1"){
						Image img = new Image();
					
						img.Source = new BitmapImage(new Uri(reader.GetString("imgBackground")));
					
						Visual vecImg = img;
					
						this.Background = new BitmapCacheBrush(vecImg);
					}
				}
				
				reader.Close();
				
				reader = cmd.ExecuteReader();
				
				//Welcome User and Add DateTitle
				while(reader.Read()){
					this.Title = "Dash@"+reader.GetString("UserName");
					//lbl_welcome.Content = "Welcome " + reader.GetString("UserName");
				}
				//cmd.ExecuteNonQuery();
				
				reader.Close();
			}catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
		}
		
		protected void UploadImage(object s, EventArgs e){
			//
		}
		
		protected void UploadVideo(object s, EventArgs e){
			//
		}
		
		protected void UploadProfilePic(object s, EventArgs e){
			//
		}
		
		private void SaveColor(object s, EventArgs e){
			ContentGrid.Visibility = Visibility.Visible;
			ColorMixer.Visibility = Visibility.Collapsed;
		}
		
		protected void UpdateBackground_Color(object s, EventArgs e){
			ContentGrid.Visibility = Visibility.Collapsed;
			ColorMixer.Visibility = Visibility.Visible;
			
			string imgB = "0";
			
			byte r = (byte)Red.Value;
			byte g = (byte)Green.Value;
			byte b = (byte)Blue.Value;
			byte a = (byte)Alpha.Value;
			
			try{
				string myConenction = "datasource=localhost;port=3306;username=root;password=@PP|E$";
				MySqlConnection myConn = new MySqlConnection(myConenction);
				
				MySqlCommand usingImg = new MySqlCommand("UPDATE trialdb.userpage SET imgBool='"+imgB+"' WHERE PageName='@"+_u+"'", myConn);
				
				MySqlCommand updateR = new MySqlCommand("UPDATE trialdb.userpage SET rValue='"+r+"' WHERE PageName='@"+_u+"'", myConn);
				MySqlCommand updateG = new MySqlCommand("UPDATE trialdb.userpage SET gValue='"+g+"' WHERE PageName='@"+_u+"'", myConn);
				MySqlCommand updateB = new MySqlCommand("UPDATE trialdb.userpage SET bValue='"+b+"' WHERE PageName='@"+_u+"'", myConn);
				MySqlCommand updateA = new MySqlCommand("UPDATE trialdb.userpage SET aValue='"+a+"' WHERE PageName='@"+_u+"'", myConn);
				
				MySqlCommand get_Theme = new MySqlCommand("SELECT rValue, gValue, bValue, aValue FROM trialdb.userpage WHERE PageName='@"+_u+"'", myConn);
				
				MySqlDataReader reader;
				
				myConn.Open();
				usingImg.ExecuteNonQuery();
				
				updateR.ExecuteNonQuery();
				updateG.ExecuteNonQuery();
				updateB.ExecuteNonQuery();
				updateA.ExecuteNonQuery();
				
				reader = get_Theme.ExecuteReader(); 
				
				
				while(reader.Read()){
					this.Background = new SolidColorBrush(Color.FromArgb(a,r,g,b));
				}
				
				reader.Close();
				myConn.Close();
			}catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
		}
		
		protected void UpdateBackground_Img(object s, EventArgs e){
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
		
			dlg.DefaultExt = ".jpeg";
			dlg.Filter = "JPEG Files (*.jpg)|*jpg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
			dlg.ShowDialog();
			
			string imgB = "1";
			
			string filename = dlg.FileName.Replace(@"\",@"\\");
			
			MessageBox.Show(filename);
			
			try{
				string myConenction = "datasource=localhost;port=3306;username=root;password=@PP|E$";
				MySqlConnection myConn = new MySqlConnection(myConenction);
				
				//MySqlCommand set_Theme = new MySqlCommand("INSERT INTO theme FROM trialdb.userpage WHERE PageName='"+_u+"'", myConn);
				//MySqlCommand getTheme = new MySqlCommand("SELECT theme FROM trialdb.userpage WHERE PageName='@"+_u+"'", myConn);
				//MySqlCommand setTheme = new MySqlCommand("INSERT INTO trialdb.userpage (Theme) VALUES ('Black')", myConn);
				
				MySqlCommand theme_update = new MySqlCommand("UPDATE trialdb.userpage SET imgBackground='"+filename+"', imgBool='"+imgB+"' WHERE PageName='@"+_u+"'", myConn);
				MySqlCommand get_Theme = new MySqlCommand("SELECT imgBackground FROM trialdb.userpage WHERE PageName='@"+_u+"'", myConn);
				//MySqlCommand usingImg = new MySqlCommand("UPDATE trialdb.userpage SET imgAsBG='"+imgB+"' WHERE PageName='@"+_u+"'");
				
				MySqlDataReader reader;
				
				myConn.Open();
				theme_update.ExecuteNonQuery();
				//usingImg.ExecuteNonQuery();
				reader = get_Theme.ExecuteReader();
				
				while(reader.Read()){
					Image img = new Image();
					
					img.Source = new BitmapImage(new Uri(reader.GetString("imgBackground")));
					
					Visual vecImg = img;
					
					this.Background = new BitmapCacheBrush(vecImg);
				}
				
				reader.Close();
				myConn.Close();
			}catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
		}
		
		protected void exit_click(object s, EventArgs e){
			Close();
		}
		
		protected void logoff_click(object s, EventArgs e){
			Window1 logIn = new Window1();
			logIn.Show();
			Close();
		}
	}
}