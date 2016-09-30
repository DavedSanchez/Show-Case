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
using System.Diagnostics;

using MVVMTrialThing.FrameWorks;

namespace VecCalc_V2.Models
{
	/// <summary>
	/// Vectors will only make vecA/B, which will be passed to Derived
	/// </summary>
	public class Vectors : ObjectBase
	{
		protected double _i, _j, _mag, _angle, _unitI, _unitJ, _bear;
		protected string _first, _last, _common, _bearS;
		
//		public Vectors(double tempI, double tempJ){
//			_i = tempI;
//			_j = tempJ;
//		}
		
		public double i{
			get{return _i;}
			set{//_i = i;
				if(_i == value)
					return;
				
				_i = value;
				OnPropertyChanged("i");
			}
		}
		
		public double j{
			get{return _j;}
			set{//_j = j;
				if(_j == value)
					return;
				
				_j = value;
				OnPropertyChanged("j");
			}
		}
		
		public double mag{
			get{return _mag;}
			set{//_mag = mag;
				if(_mag == value)
					return;
				
				_mag = calcMag();
				OnPropertyChanged("mag");
			}
		}
		
		public double angle{
			get{return _angle;}
			set{//_angle = angle;
				if(_angle == value)
					return;
				
				_angle = calcAngle();
				OnPropertyChanged("angle");
			}
		}
		
		public double unitI{
			get{return _unitI;}
			set{//_unitI = unitI;
				if(_unitI == value)
					return;
				
				_unitI = calcUnitI();
				OnPropertyChanged("unitI");
			}
		}
		
		public double unitJ{
			get{return _unitJ;}
			set{//_unitJ = unitJ;
				if(_unitJ == value)
					return;
				
				_unitJ = calcUnitJ();
				OnPropertyChanged("unitJ");
			}
		}
		
		public double bear{
			get{
				return this._bear;}
			set{//_bear = bear;
				if(this._bear == value)
					return;
				
				//Convert.ToDouble(this._bear);
				this._bear = calcBear();
				
				//String.Format("{0}0.00", this.bear);
				
				OnPropertyChanged("bear");
			}
		}
		
		public string bearS{
			get{return this._bearS;}
			set{
				if(this._bearS == value)
					return;
				
				this._bearS = value;
				OnPropertyChanged("bearS");
			}
		}
		
		public string first{
			get{return _first;}
			set{//_first = first;
				if(_first == value)
					return;
				
				_first = findFirst();
				OnPropertyChanged("first");
			}
		}
		
		public string last{
			get{return _last;}
			set{//_last = last;
				if(_last == value)
					return;
				
				_last = findLast();
				OnPropertyChanged("last");
			}
		}
		
		public string common{
			get{return _common;}
			set{_common = common;}
		}
		
		private double calcMag(){
			this._mag = Math.Sqrt(Math.Pow(i,2) + Math.Pow(j, 2));
			
			Debug.Print("Mag: {0}", mag);
			
			return this._mag;
		}
		
		private double calcAngle(){
			this._angle = Math.Atan2(_j, _i);
			this._angle = (this._angle * 180)/Math.PI;
			
			if(this._i < 0 && this._j > 0){			//quad 2
				//No need to tweak, com knows
			}else if(this._i < 0 && this._j < 0){	//quad 3
				this._angle += 360;
			}else if(this._i > 0 && this._j <0 ){	//quad 4
				this._angle += 360;
			}else if(this._i == 0 && this._j < 0){	//27 logic
				this._angle += 360;
			}
			
			return this._angle;
		}
		
		private double calcUnitI(){
			this._unitI = this._i/this._mag;
			
			Debug.Print("I: {0}", i);
			Debug.Print("Mag: {0}", mag);
			
			Debug.Print("Unit I: {0}", unitI);
			
			return this._unitI;
		}
		
		private double calcUnitJ(){
			this._unitJ = this._j/this._mag;
			
			Debug.Print("J: {0}", j);
			Debug.Print("Mag: {0}", mag);
			
			Debug.Print("Unit J: {0}", unitJ);
			
			return this._unitJ;
		}
		
		private double calcBear(){
			double theta = Math.Atan2(this._j, this._i);
			theta = (theta * 180)/Math.PI;
			Math.Abs(theta);
			
			//Console.WriteLine("Theta: {0}", theta);
			
			if(this._i > 0 && this._j > 0){			//quad 1
				//this._first = "N";
				//this._last = "E";
				this._bear = 90 - theta;
			}else if(this._i < 0 && this._j > 0){	//quad 2
				//this._first = "N";
				//this._last = "W";
				
				this._bear = theta - 90;
				//Math.Abs(this._bear);
			}else if(this._i < 0 && this._j < 0){	//quad 3
				//this._first = "S";
				//this._last = "W";
				
				this._bear = Math.Abs(90 + theta);
			}else if(this._i > 0 && this._j < 0){	//quad 3
				//this._first = "S";
				//this._last = "E";
				
				this._bear = 90 + theta;
			}else if(this._i == 0 || this._j == 0){
				//IDK
			}
			
			return this._bear;
		}
		
		private string CalcBearStr(){
			if(this._i == 0 && this._j == 0){
				this._bearS = "Does not Exit";
			}else if(this._i == 0 && this._j > 0){
				this._bearS = "North"; 
			}else if(this._i == 0 && this._j < 0){
				this._bearS = "South";
			}else if(this._i > 0 && this._j == 0){
				this._bearS = "East";
			}else if(this._i < 0 && this._j == 0){
				this._bearS = "West";
			}
			
			return this._bearS;
		}
		
		private string findFirst(){
			if(this._j > 0){		
				this._first = "N";
					
			}else if(this._i < 0){
				this._first = "S";
			}else{
				return null;
			}
			
			return this._first;
		}
		
		private string findLast(){
			if(this._i > 0){			//quad 1
				this._last = "E";
					
				//this._bear = 90 - theta;
			}else if(this._i < 0){
				this._last = "W";
			}else{
				return null;
			}
			
			return this._last;
		}
		
		public string quads(){
			this._common= this._bear.ToString();
			
			if(this._i > 0 && this._j == 0){		//easts
				this._common = "East";
			}else if(this._i == 0 && this._j > 0){	//North
				this._common = "North";
			}else if(this._i < 0 && this._j == 0){	//West
				this._common = "West";
			}else if(this._i == 0 && this._j < 0){	//south
				this._common = "South";
			}
			
			return this._common;
		}
		
		public void getVec(){
			Console.WriteLine("<{0} , {1}>", i,j);
		}
		
		public void getMag(){
			calcMag();
			
			Debug.Print("Mag: {0}", mag);
			
			//Console.WriteLine("Mag: {0}", mag);
		}
		
		public void getAngle(){
			calcAngle();
			Console.WriteLine("Angle: {0}", angle);
		}
		
		public void getUnit(){
			calcUnitI();
			calcUnitJ();
			Console.WriteLine("Unit Vector: <{0} , {1}>", unitI, unitJ);
		}
		
		public void getBear(){
			calcBear();
			
//			if((this._i > 0 && this._j == 0) || (this._i == 0 && this._j > 0) || (this._i < 0 && this._j == 0) || (this._i == 0 && this._j < 0)){
//				Console.WriteLine("Bearing: {0}", common);
//			}else{
//				Console.WriteLine("Bearing: {0} {1} {2}", first, string.Format("{0:0.00}", Math.Abs(bear)), last);
//			}
		}
		
		public static Vectors Create(){
			Vectors newVec = new Vectors(){
				//created and ini vec
				
			};
			
			return newVec;
		}
		
	}
}
