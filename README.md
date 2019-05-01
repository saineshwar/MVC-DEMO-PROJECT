# MVCDEMOPROJECT

DEMO Project IN ASP.NET MVC For Beginners

Project Name: - GYMONE

I had provided entire GYMONE project to download with Sql server Script which
contains tables and store procedures which are used.
Please read Text files ReadMe_First.txt file first.
In this Application there are 2 Types of Users one will be Admin and other will be System
User.

Project System User Process:-

This project aims complete on GYM process. The Process starts with Membership when
Member is newly joining GYM he is been registered in GYM Software by System User
while registering he will ask to Member for which type of Scheme he will like to take
(GYM+CARDIO OR GYM) and for how much period he will be joining GYM (Quarterly,
Half Yearly, Yearly) According to this the Fees of GYM will be decided. After registering
The Member is given a Receipt and Declaration form. the Receipt is sign be System User
and Declaration form is been sign by Member who is joining the GYM along with this
Member is told to provide legal Document proof of Address and Photo Identity. Now the
Member can use GYM facilities till Period has paid for Services after completion of service
he need to renew Membership it from System User for next Period ( Quarterly , Half Yearly ,
Yearly) and pay Fee to System User for that Period.
The System user also has a Rights to check How much Amount has been Collected this
month by seeing Month wise (Month wise Report Download) Report and if he want to check
how much Amount has been collected this year than he can view Year wise Report (Year
wise Report Download) and Finally he can also check which user renewal is upcoming to
notify him about his renewal of Membership by viewing renewal Report.

Admin System User Process:-

The Role of Admin is to Create and Delete System User and also assign roles (Admin ,
System User ) to them . He also has rights for Adding Scheme ( GYM+CARDIO OR GYM
etc ) and Plan ( Quarterly , Half Yearly , Yearly) he can view All Reports Month wise
collection Report and Year wise collection Report , Renewal Report according to this he can
plan scheme for getting more Member to his GYM.


Database Part
-------------------------------------------
1) First thing to do is Create Database with Name :- GYMONEDBMVC .

-------------------------------------------
2) After Creating Database now make changes ConnectionStrings in Web.Config

  Change this connectionStrings your Own Data Source and Sql UserName and Password.

  <connectionStrings>
    <add name="Mystring" connectionString="Data Source=sai-pc;Database=GYMONEDBMVC;UID=sa;Password=Pass$123" providerName="System.Data.SqlClient" />
  </connectionStrings>
-------------------------------------------
3) After making changes in connectionStrings Now Run this Project and it will create Simple Member ship Table.

1. Users
2. webpages_Membership
3. webpages_OAuthMembership
4. webpages_Roles
5. webpages_UsersInRoles

-------------------------------------------

4) After that now Run Script GYMONEDBMVC.sql Script .

If you Get Error just try to remove it the Error " will Table already Exits of Membership " just Remove Creating tables Script for Below listed tables 

1. Users
2. webpages_Membership
3. webpages_OAuthMembership
4. webpages_Roles
5. webpages_UsersInRoles

And don't remove Insert Script of this Tables.

-------------------------------------------
   
5) Login Details
  
   1) Admin 
       UserID : Admin 
       Password : 123456

  2) System User
      UserID : User
       Password : 123456

