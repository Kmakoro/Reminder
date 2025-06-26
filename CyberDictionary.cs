using System.Collections;
using System.Collections.Generic;


namespace Reminder
{
    class CyberDictionary
    {
        //dictionary to check the keys based on topic
        private List<string> keywords = new List<string>();
        //dictionary for password topic
        private List<string> passwordsTopic = new List<string>();
        //dictionary for phishing topic
        private List<string> phishingTopic = new List<string>();
        //dictionary for safe browsing topic
        private List<string> safeBrowsingTopic = new List<string>();
        //dictionary for virus topic
        private List<string> virusTopic = new List<string>();
        //dictionary for malware topic
        private List<string> malwareTopic = new List<string>();
        //dictionary for ransomware topic
        private List<string> ransomwareTopic = new List<string>();
        //dictionary for sentiment keywords
        public Dictionary<string, string> sentimentKeyword = new Dictionary<string, string>();

        public CyberDictionary()
        {
            //initialize all dictionaries
            initializeKeywords();
            password();
            phishing();
            safebrowsing();
            virus();
            malware();
            ransomware();
            sentiment();
        }

        //return method for password dictionary
        public List<string> getPasswordDictionary()
        {
            return this.passwordsTopic;
        }
        //return method for phishing dictionary
        public List<string> getPhishingDictionary()
        {
            return this.phishingTopic;
        }
        //return method for safe browsing dictionary
        public List<string> getSafeBrosingDictionary()
        {
            return this.safeBrowsingTopic;
        }
        //return method for keywords dictionary
        public List<string> getKeywords()
        {
            return this.keywords;
        }
        //return method for virus dictionary
        public List<string> getVirusDictionary()
        {
            return this.virusTopic;
        }
        //return method for malware dictionary
        public List<string> getMalwareDictionary()
        {
            return this.malwareTopic;
        }
        //return method for ransomware dictionary
        public List<string> getRansomwareDictionary()
        {
            return this.ransomwareTopic;
        }

