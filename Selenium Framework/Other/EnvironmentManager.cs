using System;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using System.IO;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;
using System.Text.RegularExpressions;

namespace Eurofins.Selenium.Framework.Other
{
    public class EnvironmentManager
    {
        readonly Type _driverType;
        static readonly EnvironmentManager instance = new EnvironmentManager();
        private readonly BrowserType _browserType;
        public IWebDriver _driver;
        private UrlBuilder _urlBuilder;
        private readonly string _remoteCapabilities;

        public EnvironmentManager()
        {
            // TODO(andre.nogueira): Error checking to guard against malformed config files
            var driverClassName = GetSettingValue("Driver");
            var assemblyName = GetSettingValue("Assembly");
            var assembly = Assembly.Load(assemblyName);
            _driverType = assembly.GetType(driverClassName);
            _browserType = (BrowserType)Enum.Parse(typeof(BrowserType), GetSettingValue("DriverName"));
            _remoteCapabilities = GetSettingValue("RemoteCapabilities");
        }

        public static TimeSpan PageLoadTimeOut
        {
            get
            {
                try
                {
                    return TimeSpan.FromSeconds(Convert.ToDouble(GetSettingValue("TimeOut")));
                }
                catch (Exception)
                {
                    return TimeSpan.FromSeconds(40);
                }
            }
        }

        ~EnvironmentManager()
        {
            if (_driver != null)
            {
                _driver.Quit();
            }
        }

        public static string GetSettingValue(string key)
        {
            var strings = System.Configuration.ConfigurationManager.AppSettings.GetValues(key);
            if (strings != null)
                return strings[0];
            else
            {
                throw new NotSupportedException("We currently cannot find your right key setting!");
            }
        }

        public BrowserType BrowserType
        {
            get { return _browserType; }
        }

        public string RemoteCapabilities
        {
            get { return _remoteCapabilities; }
        }

        public IWebDriver GetCurrentDriver()
        {
            return _driver ?? CreateFreshDriver();
        }

        public IWebDriver CreateSecondDriver()
        {
            return (IWebDriver)Activator.CreateInstance(_driverType);
        }
        public EdgeDriverService service;
        public IWebDriver CreateFreshDriver()
        {
            string value = _driverType.Name;
            try
            {
                var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                String[] extract = Regex.Split(path, "_temp");
                //string CurrentExecution = EnvironmentManager.GetSettingValue("ConfigFileName");
                string driverPath = new Uri(extract[0].TrimEnd('\\') + "\\Drivers\\").AbsolutePath;//"\\r2\\a\\_TA_B2C-EBE\\TA_B2C-EBE\\"+CurrentExecution+
                Console.WriteLine("Driver " + driverPath);
                if (_driverType.Name.Equals("ChromeDriver"))
                {
                    // _driver = (IWebDriver)Activator.CreateInstance(_driverType);
                    var options = new ChromeOptions();
                    options.AddArgument("--no-sandbox");
                    //options.AddArgument("--headless");
                    options.AddArgument("--start-maximized");
                    options.AddArgument("port=8080");
                    options.AddArgument("Zoom 100%");
                    _driver = new ChromeDriver(driverPath, options);
                }
                else if (_driverType.Name.Equals("FirefoxDriver"))
                {
                    _driver = (IWebDriver)Activator.CreateInstance(_driverType);
                }
                else if (_driverType.Name.Equals("EdgeDriver"))
                {
                    var options = new EdgeOptions();
                    //options.AddAdditionalCapability("download.default_directory"," ");
                    //options.AddAdditionalCapability("download.prompt_for_download", "false");
                    using (service = EdgeDriverService.CreateDefaultService(driverPath, "msedgedriver.exe", 9515))
                    {
                        service.UseVerboseLogging = true;
                    }
                    _driver = new EdgeDriver(service, options);
                    _driver.Manage().Window.Maximize();
                }
                else
                {
                    InternetExplorerOptions customProfile = new InternetExplorerOptions()
                    {
                        IgnoreZoomLevel = true,
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                    };
                    _driver = new InternetExplorerDriver(driverPath, customProfile);
                }
            }
            catch (Exception e)
            {
                var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                String[] extract = Regex.Split(path, "TestResults");
                string driverPath = new Uri(extract[0].TrimEnd('\\') + "\\Drivers\\").AbsolutePath;
                Console.WriteLine("Driver " + driverPath);
                if (_driverType.Name.Equals("ChromeDriver"))
                {
                    var options = new ChromeOptions();
                    options.AddArgument("--no-sandbox");
                    //options.AddArgument("--headless");
                    options.AddArgument("--start-maximized");
                    options.AddArgument("port=8080");
                    options.AddArgument("Zoom 100%");
                    _driver = new ChromeDriver(driverPath, options);
                }
                else if (_driverType.Name.Equals("EdgeDriver"))
                {
                    var options = new EdgeOptions();
                    using (service = EdgeDriverService.CreateDefaultService(driverPath, "msedgedriver.exe", 9515))
                    {
                        service.UseVerboseLogging = true;
                    }
                    _driver = new EdgeDriver(service, options);
                    _driver.Manage().Window.Maximize();
                }
                else if (_driverType.Name.Equals("FirefoxDriver"))
                {
                    _driver = (IWebDriver)Activator.CreateInstance(_driverType);
                }
                else
                {
                    InternetExplorerOptions customProfile = new InternetExplorerOptions()
                    {
                        IgnoreZoomLevel = true,
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                    };
                    _driver = new InternetExplorerDriver(driverPath, customProfile);
                }

            }
            _driver.Manage().Cookies.DeleteAllCookies();
            return _driver;
        }

        public void CloseCurrentDriver()
        {
            if (_driver != null)
            {
                //_driver.Close();
                try
                {
                    //_driver.Quit();
                    if (EnvironmentManager.GetSettingValue("DriverName").Equals("Firefox"))
                    {
                        foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("firefox"))
                        {
                            proc.Kill();
                        }
                    }
                    else if (EnvironmentManager.GetSettingValue("DriverName").Equals("Chrome"))
                    {
                        foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("chrome"))
                        {
                            proc.Kill();
                        }
                        foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("chromedriver"))
                        {
                            proc.Kill();
                        }
                    }
                    else
                    {
                        foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("IEDriverServer"))
                        {
                            proc.Kill();
                        }
                        foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("iexplore"))
                        {
                            proc.Kill();
                        }
                        foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("WerFault"))
                        {
                            proc.Kill();
                        }
                    }
                    _driver.Quit();
                }
                catch(DriverServiceNotFoundException ex) { throw ex; }
                //driver.Dispose();                
            }

            _driver = null;
        }

        public static EnvironmentManager Instance
        {
            get
            {
                return instance;
            }
        }

        public UrlBuilder UrlBuilder
        {
            get
            {
                if (_urlBuilder == null)
                    _urlBuilder = new UrlBuilder();
                return _urlBuilder;
            }
        }
    }
}
