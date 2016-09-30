/*
 * Created by SharpDevelop.
 * User: Trippy
 * Date: 8/27/2014
 * Time: 8:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MVVMTrialThing.Commands
{
	/// <summary>
	/// Description of DelegateCommand.
	/// </summary>
	public class DelegateCommand<T> : ICommand
	{
		private readonly Predicate<T> _canExecute = null;
		private readonly Action<T> _execute = null;
		
		public DelegateCommand(Action<T> execute) : this(execute, null){
			
		}
		
		public DelegateCommand(Action<T> execute, Predicate<T> canExecute){
			_execute = execute;
			_canExecute = canExecute;
		}
		
		public bool CanExecute(object parameter){
			if(_canExecute == null)
				return true;
			
			return _canExecute((T)parameter);
		}
		
		public void Execute(object parameter){
			_execute((T)parameter);
		}
		
		public event EventHandler CanExecuteChanged{
			add{
				if(_canExecute != null)
					CommandManager.RequerySuggested += value;
			}
			remove{
				if(_canExecute != null)
					CommandManager.RequerySuggested -= value;
			}
		}
		
	}
}