        //initialize the keywords
        private void initializeKeywords()
        {
            keywords.Add($"what");
            keywords.Add($"is");
            keywords.Add($"tell");
            keywords.Add($"me");
            keywords.Add($"about");
            keywords.Add($"how");
            keywords.Add($"are");
            keywords.Add($"you");
            keywords.Add($"what's");
            keywords.Add($"your");
            keywords.Add($"purpose");
            keywords.Add($"can");
            keywords.Add($"i");
            keywords.Add($"ask");
            keywords.Add($"password");
            keywords.Add($"phishing");
            keywords.Add($"safe");
            keywords.Add($"browsing");
            keywords.Add($"safety?");
            keywords.Add($"safety");
            keywords.Add($"more");
            keywords.Add($"how are you?");
            keywords.Add($"what's your purpose?");
            keywords.Add($"what can i ask you about?");
            keywords.Add($"and");
            keywords.Add($"hello");
            keywords.Add($"hi");
            keywords.Add($"hey");
            keywords.Add($"secure");
            keywords.Add($"difference");
            keywords.Add($"unsafe");
            keywords.Add($"browser");
            keywords.Add($"site");
            keywords.Add($"malware");
            keywords.Add($"ransomware");
            keywords.Add($"virus");
            keywords.Add($"a");
            keywords.Add($"spread");
            keywords.Add($"warns");
            keywords.Add("if");
            keywords.Add("to");
            keywords.Add("against");
            keywords.Add("the");
            keywords.Add("of");
            keywords.Add("and");
            keywords.Add("for");
            keywords.Add("on");
            keywords.Add("in");
            keywords.Add("at");
            keywords.Add("with");
            keywords.Add("by");
            keywords.Add("from");
            keywords.Add("that");
            keywords.Add("this");
            keywords.Add("it");
            keywords.Add("as");
            keywords.Add("be");
            keywords.Add("are");
            keywords.Add("not");
            keywords.Add("do");
            keywords.Add("does");
            keywords.Add("any");
            keywords.Add("all");
            keywords.Add("should");
            keywords.Add("can");
            keywords.Add("could");
            keywords.Add("tip");
            keywords.Add("tips");
            keywords.Add("advice");
            keywords.Add("am");
            keywords.Add("i'm");
            keywords.Add("you");
            keywords.Add("my");
            keywords.Add("worried");
            keywords.Add("sad");
            keywords.Add("happy");
            keywords.Add("angry");
            keywords.Add("excited");
            keywords.Add("frustrated");
            keywords.Add("curious");
            keywords.Add("scared");
            keywords.Add("sentiment");
            keywords.Add("task");
            keywords.Add("reminder");
            keywords.Add("reminders");
            keywords.Add("help");
            keywords.Add("add");
            keywords.Add("remove");
        }
        //initialize the password topic
        private void password()
        {
            passwordsTopic.Add($"Create passwords that are at least 12-16 characters long and include a mix of uppercase letters, lowercase letters, numbers, and special characters (e.g., !, @, #, $).");
            passwordsTopic.Add($"Reusing the same password across multiple accounts is risky. If one account is compromised, hackers can access all your other accounts.");
            passwordsTopic.Add($"Use unique passwords for every account, especially for sensitive accounts like email, banking, and social media");
            passwordsTopic.Add($"Adding an extra layer of security, such as a one-time code sent to your phone or an authenticator app, makes it much harder for attackers to access your accounts, even if they have your password.");
            passwordsTopic.Add($"Password managers securely store and generate strong, unique passwords for all your accounts. You only need to remember one master password to access them.");
            passwordsTopic.Add($"Change your passwords periodically, especially for critical accounts. If you suspect a breach or notice unusual activity, update your password immediately.");
            passwordsTopic.Add($"Avoid Dictionary Words: Hackers use tools that can easily guess passwords based on common words or phrases.");
            passwordsTopic.Add($"Hackers often trick people into revealing their passwords through fake emails, websites, or messages. Always verify the sender's identity and avoid clicking on suspicious links.");
        }
        //initialize the phishing topic
        private void phishing()
        {
            phishingTopic.Add($"Phishing attacks trick victims into revealing sensitive information by pretending to be a trusted entity, such as a bank, government agency, or popular website.");
            phishingTopic.Add($"Suspicious Sender Addresses: Check the email address carefully—phishing emails often use misspelled or fake domains (e.g., support@amaz0n.com).");
            phishingTopic.Add($"Unexpected Attachments or Links: Be cautious of unsolicited emails with attachments or links, especially if they claim to be invoices, receipts, or important documents");
            phishingTopic.Add($"Phishing attacks often direct you to fake websites that look identical to legitimate ones (e.g., a fake banking login page).");
            phishingTopic.Add($"If you fall for a phishing scam, attackers can steal your personal information, access your bank accounts, or even commit identity theft.");
            phishingTopic.Add($"Phishing is also a common way for hackers to install malware or ransomware on your device.");
            phishingTopic.Add($"Phishing is Constantly Evolving: Attackers are using more sophisticated techniques, such as AI-generated emails and deepfake voice calls, to make their scams harder to detect.");
            phishingTopic.Add($"Report Phishing Attempts: If you receive a phishing email, report it to your email provider or the organization being impersonated. Many companies have dedicated channels for reporting phishing.");
        }
        //initialize the safe browsing topic
        private void safebrowsing()
        {
            safeBrowsingTopic.Add($"Always ensure the websites you visit use HTTPS (Hypertext Transfer Protocol Secure) instead of HTTP. HTTPS encrypts data between your browser and the website, protecting it from interception.");
            safeBrowsingTopic.Add($"Regularly update your web browser, operating system, and plugins/extensions to patch security vulnerabilities.");
            safeBrowsingTopic.Add($"Be cautious of links in emails, social media messages, or pop-up ads, especially if they seem too good to be true or create a sense of urgency.");
            safeBrowsingTopic.Add($"Hover over links to see the actual URL before clicking, and avoid visiting untrusted or unfamiliar websites.");
            safeBrowsingTopic.Add($"Install and maintain antivirus software to detect and block malware, phishing attempts, and other online threats.");
            safeBrowsingTopic.Add($"Enable a firewall to monitor and control incoming and outgoing network traffic.");
            safeBrowsingTopic.Add($"Only download files or software from trusted sources, such as official websites or app stores.");
            safeBrowsingTopic.Add($"Avoid downloading pirated software or media, as they often contain malware.");
        }

