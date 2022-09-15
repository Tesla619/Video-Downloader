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
using System.Collections.ObjectModel;
using System.Collections;
using VideoDownloaderV1.Properties;
using static System.Windows.Forms.AxHost;

namespace VideoDownloaderV1
{
    public partial class AnimeForm : Form
    {
        /// <summary>
        /// make try and catch so that if an error occurs will skip the episode and continue to next one       DONE TO MANNY FAILURES
        /// HAVE TO MAKE all in one window so as to see all downloads via download manager 
        /// </summary>                               

        bool fail = false;
        bool customON = false;
        SmtpClient Client;
        IWebDriver Driver;
        string emailS = "karl.methode.spiteri@gmail.com";
        //string emailR = String.Empty;
        List<int> customRange = new List<int>();

        List<string> failList = new List<string>();

        public AnimeForm()
        {
            InitializeComponent();
        }

        private void AnimeForm_Load(object sender, EventArgs e)
        {               
            Email_TextBox.Text = "karl.spiteri777@gmail.com";   //make save in memory
        }           

        private void Start_Button_Click(object sender, EventArgs e)
        {
            if (AnimeLink_TextBox.Text != "")
            {
                if (Start_TextBox.Text != "")
                {
                    if (End_TextBox.Text != "")
                    {
                        if (Email_TextBox.Text.Contains("@"))
                        {
                            int end = Int32.Parse(End_TextBox.Text);
                            int start = Int32.Parse(Start_TextBox.Text);

                            if (customON)
                            {
                                if (AnimeLink_TextBox.Text.Contains("9anime"))
                                    foreach (var ep in customRange)
                                        MainFunction(start, ep, "?ep=");
                            }
                            else
                            {
                                if (AnimeLink_TextBox.Text.Contains("9anime"))
                                    for (int i = 0; i <= end; i++)
                                        MainFunction(start, i, "?ep=");
                            }

                            #region Sending Response Email
                            string failed = string.Join("\n", failList);

                            if (failList.Count > 0) sendEmail("Downloader Finished", "Finished Downloading Episodes except the following: \n" + failed);
                            else sendEmail("Downloader Finished", "Finished Downloading Episodes");
                            #endregion

                        } else ErrorMessage("Invalid email or field has been left empty.\n\nPlease check and try again.");                        
                    } else ErrorMessage("End field has been left empty.\n\nPlease check and try again.");
                } else ErrorMessage("Start field has been left empty.\n\nPlease check and try again.");
            } else ErrorMessage("Anime Link field has been left empty.\n\nPlease check and try again.");

        }

        private void ErrorMessage(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        }

        private void MainFunction(int startEP, int counter, string EpCountString)   // "?ep="
        {
            try   //so as to not stop
            {
                if (counter == 0)
                {
                    OpenChrome(AnimeLink_TextBox.Text + EpCountString + counter); //to acts as buffer testing it still
                    if (!customON) counter = counter + startEP - 1;
                }
                else download9anime(counter);
            }
            catch (Exception) //Will keep count of failed episodes
            {
                failList.Add("Episode: " + counter.ToString());
            }

            Thread.Sleep(2000); //wait a bit
        }

        private string AskForInput(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 165,
                Text = caption,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
            };

            System.Drawing.Font font = new System.Drawing.Font("Microsoft Sans Serif", 16);

            System.Windows.Forms.Label textLabel = new System.Windows.Forms.Label() { Left = 25, Top = 15, Text = text, Font = font };
            System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox() { Left = 32, Top = 48, Width = 325, Font = font };

            System.Windows.Forms.Button confirmation = new System.Windows.Forms.Button()
            {
                Text = "OK",
                Left = 305,
                Width = 52,
                Height = 32,
                Font = font,
                Top = 85,
                DialogResult = DialogResult.OK,
                TextAlign = ContentAlignment.MiddleCenter
            };

            confirmation.Click += (sender, e) => { prompt.Close(); };
            textLabel.AutoSize = true;

