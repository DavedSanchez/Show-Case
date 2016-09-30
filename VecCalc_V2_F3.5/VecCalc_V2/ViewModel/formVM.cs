/*
 * Created by SharpDevelop.
 * User: Trippy
 * Date: 9/1/2014
 * Time: 1:38 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;

using VecCalc_V2.Formsheet;

namespace VecCalc_V2.ViewModel
{
	/// <summary>
	/// Description of formVM.
	/// </summary>
	public class formVM
	{
		public formSheet fModel{get; private set;}
		
		public formVM() : this(formSheet.Dis()){
			
		}
		
		public formVM(formSheet fSheet){
			fModel = fSheet;
		}
	}
}
