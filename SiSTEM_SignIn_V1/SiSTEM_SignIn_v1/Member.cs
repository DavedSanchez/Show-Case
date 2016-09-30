/*
 * Created by SharpDevelop.
 * User: TrippyAK
 * Date: 7/23/2016
 * Time: 6:50 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;

namespace SiSTEM_SignIn_v1
{
	/// <summary>
	/// Description of Member.
	/// </summary>
	public class Member{
		private string _lastName, _firstName, _nid, _time, _nonMember;
		
		public Member(){}
		public Member(string last, string first, string nid, string signInTime){
			_lastName = last;
			_firstName = first;
			_nid = nid;
			_time = signInTime;
		}
		
		public string LastName{
			get{return _lastName;}
		}
		
		public string FirstName{
			get{return _firstName;}
		}
		
		public string NID{
			get{return _nid;}
		}
		
		public string SignInTime{
			get{return _time;}
		}
		
		public string NonMember{
			get{return _nonMember;}
			set{_nonMember = value;}
		}
	}
}