        private void virus()
        {
            // Initialize the virus topic dictionary
            // Add virus-related information to the dictionary
            virusTopic.Add($"A computer virus is a malicious program that attaches itself to legitimate software or files, spreading from one computer to another.");
            virusTopic.Add($"Viruses can corrupt or delete files, steal personal information, and disrupt system performance.");
            virusTopic.Add($"Viruses often spread through infected email attachments, downloads from untrusted websites, or removable media like USB drives.");
            virusTopic.Add($"To protect against viruses, use reputable antivirus software, keep your operating system and applications updated, and avoid clicking on suspicious links or downloading unknown files.");
            virusTopic.Add($"Regularly back up your important files to an external drive or cloud storage to prevent data loss in case of a virus infection.");
            virusTopic.Add($"Be cautious when using public Wi-Fi networks, as they can be breeding grounds for malware and viruses.");
            virusTopic.Add($"Educate yourself about common virus types, such as file infectors, macro viruses, and boot sector viruses, to better understand the risks.");
            virusTopic.Add($"Always scan external drives and downloads with antivirus software before opening them.");
        }

        private void malware()
        {
            // Initialize the malware topic dictionary
            // Add malware-related information to the dictionary
            malwareTopic.Add("Malware is a broad term that refers to any malicious software designed to harm, exploit, or otherwise compromise a computer system.");
            malwareTopic.Add("Common types of malware include viruses, worms, Trojans, ransomware, spyware, adware, and rootkits.");
            malwareTopic.Add("Malware can be spread through infected email attachments, malicious downloads, compromised websites, and removable media like USB drives.");
            malwareTopic.Add("To protect against malware, use reputable antivirus software, keep your operating system and applications updated, and avoid clicking on suspicious links or downloading unknown files.");
            malwareTopic.Add("Be cautious when using public Wi-Fi networks, as they can be breeding grounds for malware and other cyber threats.");
            malwareTopic.Add("Regularly back up your important files to an external drive or cloud storage to prevent data loss in case of a malware infection.");
            malwareTopic.Add("Fileless malware avoids detection – Instead of relying on executable files, it runs in memory, making it harder for traditional antivirus to detect.");
            malwareTopic.Add("Spyware steals data silently – Keyloggers and screen recorders can capture passwords, credit card details, and browsing habits.");
        }

        private void ransomware()
        {
            // Initialize the ransomware topic dictionary
            // Add ransomware-related information to the dictionary
            ransomwareTopic.Add("Ransomware is a type of malware that encrypts a victim's files, rendering them inaccessible until a ransom is paid to the attacker.");
            ransomwareTopic.Add("Ransomware often spreads through phishing emails, malicious downloads, or exploiting vulnerabilities in software.");
            ransomwareTopic.Add("To protect against ransomware, regularly back up your important files to an external drive or cloud storage, and keep your operating system and applications updated.");
            ransomwareTopic.Add("Be cautious when clicking on links or downloading attachments from unknown sources, as they may contain ransomware.");
            ransomwareTopic.Add("Use reputable antivirus software that includes ransomware protection features.");
            ransomwareTopic.Add("Educate yourself about common ransomware tactics, such as fake software updates or urgent security alerts, to avoid falling victim.");
            ransomwareTopic.Add("Ransomware attacks can target individuals, businesses, and even critical infrastructure, making it a significant threat to cybersecurity.");
            ransomwareTopic.Add("Paying the ransom does not guarantee that you will regain access to your files, and it may encourage further attacks.");
        }

        private void sentiment()
        {
            // Initialize the sentiment keyword dictionary
            // Add sentiment-related information to the dictionary
            sentimentKeyword.Add("happy", "I so appreciate you saying that!\n");
            sentimentKeyword.Add("sad", "I’m sorry to hear that. \nThat sounds really difficult.\n");
            sentimentKeyword.Add("angry", "I know you are really angry right now, but you need to take some deep breathes and think rationally. \nI understand  where you are coming from.\n");
            sentimentKeyword.Add("excited", "I’m so happy for you!\n");
            sentimentKeyword.Add("frustrated", "Take a deep breath and try to calm down. Is there anything I can do to help? \nLet's find a solution together. \nRemember to be patient with yourself. \nIt's okay to feel frustrated, but don't let it consume you. \nTake a break and come back to it later.\nLet's focus on what we can control.\n");
            sentimentKeyword.Add("worried", "It’s okay to feel anxious, it doesn’t define who you are.\nYou’re stronger than you think, and you’ll get through this.\n");
            sentimentKeyword.Add("curious", "Well, there are several words that perfectly capture the essence of curiosity: inquisitive, eager, adventurous, probing, and open-minded. \nThese words embody the spirit of curiosity, driving us to uncover new ideas and embrace the wonders of the unknown\n");
            sentimentKeyword.Add("scared", "This feeling will pass; you will get through this.\nTake a deep breath and exhale slowly. Just focus on your breathing.\n");
        }
    }
}
