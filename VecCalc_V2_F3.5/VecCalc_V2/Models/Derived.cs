/*
 * Created by SharpDevelop.
 * User: Trippy
 * Date: 08/18/2014
 * Time: 20:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using MVVMTrialThing.FrameWorks;
using VecCalc_V2.ViewModel;

namespace VecCalc_V2.Models
{
	/// <summary>
	/// Derived is vecR and the Dot/Cross Product with Angle Difference
	/// </summary>
	public class Derived : Vectors{
		private double _dot, _cross, _diff, _magCross;
		//private Vectors tempA, tempB;

//		public Derived(Vectors a, Vectors b)
//			:base(0,0)
//		{
//			_i = a.i + b.i;
//			_j = a.j + b.j;
//			
//			tempA = a;
//			tempB = b;
//		}
		
		public double dot{
			get{return _dot;}
			set{//_dot = dot;
				if(_dot == value)
					return;
				
				_dot = value;
				OnPropertyChanged("dot");
			}
		}
		
		public double cross{
			get{return _cross;}
			set{//_cross = cross;
				if(_cross == value)
					return;
				
				_cross = value;
				OnPropertyChanged("cross");
			}
		}
		
		public double diff{
			get{return _diff;}
			set{//_diff = diff;
				if(_diff == value)
					return;
				
				_diff = value;
				OnPropertyChanged("diff");
			}
		}
		
		public double magCross{
			get{return _magCross;}
			set{
				if(_magCross == value)
					return;
				
				_magCross = value;
				OnPropertyChanged("magCross");
			}
		}
		
//		private double calcDot(){
//			this._dot = ((tempA.i * tempB.i) + (tempA.j * tempB.j));
//			
//			return this._dot;
//		}
		
		private double calcCross(){
			//TODO:
			
			return this._cross;
		}
		
		public void getDot(){
			//calcDot();
			Console.WriteLine("Dot Product: {0}", dot);
		}
		
		public static Derived Create(){
			Derived newDer = new Derived(){
				//
			};
			
			return newDer;
		}
		
	}
}