            prompt.Controls.Add(textBox);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        private void chooseQuality()
        {
            try
            {
                ChromeClick("//*[@id=\"container\"]/div/table/tbody/tr[3]/td[1]/a", 500);          //high res
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
                    ChromeClick("//*[@id=\"container\"]/div/table/tbody/tr[2]/td[1]/a", 500);
                    fail = false;
                }
                catch (Exception) { }
            }
        }

        private void download9anime(int episode)
        {
            #region Setup
            Driver.SwitchTo().NewWindow(WindowType.Tab);
            Driver.Navigate().GoToUrl(AnimeLink_TextBox.Text + "?ep=" + episode);
            Thread.Sleep(200);
            #endregion                             

            #region Play & Pause Video
            ChromeClick("//*[@id=\"player\"]", 3000);
            ChromeClick("//*[@id=\"player\"]", 0200);
            ChromeClick("//*[@id=\"player\"]", 0200);
            ChromeClick("//*[@id=\"player\"]", 0200); //to pause vid
            #endregion


            #region Switch Frame & Press Download
            Driver.SwitchTo().Frame("iframe-embed");
            Driver.SwitchTo().Frame("external-embed");
            ChromeClick("//*[@id=\"mediaplayer\"]/div[2]/div[13]/div[4]/div[2]/div[13]", 100);
            #endregion


            #region Going to process to download video
            Driver.SwitchTo().Window(Driver.WindowHandles.Last()); //seems reduntant but needed

            chooseQuality();

            try
            {
                ChromeClick("//*[@id=\"F1\"]/button", 1000);
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
                    ChromeClick("//*[@id=\"F1\"]/button", 1000);
                }
                catch (Exception) { }
            }

            IList<IWebElement> elements = Driver.FindElements(By.TagName("div"));
            ReadOnlyCollection<IWebElement> link = elements[8].FindElements(By.TagName("a"));

            Driver.Navigate().GoToUrl(link[0].GetAttribute("href"));
            #endregion

            #region Open NewTab & Close prev 2 tabs
            for (int i = 0; i < 2; i++)
            {
                Driver.Close();
                Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            }
            #endregion
        }

        private void ChromeClick(string xPath, int delayMS)
        {
            Driver.FindElement(By.XPath(xPath)).Click();
            Thread.Sleep(delayMS);
        }

        private void OpenChrome(string URL)
        {
            var ChromeOptions = new ChromeOptions();
            //ChromeOptions.PageLoadStrategy = PageLoadStrategy.None; //so as to not wait for page to load to continue
            //ChromeOptions.AddArgument("headless"); doesn't work with tab handeling 
            //ChromeOptions.AddUserProfilePreference("download.default_directory", "D:\\Conan");  //change download folder
            
            //ChromeOptions.BinaryLocation = "C:\\Program Files\\BraveSoftware\\Brave-Browser\\Application\\brave.exe";       //works opens brave but code perfomance is effected
            //ChromeOptions.AddUserProfilePreference("download.prompt_for_download", false);

            ChromeOptions.AddExtension(@"uBlock-Origin.crx");

            new DriverManager().SetUpDriver(new ChromeConfig());
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;

            Driver = new ChromeDriver(chromeDriverService, ChromeOptions);
            Driver.Navigate().GoToUrl(URL);
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
            string Reader = "User";


            MailAddress FromEmail = new MailAddress(emailS, Sender);
            MailAddress ToEmail = new MailAddress(Email_TextBox.Text, Reader);
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
                Driver.Quit();
                Driver.Dispose();
            }
            catch (Exception) { }
        }

        private void Custom_Button_Click(object sender, EventArgs e)
        {
            if (customON == false)
            {
                customON = true;
                var rawInput = AskForInput("Enter a list of cells to be checked.", "Custom Range Input.");

                var splitInput = rawInput.Split(' ');
                customRange.Add(0);

                for (int i = 0; i < splitInput.Length; i++)
                    if (splitInput[i] != "") customRange.Add(Int32.Parse(splitInput[i]));

                //Add sort for list

                Start_TextBox.Text = customRange[1].ToString();
                End_TextBox.Text = customRange[customRange.Count - 1].ToString();
            }
        }
    }
}
