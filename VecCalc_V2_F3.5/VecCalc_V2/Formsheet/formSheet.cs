/*
 * Created by SharpDevelop.
 * User: Trippy
 * Date: 9/1/2014
 * Time: 1:31 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MVVMTrialThing.FrameWorks;

namespace VecCalc_V2.Formsheet
{
	/// <summary>
	/// Description of formSheet.
	/// </summary>
	public class formSheet : ObjectBase
	{
		private string _res, _mag, _unit, _angle, _bear, _dot, _angleDiff, _cross, _crossMag;
		
		public formSheet(){
		}
		
		public string Res{
			get{return _res;}
			set{
				if(_res == value)
					return;
				
				_res = value;
				OnPropertyChanged("Res");
			}
		}
		
		public string Mag{
			get{return _mag;}
			set{
				if(_mag == value)
					return;
				
				_mag = value;
				OnPropertyChanged("Mag");
			}
		}
		
		public string Unit{
			get{return _unit;}
			set{
				if(_unit == value)
					return;
				
				_unit = value;
				OnPropertyChanged("Unit");
			}
		}
		
		public string Angle{
			get{return _angle;}
			set{
				if(_angle == value)
					return;
				
				_angle = value;
				OnPropertyChanged("Angle");
			}
		}
		
		public string Bear{
			get{return _bear;}
			set{
				if(_bear == value)
					return;
				
				_bear = value;
				OnPropertyChanged("Bear");
			}
		}
		
		public string Dot{
			get{return _dot;}
			set{
				if(_dot == value)
					return;
				
				_dot = value;
				OnPropertyChanged("Dot");
			}
		}
		
		public string AngleDiff{
			get{return _angleDiff;}
			set{
				if(_angleDiff == value)
					return;
				
				_angleDiff = value;
				OnPropertyChanged("AngleDiff");
			}
		}
		
		public string Cross{
			get{return _cross;}
			set{
				if(_cross == value)
					return;
				
				_cross = value;
				OnPropertyChanged("Cross");
			}
		}
		
		public string CrossMag{
			get{return _crossMag;}
			set{
				if(_crossMag == value)
					return;
				
				_crossMag = value;
				OnPropertyChanged("CrossMag");
			}
		}
		
		public static formSheet Dis(){
			formSheet newForm = new formSheet(){
				Res = "<a.i + b.i , a.j + b.j> = <r.i, r.j>",
				Mag = "\u221A(i\u00B2 + j\u00B2) = ||V||",
				Unit = "Vector V / ||V|| = <i/Mag, j/Mag>",
				Angle = "\u03B8 = arctan(j/i). For Degrees, (\u03B8 * 180)/\u03C0\n" +
						"If V is in Quadrant 1,\n" +
						"\t \u03B8\n" + 
						"If V is in Quadrant 2,\n" +
						"\t 180 - \u03B8\n" +
						"If V is in Quadrant 3,\n" +
						"\t 180 + \u03B8\n" +
						"If V is in Quadrant 4\n" +
						"\t 360 - \u03B8\n",
				Bear = "\u03B8 = arctan(j/i). For Degrees, (\u03B8 * 180)/\u03C0\n" +
						"If V is in Quadrant 1,\n" +
						"\t N 90 - \u03B8 E\n" + 
						"If V is in Quadrant 2,\n" +
						"\t N 90 - \u03B8 W\n" +
						"If V is in Quadrant 3,\n" +
						"\t S 90 - \u03B8 W\n" +
						"If V is in Quadrant 4,\n" +
						"\t S 90 - \u03B8 E\n",
				Dot = "((a.i * b.i) + (a.j * b.j))",
				AngleDiff = "arccos(Dot Product/(Mag A * Mag B))",
				//Cross = "",
				//CrossMag = ""
			};
			
			return newForm;
		}
	}
}
