/*
 * Created by SharpDevelop.
 * User: Trippy
 * Date: 7/28/2015
 * Time: 11:43 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SocialProj.UserClassTemp
{
	/// <summary>
	/// Description of UserTemp.
	/// </summary>
	public class UserTemp{
		private string _userName, _domainName;
		
		public UserTemp(){
		
		}
		
		public UserTemp(string u){
			u = _userName;
		}
		
		public string UserName{
			get{return _userName;}
			set{_userName = value;}
		}
		
		public string DomainName{
			get{return _domainName;}
			set{_domainName = value;}
		}
	}
}
