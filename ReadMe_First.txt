Database Part
-------------------
1) First thing to do is Create Database with Name :- GYMONEDBMVC .
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
2) After Creating Database now make changes ConnectionStrings in Web.Config

  Change this connectionStrings your Own Data Source and Sql UserName and Password.

  <connectionStrings>
    <add name="Mystring" connectionString="Data Source=sai-pc;Database=GYMONEDBMVC;UID=sa;Password=Pass$123" providerName="System.Data.SqlClient" />
  </connectionStrings>

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
3) After making changes in connectionStrings Now Run this Project and it will create Simple Member ship Table.

1. Users
2. webpages_Membership
3. webpages_OAuthMembership
4. webpages_Roles
5. webpages_UsersInRoles

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

4) After that now Run Script GYMONEDBMVC.sql Script .

If you Get Error just try to remove it the Error " will Table already Exits of Membership " just Remove Creating tables Script for Below listed tables 

1. Users
2. webpages_Membership
3. webpages_OAuthMembership
4. webpages_Roles
5. webpages_UsersInRoles

And don't remove Insert Script of this Tables.

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   
5) Login Details
  
   1) Admin 
       UserID : Admin 
       Password : 123456

  2) System User
      UserID : User
       Password : 123456



      
  


