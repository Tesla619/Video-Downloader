using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Reflection;
using OpenQA.Selenium.Support.Extensions;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;
using AngleSharp.Dom;
using System.Security.Policy;
using OpenQA.Selenium.Firefox;
using System.Collections.ObjectModel;
using System.Collections;

namespace VideoDownloaderV1
{
    public partial class AnimeForm : Form
    {
        /// <summary>
        /// make try and catch so that if an error occurs will skip the episode and continue to next one       DONE TO MANNY FAILURES
        /// HAVE TO MAKE all in one window so as to see all downloads via download manager 
        /// </summary>                               

        bool fail = false;
        SmtpClient Client;
        IWebDriver driver;
        string emailS = "karl.methode.spiteri@gmail.com";
        string emailR = "karl.spiteri777@gmail.com";

        List<string> failList = new List<string>();

        public AnimeForm()
        {
            InitializeComponent();
        }

        private void AnimeForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 5 + 1; i++)
            {
                if (i == 0) openChrome("https://9anime.tube/watch/detective-conan/?ep=" + i); //to acts as buffer testing it still                
                else
                {
                    driver.SwitchTo().NewWindow(WindowType.Tab);
                    driver.Navigate().GoToUrl("https://9anime.tube/watch/detective-conan/?ep=" + i);
                    Thread.Sleep(500);
                    driver.Close();
                    driver.SwitchTo().Window(driver.WindowHandles[0]); //seems reduntant but needed
                }                
            }

            AnimeLink_TextBox.Text = "https://9anime.tube/watch/detective-conan/";
            Start_TextBox.Text = "1002";
            End_TextBox.Text = "1057";
        }

        private void Start_Button_Click(object sender, EventArgs e)
        {
            int end = Int32.Parse(End_TextBox.Text);
            int start = Int32.Parse(Start_TextBox.Text);

            for (int i = start; i <= end; i++)
            {
                openChrome(AnimeLink_TextBox.Text + "?ep=" + i.ToString());
                Thread.Sleep(5000);

                if (AnimeLink_TextBox.Text.Contains("9anime"))
                {
                    try   //so as to not stop
                    {
                        download9anime();
                    }
                    catch (Exception) //Will keep count of failed episodes
                    {
                        failList.Add("Episode: " + i.ToString());
                    }                    
                }                                            

                Thread.Sleep(10000); //wait a bit for the video to download
            }

            #region Sending Response Email
            if (failList.Count > 0)
            {
                string failed = string.Join("\n", failList);
                sendEmail("Downloader Finished", "Finished Downloading Episodes except the following: " + failed);
            }
            else
            {
                sendEmail("Downloader Finished", "Finished Downloading Episodes");
            }
            #endregion
        }

        private void chooseQuality()
        {
            try
            {
                edgeClick("//*[@id=\"container\"]/div/table/tbody/tr[3]/td[1]/a", 500);          //high res
                fail = false;
            }
            catch (Exception)
            {
                fail = true;
            }

            if (fail == true)
            {
                try
                {
                    edgeClick("//*[@id=\"container\"]/div/table/tbody/tr[2]/td[1]/a", 500);
                    fail = false;
                }
                catch (Exception) { }
            }
        }

        private void download9anime()
        {
            #region Play & Pause Video
            edgeClick("//*[@id=\"player\"]", 3000);
            edgeClick("//*[@id=\"player\"]", 0200);
            edgeClick("//*[@id=\"player\"]", 0200);
            edgeClick("//*[@id=\"player\"]", 0200); //to pause vid
            #endregion


            #region Switch Frame & Press Download
            driver.SwitchTo().Frame("iframe-embed");
            driver.SwitchTo().Frame("external-embed");
            edgeClick("//*[@id=\"mediaplayer\"]/div[2]/div[13]/div[4]/div[2]/div[13]", 100);
            #endregion


            #region Going to process to download video
            driver.SwitchTo().Window(driver.WindowHandles[1]); //seems reduntant but needed

            chooseQuality();

            try
            {
                edgeClick("//*[@id=\"F1\"]/button", 1000);
            }
            catch (Exception)
            {
                fail = true;
            }

            if (fail == true) //might switch flase firtst then true
            {
                chooseQuality();
            }
            else
            {
                try
                {
                    edgeClick("//*[@id=\"F1\"]/button", 1000);
                }
                catch (Exception) { }
            }

            IList<IWebElement> elements = driver.FindElements(By.TagName("div"));
            ReadOnlyCollection<IWebElement> link = elements[8].FindElements(By.TagName("a"));

            driver.Navigate().GoToUrl(link[0].GetAttribute("href"));
            #endregion

            #region Open NewTab & Close prev 2 tabs
            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");

            for (int i = 0; i < 2; i++)
            {
                driver.SwitchTo().Window(driver.WindowHandles.First());
                driver.Close();
            }
            #endregion
        }

        private void edgeClick(string xPath, int delayMS)
        {
            driver.FindElement(By.XPath(xPath)).Click();
            Thread.Sleep(delayMS);
        }

        private void openChrome(string URL)
        {
            var options = new ChromeOptions();
            //options.PageLoadStrategy = PageLoadStrategy.None; //so as to not wait for page to load to continue
            options.AddExtension(@"uBlock-Origin.crx");

            new DriverManager().SetUpDriver(new ChromeConfig());
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;

            driver = new ChromeDriver(chromeDriverService, options);            
            driver.Navigate().GoToUrl(URL); 
        }

        private void sendEmail(string subject, string msg)
        {
            #region SMTP Setup
            Client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential()
                {
                    UserName = emailS,
                    Password = "yaiopydewhynacpm"
                }
            };
            #endregion

            #region Building Email
            string Sender = "Downloader Bot";
            string Reader = "Karl Spiteri";


            MailAddress FromEmail = new MailAddress(emailS, Sender);
            MailAddress ToEmail = new MailAddress(emailR, Reader);
            MailMessage Message = new MailMessage()
            {
                From = FromEmail,
                Subject = subject,
                Body = msg,
            };


            Message.To.Add(ToEmail);
            #endregion

            Client.Send(Message);
        }

        private void AnimeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                driver.Quit();
                driver.Dispose();
            }
            catch (Exception) { }
        }
    }
}
