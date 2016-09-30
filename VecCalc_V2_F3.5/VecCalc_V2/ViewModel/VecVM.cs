/*
 * Created by SharpDevelop.
 * User: Trippy
 * Date: 8/28/2014
 * Time: 4:46 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

using MVVMTrialThing.Commands;
using VecCalc_V2.Models;

namespace VecCalc_V2.ViewModel
{
	/// <summary>
	/// Description of VecVM.
	/// </summary>
	public class VecVM
	{
		public Vectors VecA{get; private set;}
		public Vectors VecB{get; private set;}
		public Derived Der{get; private set;}
		
		public VecVM() : this(Vectors.Create(), Vectors.Create(), Derived.Create()){
			//overloaded crt to create vec
		}
		
		public VecVM(Vectors vecA, Vectors vecB, Derived der){
			VecA = vecA;
			VecB = vecB;
			Der = der;
			
			Calculate = new DelegateCommand<object>(Calculate_Exec, Calculate_CanExec);
			Clear = new DelegateCommand<object>(Clear_Exec, Clear_CanExec);
		}
		
		public DelegateCommand<object> Calculate{get; private set;}
		public DelegateCommand<object> Clear{get; private set;}
		
		void Calculate_Exec(object arg){
			Der.i = VecA.i + VecB.i;
			Der.j = VecA.j + VecB.j;
			
			VecA.mag = -1;
			VecB.mag = -1;
			Der.mag = -1;
			
			VecA.unitI = VecA.i / VecA.mag;
			VecA.unitJ = VecA.j / VecA.mag;
			VecB.unitI = VecB.i / VecB.mag;
			VecB.unitJ = VecB.j / VecB.mag;
			Der.unitI = Der.i / Der.mag;
			Der.unitJ = Der.j / Der.mag;
			
			VecA.angle = -1;
			VecB.angle = -1;
			Der.angle = -1;
			
			VecA.bear = -1;
			VecB.bear = -1;
			Der.bear = -1;
			
			VecA.bearS = "null";
			VecB.bearS = "null";
			Der.bearS = "null";
			
			VecA.first = "null";
			VecA.last = "null";
			VecB.first = "null";
			VecB.last = "null";
			Der.first = "null";
			Der.last = "null";
			
			Der.dot = ((VecA.i * VecB.i) + (VecA.j * VecB.j));
			
			double x = Math.Acos(Der.dot/(VecA.mag * VecB.mag));
			double y = (x * 180)/Math.PI;
			Der.diff = y;
			
			Der.cross = ((VecA.i * VecB.j) - (VecA.j * VecB.i));
			
			Der.magCross = Math.Sqrt(Math.Abs(Der.cross));
		}
		
		bool Calculate_CanExec(object arg){
			if(VecA.i == 0 && VecA.j == 0 && VecB.i == 0 && VecB.j == 0){
				return false;
			}
			
			return true;
		}
		
		void Clear_Exec(object arg){
			VecA.i = 0;
			VecA.j = 0;
			VecB.i = 0;
			VecB.j = 0;
			Der.i = 0;
			Der.j = 0;
			
			VecA.unitI = 0;
			VecA.unitJ = 0;
			VecB.unitI = 0;
			VecB.unitJ = 0;
			Der.unitI = 0;
			Der.unitJ = 0;
			
			VecA.mag = 0;
			VecB.mag = 0;
			Der.mag = 0;
			
			VecA.angle = 0;
			VecB.angle = 0;
			Der.angle = 0;
			
			VecA.bear = 0;
			VecB.bear = 0;
			Der.bear = 0;
			
			Der.dot = 0;
			Der.diff = 0;
			Der.cross = 0;
			Der.magCross = 0;
		}
		
		bool Clear_CanExec(object arg){
			if(VecA.i != 0 || VecA.j != 0 || VecB.i != 0 || VecB.j != 0){
				return true;
			}
			
			return false;
		}
	}
}
