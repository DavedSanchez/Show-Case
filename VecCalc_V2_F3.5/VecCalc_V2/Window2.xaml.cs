/*
 * Created by SharpDevelop.
 * User: Trippy
 * Date: 9/1/2014
 * Time: 4:15 AM
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

using VecCalc_V2.ViewModel;

namespace VecCalc_V2
{
	/// <summary>
	/// Interaction logic for Window2.xaml
	/// </summary>
	public partial class Window2 : Window
	{
		public Window2()
		{
			InitializeComponent();
			this.DataContext = fSheet;
		}
		
		formVM fSheet = new formVM();
	}
}