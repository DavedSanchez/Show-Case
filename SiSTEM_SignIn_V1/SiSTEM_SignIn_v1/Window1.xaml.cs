/*
 * Created by SharpDevelop.
 * User: TrippyAK (Daved Sanchez)
 * Date: 7/23/2016
 * Time: 5:20 AM
 * Fin~: 7/25/2016 4:55 AM
 * Patch 1.0.1: 7/31/16
 * Patch 1.0.2: 8/1/16
 * Patch 1.0.3: 8/2/16
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
using System.Linq;
using System.ComponentModel;
using System.IO;
using System.Windows.Media.Imaging;

namespace SiSTEM_SignIn_v1{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// SiSTEM Sign in Software Version 1.0
	/// Used for members to sign in for events, orders by last name, and if needed, appends late comers
	/// Dynamic Design Enabled, background and logo swap by client/end-user supported
	/// Patch 1.0.1
	/// 	Data now has MS Excel Suport, along with time stamps
	/// 	All pre-existing data is ripped and re-ordered with new, in-coming data
	/// 	Remove duplicate/multiple sign-ins
	/// Patch 1.0.2
	/// 	Implimentation of a Master Member CSV
	/// 	Master CSV used to cross reference individuals singing in
	/// 		If individual is not a member, a '*' is next to the NID
	/// 	Admin is able to add new member(s) via the Option dropdown at any time
	/// Patch 1.0.3
	/// 	Files are now saved in designated sub-folders marked by month
	/// 	count var is now incremented instead of assigned to prevent 'over-write' of multi user load by admin
	/// 	Admin is now aware they are in 'Admin Mode' with text label appearing in top-left corner
	/// </summary>
	public partial class Window1 : Window{
		//make private string to allow ripping form textfields
		private string _first, _last, _nid, _fileName, _csvFile, _masterFile, _modedDate, _modedTime, _month;
		//make a list to store members
		private List<Member> memList, masterList;
		private DateTime _toDay;
		
		private const string password = "connect1";
		private bool single, multi, sHit, mHit;
		
		private int _count, _oldCount;
		
		
		public Window1(){
			InitializeComponent();
			
			last.Focus();
			ColourMixer.Visibility = Visibility.Collapsed;
			
			loadMedia();
			
			_toDay = DateTime.Today;
			_modedDate = _toDay.ToString("d");
			
			_month = DateTime.Now.ToString("MMM");
			
			_fileName = "MemberData\\TXTFiles\\"+ _month +"\\Signin_" + _modedDate.Replace('/', '_') + ".txt";
			_csvFile = "MemberData\\CSVFiles\\"+ _month +"\\Signin_" + _modedDate.Replace('/', '_') + ".csv";
			_masterFile = "MemberData\\CSVFiles\\SiSTEM_Master_File.csv";
			
			memList = new List<Member>();
			masterList = new List<Member>();
			
			if(File.Exists(_masterFile))
				ripMasterList(); /*masterList = ripMasterList();*/
			
			_oldCount = masterList.Count;
			_count = 0;
			single = multi = false;
			sHit = mHit = false;
		}
		
		private void SignIn_Click(object sender, RoutedEventArgs e){
			_last = last.Text;
			_first = first.Text;
			_nid = nid.Text;
			
			bool firstValid = true;
			bool lastValid = true;
			bool nidValid = true;
			bool filedsFilled = true;
			//check if any fields are empty
			if(string.IsNullOrEmpty(_last) || string.IsNullOrEmpty(_first) || string.IsNullOrEmpty(_nid)){
				MessageBox.Show("Error: Missing field(s)");
				filedsFilled = false;
				if(string.IsNullOrEmpty(_last))
					last.Focus();
				else if(string.IsNullOrEmpty(_first))
					first.Focus();
				else if(string.IsNullOrEmpty(_nid))
					nid.Focus();
				else
					last.Focus();
			}
			else{
				if(_nid.Length != 8){
					MessageBox.Show("Error: Invalid length for NID");
					nidValid = false;
					nid.Clear();
					nid.Focus();
				}
			
				if(!Char.IsLetter(_nid[0]) && !Char.IsLetter(_nid[1])){
					MessageBox.Show("Error: First two symbols in NID are not letters");
					nidValid = false;
					nid.Clear();
					nid.Focus();
				}
			
				if(!validateNID()){
					MessageBox.Show("Error: Last six symbols in NID are not numbers");
					nidValid = false;
					nid.Clear();
					nid.Focus();
				}
			
				if(!validateName(_last)){
					MessageBox.Show("Error: Invalid last name (non-letter symbol found)");
					lastValid = false;
					last.Clear();
					last.Focus();
				}
			
				if(!validateName(_first)){
					MessageBox.Show("Error: Invalid first name (non-letter symbol found)");
					firstValid = false;
					first.Clear();
					first.Focus();
				}
			}
			
			if(lastValid && firstValid && nidValid && filedsFilled){
				_modedTime = DateTime.Now.ToShortTimeString();
				
				//make a new member
				Member mem = new Member(_last, _first, _nid, _modedTime);
				
				//finds if in-coming member signing in is within master file (is a member)
				bool b = masterList.Exists(m => m.LastName == mem.LastName && m.FirstName == mem.FirstName && m.NID == mem.NID);
				
				if(b)
					mem.NonMember = "";
				else
					mem.NonMember = "*";
				
				//add member to memberList
				if(!single && !multi)
					memList.Add(mem);
				
				//if admin is adding users to master list,
				//memberList will be ignored and data will be stored in masterList for later write
				if(single && !multi){
					masterList.Add(mem);
					sHit = false;
					single = false;
					adminMode.Visibility = Visibility.Hidden;
				}else if(!single && multi){
					_count--;
					masterList.Add(mem);
					
					if(_count <= 0){
						multi = false;
						mHit = false;
						_count = 0;
						adminMode.Visibility = Visibility.Hidden;
					}
				}
				
				last.Clear();
				first.Clear();
				nid.Clear();
				last.Focus();
				
				MessageBox.Show("Signed In: " + _first + " " + _last);
			}
		}
		
		//validates nid for if the last 6 symbols are numbers
		private bool validateNID(){
			int i, lenght = _nid.Length;
			bool validNid = true;
			
			for(i = 2; i < lenght; i++){
				if(!Char.IsDigit(_nid[i])){
					validNid = !validNid;
					break;
				}
			}
			
			return validNid;
		}
		
		//validates names for only letter symbols and '-'
		private bool validateName(String name){
			int i, length = name.Length;
			bool validName = true;
			
			for(i = 0; i < length; i++){
				if(name[i] == '-')
					continue;
				else if(!Char.IsLetter(name[i])){
					validName = !validName;
					break;
				}
			}
			
			return validName;
		}
		
		//func to save sing in data to file
		private void SignInWindow_Closing(object sender, CancelEventArgs e){
			MessageBoxResult result = MessageBox.Show("Exiting sign in. All members signed in?", "SiSTEM Sign In App", MessageBoxButton.YesNo, MessageBoxImage.Warning);
			
			//cancel close message box
			if(result == MessageBoxResult.No)
				e.Cancel = true;
			else{
				//only write data to file if there is data
				if(memList.Count > 0){
					StreamWriter myFile = null;
					StreamWriter csvFile = null;
					
					//rip current data from pre-existing file
					if(File.Exists(_csvFile)){
						StreamReader csvRip = new StreamReader(_csvFile);
						
						List<string> csvData = new List<string>();
						
						string csvLine;
						
						while((csvLine = csvRip.ReadLine()) != null)
							csvData.Add(csvLine);
						
						csvRip.Close();
						
						splitData(csvData);
					}
				
					//order data by last name (Ascending)
					memList = memList.OrderBy(m => m.LastName).ToList();
					
					memList = findDuplicates(memList);
					
					//rewrite data to files
					myFile = new StreamWriter(_fileName);
					csvFile = new StreamWriter(_csvFile);
					
					myFile.WriteLine(memList.Count);
					myFile.WriteLine(String.Format("{0,-20} {1,-20} {2,15}", "Last Name", "First Name", "NID"));
					myFile.WriteLine("---------------------------------------------------------");
						
					int i;
					for(i = 0; i < memList.Count; i++){
						myFile.WriteLine(String.Format("{0,-20} {1,-20} {2,15} {3}", memList[i].LastName, memList[i].FirstName, memList[i].NID, memList[i].NonMember));
					}
						
					csvFile.WriteLine("Last Name,First Name,NID,Time,Non-Member");
						
					for(i = 0; i < memList.Count; i++)
						csvFile.WriteLine("{0},{1},{2},{3},{4}", memList[i].LastName, memList[i].FirstName, memList[i].NID, memList[i].SignInTime, memList[i].NonMember);
					
					myFile.Close();
					csvFile.Close();	
				}
				
				//only update/over-write master member list if new members were added 
				if(_oldCount <= masterList.Count && masterList.Count > 0){
					//write all new and old data in masterList to master member file
					StreamWriter master = new StreamWriter(_masterFile);
					
					masterList = masterList.OrderBy(m => m.LastName).ToList();
					masterList = findDuplicates(masterList);
				
					master.WriteLine("Last Name,First Name,NID");
				
					int j;
					for(j = 0; j < masterList.Count; j++)
						master.WriteLine("{0},{1},{2}", masterList[j].LastName, masterList[j].FirstName, masterList[j].NID);
				
					master.Close();
				}
			}
		}
		
		//Splits ripped string data from files into objs for ordering old and new data
		private void splitData(List<string> csvData){
			int i;
			
			for(i = 1; i < csvData.Count; i++){
				string[] str = csvData[i].Split(',');
				
				Member mem = new Member(str[0], str[1], str[2], str[3]);
				mem.NonMember = str[4];
				memList.Add(mem);
			}
		}
		
		//removes multiple sign-ins from the same member
		private List<Member> findDuplicates(List<Member> temp){
			int i;
			
			for(i = 0; i < temp.Count - 1; i++){
				if(temp[i].NID == temp[i+1].NID){
					temp.RemoveAt(i);
					i--;
				}
			}
			
			return temp;
		}
		
		//load in master member list to cross check if person signing in is a member
		private void ripMasterList(){
			StreamReader file = new StreamReader(_masterFile);
			
			List<string> masterData = new List<string>();
			string line;
			
			while((line = file.ReadLine()) != null)
				masterData.Add(line);
			
			int i;
			
			for(i = 1; i < masterData.Count; i++){
				string[] str = masterData[i].Split(',');
				
				Member mem = new Member(str[0], str[1], str[2], "0");
				mem.NonMember = "";
				masterList.Add(mem);
			}
			
			file.Close();
		}
		
		//utility func to allow admin in put in password
		//if password is correct, then the admin will be able to write to master member file
		private void AddSingle(object sender, RoutedEventArgs e){
			PassWordHolder.Visibility = Visibility.Visible;
			applyButton.Visibility = Visibility.Visible;
			sHit = true;
			mHit = false;
			
			passWordText.Focus();
		}
		
		//same thing as AddSingle, but for multiple adds
		private void AddMulti(object sender, RoutedEventArgs e){
			PassWordHolder.Visibility = Visibility.Visible;
			applyButton.Visibility = Visibility.Visible;
			CountBoxHolder.Visibility = Visibility.Visible;
			mHit = true;
			sHit = false;
			
			passWordText.Focus();
		}
		
		//utility func to ultimately make adding new users available
		private void Apply_Pass(object sender, RoutedEventArgs e){
			string _pass = passWordText.Password;
			
			if(_pass == password && sHit == true){
				single = true;
				adminMode.Visibility = Visibility.Visible;
			}
			else if(_pass == password && mHit == true){
				multi = true;
				adminMode.Visibility = Visibility.Visible;
				_count += int.Parse(countBox.Text);
			}
			
			passWordText.Clear();
			countBox.Clear();
			
			PassWordHolder.Visibility = Visibility.Collapsed;
			applyButton.Visibility = Visibility.Collapsed;
			CountBoxHolder.Visibility = Visibility.Collapsed;
		}
		
		
		//design fun to change the background colour
		private void UpdateColour(object sender, RoutedEventArgs e){
			ColourMixer.Visibility = Visibility.Visible;
			
			byte r = (byte)Red.Value;
			byte g = (byte)Green.Value;
			byte b = (byte)Blue.Value;
			byte a = (byte)Alpha.Value;
			
			this.Background = new SolidColorBrush(Color.FromArgb(a,r,g,b));
		}
		
		//simple func to write colour data to colour data file
		private void Exit_ColourMixer(object sender, RoutedEventArgs e){
			StreamWriter file = new StreamWriter("ThemeData\\colourData.txt");
			
			file.WriteLine(Alpha.Value);
			file.WriteLine(Red.Value);
			file.WriteLine(Green.Value);
			file.WriteLine(Blue.Value);
			
			file.Close();
			
			ColourMixer.Visibility = Visibility.Collapsed;
		}
		
		//design fun to change logo
		//file name is saved to img data file
		private void UpdateImage(object sender, RoutedEventArgs e){
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
			
			dlg.DefaultExt = ".jpeg";
			dlg.Filter = "JPEG Files (*.jpg)|*jpg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
			dlg.ShowDialog();
			
			if(!String.IsNullOrEmpty(dlg.FileName)){
				StreamWriter imgFile = new StreamWriter("ThemeData\\imgData.txt");
				imgFile.WriteLine(dlg.FileName.ToString());
				imgFile.Close();
			
				string imgFileName = dlg.FileName.Replace(@"\", @"\\");
				
				logo.Source = new BitmapImage(new Uri(imgFileName));
			}
		}
		
		//read in last img and colour data files to keep theme(s) consistent
		private void loadMedia(){
			StreamReader colourFile = new StreamReader("ThemeData\\colourData.txt");
			StreamReader imgFile = new StreamReader("ThemeData\\imgData.txt");
			
			this.Background = new SolidColorBrush(Color.FromArgb((byte)float.Parse(colourFile.ReadLine()), (byte)float.Parse(colourFile.ReadLine()), (byte)float.Parse(colourFile.ReadLine()), (byte)float.Parse(colourFile.ReadLine())));
			string img = imgFile.ReadLine();
			img = img.Replace(@"\", @"\\");
			logo.Source = new BitmapImage(new Uri(img));
			
			colourFile.Close();
			imgFile.Close();
		}
	}
}