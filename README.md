	I. Application Setup:
	
	1. Cloning the app in Visual Studio 2015 or higher. 
	2. Deploying the App to a new Azure App service in your Azure subscription. 
	
	Running in Visual Studio:
	
	To run the app:
	
		a. Clone the app repository.
		b. Open the solution in Visual Studio (using the SitefinityWebApp.sln file).
		c. Run the app.
	
	
	Running in Azure:
	
	<a href="https://azuredeploy.net/" target="_blank">
    	<img src="http://azuredeploy.net/deploybutton.png"/>
	</a>
	
	
	II. Post Deployment tasks:
	
	1. Create Sitefinity admin user. ( After app deployment, the initial page that your app URL will load will prompt you to enter details about the Sitefinity admin user profile);
	2. Configure Redis Cache if needed.
	3. Access content via Kudu;
	
Content administration: