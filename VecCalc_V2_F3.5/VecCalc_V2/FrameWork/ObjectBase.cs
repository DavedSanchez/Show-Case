/*
 * Created by SharpDevelop.
 * User: Trippy
 * Date: 8/27/2014
 * Time: 7:48 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;

namespace MVVMTrialThing.FrameWorks
{
	/// <summary>
	/// Description of ObjectBase.
	/// </summary>
	public class ObjectBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected internal void OnPropertyChanged(string propertyName){
			if(this.PropertyChanged != null)
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
