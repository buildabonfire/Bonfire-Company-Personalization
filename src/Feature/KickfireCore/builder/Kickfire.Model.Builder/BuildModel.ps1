.\bin\Debug\KickfireCore.Model.Builder.exe
Copy-Item ".\CompanyDataModel, 1.0.json" "C:\inetpub\wwwroot\ML.xconnect\App_data\Models\"
Copy-Item ".\CompanyDataModel, 1.0.json" "C:\inetpub\wwwroot\ML.xconnect\App_data\jobs\continuous\IndexWorker\App_data\Models\"

net stop ML.xconnect-MarketingAutomationService
net stop ML.xconnect-IndexWorker

Copy-Item ".\bin\debug\Bonfire.Feature.KickfireCore.*"  "C:\inetpub\wwwroot\ML.xconnect\bin\"
Copy-Item ".\bin\debug\Bonfire.Feature.KickfireCore.*"  "C:\inetpub\wwwroot\ML.xconnect\App_data\jobs\continuous\IndexWorker\"
Copy-Item ".\bin\debug\Bonfire.Feature.KickfireCore.*"  "C:\inetpub\wwwroot\ML.xconnect\App_data\jobs\continuous\AutomationEngine\"

net start ML.xconnect-MarketingAutomationService
net start ML.xconnect-IndexWorker