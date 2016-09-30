/*
 * Created by SharpDevelop.
 * User: Trippy
 * Date: 8/18/2014
 * Time: 3:58 PM
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
using System.Windows.Navigation;
using System.Linq;

using VecCalc_V2.ViewModel;

namespace VecCalc_V2
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1(){
			InitializeComponent();
			openPop();
			
			this.DataContext = vm;
		}
		
		VecVM vm = new VecVM();
		
		void openPop(){
			MessageBox.Show("Enter I and J Components into respective inputs. To activate Hot Keys, tap 'Alt'.\n\n" +
			               	"This program was developed in SharpDevelop 4.4");
		}
		
		void mExit_Click(object sender, RoutedEventArgs e){
			Close();
		}
		
		void mDocs_Click(object sender, RoutedEventArgs e){
			MessageBox.Show("VectorCalc is a simple and easy to use tool to help quickly solve two vector (a total of three), for basic vector attributes.\n\n" +
			                "It is advised to learn how vectors work and do their basic calculations, yet it is always good to have a fast analysis tool.\n\n" +
			               	"Designed and Developed By Daved Sanchez\n\n" +
			               	"Developed in SharpDevelop 4.4");
		}
		
		void mLeg_Click(object sender, RoutedEventArgs e){
			MessageBox.Show("\u2992 = Unit Vector\n" +
							"|| || = Magnitude\n" +
			                "\u2220 = Angle\n" +
							"\u2638 = Bearing\n" +
			                "\u2022 = Dot Product\n" +
			               	"\u2A2F = Cross Product\n" +
			               	"\u0394\u00B0 = Angle Difference between <A> & <B>\n" + 
			                "NaN = Not Applicable/ Not Available/ Does Not Exist. Usually due to the \t\tabsence of a second vector.");
		}
		
		void mForm_Click(object sender, RoutedEventArgs e){
			Window2 formulas = new Window2();
			formulas.ShowDialog();
		}
	}
}