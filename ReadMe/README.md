# Application needed

1. [Visual Studio](https://visualstudio.microsoft.com/vs/) any version 
2. [SQL Server 2016 Developer Edition](https://drive.google.com/file/d/1xoRn5X067lU_IvzPyHBTTyMWfFE5t1BH/view?usp=sharing)
3. [SSMS SQL Server Management Studio](https://aka.ms/ssmsfullsetup)


# Setup Visual Studio

1. Run Visual Studio Installer
2. Install ASP.NET and web development and .NET desktop development
3. Tick the following item and install
![ASP.NET web development]()
![ASP.NET desktop development]()


# Setup Sql Server 2016 Developer Edition

1. Extract the file to any location
2. Run setup.exe
3. Click on Installation and New SQL Server stand-alone installation
![setup_img_1]()
4. Press next until Feature Selection
5. Check Database Engine Service
6. Select Named instance
7. Enter instance name: DEV2016
![setup_img_2]()
8. In Database Engine Configuration check Mixed Mode
9. Enter the password: P@ssw0rd!@#
10. Click on Add Current User and next
![setup_img_3]()
11. Click on Install and you are done


# Turn IIS Service On (Optinal if you don't want to host to IIS)

1. Search Turn Windows features on or off
![turn_iis_on1]()
2. Find Internet Information Services and check the following item
![turn_iis_on2]()


# Setup Database

1. Open SQL Server Management Studio 2018
2. Select SQL Server Authentication
3. Enter server name: laptopID\DEV2016
   Login: sa
   Password: P@ssw0rd!@#
4. After login go to View -> Registered Server
5. Expend Database Engine and Local Server Group
6. Right click on your laptopID and select Properties
![setup_database_img1]()
7. Select Server Name: laptopID\DEV2016
   Registered Server Name: .
![setup_database_img2]()
8. Click on save
![setup_database_img3]()
9. Disconnect database
![setup_database_img4]()
10. Connect Back Database
   Server name: .\DEV2016
   Login: sa
   Password: P@ssw0rd!@#
![setup_database.img5]()
11. Expend .\DEV2016(SQL Server)
12. Right click Databases and select Restore Database
13. Change the Source to Device and click 3 dot to add .bak file (Make sure you move the mWalletDb.bak to your C or D drive)
![setup_database_img6]()
14. Click OK
15. Expend your Databases and you should see mWallet


# Clone Project

1. Click Clone a repository
![clone_project_img1](https://github.com/Benz28/mWallet/blob/master/ReadMe/Image/clone_project_img1.png)
2. Paste the github url: https://github.com/Benz28/mWallet.git
3. Set your file path
![clone_project_img2]()
4. Click clone
5. Double click on mWallet.sln to launch the project
![clone_project_img3]()
6. Go to View -> Other Windows -> Package Manager Control
![clone_project_img4]()
7. Update the project package: Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r
![clone_project_img5]()

You have done setup everything, you can debug or run without debug (ctrl + f5) to run the program.

Below are the additional settings if you want to host it to IIS and access directly on website.
8. Right click on mWallet and select Publish
9. Select Folder and next
10. Select a path to save the publish info (You can create a file to store it in anywhere) 
11. Click Finish
12. Click on the pensil icon (any)
![clone_project_img6]()
13. Expend File Publish Options and check Delete all existing files prior to publish
![clone_project_img7]()
14. Click on Save and Publish the application


# Setup IIS Hosting

1. Search IIS Manager in start
![setup_iis-img1]()
2. Right Click Sites and Add Website
![setup_iis_img2]()
3. Site Name: mwallet
   Physical Path: the path where you Publish your application (Clone Project step 10)
   Host Name: mwallet
![setup_iis_img3]()
4. Click OK and the application is hosted
5. Open notepad or notepad++ with administrator mode
6. File -> Open and paste the file path: C:\Windows\System32\drivers\etc and select hosts
![setup_iis_img4]()
7. Add the following IP address at the bottom: 127.0.0.1 mwallet
![setup_iis_img5]()
8. Save the file
9. Open your browser and search: http://mwallet
