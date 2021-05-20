using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graded_Unit_research_4
{
    /// <summary>
    /// A class which creates a member objects which holds the information for a member and validates the input from the ViewMembersAdmin Page.
    /// Grant Shaw
    /// 01/05/2019
    /// </summary>



    class Member
    {
        private string name;
        private double sruNum;
        private DateTime dob;
        private long phoneNum;
        private string email;
        private string squad;
        private string permission;
        

        //constructor
        public Member(string names, double SRU, DateTime datebirth, long phonenumber, string emailad, string team, string permiss)
        {
            this.Name = names;
            this.SruNum = SRU;
            this.Dob = datebirth;
            this.Phonenum = phonenumber;
            this.Email = emailad;
            this.Squad = team;
            this.Permission = permiss;


        }
            
        //Properties which validate data entered into boxes.
        public string Name
        {
            get { return name; }

            set
            {
                 if (string.IsNullOrWhiteSpace(value))
                {
                    
                    throw new Exception("Name can not be blank");
                }

                 else
                    name = value;            
                
            }
        }

        public double SruNum
        {
            get { return sruNum; }

            set
            {
                if (value >= 00001 && value <= 99999)
                {
                    sruNum = value;
                }
                else
                    throw new Exception("SRU number can not be blank and can only have a maximum of 5 characters");
            }
        }


        public DateTime Dob
        {
            get { return dob; }

            set
            {

                if (value != null)
                {
                    dob = value;
                }
                else
                    throw new Exception("Please enter a valid date");
                 
            }

        }

        public long Phonenum
        {
            get { return phoneNum; }
            
            set
            {

               
                if (value < 100000000000 && value > 999999999)
                {
                    
                    phoneNum = value;
                }
                else
                    throw new Exception("Phone number must be 11 digits long, no other characters");
            }

        }

        public string Email
        {
            get { return email; }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Please enter a valid email");
                }
                else
                    email = value;
            }
        }

        public string Squad
        {

            get { return squad; }

            set
            {
                if (value == "U16" || value == "U18" || value == "U21" || value == "Senior" || value == "Junior")
                {
                    
                    squad = value;
                }
                else
                    throw new Exception("You must choose a team");


            }

        }

        public string Permission
        {
            get { return permission; }

            set
            {
                if (value == "Yes" || value == "yes" || value == "No" || value == "no")
                {
                    permission = value;
                }
                else
                    throw new Exception("Please enter permission");
            }
        }



    }
}
